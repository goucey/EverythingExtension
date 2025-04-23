using EverythingExtension.Commands;
using EverythingExtension.Pages;
using EverythingExtension.Properties;
using EverythingExtension.SDK;
using EverythingExtension.Search;

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Windows.Storage.Streams;

namespace EverythingExtension.Settings
{
    internal sealed partial class EverythingListItem : ListItem
    {
        #region Public Constructors

        public EverythingListItem(SearchResult search, bool isFirstLevelFolder = false) : base(new NoOpCommand())
        {
            //new DirectoryExplorePage(search.FullPath)
            Title = search.FileName;
            Subtitle = $"{search.FullPath}";

            if (search.Size.HasValue)
                Tags = [new Tag(search.GetFileSizeDisplay() ?? string.Empty)];

            if (search.Type == ResultType.Folder)
                Command = isFirstLevelFolder ? new DirectoryExplorePage(search.FullPath) : new OpenCommand(search);
            else
                Command = new OpenCommand(search);

            _search = search;

            _details = new Lazy<Details?>(BuildDetails);

            _icon = new Lazy<IconInfo?>(() =>
            {
                var t = FetchIcon();
                t.Wait();
                return t.Result;
            });

            MoreCommands = [.. ContextMenuLoader.LoadContextMenus(search, isFirstLevelFolder)];
        }

        #endregion Public Constructors

        #region Fields

        private readonly SearchResult _search;
        private readonly Lazy<Details?> _details;
        private readonly Lazy<IconInfo?> _icon;

        #endregion Fields

        #region Properties

        public SearchResult Item => _search;
        public override IDetails? Details { get => _details.Value; set => base.Details = value; }

        public override IIconInfo? Icon { get => _icon.Value; set => base.Icon = value; }

        #endregion Properties

        #region Private Methods

        private Details? BuildDetails()
        {
            if (_search.Type != ResultType.File || !_search.IsPreview)
                return default;

            var metadata = new List<DetailsElement>
            {
                new DetailsElement() { Key = _search.FileName, Data = new DetailsLink() { Text = $"🔗 {_search.FullPath}" } },
            };

            if (_search.Type != ResultType.Folder)
            {
                metadata.Add(new DetailsElement() { Data = new DetailsSeparator() });
                metadata.Add(new DetailsElement()
                {
                    Key = Resources.everything_item_tags_name,
                    Data = new DetailsTags()
                    {
                        Tags = [
                            new Tag(_search.Extension?? string.Empty),
                            new Tag(_search.GetFileSizeDisplay() ?? string.Empty)
                    ]
                    }
                });
                //metadata.Add(new DetailsElement() { Key = _search.FileName, Data = new DetailsLink() { Text = $"🔗 {_search.FullPath}" } });
            }

            Details details = new Details()
            {
                Metadata = [.. metadata],
            };

            if (_search.IsPreview)
            {
                if (_search.IsTextPreview)
                {
                    details.Title = this.Title;
                }
                details.Body = _search.GetContent() ?? "";
            }
            else
            {
                details.Title = string.Empty;
                details.HeroImage = this.Icon ?? new IconInfo(string.Empty);
            }
            return details;
        }

        private async Task<IconInfo?> FetchIcon()
        {
            try
            {
                var stream = await ThumbnailHelper.GetThumbnail(_search.FullPath);
                if (stream != null)
                {
                    var data = new IconData(RandomAccessStreamReference.CreateFromStream(stream));
                    return new IconInfo(data, data);
                }
            }
            catch
            {
                // ignored
            }

            return null; //new IconInfo($"{_search.FullPath},1");
        }

        #endregion Private Methods
    }
}