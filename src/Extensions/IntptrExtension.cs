using System;
using System.Runtime.InteropServices;

namespace EverythingExtension.Extensions
{
    internal static class IntPtrExtension
    {
        #region Public Methods

        public static string GetString(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return string.Empty;

            return Marshal.PtrToStringUni(ptr) ?? string.Empty;
        }

        #endregion Public Methods
    }
}