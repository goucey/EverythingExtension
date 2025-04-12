// Copyright (c) Microsoft Corporation The Microsoft Corporation licenses this file to you under the
// MIT license. See the LICENSE file in the project root for more information.

using EverythingExtension.Commands;
using EverythingExtension.Pages;
using EverythingExtension.SDK;
using EverythingExtension.Search;

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using System;
using System.Collections.Generic;
using System.IO;

namespace EverythingExtension
{
    internal static class ContextMenuLoader
    {
        #region Fields

        //private static readonly IFileSystem FileSystem = new FileSystem();
        //private static readonly IPath Path = FileSystem.Path;
        //private static readonly IFile File = FileSystem.File;
        //private static readonly IDirectory Directory = FileSystem.Directory;

        //private readonly PluginInitContext _context;

        // Extensions for adding run as admin context menu item for applications
        private static readonly string[] AppExtensions = [".exe", ".bat", ".appref-ms", ".lnk", ".msi", ".msix"];

        #endregion Fields

        #region Public Methods

        public static IContextItem[] LoadContextMenus(SearchResult searchResult, bool isFirstLevelFolder = false)
        {
            List<IContextItem> contextMenus = [];
            // Test to check if File can be Run as admin, if yes, we add a 'run as admin' context
            // menu item

            if (searchResult.IsPreview)
            {
                contextMenus.Add(new CommandContextItem(new TextPreviewPage(searchResult)));
            }

            if (CanFileBeRunAsAdmin(searchResult.FullPath))
            {
                contextMenus.Add(new CommandContextItem(new RunAsAdminCommand(searchResult)));
                contextMenus.Add(new CommandContextItem(new RunAsUserCommand(searchResult)));
            }

            if (searchResult.Type == ResultType.File)
            {
                contextMenus.Add(new CommandContextItem(new OpenInShellCommand("explorer.exe", $"/select,\"{searchResult.FullPath}\"")));
            }
            else
            {
                contextMenus.Add(isFirstLevelFolder
                    ? new CommandContextItem(new OpenCommand(searchResult))
                    : new CommandContextItem(new DirectoryExplorePage(searchResult.FullPath)));
            }

            contextMenus.Add(new CommandContextItem(new CopyPathCommand(searchResult)));
            contextMenus.Add(new CommandContextItem(new OpenInConsoleCommand(searchResult)));
            contextMenus.Add(new CommandContextItem(new DeleteCommand(searchResult)));

            return [.. contextMenus];
        }

        #endregion Public Methods

        #region Private Methods

        // Function to test if the file can be run as admin
        private static bool CanFileBeRunAsAdmin(string path)
        {
            string fileExtension = Path.GetExtension(path);
            foreach (string extension in AppExtensions)
            {
                // Using OrdinalIgnoreCase since this is internal
                if (extension.Equals(fileExtension, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion Private Methods
    }
}