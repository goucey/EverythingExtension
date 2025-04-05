using Microsoft.CommandPalette.Extensions.Toolkit;
using Microsoft.CommandPalette.Extensions;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Windows.Storage.Streams;
using EverythingExtension.Properties;
using EverythingExtension.Settings;
using EverythingExtension.Search;

namespace EverythingExtension.Pages
{
    internal partial class DirectoryExplorePage : DynamicListPage
    {
        #region Public Constructors

        public DirectoryExplorePage(string path)
        {
            _path = path;
            Title = path;
            Name = Resources.everything_folder_browse;
            Icon = new IconInfo("\uec50");
        }

        #endregion Public Constructors

        #region Fields

        private string _path;
        private List<EverythingListItem>? _directoryContents;
        private List<EverythingListItem>? _filteredContents;

        #endregion Fields

        #region Public Methods

        public override void UpdateSearchText(string oldSearch, string newSearch)
        {
            if (_directoryContents == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(newSearch))
            {
                if (_filteredContents != null)
                {
                    _filteredContents = null;
                    RaiseItemsChanged(-1);
                }

                return;
            }

            // Need to break this out the manual way so that the compiler can know this is an ExploreListItem
            var filteredResults = ListHelpers.FilterList(
                _directoryContents,
                newSearch,
                (s, i) => ListHelpers.ScoreListItem(s, i));

            if (_filteredContents != null)
            {
                lock (_filteredContents)
                {
                    ListHelpers.InPlaceUpdateList<EverythingListItem>(_filteredContents, filteredResults);
                }
            }
            else
            {
                _filteredContents = filteredResults.ToList();
            }

            RaiseItemsChanged(-1);
        }

        public override IListItem[] GetItems()
        {
            if (_filteredContents != null)
            {
                if (_filteredContents.Count > 0)
                    return _filteredContents.ToArray();
                else
                {
                    return CreateFolderIsEmptyCommandItem("搜索结果为空");
                }
            }

            if (_directoryContents != null)
            {
                if (_directoryContents.Count > 0)
                    return _directoryContents.ToArray();
                else
                {
                    return CreateFolderIsEmptyCommandItem(Resources.everything_folder_is_empty);
                }
            }

            IsLoading = true;
            if (!Path.Exists(_path))
            {
                EmptyContent = new CommandItem(title: Resources.everything_file_does_not_exit)
                {
                    Icon = new IconInfo("\ue783"),
                    Command = new AnonymousCommand(() => { })
                    {
                        Name = Resources.everything_go_back,
                        Result = CommandResult.GoBack()
                    }
                };
                IsLoading = false;
                return [];
            }

            var attr = File.GetAttributes(_path);

            // detect whether its a directory or file
            if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
            {
                EmptyContent = new CommandItem(title: Resources.everything_file_is_file_not_folder)
                {
                    Icon = new IconInfo("\ue838"),
                    Command = new AnonymousCommand(() => { })
                    {
                        Name = Resources.everything_go_back,
                        Result = CommandResult.GoBack()
                    }
                };
                IsLoading = false;
                return [];
            }

            var contents = Directory.EnumerateFileSystemEntries(_path);

            if (!contents.Any())
            {
                IsLoading = false;

                return CreateFolderIsEmptyCommandItem(Resources.everything_folder_is_empty);
            }

            _directoryContents = [.. contents
                .Select(s => new SearchResult(s))
                .Select(i => new EverythingListItem(i,true))];

            //_ = Task.Run(() =>
            //{
            //    foreach (var item in _directoryContents)
            //    {
            //        IconInfo? icon = null;
            //        try
            //        {
            //            var stream = ThumbnailHelper.GetThumbnail(item.Item.FullPath).Result;
            //            if (stream != null)
            //            {
            //                var data = new IconData(RandomAccessStreamReference.CreateFromStream(stream));
            //                icon = new IconInfo(data, data);
            //            }
            //        }
            //        catch
            //        {
            //        }

            //        item.Icon = icon;
            //    }
            //});

            IsLoading = false;

            return _directoryContents.ToArray();
        }

        #endregion Public Methods

        #region Private Methods

        private void HandlePathChangeRequested(EverythingListItem sender, string path)
        {
            _directoryContents = null;
            _filteredContents = null;
            _path = path;
            Title = path;
            SearchText = string.Empty;
            RaiseItemsChanged(-1);
        }

        private IListItem[] CreateFolderIsEmptyCommandItem(string title)
        {
            SearchResult result = new SearchResult(_path);
            EverythingListItem listItem = new EverythingListItem(result);
            EmptyContent = new CommandItem(title: title)
            {
                Icon = new IconInfo("\uE838"),
                Command = listItem.Command,
                MoreCommands = ContextMenuLoader.LoadContextMenus(result)
            };

            return [];
        }

        #endregion Private Methods
    }
}