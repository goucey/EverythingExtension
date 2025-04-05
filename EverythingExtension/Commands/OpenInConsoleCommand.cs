using EverythingExtension.Properties;
using EverythingExtension.Search;

using Microsoft.CommandPalette.Extensions.Toolkit;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EverythingExtension.Commands
{
    internal partial class OpenInConsoleCommand : InvokableCommand
    {
        #region Public Constructors

        public OpenInConsoleCommand(SearchResult searchResult)
        {
            _searchResult = searchResult;

            Icon = TheIcon;
            Name = Resources.everything_open_in_console;
        }

        #endregion Public Constructors

        #region Fields

        private static readonly IconInfo TheIcon = new("\ue756");
        private readonly SearchResult _searchResult;

        #endregion Fields

        #region Public Methods

        public override CommandResult Invoke()
        {
            string? path = _searchResult.GetDirectoryPath();

            if (!string.IsNullOrEmpty(path))
                _ = LaunchTarget(path).ConfigureAwait(false);

            return CommandResult.Dismiss();
        }

        #endregion Public Methods

        #region Internal Methods

        internal static async Task LaunchTarget(string t)
        {
            await Task.Run(() =>
            {
                try
                {
                    var processStartInfo = new ProcessStartInfo
                    {
                        WorkingDirectory = t,
                        FileName = "cmd.exe",
                    };

                    Process.Start(processStartInfo);
                }
                catch (Exception)
                {
                    // Log.Exception($"Failed to open {Name} in console, {e.Message}", e, GetType());
                }
            });
        }

        #endregion Internal Methods
    }
}