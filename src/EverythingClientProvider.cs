using EverythingExtension.SDK;
using EverythingExtension.Settings;

using Serilog;

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
            //Log.Information("检测到当前环境下1.5版本以下的Everything是否正在运行：{ver}", majorVersion);
            if (majorVersion > 0)
            {
                return new Internal.EverythingClient(settings);
            }
            else
            {
                return new Internal.V3.EverythingClient(settings);
            }
        }

        #endregion Public Methods
    }
}