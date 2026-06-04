using EverythingExtension.Exceptions;
using EverythingExtension.Extensions;
using EverythingExtension.SDK;
using EverythingExtension.Settings;

using Microsoft.CommandPalette.Extensions.Toolkit;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using static EverythingExtension.SDK.EverythingSdk;

namespace EverythingExtension.Internal
{
    internal sealed partial class EverythingClient : IEverythingClient
    {
        #region Fields

        public EverythingClient(EverythingSettings settings)
        {
            _settings = settings;
        }

        private readonly Lock _lockObject = new();
        private readonly EverythingSettings _settings;
        private Version _version = new("0.0.0.0");

        #endregion Fields

        #region Properties

        public Version Version => _version;

        public CancellationTokenSource? CancellationTokenSource { get; private set; }

        public string? SearchText { get; private set; }
        public ConcurrentQueue<SearchResult> SearchResults { get; } = new();
        public long Cookie { get; private set; }

        public void Dispose()
        {
            Cancel();
            Everything_CleanUp();
        }

        public void Cancel()
        {
            if (CancellationTokenSource == null)
                return;
            CancellationTokenSource.Cancel();
            CancellationTokenSource.Dispose();
            CancellationTokenSource = null;
        }

        public void Execute(string searchText, long cookie)
        {
            Cookie = cookie;
            SearchText = searchText;

            Cancel();

            CancellationTokenSource = new CancellationTokenSource();

            ExecuteInternal(CancellationTokenSource.Token);
        }

        public uint IncRunCountFromFilename(string filename)
           => EverythingSdk.Everything_IncRunCountFromFileNameW(filename);

        private void ExecuteInternal(CancellationToken token)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                return;
            }

            lock (_lockObject)
            {
                string searchKeyword = SearchKeywordFormat(SearchText, out bool isRegex, out int maxCount, _settings.MacroEnabled);

                ApplySearchSettings(isRegex);

                SetRequestFlags();

                // 统一取消校验
                token.ThrowIfCancellationRequested();

                // if (keyWord.StartsWith("@")) { EverythingApiDllImport.Everything_SetRegex(true);
                // //keyWord = keyWord.Substring(1); } else {
                // EverythingApiDllImport.Everything_SetRegex(false); } if
                // (token.IsCancellationRequested) { return results; }

                EverythingSdk.Everything_SetOffset(0);
                token.ThrowIfCancellationRequested();

                EverythingSdk.Everything_SetMax(maxCount);
                token.ThrowIfCancellationRequested();

                EverythingSdk.Everything_SetSearchW(searchKeyword);
                token.ThrowIfCancellationRequested();

                if (!EverythingSdk.Everything_QueryW(true))
                {
                    CheckAndThrowExceptionOnError();
                    return;
                }

                token.ThrowIfCancellationRequested();

                var count = EverythingSdk.Everything_GetNumResults();

                EnumerateSearchResults(count, token);
            }
        }

        /// <summary>
        /// 应用搜索配置参数到Everything SearchState
        /// </summary>
        private void ApplySearchSettings(bool isRegex)
        {
            Everything_SetMatchCase(_settings.MatchCase);
            Everything_SetMatchWholeWord(_settings.MatchWholeWords);
            if (!isRegex)
            {
                Everything_SetMatchPath(_settings.MatchPath);
            }
        }

        /// <summary>
        /// 遍历查询结果并入队，循环内统一取消校验
        /// </summary>
        private void EnumerateSearchResults(int totalCount, CancellationToken token)
        {
            for (var idx = 0; idx < totalCount; ++idx)
            {
                token.ThrowIfCancellationRequested();

                var context = new SearchItemContext(idx, _version);
                SearchResults.Enqueue(context.Result());
            }
        }

        #endregion Properties

        #region Public Methods

        public bool Initialize()
        {
            int majorVersion = Everything_GetMajorVersion();
            if (majorVersion == 0)
            {
                return false;
            }
            int minorVersion = Everything_GetMinorVersion();

            int revision = Everything_GetRevision();

            int build = Everything_GetBuildNumber();

            _version = new Version(majorVersion, minorVersion, revision, build);

            return true;
        }

        #endregion Public Methods

        #region Private Methods

        public void EnsureEverythingAvailable()
        {
            _ = Everything_GetMajorVersion();
            CheckAndThrowExceptionOnError();
        }

        private static void CheckAndThrowExceptionOnError()
        {
            switch (EverythingSdk.Everything_GetLastError())
            {
                case StateCode.CreateThreadError:
                    throw new CreateThreadException();
                case StateCode.CreateWindowError:
                    throw new CreateWindowException();
                case StateCode.InvalidCallError:
                    throw new InvalidCallException();
                case StateCode.InvalidIndexError:
                    throw new InvalidIndexException();
                case StateCode.IpcError:
                    throw new IpcErrorException();
                case StateCode.MemoryError:
                    throw new MemoryErrorException();
                case StateCode.RegisterClassExError:
                    throw new RegisterClassExException();
            }
        }

        private string SearchKeywordFormat(string query, out bool isRegex, out int maxCount, bool macroEnabled)
        {
            var keyword = query;
            if (query.StartsWith("@", StringComparison.CurrentCultureIgnoreCase))
            {
                EverythingSdk.Everything_SetRegex(true);
                isRegex = true;
                // keyWord = keyWord.Substring(1);
                keyword = query[1..];
            }
            else
            {
                EverythingSdk.Everything_SetRegex(false);
                isRegex = false;
            }

            CheckAndThrowExceptionOnError();

            Match match = Regex.Match(keyword, @"(count:(?<count>\d+))", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            if (match.Success)
            {
                maxCount = int.Parse(match.Groups["count"].Value, CultureInfo.DefaultThreadCurrentCulture);
                keyword = keyword.Replace(match.Value, string.Empty).Trim();
            }
            else
                maxCount = _settings.MaxSearchCount;

            if (!macroEnabled)
            {
                return keyword;
            }

            if (!keyword.Contains(':'))
            {
                return keyword;
            }

            var keywordItems = keyword.Split(':');
            var separator = keywordItems.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(separator))
            {
                return keyword;
            }

            var macro = _settings.Macros.FirstOrDefault(m => m.Prefix.Equals(separator, StringComparison.OrdinalIgnoreCase));
            return macro == null ? keyword : $"{macro} {string.Join(":", keywordItems.Skip(1))}";
        }

        private void SetRequestFlags()
        {
            var methodInfo = typeof(EverythingSdk).GetMethod("Everything_SetRequestFlags", BindingFlags.Static | BindingFlags.NonPublic);

            if (!methodInfo.IsVersionAvailable(_version))
                return;

            if (methodInfo == null)
                return;

            methodInfo.Invoke(null, [RequestFlag.FileName | RequestFlag.Path | RequestFlag.FullPathAndFileName | RequestFlag.HighlightedFileName | RequestFlag.HighlightedFullPathAndFileName | RequestFlag.Size | RequestFlag.Extension]);
        }

        #endregion Private Methods
    }
}