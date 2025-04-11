using EverythingExtension.Extensions;
using EverythingExtension.Search;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using static EverythingExtension.SDK.EverythingSDK;

namespace EverythingExtension.SDK
{
    internal sealed class SearchItemContext(int index, Version version)
    {
        #region Properties

        public string FullPath => GetFullPath() ?? "";

        public string Path => Invoke<IntPtr>("Everything_GetResultPathW").GetString();

        public string FileName => Invoke<IntPtr>("Everything_GetResultFileNameW").GetString();

        public string HighlightedFileName => Invoke<IntPtr>("Everything_GetResultHighlightedFileNameW").GetString();

        public string HighlightedFullPathAndFileName => Invoke<IntPtr>("Everything_GetResultHighlightedFullPathAndFileNameW").GetString();

        public string Extension => Invoke<IntPtr>("Everything_GetResultExtensionW").GetString();

        public ResultType Type => GetResultType();

        public int? Size => GetFileSize();

        #endregion Properties

        #region Public Methods

        public SearchResult Result(int index)
        {
            return new SearchResult(FileName, FullPath, Type, index, Extension)
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
            MethodInfo? methodInfo = typeof(EverythingSDK).GetMethod("Everything_GetResultFullPathNameW", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            if (!methodInfo.IsVersionAvailable(version))
                return default;

            if (methodInfo == null)
                return default;

            char[] path = new char[1024];

            _ = methodInfo.Invoke(null, [index, path, 1024]);

            return new string(path).TrimEnd('\0');
        }

        private int? GetFileSize()
        {
            if (Type != ResultType.File)
                return default;

            MethodInfo? methodInfo = typeof(EverythingSDK).GetMethod("Everything_GetResultSize", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            if (!methodInfo.IsVersionAvailable(version))
                return default;

            if (methodInfo == null)
                return default;

            object[] args = [index, new LARGE_INTEGER()];
            _ = methodInfo.Invoke(null, args);
            LARGE_INTEGER size = (LARGE_INTEGER)args[1];
            return size.LowPart;
        }

        private T? Invoke<T>(string methodName)
        {
            MethodInfo? methodInfo = typeof(EverythingSDK).GetMethod(methodName, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            if (!methodInfo.IsVersionAvailable(version))
                return default;

            if (methodInfo == null)
                return default;

            return (T?)methodInfo.Invoke(null, [index]);
        }

        #endregion Private Methods
    }
}