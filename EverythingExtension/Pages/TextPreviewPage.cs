using EverythingExtension.Commands;
using EverythingExtension.Properties;
using EverythingExtension.Search;

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverythingExtension.Pages
{
    internal sealed partial class TextPreviewPage : ContentPage
    {
        #region Fields

        private readonly MarkdownContent _mdContent;
        private readonly IconInfo TheIcon = new IconInfo("\ue7b3");

        #endregion Fields

        #region Public Constructors

        public TextPreviewPage(string fileName, string content)
        {
            Icon = TheIcon;
            Title = fileName;
            Name = Resources.everything_text_preview;
            //action:()=>{ this.RaiseItemsChanged(); },
            _mdContent = new MarkdownContent(content);
            Commands = [
                    new CommandContextItem(Resources.everything_go_back,name:Resources.everything_go_back,result:CommandResult.GoBack())
                ];
        }

        public TextPreviewPage(SearchResult searchResult)
        {
            Icon = TheIcon;
            Title = searchResult.FileName;
            Name = Resources.everything_text_preview;

            string content = Resources.everything_text_preview_not_supported;

            if (searchResult.IsPreviewable)
            {
                content = searchResult.GetContent() ?? Resources.everything_text_preview_not_supported;
            }

            _mdContent = new MarkdownContent(content);

            Commands = [
                   new CommandContextItem(Resources.everything_go_back,name:Resources.everything_go_back,result:CommandResult.GoBack()),
                   new CommandContextItem(new OpenCommand(searchResult))
               ];
        }

        #endregion Public Constructors

        #region Public Constructors

        public override IContent[] GetContent() => [_mdContent];

        #endregion Public Constructors
    }
}