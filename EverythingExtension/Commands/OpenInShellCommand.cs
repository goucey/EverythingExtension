using EverythingExtension.Properties;

using Microsoft.CommandPalette.Extensions.Toolkit;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EverythingExtension.Commands
{
    internal partial class OpenInShellCommand : InvokableCommand
    {
        #region Fields

        private static readonly IconInfo TheIcon = new("\ue838");
        private readonly string _fileName;
        private readonly string _arguments;

        #endregion Fields

        #region Public Constructors

        public OpenInShellCommand(string fileName, string argumants)
        {
            Name = Resources.everything_open_containing_folder;
            Icon = TheIcon;
            _fileName = fileName;
            _arguments = argumants;
        }

        #endregion Public Constructors

        #region Public Methods

        public override CommandResult Invoke()
        {
            _ = LaunchTarget(_fileName, _arguments).ConfigureAwait(false);
            return CommandResult.Dismiss();
        }

        internal static async Task LaunchTarget(string fileName, string argumants)
        {
            await Task.Run(() =>
            {
                Process.Start(new ProcessStartInfo(fileName, argumants) { UseShellExecute = true });
            });
        }

        #endregion Public Methods
    }
}