// Copyright (c) Microsoft Corporation The Microsoft Corporation licenses this file to you under the
// MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Linq;

namespace EverythingExtension.Settings
{
    public class MacroSettings(string prefix, string fileExtensions)
    {
        #region Properties

        public string Prefix { get; set; } = prefix;
        private string FileExtensions { get; set; } = fileExtensions;

        #endregion Properties

        #region Internal Methods

        public override string ToString()
        {
            var itemArr = FileExtensions.Split([',', '，'], StringSplitOptions.RemoveEmptyEntries)
                 .Where(i => !string.IsNullOrWhiteSpace(i));
            return $"ext:{string.Join(";", itemArr)}";
        }

        #endregion Internal Methods
    }
}