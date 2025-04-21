using EverythingExtension.Extensions;
using EverythingExtension.Search;

using Serilog;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

using static EverythingExtension.SDK.EverythingSdk;

namespace EverythingExtension.SDK
{
    internal sealed class SearchItemContext(int index, Version version)
    {
        #region Properties

        public string HighlightedFileName => Invoke<IntPtr>("Everything_GetResultHighlightedFileNameW").GetString();
        public string HighlightedFullPathAndFileName => Invoke<IntPtr>("Everything_GetResultHighlightedFullPathAndFileNameW").GetString();
        private string FullPath => GetFullPath() ?? ConvertHighlightToFullPath();

        private string Path => Invoke<IntPtr>("Everything_GetResultPathW").GetString();

        private string FileName => Invoke<IntPtr>("Everything_GetResultFileNameW").GetString();
        private string Extension => Invoke<IntPtr>("Everything_GetResultExtensionW").GetString();

        private ResultType Type => GetResultType();

        private int? Size => GetFileSize();

        #endregion Properties

        #region Public Methods

        public SearchResult Result()
        {
            if (string.IsNullOrWhiteSpace(FullPath))
                Log.Information("Result: {FileName} → {FullPath}", FileName, FullPath);

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
            var path = new char[1024];

            object[] args = [index, path, 1024];

            _ = Invoke<object>("Everything_GetResultFullPathNameW", ref args);

            string fullPath = new string(path).TrimEnd('\0');

            return string.IsNullOrWhiteSpace(fullPath) ? null : fullPath;
        }

        private int? GetFileSize()
        {
            if (Type != ResultType.File)
                return null;

            object[] args = [index, new LargeInteger()];

            _ = Invoke<object>("Everything_GetResultSize", ref args);

            LargeInteger size = (LargeInteger)args[1];
            return size.LowPart;
        }

        private T? Invoke<T>(string methodName)
        {
            MethodInfo? methodInfo = typeof(EverythingSdk).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);

            if (!methodInfo.IsVersionAvailable(version))
            {
                Log.Warning("{methodName} 当前版本不支持", methodName);
                return default;
            }

            if (methodInfo == null)
            {
                Log.Warning("{methodName} 接口不存在", methodName);
                return default;
            }

            return (T?)methodInfo.Invoke(null, [index]);
        }

        private T? Invoke<T>(string methodName, ref object[] args)
        {
            MethodInfo? methodInfo = typeof(EverythingSdk).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);

            if (!methodInfo.IsVersionAvailable(version))
            {
                Log.Warning("{methodName} 当前版本不支持", methodName);
                return default;
            }

            if (methodInfo == null)
            {
                Log.Warning("{methodName} 接口不存在", methodName);
                return default;
            }

            return (T?)methodInfo.Invoke(null, args);
        }

        private void Invoke(string methodName, ref object[] args)
        {
            MethodInfo? methodInfo = typeof(EverythingSdk).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);

            if (!methodInfo.IsVersionAvailable(version))
            {
                Log.Warning("{methodName} 当前版本不支持", methodName);
                return;
            }

            if (methodInfo == null)
            {
                Log.Warning("{methodName} 接口不存在", methodName);
                return;
            }

            _ = methodInfo.Invoke(null, args);
        }

        private string ConvertHighlightToFullPath()
        {
            StringBuilder content = new StringBuilder();
            bool flag = false;
            char[]? contentArray = HighlightedFullPathAndFileName?.ToCharArray();
            int count = 0;
            for (int i = 0; i < contentArray!.Length; i++)
            {
                char current = HighlightedFullPathAndFileName![i];
                if (current == '*')
                {
                    flag = !flag;
                    count++;
                }
                else
                {
                    content.Append(current);
                }
            }

            return content.ToString();
        }

        #endregion Private Methods
    }
}