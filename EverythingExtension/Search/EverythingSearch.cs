// Copyright (c) Microsoft Corporation The Microsoft Corporation licenses this file to you under the
// MIT license. See the LICENSE file in the project root for more information.

using EverythingExtension.Exceptions;
using EverythingExtension.SDK;
using EverythingExtension.Settings;

using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using EverythingExtension.Extensions;
using static EverythingExtension.SDK.EverythingSdk;

namespace EverythingExtension.Search
{
    internal sealed class EverythingSearch(EverythingSettings settings)
    {
        #region Fields

        private readonly Lock _lockObject = new();
        private CancellationTokenSource? _cancellationToken;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether [match path].
        /// </summary>
        /// <value> <c> true </c> if [match path]; otherwise, <c> false </c>. </value>
        public static bool MatchPath
        {
            get => EverythingSdk.Everything_GetMatchPath();

            set => EverythingSdk.Everything_SetMatchPath(value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [match case].
        /// </summary>
        /// <value> <c> true </c> if [match case]; otherwise, <c> false </c>. </value>
        public static bool MatchCase
        {
            get => EverythingSdk.Everything_GetMatchCase();

            set => EverythingSdk.Everything_SetMatchCase(value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [match whole word].
        /// </summary>
        /// <value> <c> true </c> if [match whole word]; otherwise, <c> false </c>. </value>
        public static bool MatchWholeWord
        {
            get => EverythingSdk.Everything_GetMatchWholeWord();

            set => EverythingSdk.Everything_SetMatchWholeWord(value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable regex].
        /// </summary>
        /// <value> <c> true </c> if [enable regex]; otherwise, <c> false </c>. </value>
        public static bool EnableRegex
        {
            get => EverythingSdk.Everything_GetRegex();

            set => EverythingSdk.Everything_SetRegex(value);
        }

        private Version Version { get; } = GetVersion();

        #endregion Properties

        public ConcurrentQueue<SearchResult> SearchResults { get; } = new();

        public long Cookie { get; private set; }

        public string? SearchText { get; private set; }

        #region Public Methods

        public void Execute(string searchText, long cookie)
        {
            Cookie = cookie;
            SearchText = searchText;

            _cancellationToken?.Cancel();
            _cancellationToken ??= new CancellationTokenSource();

            ExecuteInternal(_cancellationToken!.Token);
        }

        public void Cancel()
        {
            if (_cancellationToken != null)
            {
                _cancellationToken.Cancel();
                _cancellationToken.Dispose();
                _cancellationToken = null;
            }
        }

        private static Version GetVersion()
        {
            int majorVersion = Everything_GetMajorVersion();
            int minorVersion = Everything_GetMinorVersion();
            int revision = Everything_GetRevision();
            int build = Everything_GetBuildNumber();
            return new Version(majorVersion, minorVersion, revision, build);
        }

        private void SetRequestFlags()
        {
            var methodInfo = typeof(EverythingSdk).GetMethod("Everything_SetRequestFlags", BindingFlags.Static | BindingFlags.NonPublic);

            if (!methodInfo.IsVersionAvailable(Version))
                return;

            if (methodInfo == null)
                return;

            methodInfo.Invoke(null, [RequestFlag.FileName | RequestFlag.Path | RequestFlag.FullPathAndFileName | RequestFlag.HighlightedFileName | RequestFlag.HighlightedFullPathAndFileName | RequestFlag.Size | RequestFlag.Extension]);
        }

        private void ExecuteInternal(CancellationToken token)
        {
            lock (_lockObject)
            {
                if (string.IsNullOrEmpty(SearchText))
                {
#pragma warning disable CA2208 // 正确实例化参数异常
                    throw new ArgumentNullException(nameof(SearchText));
#pragma warning restore CA2208 // 正确实例化参数异常
                }
                string searchKeyword = SearchKeywordFormat(settings, SearchText, out int maxCount, settings.MacroEnabled);

                if (maxCount < 0)
                {
#pragma warning disable CA2208 // 正确实例化参数异常
                    throw new ArgumentOutOfRangeException(nameof(settings.MaxSearchCount));
#pragma warning restore CA2208 // 正确实例化参数异常
                }
                //#if DEBUG
                //            Logger.Info($"Search:{searchKeyword}", this.GetType(), "Search");
                //#endif

                if (token.IsCancellationRequested)
                {
                    return;
                }

                // if (keyWord.StartsWith("@")) { EverythingApiDllImport.Everything_SetRegex(true);
                // //keyWord = keyWord.Substring(1); } else {
                // EverythingApiDllImport.Everything_SetRegex(false); } if
                // (token.IsCancellationRequested) { return results; }
                SetRequestFlags();
                if (token.IsCancellationRequested)
                {
                    return;
                }

                EverythingSdk.Everything_SetOffset(0);
                if (token.IsCancellationRequested)
                {
                    return;
                }

                EverythingSdk.Everything_SetMax(maxCount);
                if (token.IsCancellationRequested)
                {
                    return;
                }

                EverythingSdk.Everything_SetSearchW(searchKeyword);

                if (token.IsCancellationRequested)
                {
                    return;
                }

                if (!EverythingSdk.Everything_QueryW(true))
                {
                    CheckAndThrowExceptionOnError();
                    return;
                }

                if (token.IsCancellationRequested)
                {
                    return;
                }

                var count = EverythingSdk.Everything_GetNumResults();

                for (var idx = 0; idx < count; ++idx)
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }

                    var context = new SearchItemContext(idx, Version);
                    SearchResults.Enqueue(context.Result());
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        /*
        private static void ConvertHighlightFormat(string? contentHighlighted, out List<int> highlightData, out string fn)
        {
            highlightData = [];
            StringBuilder content = new();
            bool flag = false;
            char[]? contentArray = contentHighlighted?.ToCharArray();
            int count = 0;
            for (int i = 0; i < contentArray!.Length; i++)
            {
                char current = contentHighlighted![i];
                if (current == '*')
                {
                    flag = !flag;
                    count++;
                }
                else
                {
                    if (flag)
                    {
                        highlightData.Add(i - count);
                    }

                    content.Append(current);
                }
            }

            fn = content.ToString();
        }
        */

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

        // public void Load(string sdkPath) { _ = LoadLibrary(sdkPath); }
        private static string SearchKeywordFormat(EverythingSettings settings, string query, out int maxCount, bool macroEnabled)
        {
            var keyword = query;
            if (query.StartsWith("@", StringComparison.CurrentCultureIgnoreCase))
            {
                EverythingSdk.Everything_SetRegex(true);

                // keyWord = keyWord.Substring(1);
                keyword = query[1..];
            }
            else
            {
                EverythingSdk.Everything_SetRegex(false);
            }

            Match match = Regex.Match(keyword, @"(count:(?<count>\d+))", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            if (match.Success)
            {
                maxCount = int.Parse(match.Groups["count"].Value, CultureInfo.DefaultThreadCurrentCulture);
                keyword = keyword.Replace(match.Value, string.Empty).Trim();
            }
            else
                maxCount = settings.MaxSearchCount;

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

            var macro = settings.Macros.FirstOrDefault(m => m.Prefix.Equals(separator, StringComparison.OrdinalIgnoreCase));
            return macro == null ? keyword : $"{macro} {string.Join(":", keywordItems.Skip(1))}";
        }

        #endregion Private Methods
    }
}