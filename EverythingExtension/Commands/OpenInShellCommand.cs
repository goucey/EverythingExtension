using EverythingExtension.Properties;
using EverythingExtension.SDK;
using EverythingExtension.Search;

using Microsoft.CommandPalette.Extensions.Toolkit;

using System.Diagnostics;
using System.Threading.Tasks;

namespace EverythingExtension.Commands
{
    internal sealed partial class OpenInShellCommand : InvokableCommand
    {
        #region Fields

        private static readonly IconInfo TheIcon = new("\ue838");
        private readonly string _fileName;
        private readonly string _arguments;

        #endregion Fields

        #region Public Constructors

        public OpenInShellCommand(string fileName, string arguments)
        {
            Name = Resources.everything_open_containing_folder;
            Icon = TheIcon;
            _fileName = fileName;
            _arguments = arguments;
        }

        #endregion Public Constructors

        #region Public Methods

        public override CommandResult Invoke()
        {
            _ = LaunchTarget(_fileName, _arguments).ConfigureAwait(false);

            return CommandResult.Dismiss();
        }

        private static async Task LaunchTarget(string fileName, string arguments)
        {
            await Task.Run(() =>
            {
                Process.Start(new ProcessStartInfo(fileName, arguments) { UseShellExecute = true });
            });
        }

        #endregion Public Methods
    }
}