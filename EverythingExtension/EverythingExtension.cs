// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CommandPalette.Extensions;

namespace EverythingExtension;

[Guid("ee1d82f8-afa1-4404-b545-6753bc0713cb")]
public sealed partial class EverythingExtension(ManualResetEvent extensionDisposedEvent) : IExtension, IDisposable
{
    private readonly EverythingExtensionCommandsProvider _provider = new();

    public object? GetProvider(ProviderType providerType)
    {
        return providerType switch
        {
            ProviderType.Commands => _provider,
            _ => null,
        };
    }

    public void Dispose() => extensionDisposedEvent.Set();
}
