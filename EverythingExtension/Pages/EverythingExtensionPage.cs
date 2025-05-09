// Copyright (c) Microsoft Corporation The Microsoft Corporation licenses this file to you under the
// MIT license. See the LICENSE file in the project root for more information.

using EverythingExtension.Exceptions;
using EverythingExtension.Properties;
using EverythingExtension.Search;
using EverythingExtension.Settings;

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EverythingExtension.Pages;

internal sealed partial class EverythingExtensionPage : DynamicListPage, IDisposable
{
    #region Fields

    private readonly HelpPage _helpPage;
    private readonly EverythingSettings _settings;
    private readonly List<IListItem> _results = [];

    private EverythingSearch? _everythingSearch;

    private long _everythingSearchCookie;

    #endregion Fields

    #region Public Constructors

    public EverythingExtensionPage(EverythingSettings settings)
    {
        Id = "goucey.cmdPal.everything";
        Icon = IconHelpers.FromRelativePath("Assets\\Everything.png");
        Title = Resources.everything_subtitle_header;
        Name = Resources.everything_plugin_name;
        PlaceholderText = Resources.everything_plugin_description;

        _settings = settings;
        ShowDetails = true;
        _everythingSearch = new EverythingSearch(_settings);
        _helpPage = new HelpPage();
        _helpPage.GoBackHomePage += GoBackHomePageHandler;
        WelcomeEmptyContentInitialize();
    }

    ~EverythingExtensionPage()
    {
        Dispose(false);
    }

    #endregion Public Constructors

    #region Properties

    private int DelayTime { get; set; }

    #endregion Properties

    #region Public Methods

    public override IListItem[] GetItems() => [.. _results];

    public async Task DelayQuery()
    {
        while (DelayTime > 0 && DelayTime-- > 0)
        {
            await Task.Delay(100);
        }
        Query(SearchText);
    }

    public override void UpdateSearchText(string oldSearch, string newSearch)
    {
        if (oldSearch != newSearch)
        {
            _ = Task.Run(() =>
            {
                //await DelayQuery();
                Query(newSearch);
                LoadMore();
            });
        }
    }

    public override void LoadMore()
    {
        IsLoading = true;
        FetchItems(30);
        IsLoading = false;
        RaiseItemsChanged(_results.Count);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _everythingSearch = null;
        }
    }

    private void Query(string query)
    {
        _everythingSearchCookie = DateTime.Now.ToFileTime();
        _results.Clear();
        _everythingSearch?.SearchResults.Clear();
        _everythingSearch?.Cancel();

        //WelcomeEmptyContentInitialize();

        if (string.IsNullOrEmpty(query))
        {
            WelcomeEmptyContentInitialize();
            return;
        }
        else
            EmptyContent = null;

        if (_settings.MaxSearchCount <= 0)
            _settings.MaxSearchCount = EverythingSettings.DefaultMaxSearchCount;

        try
        {
            _everythingSearch?.Execute(query, _everythingSearchCookie);
        }
        catch (IpcErrorException)
        {
            EmptyContent = new CommandContextItem(title: Resources.everything_is_not_running)
            {
                Icon = IconHelpers.FromRelativePath("Assets\\Images\\Warning.png"),
                Command = new NoOpCommand()
            };
        }
        catch (Exception e)
        {
            EmptyContent = new CommandContextItem(title: Resources.everything_query_error, subtitle: e.Message)
            {
                Icon = IconHelpers.FromRelativePath("Assets\\Images\\Error.png"),
                Command = new CopyTextCommand(e.Message + "\r\n" + e.StackTrace)
            };
        }
    }

    private void WelcomeEmptyContentInitialize()
    {
        EmptyContent = new CommandContextItem(_helpPage)
        {
            Title = Resources.everything_welcome,
            Subtitle = Resources.everything_welcome_subtitle
        };
    }

    private void FetchItems(int limit)
    {
        if (_everythingSearch == null)
            return;

        var cookie = _everythingSearch.Cookie;
        if (cookie != _everythingSearchCookie)
            return;

        var index = 0;
        while (!_everythingSearch.SearchResults.IsEmpty && _everythingSearch.SearchResults.TryDequeue(out var result) && ++index <= limit)
        {
            //IconInfo icon = null;
            //try
            //{
            //    var stream = ThumbnailHelper.GetThumbnail(result.FullPath).Result;
            //    if (stream != null)
            //    {
            //        var data = new IconData(RandomAccessStreamReference.CreateFromStream(stream));
            //        icon = new IconInfo(data, data);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.Write(ex);
            //    //Logger.LogError("Failed to get the icon.", ex);
            //}
            var item = new EverythingListItem(result);

            //_item.Icon = icon;

            result.Deleted = () => DeletedListItemHandler(item);

            _results.Add(item);
        }

        HasMoreItems = !_everythingSearch.SearchResults.IsEmpty;
    }

    private void DeletedListItemHandler(EverythingListItem item)
    {
        _results.Remove(item);
        RaiseItemsChanged(_results.Count);
    }

    private void GoBackHomePageHandler(object? sender, EventArgs e)
    {
        _results.Clear();
        _everythingSearch?.SearchResults.Clear();
        _everythingSearch?.Cancel();
        RaiseItemsChanged();
    }

    #endregion Public Methods
}