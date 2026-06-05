using EverythingExtension.Pages;
using EverythingExtension.Properties;

using Microsoft.CommandPalette.Extensions.Toolkit;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverythingExtension
{
    internal sealed partial class FallbackEverythingItem : FallbackCommandItem
    {
        #region Fields

        private const string CommandId = "goucey.cmdPal.everything.fallback";
        private static readonly NoOpCommand BaseCommandWithId = new() { Id = CommandId };
        private readonly CompositeFormat _fallbackItemSearchPageTitleFormat = CompositeFormat.Parse(Resources.everything_search_format);
        private readonly EverythingExtensionPage _page;

        #endregion Fields

        #region Public Constructors

        public FallbackEverythingItem(EverythingExtensionPage page) : base(BaseCommandWithId, Resources.everything_find_fallback_display_title, CommandId)
        {
            _page = page;
            Title = string.Empty;
            Subtitle = Resources.everything_fallback_multipleResults_subtitle;
            Icon = IconHelpers.FromRelativePath("Assets\\Everything.png");
        }

        public override void UpdateQuery(string query)
        {
            Title = string.Empty;
            Subtitle = string.Empty;
            Icon = null;

            if (string.IsNullOrWhiteSpace(query))
            {
                return;
            }

            //_page.UpdateSearchText("", query);
            _page.SearchText = query;
            Title = string.Format(CultureInfo.CurrentCulture, _fallbackItemSearchPageTitleFormat, query);
            Subtitle = Resources.everything_fallback_multipleResults_subtitle;
            Icon = IconHelpers.FromRelativePath("Assets\\Everything.png");
            Command = _page;
        }

        #endregion Public Constructors
    }
}