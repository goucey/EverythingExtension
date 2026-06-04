using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverythingExtension
{
    internal sealed partial class ExtensionHostInstance
    {
        #region Properties

        public IExtensionHost? Host { get; private set; }

        #endregion Properties

        #region Public Methods

        public void Initialize(IExtensionHost host) => Host = host;

        /// <summary>
        /// Fire-and-forget a log message to the Command Palette host app. Since the host is in
        /// another process, we do this in a try/catch in a background thread, as to not block the
        /// calling thread, nor explode if the host app is gone.
        /// </summary>
        /// <param name="message"> The log message to send </param>
        public void LogMessage(ILogMessage message)
        {
            if (Host is not null)
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await Host.LogMessage(message);
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }

        public void LogMessage(string message)
        {
            var logMessage = new LogMessage() { Message = message };
            LogMessage(logMessage);
        }

        public void ShowStatus(IStatusMessage message, StatusContext context)
        {
            if (Host is not null)
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await Host.ShowStatus(message, context);
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }

        public void ShowStatusWithAutoHide(IStatusMessage message, StatusContext context)
        {
            if (Host is not null)
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await Host.ShowStatus(message, context);
                        _ = Task.Run(async () =>
                        {
                            await Task.Delay(2500).ConfigureAwait(false);
                            await Host.HideStatus(message);
                        });
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }

        public void HideStatus(IStatusMessage message)
        {
            if (Host is not null)
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await Host.HideStatus(message);
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }

        #endregion Public Methods
    }

    internal sealed partial class EverythingExtensionHost
    {
        #region Properties

        internal static ExtensionHostInstance Instance { get; } = new();

        #endregion Properties
    }
}