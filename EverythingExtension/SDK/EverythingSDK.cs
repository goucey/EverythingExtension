// Copyright (c) Microsoft Corporation The Microsoft Corporation licenses this file to you under the
// MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace EverythingExtension.SDK
{
    internal sealed class EverythingSDK
    {
        #region Fields

        internal const string DllPath = "Everything64.dll";

        #endregion Fields

        #region Enums

        public enum StateCode
        {
            OK,
            MemoryError,
            IPCError,
            RegisterClassExError,
            CreateWindowError,
            CreateThreadError,
            InvalidIndexError,
            InvalidCallError,
        }

        public enum RequestFlag
        {
            FileName = 0x00000001,
            Path = 0x00000002,
            FullPathAndFileName = 0x00000004,
            Extension = 0x00000008,
            Size = 0x00000010,
            DateCreated = 0x00000020,
            DateModified = 0x00000040,
            DateAccessed = 0x00000080,
            Attributes = 0x00000100,
            FileListFileName = 0x00000200,
            RunCount = 0x00000400,
            DateRun = 0x00000800,
            DateRecentlyChanged = 0x00001000,
            HighlightedFileName = 0x00002000,
            HighlightedPath = 0x00004000,
            HighlightedFullPathAndFileName = 0x00008000,
        }

        #endregion Enums

        #region Public Methods

        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultHighlightedPathW(int nIndex);

        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultHighlightedFileNameW(int nIndex);

        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultHighlightedFullPathAndFileNameW(int nIndex);

        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultExtensionW(int nIndex);

        [DllImport(DllPath)]
        public static extern int Everything_GetMajorVersion();

        [DllImport(DllPath)]
        public static extern int Everything_GetMinorVersion();

        [DllImport(DllPath)]
        public static extern int Everything_GetRevision();

        [DllImport(DllPath)]
        public static extern void Everything_SetRequestFlags(RequestFlag flag);

        #endregion Public Methods

        #region Internal Methods

        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything_GetResultFileNameW(int nIndex);

        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        internal static extern int Everything_SetSearchW(string lpSearchString);

        [DllImport(DllPath)]
        internal static extern void Everything_SetMatchPath(bool bEnable);

        [DllImport(DllPath)]
        internal static extern void Everything_SetMatchCase(bool bEnable);

        [DllImport(DllPath)]
        internal static extern void Everything_SetMatchWholeWord(bool bEnable);

        [DllImport(DllPath)]
        internal static extern void Everything_SetRegex(bool bEnable);

        [DllImport(DllPath)]
        internal static extern void Everything_SetMax(int dwMax);

        [DllImport(DllPath)]
        internal static extern void Everything_SetOffset(int dwOffset);

        [DllImport(DllPath)]
        internal static extern bool Everything_GetMatchPath();

        [DllImport(DllPath)]
        internal static extern bool Everything_GetMatchCase();

        [DllImport(DllPath)]
        internal static extern bool Everything_GetMatchWholeWord();

        [DllImport(DllPath)]
        internal static extern bool Everything_GetRegex();

        [DllImport(DllPath)]
        internal static extern uint Everything_GetMax();

        [DllImport(DllPath)]
        internal static extern uint Everything_GetOffset();

        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        internal static extern string Everything_GetSearchW();

        [DllImport(DllPath)]
        internal static extern StateCode Everything_GetLastError();

        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        internal static extern bool Everything_QueryW(bool bWait);

        [DllImport(DllPath)]
        internal static extern void Everything_SortResultsByPath();

        [DllImport(DllPath)]
        internal static extern int Everything_GetNumFileResults();

        [DllImport(DllPath)]
        internal static extern int Everything_GetNumFolderResults();

        [DllImport(DllPath)]
        internal static extern int Everything_GetNumResults();

        [DllImport(DllPath)]
        internal static extern int Everything_GetTotFileResults();

        [DllImport(DllPath)]
        internal static extern int Everything_GetTotFolderResults();

        [DllImport(DllPath)]
        internal static extern int Everything_GetTotResults();

        [DllImport(DllPath)]
        internal static extern bool Everything_IsVolumeResult(int nIndex);

        [DllImport(DllPath)]
        internal static extern bool Everything_IsFolderResult(int nIndex);

        [DllImport(DllPath)]
        internal static extern bool Everything_IsFileResult(int nIndex);

        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        internal static extern void Everything_GetResultFullPathNameW(int nIndex, string lpString, int nMaxCount);

        [DllImport(DllPath)]
        internal static extern void Everything_Reset();

        #endregion Internal Methods
    }
}