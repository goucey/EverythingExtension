﻿using EverythingExtension.Properties;
using EverythingExtension.SDK;
using EverythingExtension.Search;

using Microsoft.CommandPalette.Extensions.Toolkit;

namespace EverythingExtension.Commands
{
    /// <summary>
    /// 将所选项目的路径复制到剪贴板
    /// </summary>
    internal sealed partial class CopyPathCommand : InvokableCommand
    {
        #region Internal Constructors

        internal CopyPathCommand(SearchResult searchResult)
        {
            _searchResult = searchResult;
            Name = Resources.everything_copy_path;
            Icon = new IconInfo("\uE71B");
        }

        #endregion Internal Constructors

        #region Fields

        private readonly SearchResult _searchResult;

        #endregion Fields

        #region Public Methods

        public override CommandResult Invoke()
        {
            try
            {
                ClipboardHelper.SetText(_searchResult.FullPath); // 🟢🔴
                _ = EverythingSdk.Everything_IncRunCountFromFileNameW(_searchResult.FullPath);
                return CommandResult.ShowToast(Resources.everything_clipboard_success);
            }
            catch
            {
                return CommandResult.ShowToast(Resources.everything_clipboard_failed);
            }

            //return CommandResult.KeepOpen();
        }

        #endregion Public Methods
    }
}