using EverythingExtension.Properties;
using EverythingExtension.Search;

using Microsoft.CommandPalette.Extensions.Toolkit;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverythingExtension.Commands
{
    internal sealed partial class OpenCommand : InvokableCommand
    {
        #region Fields

        private static readonly IconInfo TheIcon = new("\uE8E5");
        private readonly SearchResult _searchResult;

        #endregion Fields

        #region Public Constructors

        public OpenCommand(SearchResult searchResult)
        {
            Name = Resources.everything_open;
            Icon = TheIcon;
            _searchResult = searchResult;
        }

        #endregion Public Constructors

        #region Public Methods

        public override CommandResult Invoke()
        {
            _ = LaunchTarget(_searchResult.FullPath).ConfigureAwait(false);
            return CommandResult.Hide();
        }

        internal static async Task LaunchTarget(string t)
        {
            await Task.Run(() =>
            {
                Process.Start(new ProcessStartInfo(t) { UseShellExecute = true });
            });
        }

        #endregion Public Methods
    }
}