// Copyright (c) Microsoft Corporation The Microsoft Corporation licenses this file to you under the
// MIT license. See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Windows.Storage;

using Serilog;

using Shmuelie.WinRTServer.CsWinRT;

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Windows.ApplicationModel.Activation;

namespace EverythingExtension;

public static class Program
{
    #region Public Methods

    [MTAThread]
    public static async Task Main(string[] args)
    {
        string path = ApplicationData.GetDefault().TemporaryPath;
        string serilogFileOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} | [{Level:u3}] | {Message:lj}{NewLine}{Exception}{NewLine}";
        string serilogConsoleOutputTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";

        // Environment.SetEnvironmentVariable("CMDPAL_LOGS_ROOT", path);

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: serilogConsoleOutputTemplate, formatProvider: CultureInfo.DefaultThreadCurrentUICulture)
            .WriteTo.Logger(l =>
                l.Filter.ByIncludingOnly(l =>
                    l.Level == Serilog.Events.LogEventLevel.Error)
                .WriteTo.File(
             Path.Combine(path, "Logs/Err_.log"),
         Serilog.Events.LogEventLevel.Error,
         serilogFileOutputTemplate,
         CultureInfo.InvariantCulture,
         rollingInterval: RollingInterval.Day))

            .WriteTo.Logger(lc => 
            lc.Filter.ByExcluding(p => 
            p.Level == Serilog.Events.LogEventLevel.Error)
            .WriteTo.File(Path.Combine(path, "Logs/Log_.log"),
         Serilog.Events.LogEventLevel.Information,
         serilogConsoleOutputTemplate,
         CultureInfo.InvariantCulture,
         rollingInterval: RollingInterval.Day))
            .CreateLogger();

        Log.Information($"Launched with args: {string.Join(' ', args.ToArray())}");

        if (args.Length > 0 && args[0] == "-RegisterProcessAsComServer")
        {
            await HandleCOMServerActivationAsync();
        }
        else
        {
            Console.WriteLine("Not being launched as a Extension... exiting.");
        }
    }

    #endregion Public Methods

    #region Private Methods

    private static async Task HandleCOMServerActivationAsync()
    {
        await using Shmuelie.WinRTServer.ComServer server = new Shmuelie.WinRTServer.ComServer();
        ManualResetEvent extensionDisposedEvent = new(false);

        // We are instantiating an extension instance once above, and returning it every time the
        // callback in RegisterExtension below is called. This makes sure that only one instance of
        // SampleExtension is alive, which is returned every time the host asks for the IExtension
        // object. If you want to instantiate a new instance each time the host asks, create the new
        // instance inside the delegate.
        EverythingExtension extensionInstance = new(extensionDisposedEvent);
        server.RegisterClass<EverythingExtension, IExtension>(() => extensionInstance);
        server.Start();

        // This will make the main thread wait until the event is signalled by the extension class.
        // Since we have single instance of the extension object, we exit as soon as it is disposed.
        extensionDisposedEvent.WaitOne();
    }

    #endregion Private Methods
}