using EverythingExtension.Exceptions;
using EverythingExtension.Extensions;
using EverythingExtension.SDK;
using EverythingExtension.Settings;

using Microsoft.CommandPalette.Extensions.Toolkit;

using Serilog;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using static EverythingExtension.SDK.EverythingSdk;

namespace EverythingExtension.Internal.V3
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
        private IntPtr _clientHandle;

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

            if (_clientHandle == IntPtr.Zero)
                return;

            Everything3_DestroyClient(_clientHandle);

            _clientHandle = IntPtr.Zero;
        }

        public void Cancel()
        {
            lock (_lockObject)
            {
                if (CancellationTokenSource == null)
                    return;
                CancellationTokenSource.Cancel();
                CancellationTokenSource.Dispose();
                CancellationTokenSource = null;
            }
        }

        public void Execute(string searchText, long cookie)
        {
            Cookie = cookie;
            SearchText = searchText;

            Cancel();

            CancellationTokenSource = new CancellationTokenSource();

            ExecuteInternal(CancellationTokenSource.Token);
        }

        /// <summary>
        /// 批量注册需要查询返回的字段属性
        /// </summary>
        private static void RegisterRequiredProperties(IntPtr searchState)
        {
            var propertyIds = new[]
            {
                EveryThing3PropertyId.Path,
                EveryThing3PropertyId.Name,
                EveryThing3PropertyId.Extension,
                EveryThing3PropertyId.Size,
                EveryThing3PropertyId.PathAndName,
                EveryThing3PropertyId.Type
            };

            foreach (var propId in propertyIds)
            {
                Everything3_AddSearchPropertyRequest(searchState, propId);
            }
        }

        private void ExecuteInternal(CancellationToken token)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                return;
            }

            lock (_lockObject)
            {
                IntPtr searchStateHandler = Everything3_CreateSearchState();

                if (searchStateHandler == IntPtr.Zero)
                    throw new IpcErrorException();

                try
                {
                    string searchKeyword = SearchKeywordFormat(searchStateHandler, SearchText, out bool isRegex, out int maxCount, _settings.MacroEnabled);
                    // 1. 批量初始化搜索参数配置
                    ApplySearchSettings(searchStateHandler, isRegex);
                    // 2. 批量注册需要返回的字段属性
                    RegisterRequiredProperties(searchStateHandler);

                    // 统一取消校验
                    token.ThrowIfCancellationRequested();

                    // if (keyWord.StartsWith("@")) { EverythingApiDllImport.Everything_SetRegex(true);
                    // //keyWord = keyWord.Substring(1); } else {
                    // EverythingApiDllImport.Everything_SetRegex(false); } if
                    // (token.IsCancellationRequested) { return results; }
                    //SetRequestFlags();
                    //if (token.IsCancellationRequested)
                    //{
                    //    return;
                    //}

                    Everything3_SetSearchViewportCount(searchStateHandler, maxCount);
                    token.ThrowIfCancellationRequested();

                    Everything3_SetSearchTextW(searchStateHandler, searchKeyword);
                    token.ThrowIfCancellationRequested();

                    var resultList = Everything3_Search(_clientHandle, searchStateHandler);

                    try
                    {
                        if (resultList == IntPtr.Zero)
                        {
                            CheckAndThrowExceptionOnError();
                            return;
                        }

                        token.ThrowIfCancellationRequested();

                        var resultCount = Everything3_GetResultListViewportCount(resultList);

                        // 遍历结果入队
                        EnumerateSearchResults(resultList, resultCount, token);
                    }
                    finally
                    {
                        Everything3_DestroyResultList(resultList);
                    }
                }
                finally
                {
                    Everything3_DestroySearchState(searchStateHandler);
                }
            }
        }

        /// <summary>
        /// 应用搜索配置参数到Everything SearchState
        /// </summary>
        private void ApplySearchSettings(IntPtr searchState, bool isRegex)
        {
            Everything3_SetSearchMatchCase(searchState, _settings.MatchCase);
            Everything3_SetSearchMatchWholeWords(searchState, _settings.MatchWholeWords);
            if (!isRegex)
            {
                Everything3_SetSearchMatchDiacritics(searchState, _settings.MatchDiacritics);
                Everything3_SetSearchMatchPath(searchState, _settings.MatchPath);
                Everything3_SetSearchMatchPrefix(searchState, _settings.MatchPrefix);
                Everything3_SetSearchMatchSuffix(searchState, _settings.MatchSuffix);
                Everything3_SetSearchIgnorePunctuation(searchState, _settings.IgnorePunctuation);
                Everything3_SetSearchWhitespace(searchState, _settings.IgnoreWhitespace);
            }

            if (_settings.SearchFoldersFirst.HasValue)
                Everything3_SetSearchFoldersFirst(searchState, _settings.SearchFoldersFirst.Value);

            Everything3_SetSearchViewportOffset(searchState, 0);
        }

        /// <summary>
        /// 遍历查询结果并入队，循环内统一取消校验
        /// </summary>
        private void EnumerateSearchResults(IntPtr resultList, IntPtr totalCount, CancellationToken token)
        {
            for (int idx = 0; idx < totalCount; idx++)
            {
                token.ThrowIfCancellationRequested();
                var context = new SearchItemContext(resultList, idx, Version);
                SearchResults.Enqueue(context.Result());
            }
        }

        #endregion Properties

        #region Public Methods

        public bool Initialize()
        {
            if (_clientHandle != IntPtr.Zero)
                return true;

            _clientHandle = Everything3_ConnectW(string.Empty);
            if (_clientHandle != IntPtr.Zero)
            {
                GetVersion();
                return true;
            }

            _clientHandle = Everything3_ConnectW("1.5a");

            if (_clientHandle != IntPtr.Zero)
            {
                GetVersion();
                return true;
            }

            _clientHandle = Everything3_ConnectW("1.5");

            if (_clientHandle != IntPtr.Zero)
            {
                GetVersion();
                return true;
            }
            Log.Warning("检测到 Everything1.5 版本尚未运行！");
            return false;
        }

        #endregion Public Methods

        #region Private Methods

        public void EnsureEverythingAvailable()
        {
            if (_clientHandle == IntPtr.Zero)
                throw new IpcErrorException();
        }

        public uint IncRunCountFromFilename(string filename)
            => Everything3_IncRunCountFromFilenameW(_clientHandle, filename);

        private static void CheckAndThrowExceptionOnError()
        {
            var err = EverythingSdk.Everything3_GetLastError();
            switch (err)
            {
                case EverythingSdk.EveryThing3Error.IpcPipeNotFound:
                    throw new IpcErrorException();
                case EverythingSdk.EveryThing3Error.OutOfMemory:
                    throw new MemoryErrorException();
                default:
                    throw new IpcErrorException();
            }
        }

        private string SearchKeywordFormat(IntPtr searchStateHandler, string query, out bool isRegex, out int maxCount, bool macroEnabled)
        {
            var keyword = query;
            if (query.StartsWith("@", StringComparison.CurrentCultureIgnoreCase))
            {
                if (!EverythingSdk.Everything3_SetSearchRegex(searchStateHandler, true))
                    CheckAndThrowExceptionOnError();
                isRegex = true;
                // keyWord = keyWord.Substring(1);
                keyword = query[1..];
            }
            else
            {
                if (!EverythingSdk.Everything3_SetSearchRegex(searchStateHandler, false))
                    CheckAndThrowExceptionOnError();
                isRegex = false;
            }

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

        private void GetVersion()
        {
            uint majorVersion = Everything3_GetMajorVersion(_clientHandle);
            uint minorVersion = Everything3_GetMinorVersion(_clientHandle);
            uint revision = Everything3_GetRevision(_clientHandle);
            uint build = Everything3_GetBuildNumber(_clientHandle);
            _version = new Version((int)majorVersion, (int)minorVersion, (int)revision, (int)build);
        }

        #endregion Private Methods
    }
}