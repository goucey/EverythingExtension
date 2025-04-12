using System;
using System.Reflection;
using EverythingExtension.SDK;

namespace EverythingExtension.Extensions

{
    internal static class MethodInfoExtension
    {
        #region Internal Methods

        internal static bool IsVersionAvailable(this MethodInfo? methodInfo, Version version)
        {
            if (methodInfo == null)
                return false;

            var minVersion = methodInfo.GetCustomAttribute<MinVersionAttribute>();
            if (minVersion == null)
                return true;

            return version >= minVersion.Version;
        }

        #endregion Internal Methods
    }
}