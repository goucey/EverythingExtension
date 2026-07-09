using EverythingExtension.Commands;
using EverythingExtension.Internal;
using EverythingExtension.Properties;

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using Serilog;

using System;
using System.IO;
using System.Text;
using System.Text.Json.Nodes;

using Windows.ApplicationModel;

namespace EverythingExtension.Pages
{
    internal sealed partial class TextPreviewPage : ContentPage
    {
        #region Fields

        private readonly IContent _content;
        private readonly IconInfo _theIcon = new IconInfo("\ue7b3");

        #endregion Fields

        #region Public Constructors

        public TextPreviewPage(string fileName, string content)
        {
            Icon = _theIcon;
            Title = fileName;
            Name = Resources.everything_text_preview;
            if (fileName.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
            {
                _content = new MarkdownContent(content);
            }
            else
            {
                var formContent = new FormContent();
                LoadContentData(formContent, content);
                GetTemplatePath(formContent);
                _content = formContent;
            }
            //action:()=>{ this.RaiseItemsChanged(); },
            //_mdContent = new MarkdownContent(content);
            Commands = [
                    new CommandContextItem(Resources.everything_go_back,name:Resources.everything_go_back,result:CommandResult.GoBack()){ Icon = new IconInfo("\ue72b")  }
                ];
        }

        public TextPreviewPage(SearchResult searchResult, IEverythingClient client)
        {
            Icon = _theIcon;
            Title = searchResult.FileName;
            Name = Resources.everything_text_preview;

            string content = Resources.everything_text_preview_not_supported;

            if (searchResult.IsPreview)
            {
                content = searchResult.GetContent() ?? Resources.everything_text_preview_not_supported;
            }

            if(searchResult.FileName.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
            {
                _content = new MarkdownContent(content);
            }
            else
            {
                var formContent = new FormContent();
                LoadContentData(formContent, content);
                GetTemplatePath(formContent);
                _content = formContent;

            }
            //PlainTextContent
            Commands = [
                   new CommandContextItem(Resources.everything_go_back,name:Resources.everything_go_back,result:CommandResult.GoBack()),
                   new CommandContextItem(new OpenCommand(searchResult, client))
               ];
        }

        #endregion Public Constructors

        #region Public Constructors

        public override IContent[] GetContent()
        {
           
            return [_content];
        }

        #endregion Public Constructors

        private static void GetTemplatePath(FormContent content)
        {
           
            try
            {
                var path = Path.Combine(Package.Current.EffectivePath, "Templates/CodeBlockViewTemplate.json");
                var template = File.ReadAllText(path, Encoding.Default) ?? throw new FileNotFoundException(path);

                //template = CoreWidgetProvider.Helpers.Resources.ReplaceIdentifersFast(template);
                content.TemplateJson = template;
            }
            catch (Exception e)
            {
                Log.Error("Error getting template.", e);
            }
        }

        private void LoadContentData(FormContent formContent,string content)
        {
            JsonObject data = new JsonObject();
            data["title"] = Path.GetFileNameWithoutExtension(Title);
            data["language"] = "Javascript";
            data["content"] = content;

            formContent.DataJson = data.ToJsonString();
        }

    }
}