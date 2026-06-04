using EverythingExtension.Extensions;
using EverythingExtension.SDK;

using Serilog;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

using Windows.Storage.Streams;

using static EverythingExtension.SDK.EverythingSdk;

namespace EverythingExtension.Internal.V3
{
    internal sealed class SearchItemContext(IntPtr list, IntPtr index, Version version)
    {
        #region Properties

        private string FullPath => Invoke("Everything3_GetResultFullPathNameW");

        private string Path => Invoke("Everything3_GetResultPathW");

        private string FileName => Invoke("Everything3_GetResultNameW");

        private string Extension => Invoke("Everything3_GetResultExtensionW");

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

        private string Invoke(string methodName)
        {
            MethodInfo? methodInfo = typeof(EverythingSdk).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);

            if (!methodInfo.IsVersionAvailable(version))
            {
                Log.Warning("{methodName} 当前版本【{version}】不支持", methodName, version);
                return string.Empty;
            }

            if (methodInfo == null)
            {
                Log.Warning("{methodName} 接口不存在", methodName);
                return string.Empty;
            }
            int size = 32767;
            var buffer = new char[size];
            var len = methodInfo.Invoke(null, [list, index, buffer, (IntPtr)size]) ?? 0;
            return new string(buffer, 0, (int)len);
        }

        private ResultType GetResultType()
        {
            var t = Invoke("Everything3_GetResultTypeW");
            if (Everything3_IsFolderResult(list, index))
                return ResultType.Folder;
            else
            {
                return ResultType.File;
            }
        }

        private int? GetFileSize()
        {
            if (Type != ResultType.File)
                return null;

            return (int)Everything3_GetResultSize(list, index);
        }

        #endregion Private Methods
    }
}