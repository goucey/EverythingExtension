using EverythingExtension.Properties;
using EverythingExtension.Search;

using Microsoft.CommandPalette.Extensions.Toolkit;

using System.Diagnostics;
using System.Threading.Tasks;

namespace EverythingExtension.Commands
{
    internal sealed partial class OpenCommand : InvokableCommand
    {
        #region Fields

        private static readonly IconInfo _theIcon = new("\uE8E5");
        private readonly SearchResult _searchResult;

        #endregion Fields

        #region Public Constructors

        public OpenCommand(SearchResult searchResult)
        {
            Name = Resources.everything_open;
            Icon = _theIcon;
            _searchResult = searchResult;
        }

        #endregion Public Constructors

        #region Public Methods

        public override CommandResult Invoke()
        {
            _ = LaunchTarget(_searchResult.FullPath).ConfigureAwait(false);
            return CommandResult.Hide();
        }

        private static async Task LaunchTarget(string t)
        {
            await Task.Run(() =>
            {
                Process.Start(new ProcessStartInfo(t) { UseShellExecute = true });
            });
        }

        #endregion Public Methods
    }
}