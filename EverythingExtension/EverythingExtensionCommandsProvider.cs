// Copyright (c) Microsoft Corporation The Microsoft Corporation licenses this file to you under the
// MIT license. See the LICENSE file in the project root for more information.

using EverythingExtension.Pages;
using EverythingExtension.Properties;
using EverythingExtension.Settings;

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using System;

namespace EverythingExtension;

public sealed partial class EverythingExtensionCommandsProvider : CommandProvider
{
    #region Fields

    private readonly EverythingExtensionPage _page;
    private readonly FallbackEverythingItem _fallback;

    #endregion Fields

    #region Public Constructors

    public EverythingExtensionCommandsProvider()
    {
        DisplayName = Resources.everything_plugin_name;
        Icon = IconHelpers.FromRelativePath("Assets\\Everything.png");
        Settings = EverythingSettings.Instance.Settings;
        _page = new EverythingExtensionPage(EverythingSettings.Instance);
        _fallback = new FallbackEverythingItem(_page);
        _commands = [
            new CommandItem(_page) {
                Title = DisplayName,
                Subtitle = Resources.everything_plugin_description,
                MoreCommands = [new CommandContextItem(Settings.SettingsPage)],
            },
        ];
    }

    #endregion Public Constructors



    #region Fields

    private readonly ICommandItem[] _commands;

    #endregion Fields

    #region Public Methods

    public override ICommandItem[] TopLevelCommands() => _commands;

    public override IFallbackCommandItem[]? FallbackCommands() => [_fallback];

    #endregion Public Methods
}