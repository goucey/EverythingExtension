using CoreWidgetProvider.Widgets.Enums;

using EverythingExtension.Internal;
using EverythingExtension.Properties;

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using Serilog;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using Windows.ApplicationModel;

namespace EverythingExtension.Pages
{
    internal sealed partial class ImageViewPage : ContentPage
    {
        #region Fields

        private readonly IconInfo _theIcon = new IconInfo("\ue7b3");
        private readonly FormContent _formContent = new();

        #endregion Fields

        #region Fields

        public ImageViewPage(SearchResult result)
        {
            _result = result;
            Icon = _theIcon;
            Title = result.FileName;
            Name = Resources.everything_text_preview;
            Commands = [
                  new CommandContextItem(Resources.everything_go_back,name:Resources.everything_go_back,result:CommandResult.GoBack()){ Icon = new IconInfo("\ue72b")  }
              ];
        }

        private readonly SearchResult _result;

        #endregion Fields

        #region Protected Methods

        public override IContent[] GetContent()
        {
            LoadContentData();
            GetTemplatePath();

            return [_formContent];
        }

        private void GetTemplatePath()
        {
            try
            {
                var path = Path.Combine(Package.Current.EffectivePath, "Templates/ImageViewTemplate.json");
                var template = File.ReadAllText(path, Encoding.Default) ?? throw new FileNotFoundException(path);

                //template = CoreWidgetProvider.Helpers.Resources.ReplaceIdentifersFast(template);
                _formContent.TemplateJson = template;
            }
            catch (Exception e)
            {
                Log.Error("Error getting template.", e);
            }
        }

        private void LoadContentData()
        {
            JsonObject data = new JsonObject();
            data["imageUrl"] = _result.FullPath;

            _formContent.DataJson = data.ToJsonString();
        }

        #endregion Protected Methods
    }
}