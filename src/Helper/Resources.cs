using Serilog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverythingExtension.Helper
{
    internal static class Resources
    {
        private static readonly Windows.ApplicationModel.Resources.Core.ResourceMap? _map;

        private static readonly string ResourcesPath = "Resources";

        static Resources()
        {
            try
            {
                var currentResourceManager = Windows.ApplicationModel.Resources.Core.ResourceManager.Current;
                if (currentResourceManager.MainResourceMap is not null)
                {
                    _map = currentResourceManager.MainResourceMap;
                }
            }
            catch (Exception)
            {
                // Resource map not available (e.g., during unit tests)
                _map = null;
            }
        }

        public static string GetResource(string identifier)
        {
            if (_map is null)
            {
                return identifier;
            }

            var fullKey = $"{ResourcesPath}/{identifier}";

            var val = _map.GetValue(fullKey);

            return val!.ValueAsString;
        }

        /// <summary>
        ///   查找类似 🔴 Failed to set to clipboard 的本地化字符串。
        /// </summary>
        internal static string everything_clipboard_failed
        {
            get
            {
                return GetResource("everything_clipboard_failed");
            }
        }

        /// <summary>
        ///   查找类似 🟢 Successfully set to clipboard! 的本地化字符串。
        /// </summary>
        internal static string everything_clipboard_success
        {
            get
            {
                return GetResource("everything_clipboard_success");
            }
        }

        /// <summary>
        ///   查找类似 Copied 的本地化字符串。
        /// </summary>
        internal static string everything_copied
        {
            get
            {
                return GetResource("everything_copied");
            }
        }

        /// <summary>
        ///   查找类似 Copy file or folder 的本地化字符串。
        /// </summary>
        internal static string everything_copy_file
        {
            get
            {
                return GetResource("everything_copy_file");
            }
        }

        /// <summary>
        ///   查找类似 Copy path 的本地化字符串。
        /// </summary>
        internal static string everything_copy_path
        {
            get
            {
                return GetResource("everything_copy_path");
            }
        }

        /// <summary>
        ///   查找类似 Delete file 的本地化字符串。
        /// </summary>
        internal static string everything_delete_file
        {
            get
            {
                return GetResource("everything_delete_file");
            }
        }

        /// <summary>
        ///   查找类似 Are you sure you want to delete &quot;{0}&quot; file? 的本地化字符串。
        /// </summary>
        internal static string everything_delete_file_tips
        {
            get
            {
                return GetResource("everything_delete_file_tips");
            }
        }

        /// <summary>
        ///   查找类似 Delete folder 的本地化字符串。
        /// </summary>
        internal static string everything_delete_folder
        {
            get
            {
                return GetResource("everything_delete_folder");
            }
        }

        /// <summary>
        ///   查找类似 Are you sure you want to delete the &quot;{0}&quot; folder (including its files and subfolders) 的本地化字符串。
        /// </summary>
        internal static string everything_delete_folder_tips
        {
            get
            {
                return GetResource("everything_delete_folder_tips");
            }
        }

        /// <summary>
        ///   查找类似 Prompt 的本地化字符串。
        /// </summary>
        internal static string everything_delete_tips_caption
        {
            get
            {
                return GetResource("everything_delete_tips_caption");
            }
        }

        /// <summary>
        ///   查找类似 The search matches multiple items 的本地化字符串。
        /// </summary>
        internal static string everything_fallback_multipleResults_subtitle
        {
            get
            {
                return GetResource("everything_fallback_multipleResults_subtitle");
            }
        }

        /// <summary>
        ///   查找类似 This file doesn&apos;t exist 的本地化字符串。
        /// </summary>
        internal static string everything_file_does_not_exit
        {
            get
            {
                return GetResource("everything_file_does_not_exit");
            }
        }

        /// <summary>
        ///   查找类似 This is a file, not a folder 的本地化字符串。
        /// </summary>
        internal static string everything_file_is_file_not_folder
        {
            get
            {
                return GetResource("everything_file_is_file_not_folder");
            }
        }

        /// <summary>
        ///   查找类似 Find files or folders 的本地化字符串。
        /// </summary>
        internal static string everything_find_fallback_display_title
        {
            get
            {
                return GetResource("everything_find_fallback_display_title");
            }
        }

        /// <summary>
        ///   查找类似 Browse 的本地化字符串。
        /// </summary>
        internal static string everything_folder_browse
        {
            get
            {
                return GetResource("everything_folder_browse");
            }
        }

        /// <summary>
        ///   查找类似 This folder is empty 的本地化字符串。
        /// </summary>
        internal static string everything_folder_is_empty
        {
            get
            {
                return GetResource("everything_folder_is_empty");
            }
        }

        /// <summary>
        ///   查找类似 Fail to open folder at 的本地化字符串。
        /// </summary>
        internal static string everything_folder_open_failed
        {
            get
            {
                return GetResource("everything_folder_open_failed");
            }
        }

        /// <summary>
        ///   查找类似 GoBack 的本地化字符串。
        /// </summary>
        internal static string everything_go_back
        {
            get
            {
                return GetResource("everything_go_back");
            }
        }

        /// <summary>
        ///   查找类似 # CmdPal Everything Help Document
        ///&gt; CmdPal Everything is a command panel extension for Windows that allows you to quickly access files and folders in the Everything database through the command panel.
        ///
        ///## Everything Features (Please refer to the official website for details)
        ///- **Quick Access**: Through the command panel, you can quickly find and open files and folders without using the file explorer.
        ///- **Search**: Supports searching for file and folder names, paths, extensions, etc.
        ///- **Sort**: Suppor [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string everything_help_content
        {
            get
            {
                return GetResource("everything_help_content");
            }
        }

        /// <summary>
        ///   查找类似 Help document 的本地化字符串。
        /// </summary>
        internal static string everything_help_title
        {
            get
            {
                return GetResource("everything_help_title");
            }
        }

        /// <summary>
        ///   查找类似 Ignore Punctuation Marks 的本地化字符串。
        /// </summary>
        internal static string everything_ignorePunctuation_enabled
        {
            get
            {
                return GetResource("everything_ignorePunctuation_enabled");
            }
        }

        /// <summary>
        ///   查找类似 Automatically skip all punctuation symbols during keyword parsing and matching. 的本地化字符串。
        /// </summary>
        internal static string everything_ignorePunctuation_enabled_description
        {
            get
            {
                return GetResource("everything_ignorePunctuation_enabled_description");
            }
        }

        /// <summary>
        ///   查找类似 Ignore Whitespace Characters 的本地化字符串。
        /// </summary>
        internal static string everything_ignoreWhitespace_enabled
        {
            get
            {
                return GetResource("everything_ignoreWhitespace_enabled");
            }
        }

        /// <summary>
        ///   查找类似 Ignore spaces, tabs and all other whitespace characters in search terms. 的本地化字符串。
        /// </summary>
        internal static string everything_ignoreWhitespace_enabled_description
        {
            get
            {
                return GetResource("everything_ignoreWhitespace_enabled_description");
            }
        }

        /// <summary>
        ///   查找类似 Everything is not running 的本地化字符串。
        /// </summary>
        internal static string everything_is_not_running
        {
            get
            {
                return GetResource("everything_is_not_running");
            }
        }

        /// <summary>
        ///   查找类似 Tags 的本地化字符串。
        /// </summary>
        internal static string everything_item_tags_name
        {
            get
            {
                return GetResource("everything_item_tags_name");
            }
        }

        /// <summary>
        ///   查找类似 Enable macro query 的本地化字符串。
        /// </summary>
        internal static string everything_macro_enabled
        {
            get
            {
                return GetResource("everything_macro_enabled");
            }
        }

        /// <summary>
        ///   查找类似 Available doc:,audio:,zip:,pic:,video:,web: and exe: and so filters macro search files 的本地化字符串。
        /// </summary>
        internal static string everything_macro_enabled_description
        {
            get
            {
                return GetResource("everything_macro_enabled_description");
            }
        }

        /// <summary>
        ///   查找类似 Case Sensitive Matching 的本地化字符串。
        /// </summary>
        internal static string everything_matchCase_enabled
        {
            get
            {
                return GetResource("everything_matchCase_enabled");
            }
        }

        /// <summary>
        ///   查找类似 Enable case-sensitive search; uppercase and lowercase letters are treated as distinct characters. 的本地化字符串。
        /// </summary>
        internal static string everything_matchCase_enabled_description
        {
            get
            {
                return GetResource("everything_matchCase_enabled_description");
            }
        }

        /// <summary>
        ///   查找类似 [V3]Match Diacritics 的本地化字符串。
        /// </summary>
        internal static string everything_matchDiacritics_enabled
        {
            get
            {
                return GetResource("everything_matchDiacritics_enabled");
            }
        }

        /// <summary>
        ///   查找类似 Distinguish diacritical characters; accented letters won&apos;t match non-accented counterparts. 的本地化字符串。
        /// </summary>
        internal static string everything_matchDiacritics_enabled_description
        {
            get
            {
                return GetResource("everything_matchDiacritics_enabled_description");
            }
        }

        /// <summary>
        ///   查找类似 Include Path In Search 的本地化字符串。
        /// </summary>
        internal static string everything_matchPath_enabled
        {
            get
            {
                return GetResource("everything_matchPath_enabled");
            }
        }

        /// <summary>
        ///   查找类似 Search covers full directory paths; keywords match content within folder locations besides filenames. 的本地化字符串。
        /// </summary>
        internal static string everything_matchPath_enabled_description
        {
            get
            {
                return GetResource("everything_matchPath_enabled_description");
            }
        }

        /// <summary>
        ///   查找类似 Prefix Only Match 的本地化字符串。
        /// </summary>
        internal static string everything_matchPrefix_enabled
        {
            get
            {
                return GetResource("everything_matchPrefix_enabled");
            }
        }

        /// <summary>
        ///   查找类似 Match search term only as the starting prefix of file names or paths. 的本地化字符串。
        /// </summary>
        internal static string everything_matchPrefix_enabled_description
        {
            get
            {
                return GetResource("everything_matchPrefix_enabled_description");
            }
        }

        /// <summary>
        ///   查找类似 Suffix Only Match 的本地化字符串。
        /// </summary>
        internal static string everything_matchSuffix_enabled
        {
            get
            {
                return GetResource("everything_matchSuffix_enabled");
            }
        }

        /// <summary>
        ///   查找类似 Match search term only as the trailing suffix of file names or paths. 的本地化字符串。
        /// </summary>
        internal static string everything_matchSuffix_enabled_description
        {
            get
            {
                return GetResource("everything_matchSuffix_enabled_description");
            }
        }

        /// <summary>
        ///   查找类似 Whole Word Matching 的本地化字符串。
        /// </summary>
        internal static string everything_matchWholeWords_enabled
        {
            get
            {
                return GetResource("everything_matchWholeWords_enabled");
            }
        }

        /// <summary>
        ///   查找类似 Match whole words only, partial substrings inside words will not be matched. 的本地化字符串。
        /// </summary>
        internal static string everything_matchWholeWords_enabled_description
        {
            get
            {
                return GetResource("everything_matchWholeWords_enabled_description");
            }
        }

        /// <summary>
        ///   查找类似 Search result limit 的本地化字符串。
        /// </summary>
        internal static string everything_max_search_result_limit
        {
            get
            {
                return GetResource("everything_max_search_result_limit");
            }
        }

        /// <summary>
        ///   查找类似 The maximum number of search results returned (too many can cause CmdPal to crash) 的本地化字符串。
        /// </summary>
        internal static string everything_max_search_result_limit_description
        {
            get
            {
                return GetResource("everything_max_search_result_limit_description");
            }
        }

        /// <summary>
        ///   查找类似 Name 的本地化字符串。
        /// </summary>
        internal static string everything_name
        {
            get
            {
                return GetResource("everything_name");
            }
        }

        /// <summary>
        ///   查找类似 Open 的本地化字符串。
        /// </summary>
        internal static string everything_open
        {
            get
            {
                return GetResource("everything_open");
            }
        }

        /// <summary>
        ///   查找类似 Open containing folder 的本地化字符串。
        /// </summary>
        internal static string everything_open_containing_folder
        {
            get
            {
                return GetResource("everything_open_containing_folder");
            }
        }

        /// <summary>
        ///   查找类似 Open folder 的本地化字符串。
        /// </summary>
        internal static string everything_open_folder
        {
            get
            {
                return GetResource("everything_open_folder");
            }
        }

        /// <summary>
        ///   查找类似 Open path in console 的本地化字符串。
        /// </summary>
        internal static string everything_open_in_console
        {
            get
            {
                return GetResource("everything_open_in_console");
            }
        }

        /// <summary>
        ///   查找类似 Path 的本地化字符串。
        /// </summary>
        internal static string everything_path
        {
            get
            {
                return GetResource("everything_path");
            }
        }

        /// <summary>
        ///   查找类似 Search for files or folders using Everything 的本地化字符串。
        /// </summary>
        internal static string everything_plugin_description
        {
            get
            {
                return GetResource("everything_plugin_description");
            }
        }

        /// <summary>
        ///   查找类似 Everything Search 的本地化字符串。
        /// </summary>
        internal static string everything_plugin_name
        {
            get
            {
                return GetResource("everything_plugin_name");
            }
        }

        /// <summary>
        ///   查找类似 Settings 的本地化字符串。
        /// </summary>
        internal static string everything_plugin_settings
        {
            get
            {
                return GetResource("everything_plugin_settings");
            }
        }

        /// <summary>
        ///   查找类似 Everything query error 的本地化字符串。
        /// </summary>
        internal static string everything_query_error
        {
            get
            {
                return GetResource("everything_query_error");
            }
        }

        /// <summary>
        ///   查找类似 Everything service not detected. Please start Everything manually and try again. 的本地化字符串。
        /// </summary>
        internal static string everything_reconnect_fail_message
        {
            get
            {
                return GetResource("everything_reconnect_fail_message");
            }
        }

        /// <summary>
        ///   查找类似 Connected successfully after detection! 的本地化字符串。
        /// </summary>
        internal static string everything_reconnect_success_message
        {
            get
            {
                return GetResource("everything_reconnect_success_message");
            }
        }

        /// <summary>
        ///   查找类似 Run as administrator 的本地化字符串。
        /// </summary>
        internal static string everything_run_as_administrator
        {
            get
            {
                return GetResource("everything_run_as_administrator");
            }
        }

        /// <summary>
        ///   查找类似 Run as different user 的本地化字符串。
        /// </summary>
        internal static string everything_run_as_user
        {
            get
            {
                return GetResource("everything_run_as_user");
            }
        }

        /// <summary>
        ///   查找类似 Search with Everything &quot;{0}&quot; 的本地化字符串。
        /// </summary>
        internal static string everything_search_format
        {
            get
            {
                return GetResource("everything_search_format");
            }
        }

        /// <summary>
        ///   查找类似 Always Show Folders First 的本地化字符串。
        /// </summary>
        internal static string everything_searchFoldersFirst_always
        {
            get
            {
                return GetResource("everything_searchFoldersFirst_always");
            }
        }

        /// <summary>
        ///   查找类似 Folders First On Ascending Sort 的本地化字符串。
        /// </summary>
        internal static string everything_searchFoldersFirst_ascending
        {
            get
            {
                return GetResource("everything_searchFoldersFirst_ascending");
            }
        }

        /// <summary>
        ///   查找类似 Folders First On Descending Sort 的本地化字符串。
        /// </summary>
        internal static string everything_searchFoldersFirst_descending
        {
            get
            {
                return GetResource("everything_searchFoldersFirst_descending");
            }
        }

        /// <summary>
        ///   查找类似 Folder Sort Priority Rule 的本地化字符串。
        /// </summary>
        internal static string everything_searchFoldersFirst_enabled
        {
            get
            {
                return GetResource("everything_searchFoldersFirst_enabled");
            }
        }

        /// <summary>
        ///   查找类似 [V3]Configure display priority between folders and files: folders first on ascending/descending sort, always first or always last. 的本地化字符串。
        /// </summary>
        internal static string everything_searchFoldersFirst_enabled_description
        {
            get
            {
                return GetResource("everything_searchFoldersFirst_enabled_description");
            }
        }

        /// <summary>
        ///   查找类似 Never Show Folders First 的本地化字符串。
        /// </summary>
        internal static string everything_searchFoldersFirst_never
        {
            get
            {
                return GetResource("everything_searchFoldersFirst_never");
            }
        }

        /// <summary>
        ///   查找类似 Everything 的本地化字符串。
        /// </summary>
        internal static string everything_subtitle_header
        {
            get
            {
                return GetResource("everything_subtitle_header");
            }
        }

        /// <summary>
        ///   查找类似 Preview 的本地化字符串。
        /// </summary>
        internal static string everything_text_preview
        {
            get
            {
                return GetResource("everything_text_preview");
            }
        }

        /// <summary>
        ///   查找类似 The current file does not support preview！ 的本地化字符串。
        /// </summary>
        internal static string everything_text_preview_not_supported
        {
            get
            {
                return GetResource("everything_text_preview_not_supported");
            }
        }

        /// <summary>
        ///   查找类似  version {0} 的本地化字符串。
        /// </summary>
        internal static string everything_version
        {
            get
            {
                return GetResource("everything_version");
            }
        }

        /// <summary>
        ///   查找类似 Welcome to use Everything search 的本地化字符串。
        /// </summary>
        internal static string everything_welcome
        {
            get
            {
                return GetResource("everything_welcome");
            }
        }

        /// <summary>
        ///   查找类似 Type the &apos;Enter&apos; key for help information 的本地化字符串。
        /// </summary>
        internal static string everything_welcome_subtitle
        {
            get
            {
                return GetResource("everything_welcome_subtitle");
            }
        }


    }
}
