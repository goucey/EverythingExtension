using EverythingExtension.Properties;
using EverythingExtension.Search;
using EverythingExtension.Utils;

using Microsoft.CommandPalette.Extensions.Toolkit;

using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EverythingExtension.Commands
{
    internal sealed partial class RunAsAdminCommand : InvokableCommand
    {
        #region Public Constructors

        public RunAsAdminCommand(SearchResult searchResult)
        {
            Icon = TheIcon;
            Name = Resources.everything_run_as_administrator;

            _searchResult = searchResult;
        }

        #endregion Public Constructors

        #region Fields

        private static readonly IconInfo TheIcon = new("\uE7EF");
        private readonly SearchResult _searchResult;

        #endregion Fields

        #region Public Methods

        public override CommandResult Invoke()
        {
            var target = _searchResult.FullPath;
            var parentDir = _searchResult.GetDirectoryPath();

            if (!string.IsNullOrEmpty(parentDir))
                _ = RunAsAdmin(target, parentDir, false).ConfigureAwait(false);

            return CommandResult.Dismiss();
        }

        #endregion Public Methods

        #region Internal Methods

        private static async Task RunAsAdmin(string target, string parentDir, bool packaged)
        {
            await Task.Run(() =>
            {
                if (packaged)
                {
                    var command = "shell:AppsFolder\\" + target;
                    command = Environment.ExpandEnvironmentVariables(command.Trim());

                    var info = command.SetProcessStartInfo(verb: "runas");
                    info.UseShellExecute = true;
                    info.Arguments = string.Empty;
                    Process.Start(info);
                }
                else
                {
                    var info = ShellCommand.GetProcessStartInfo(target, parentDir, string.Empty, ShellCommand.RunAsType.Administrator);

                    Process.Start(info);
                }
            });
        }

        #endregion Internal Methods
    }
}