using Microsoft.CommandPalette.Extensions.Toolkit;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using EverythingExtension.Search;
using EverythingExtension.Properties;
using EverythingExtension.Utils;

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
            var _target = _searchResult.FullPath;
            var _parentDir = _searchResult.GetDirectoryPath();

            if (!string.IsNullOrEmpty(_parentDir))
                _ = RunAsAdmin(_target, _parentDir).ConfigureAwait(false);

            return CommandResult.Dismiss();
        }

        #endregion Public Methods

        #region Internal Methods

        internal static async Task RunAsAdmin(string target, string parentDir)
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