using System;
using System.Globalization;

namespace EverythingExtension.SDK
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class MinVersionAttribute(string version) : Attribute
    {
        #region Properties

        public Version Version { get; } = Version.Parse(version.ToString(CultureInfo.InvariantCulture));

        #endregion Properties
    }
}