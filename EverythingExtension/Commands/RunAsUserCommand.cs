using Microsoft.CommandPalette.Extensions.Toolkit;

using System.Diagnostics;
using System.Threading.Tasks;
using EverythingExtension.Search;
using EverythingExtension.Properties;
using EverythingExtension.Utils;
using EverythingExtension.SDK;

namespace EverythingExtension.Commands
{
    internal sealed partial class RunAsUserCommand : InvokableCommand
    {
        #region Public Constructors

        public RunAsUserCommand(SearchResult searchResult)
        {
            Name = Resources.everything_run_as_user;
            Icon = TheIcon;

            _searchResult = searchResult;
        }

        #endregion Public Constructors

        #region Fields

        private static readonly IconInfo TheIcon = new("\uE7EE");

        private readonly SearchResult _searchResult;

        #endregion Fields

        #region Public Methods

        public override CommandResult Invoke()
        {
            var target = _searchResult.FullPath;
            var parentDir = _searchResult.GetDirectoryPath();

            if (!string.IsNullOrEmpty(parentDir))
            {
                _ = RunAsAdmin(target, parentDir).ConfigureAwait(false);

                _ = EverythingSdk.Everything_IncRunCountFromFileNameW(_searchResult.FullPath);
            }

            return CommandResult.Dismiss();
        }

        #endregion Public Methods

        #region Internal Methods

        private static async Task RunAsAdmin(string target, string parentDir)
        {
            await Task.Run(() =>
            {
                var info = ShellCommand.GetProcessStartInfo(target, parentDir, string.Empty, ShellCommand.RunAsType.OtherUser);

                Process.Start(info);
            });
        }

        #endregion Internal Methods
    }
}