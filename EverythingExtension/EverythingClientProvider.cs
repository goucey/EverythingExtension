using EverythingExtension.SDK;
using EverythingExtension.Settings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverythingExtension
{
    internal sealed partial class EverythingClientProvider
    {
        #region Public Constructors

        public EverythingClientProvider()
        { }

        #endregion Public Constructors

        #region Public Methods

        public static IEverythingClient GetClient(EverythingSettings settings)
        {
            int majorVersion = EverythingSdk.Everything_GetMajorVersion();
            if (majorVersion > 0)
                return new Internal.EverythingClient(settings);
            else
                return new Internal.V3.EverythingClient(settings);
        }

        #endregion Public Methods
    }
}