using EverythingExtension.Properties;
using EverythingExtension.SDK;
using EverythingExtension.Search;

using Microsoft.CommandPalette.Extensions.Toolkit;

using System.Globalization;
using System.IO;

namespace EverythingExtension.Commands
{
    /// <summary>
    /// 删除文件或文件夹命令
    /// </summary>
    internal sealed partial class DeleteCommand : InvokableCommand
    {
        #region Public Constructors

        public DeleteCommand(SearchResult searchResult, bool isConfirm = true)
        {
            Name = searchResult.GetDeleteCommandName();
            Icon = TheIcon;

            _searchResult = searchResult;
            _isConfirm = isConfirm;
        }

        #endregion Public Constructors

        #region Fields

        private static readonly IconInfo TheIcon = new("\ue74D");
        private readonly SearchResult _searchResult;
        private readonly bool _isConfirm;

        #endregion Fields

        #region Public Methods

        public override CommandResult Invoke()
        {
            if (_isConfirm)
            {
                string message = _searchResult.Type == ResultType.File ?
                           Resources.everything_delete_file_tips :
                           Resources.everything_delete_folder_tips;

                ConfirmationArgs args = new()
                {
                    Title = Resources.everything_delete_tips_caption,
                    Description = string.Format(CultureInfo.CurrentCulture, message, _searchResult.FileName),
                    IsPrimaryCommandCritical = true,
                    PrimaryCommand = new DeleteCommand(_searchResult, false)
                };
                return CommandResult.Confirm(args);
            }

            if (_searchResult.Type == ResultType.File)
            {
                File.Delete(_searchResult.FullPath);
            }
            else
            {
                Directory.Delete(_searchResult.FullPath, true);
            }
            _searchResult.Deleted?.Invoke();
            return CommandResult.KeepOpen();
        }

        #endregion Public Methods
    }
}