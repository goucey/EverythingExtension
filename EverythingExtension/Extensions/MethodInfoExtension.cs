using EverythingExtension.SDK;

namespace System.Reflection

{
    internal static class MethodInfoExtension
    {
        #region Internal Methods

        internal static bool IsVersionAvailable(this MethodInfo? methodInfo, Version version)
        {
            if (methodInfo == null)
                return false;

            MinVersionAttribute? minVersion = methodInfo.GetCustomAttribute<MinVersionAttribute>();
            if (minVersion == null)
                return true;

            return version >= minVersion.Version;
        }

        #endregion Internal Methods
    }
}