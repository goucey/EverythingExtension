using EverythingExtension.Extensions;
using EverythingExtension.Search;

using System;
using System.Reflection;

using static EverythingExtension.SDK.EverythingSdk;

namespace EverythingExtension.SDK
{
    internal sealed class SearchItemContext(int index, Version version)
    {
        #region Properties

        private string FullPath => GetFullPath() ?? "";

        private string Path => Invoke<IntPtr>("Everything_GetResultPathW").GetString();

        private string FileName => Invoke<IntPtr>("Everything_GetResultFileNameW").GetString();

        public string HighlightedFileName => Invoke<IntPtr>("Everything_GetResultHighlightedFileNameW").GetString();

        public string HighlightedFullPathAndFileName => Invoke<IntPtr>("Everything_GetResultHighlightedFullPathAndFileNameW").GetString();

        private string Extension => Invoke<IntPtr>("Everything_GetResultExtensionW").GetString();

        private ResultType Type => GetResultType();

        private int? Size => GetFileSize();

        #endregion Properties

        #region Public Methods

        public SearchResult Result(int index)
        {
            return new SearchResult(FileName, FullPath, Type, Extension)
            {
                Size = Size,
                ParentPath = Path
            };
        }

        #endregion Public Methods

        #region Private Methods

        private ResultType GetResultType()
        {
            if (Invoke<bool>("Everything_IsFolderResult"))
                return ResultType.Folder;
            else if (Invoke<bool>("Everything_IsFileResult"))
                return ResultType.File;
            else if (Invoke<bool>("Everything_IsVolumeResult"))
                return ResultType.Volume;
            else
                return ResultType.None;
        }

        private string? GetFullPath()
        {
            var methodInfo = typeof(EverythingSdk).GetMethod("Everything_GetResultFullPathNameW", BindingFlags.Static | BindingFlags.NonPublic);

            if (!methodInfo.IsVersionAvailable(version))
                return null;

            if (methodInfo == null)
                return null;

            var path = new char[1024];

            _ = methodInfo.Invoke(null, [index, path, 1024]);

            return new string(path).TrimEnd('\0');
        }

        private int? GetFileSize()
        {
            if (Type != ResultType.File)
                return null;

            var methodInfo = typeof(EverythingSdk).GetMethod("Everything_GetResultSize", BindingFlags.Static | BindingFlags.NonPublic);

            if (!methodInfo.IsVersionAvailable(version))
                return null;

            if (methodInfo == null)
                return null;

            object[] args = [index, new LargeInteger()];
            _ = methodInfo.Invoke(null, args);
            LargeInteger size = (LargeInteger)args[1];
            return size.LowPart;
        }

        private T? Invoke<T>(string methodName)
        {
            MethodInfo? methodInfo = typeof(EverythingSdk).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);

            if (!methodInfo.IsVersionAvailable(version))
                return default;

            if (methodInfo == null)
                return default;

            return (T?)methodInfo.Invoke(null, [index]);
        }

        #endregion Private Methods
    }
}