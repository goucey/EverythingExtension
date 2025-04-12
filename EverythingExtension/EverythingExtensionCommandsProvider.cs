// Copyright (c) Microsoft Corporation The Microsoft Corporation licenses this file to you under the
// MIT license. See the LICENSE file in the project root for more information.

using EverythingExtension.Pages;
using EverythingExtension.Properties;
using EverythingExtension.Settings;

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace EverythingExtension;

public sealed partial class EverythingExtensionCommandsProvider : CommandProvider
{
    #region Public Constructors

    public EverythingExtensionCommandsProvider()
    {
        DisplayName = Resources.everything_plugin_name;
        Icon = IconHelpers.FromRelativePath("Assets\\Everything.png");
        Settings = EverythingSettings.Instance.Settings;

        _commands = [
            new CommandItem(new EverythingExtensionPage(EverythingSettings.Instance)) {
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

    public override ICommandItem[] TopLevelCommands()
    {
        return _commands;
    }

    #endregion Public Methods
}