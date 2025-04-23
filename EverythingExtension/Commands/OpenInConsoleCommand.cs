using EverythingExtension.Properties;
using EverythingExtension.SDK;
using EverythingExtension.Search;

using Microsoft.CommandPalette.Extensions.Toolkit;

using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EverythingExtension.Commands
{
    internal sealed partial class OpenInConsoleCommand : InvokableCommand
    {
        #region Public Constructors

        public OpenInConsoleCommand(SearchResult searchResult)
        {
            _searchResult = searchResult;

            Icon = _theIcon;
            Name = Resources.everything_open_in_console;
        }

        #endregion Public Constructors

        #region Fields

        private static readonly IconInfo _theIcon = new("\ue756");
        private readonly SearchResult _searchResult;

        #endregion Fields

        #region Public Methods

        public override CommandResult Invoke()
        {
            string? path = _searchResult.GetDirectoryPath();

            if (!string.IsNullOrEmpty(path))
            {
                _ = LaunchTarget(path).ConfigureAwait(false);

                _ = EverythingSdk.Everything_IncRunCountFromFileNameW(_searchResult.FullPath);
            }

            return CommandResult.Dismiss();
        }

        #endregion Public Methods

        #region Internal Methods

        private static async Task LaunchTarget(string t)
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