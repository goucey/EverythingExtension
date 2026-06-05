
using System.Diagnostics;

namespace EverythingExtension.Utils
{
    internal static class ShellCommand
    {
        #region Enums

        public enum RunAsType
        {
            None,
            Administrator,
            OtherUser,
        }

        #endregion Enums

        #region Public Methods

        public static ProcessStartInfo GetProcessStartInfo(string target, string parentDir, string programArguments, RunAsType runAs = RunAsType.None)
        {
            return new ProcessStartInfo
            {
                FileName = target,
                WorkingDirectory = parentDir,
                UseShellExecute = true,
                Arguments = programArguments,
                Verb = runAs switch
                {
                    RunAsType.Administrator => "runAs",
                    RunAsType.OtherUser => "runAsUser",
                    _ => string.Empty
                },
            };
        }

        public static ProcessStartInfo SetProcessStartInfo(this string fileName, string workingDirectory = "", string arguments = "", string verb = "")
        {
            var info = new ProcessStartInfo
            {
                FileName = fileName,
                WorkingDirectory = workingDirectory,
                Arguments = arguments,
                Verb = verb,
            };

            return info;
        }

        #endregion Public Methods
    }
}