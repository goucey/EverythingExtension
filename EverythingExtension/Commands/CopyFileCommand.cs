using EverythingExtension.Properties;
using EverythingExtension.SDK;
using EverythingExtension.Search;

using Microsoft.CommandPalette.Extensions.Toolkit;

using Serilog;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;

namespace EverythingExtension.Commands
{
    internal class CopyFileCommand : InvokableCommand
    {
        #region Internal Constructors

        internal CopyFileCommand(SearchResult searchResult)
        {
            _searchResult = searchResult;
            Name = Resources.everything_copy_file;
            Icon = new IconInfo("\uE8c8");
        }

        #endregion Internal Constructors

        #region Fields

        private readonly SearchResult _searchResult;

        #endregion Fields

        #region Public Methods

        public override CommandResult Invoke()
        {
            var result = Task.Run(async () =>
            {
                try
                {
                    DataPackage package = new DataPackage();
                    package.RequestedOperation = DataPackageOperation.Copy;

                    IStorageItem storage = _searchResult.IsDirectory() ?
                    await StorageFolder.GetFolderFromPathAsync(_searchResult.FullPath) :
                    await StorageFile.GetFileFromPathAsync(_searchResult.FullPath);

                    package.SetStorageItems([storage]);

                    Clipboard.SetContentWithOptions(package, new ClipboardContentOptions());
                    _ = EverythingSdk.Everything_IncRunCountFromFileNameW(_searchResult.FullPath);
                    Clipboard.Flush();
                    return CommandResult.ShowToast(Resources.everything_clipboard_success);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Failed to copy file to clipboard");
                    return CommandResult.ShowToast(Resources.everything_clipboard_failed);
                }
            }).GetAwaiter().GetResult();

            return result ?? CommandResult.KeepOpen();

            //try
            //{
            //    Clipboard.Clear();
            //    DataPackage package = new DataPackage();
            //    package.RequestedOperation = DataPackageOperation.Copy;
            //    package.SetStorageItems()
            //    package.se(_searchResult.FileName, _searchResult.FileName);
            //    StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(_searchResult.FullPath);
            //    Clipboard.SetContent(package); // 🟢🔴
            //    return CommandResult.ShowToast(Resources.everything_clipboard_success);
            //}
            //catch
            //{
            //    return CommandResult.ShowToast(Resources.everything_clipboard_failed);
            //}

            //return CommandResult.KeepOpen();
        }

        private async Task COpyAsync()
        {
            Clipboard.Clear();
            DataPackage package = new DataPackage();
            package.RequestedOperation = DataPackageOperation.Copy;
            await Task.Run(async () =>
            {
                try
                {
                    IStorageItem storage = _searchResult.IsDirectory() ?
            await StorageFolder.GetFolderFromPathAsync(_searchResult.FullPath) :
            await StorageFile.GetFileFromPathAsync(_searchResult.FullPath);

                    package.SetStorageItems([storage]);

                    Clipboard.SetContentWithOptions(package, new ClipboardContentOptions());
                }
                catch (Exception)
                {
                }
            });
        }

        #endregion Public Methods
    }
}