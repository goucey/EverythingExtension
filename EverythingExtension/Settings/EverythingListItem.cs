using EverythingExtension.Commands;
using EverythingExtension.Pages;
using EverythingExtension.SDK;
using EverythingExtension.Search;

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Windows.Foundation;
using Windows.Storage.Streams;

namespace EverythingExtension.Settings
{
    internal sealed partial class EverythingListItem : ListItem
    {
        #region Public Constructors

        public EverythingListItem(SearchResult search, bool isFirstlevelFolder = false) : base(new NoOpCommand())
        {
            //new DirectoryExplorePage(search.FullPath)
            Title = search.FileName;
            Subtitle = $"{search.FullPath}";

            if (search.Size.HasValue)
                Tags = [new Tag(search.GetFileSizeDisplay() ?? string.Empty)];

            if (search.Type == ResultType.Folder)
                Command = isFirstlevelFolder ? new DirectoryExplorePage(search.FullPath) : new OpenCommand(search);
            else
                Command = new OpenCommand(search);

            _search = search;

            _details = new Lazy<Details?>(() => BuildDetails());

            _icon = new Lazy<IconInfo?>(() =>
            {
                var t = FetchIcon();
                t.Wait();
                return t.Result;
            });

            MoreCommands = [.. ContextMenuLoader.LoadContextMenus(search, isFirstlevelFolder)];
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

        private static Details? BuildDetails()
        {
            return default;
            //if (_search.Type != ResultType.File)
            //    return default;

            //var metadata = new List<DetailsElement>();
            //metadata.Add(new DetailsElement() { Key = "Type", Data = new DetailsTags() { Tags = [new Tag(_search.Type.ToString())] } });

            //if (_search.Type != ResultType.Folder)
            //{
            //    metadata.Add(new DetailsElement() { Key = "File", Data = new DetailsLink() { Text = _search.FileName } });
            //    metadata.Add(new DetailsElement() { Key = "Path", Data = new DetailsLink() { Text = _search.FullPath } });
            //}

            //Details details = new Details()
            //{
            //    Title = this.Title,
            //    HeroImage = this.Icon ?? new IconInfo(string.Empty),
            //    Metadata = metadata.ToArray(),
            //};
            //if (_search.FileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            //{
            //    details.Body = File.ReadAllText(_search.FullPath);
            //}

            //return details;
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
            }
            return default; //new IconInfo($"{_search.FullPath},1");
        }

        #endregion Private Methods
    }
}