using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EverythingExtension.Extensions
{
    internal static class IntptrExtension
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