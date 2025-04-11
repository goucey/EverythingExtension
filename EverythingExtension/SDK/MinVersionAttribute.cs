using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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