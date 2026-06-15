// Copyright (c) Microsoft Corporation The Microsoft Corporation licenses this file to you under the
// MIT license. See the LICENSE file in the project root for more information.

using EverythingExtension.Commands;
using EverythingExtension.Exceptions;
using EverythingExtension.Helper;
using EverythingExtension.Settings;

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using Serilog;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EverythingExtension.Pages;

internal sealed partial class EverythingExtensionPage : DynamicListPage, IDisposable
{
    #region Fields

    private readonly HelpPage _helpPage;
    private readonly EverythingSettings _settings;
    private readonly List<IListItem> _results = [];

    private readonly CompositeFormat versionCompositeFormat = CompositeFormat.Parse(Resources.everything_version);
    private IEverythingClient? _everythingClient;
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
        _ = EverythingInitializeAsync();
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
                IsLoading = true;
                //await DelayQuery();
                Query(newSearch);
                FetchItems(30);
                RaiseItemsChanged(_results.Count);
                IsLoading = false;
            });
        }
    }

    public override void LoadMore()
    {
        IsLoading = true;
        FetchItems(30);
        RaiseItemsChanged(_results.Count);
        IsLoading = false;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    internal bool EverythingInitialize()
    {
        if (_everythingClient != null && _everythingClient.Version != Version.Parse("0.0.0.0"))
            return true;

        _everythingClient = EverythingClientProvider.GetClient(_settings);

        if (_everythingClient.Initialize())
        {
            EmptyContent = null;
            WelcomeEmptyContentInitialize();
            Title = Resources.everything_subtitle_header + string.Format(CultureInfo.InvariantCulture, versionCompositeFormat, _everythingClient.Version);
            return true;
        }
        else
        {
            NotRunningEmptyContentInitialize();
            return false;
        }
    }

    private async Task EverythingInitializeAsync()
    {
        using CancellationTokenSource _initCts = new();
        _initCts.CancelAfter(TimeSpan.FromSeconds(60));
        CancellationToken token = _initCts.Token;
        try
        {
            while (!token.IsCancellationRequested)
            {
                if (EverythingInitialize())
                    return;

                await Task.Delay(TimeSpan.FromSeconds(1), token);
            }
        }
        catch (TaskCanceledException)
        {
            Log.Warning("初始化操作已终止（30秒超时）");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "初始化发生异常");
        }
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _everythingClient?.Dispose();
            _everythingClient = null;
        }
    }

    private void Query(string query)
    {
        if (_everythingClient == null)
        {
            EverythingInitialize();
        }
        _everythingSearchCookie = DateTime.Now.ToFileTime();
        _results.Clear();
        _everythingClient?.SearchResults.Clear();
        _everythingClient?.Cancel();

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
            _everythingClient?.EnsureEverythingAvailable();
            _everythingClient?.Execute(query, _everythingSearchCookie);
            //_everythingSearch?.Execute(query, _everythingSearchCookie);
        }
        catch (IpcErrorException)
        {
            NotRunningEmptyContentInitialize();
        }
        catch (OperationCanceledException ex)
        {
            Log.Error(ex, "搜索任务取消");
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
            Subtitle = Resources.everything_welcome_subtitle,
        };
    }

    private void NotRunningEmptyContentInitialize()
    {
        EmptyContent = new CommandContextItem(title: Resources.everything_is_not_running)
        {
            Icon = IconHelpers.FromRelativePath("Assets\\Images\\Warning.png"),
            Command = new ReconnectEverythingCommand(this)
        };
    }

    private void FetchItems(int limit)
    {
        if (_everythingClient == null)
            return;

        var cookie = _everythingClient.Cookie;
        if (cookie != _everythingSearchCookie)
            return;

        var index = 0;
        while (!_everythingClient.SearchResults.IsEmpty && _everythingClient.SearchResults.TryDequeue(out var result) && ++index <= limit)
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
            var item = new EverythingListItem(result, _everythingClient);

            //_item.Icon = icon;

            result.Deleted = () => DeletedListItemHandler(item);

            _results.Add(item);
        }

        HasMoreItems = !_everythingClient.SearchResults.IsEmpty;
    }

    private void DeletedListItemHandler(EverythingListItem item)
    {
        _results.Remove(item);
        RaiseItemsChanged(_results.Count);
    }

    private void GoBackHomePageHandler(object? sender, EventArgs e)
    {
        _results.Clear();
        _everythingClient?.SearchResults.Clear();
        _everythingClient?.Cancel();
        RaiseItemsChanged();
    }

    #endregion Public Methods
}