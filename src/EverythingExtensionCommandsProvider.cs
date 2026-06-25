// Copyright (c) Microsoft Corporation The Microsoft Corporation licenses this file to you under the
// MIT license. See the LICENSE file in the project root for more information.

using EverythingExtension.Pages;
using EverythingExtension.Helper;
using EverythingExtension.Settings;

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using System;

namespace EverythingExtension;

public sealed partial class EverythingExtensionCommandsProvider : CommandProvider
{
    #region Fields

    private readonly FallbackEverythingItem _fallback;
    private readonly EverythingExtensionPage _page;

    #endregion Fields

    #region Public Constructors

    public EverythingExtensionCommandsProvider()
    {
        DisplayName = Resources.everything_plugin_name;
        Icon = IconHelpers.FromRelativePath("Assets\\Everything.png");
        _page = new EverythingExtensionPage(EverythingSettings.Instance);
        _fallback = new FallbackEverythingItem(_page);
        _commands = [
            new CommandItem(_page) {
                Title = DisplayName,
                Subtitle = Resources.everything_plugin_description,
                MoreCommands = [new CommandContextItem(GetSettingsPage())],
            },
        ];
    }

  
    private ICommand GetSettingsPage()
    {
        Settings = EverythingSettings.Instance.Settings;

        if (Settings.SettingsPage is Command settingPage)
        {
            settingPage.Name = Resources.everything_plugin_settings;
            return settingPage;
        }
        else
            return Settings.SettingsPage;
    }

    #endregion Public Constructors



    #region Fields

    private readonly ICommandItem[] _commands;

    #endregion Fields

    #region Public Methods

    public override ICommandItem[] TopLevelCommands() => _commands;

    public override IFallbackCommandItem[]? FallbackCommands() => [_fallback];
    

    public override void InitializeWithHost(IExtensionHost host)
    {
        EverythingExtensionHost.Instance.Initialize(host);
    }

    #endregion Public Methods
}