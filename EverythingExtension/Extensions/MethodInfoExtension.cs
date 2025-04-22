using System;
using System.Reflection;

using EverythingExtension.SDK;

using Serilog;

namespace EverythingExtension.Extensions

{
    internal static class MethodInfoExtension
    {
        #region Internal Methods

        internal static bool IsVersionAvailable(this MethodInfo? methodInfo, Version version)
        {
            if (methodInfo == null)
            {
                Log.Warning("{methodName}:方法信息为空。", "IsVersionAvailable");
                return false;
            }

            var minVersion = methodInfo.GetCustomAttribute<MinVersionAttribute>();
            if (minVersion == null)
            {
                Log.Warning("{methodName}:方法信息没有 MinVersion 属性。", "IsVersionAvailable");
                return true;
            }

            return version >= minVersion.Version;
        }

        #endregion Internal Methods
    }
}