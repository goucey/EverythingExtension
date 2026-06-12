using EverythingExtension.Pages;
using EverythingExtension.Helper;

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.ApplicationModel;

namespace EverythingExtension.Commands
{
    internal sealed partial class ReconnectEverythingCommand : InvokableCommand
    {
        #region Public Constructors

        public ReconnectEverythingCommand(EverythingExtensionPage page)
        {
            _page = page;
            Name = "检测重连";
            Icon = new IconInfo("\ue72c");
        }

        #endregion Public Constructors

        #region Fields

        private readonly EverythingExtensionPage _page;
        private readonly StatusMessage _installBanner = new();

        #endregion Fields

        #region Public Methods

        public override ICommandResult Invoke(object? sender)
        {
            if (_page.EverythingInitialize())
            {
                _installBanner.Message = Resources.everything_reconnect_success_message;
                _installBanner.State = MessageState.Success;
                EverythingExtensionHost.Instance.ShowStatusWithAutoHide(_installBanner, StatusContext.Extension);
                return CommandResult.GoToPage(new GoToPageArgs { PageId = "goucey.cmdPal.everything" });
            }
            else
            {
                _installBanner.State = MessageState.Error;
                _installBanner.Message = Resources.everything_reconnect_fail_message;
                EverythingExtensionHost.Instance.ShowStatusWithAutoHide(_installBanner, StatusContext.Extension);
                return CommandResult.KeepOpen();
            }
        }

        #endregion Public Methods
    }
}