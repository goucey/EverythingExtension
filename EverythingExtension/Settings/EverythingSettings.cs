// Copyright (c) Microsoft Corporation The Microsoft Corporation licenses this file to you under the
// MIT license. See the LICENSE file in the project root for more information.

using EverythingExtension.Properties;

using Microsoft.CommandPalette.Extensions.Toolkit;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace EverythingExtension.Settings
{
    internal sealed class EverythingSettings : JsonSettingsManager
    {
        #region Fields

        public static readonly int DefaultMaxSearchCount = 200;

        #endregion Fields



        #region Fields

        private static readonly string _namespace = "everything";

        private readonly ToggleSetting _macroEnabled = new(Namespaced(nameof(MacroEnabled)), Resources.everything_macro_enabled, Resources.everything_macro_enabled_description, true);
        private readonly TextSetting _maxSearchCount = new(Namespaced(nameof(MaxSearchCount)), Resources.everything_max_search_result_limit, Resources.everything_max_search_result_limit_description, DefaultMaxSearchCount.ToString(CultureInfo.InvariantCulture));

        #endregion Fields

        #region Private Methods

        private static string Namespaced(string propertyName) => $"{_namespace}.{propertyName}";

        #endregion Private Methods

        #region Public Constructors

        public EverythingSettings()
        {
            FilePath = SettingsJsonPath();

            LoadSettings();

            Settings.Add(_macroEnabled);
            Settings.Add(_maxSearchCount);

            Settings.SettingsChanged += (s, a) => SaveSettings();
        }

        #endregion Public Constructors

        internal static EverythingSettings Instance = new();

        internal List<MacroSettings> Macros { get; } =
                [
            new("doc","c,chm,cpp,doc,dot,h,htm,html,mht,mhtml,nfo,pdf,pps,ppt,rtf,txt,vsd,wpd,wps,wri,xls,xml,txt,docx,htm,html,pdf,c,cpp,h,xls,odp,odt,ods,pptx,xlsx,csv,docx,ppsx,java,hpp,ini,dotx,xlsb"),
            new("audio", "aac,ac3,aif,aifc,aiff,au,cda,dts,fla,flac,gym,it,m1a,m2a,m4a,midi,mka,mod,mp2,mp3,mpa,ogg,ra,spc,rmi,snd,umx,vgm,vgz,voc,wav,wma,xm") ,
            new("zip", "ace,arj,bz2,cab,gz,gzip,r00,r01,r02,r03,r04,r05,r06,r07,r08,r09,r10,r11,r12,r13,r14,r15,r16,r17,r18,r19,r20,r21,r22,r23,r24,r25,r26,r27,r28,r29,rar,tar,7z，zip"),
            new("pic", "ani,bmp,gif,ico,jpe,jpeg,jpg,pcx,png,psd,tga,tif,tiff,wmf,wbmp,icl,jp2,mpng,raw,nef,wdp,hdp") ,
            new("video", "3g2,3gp,3gp2,3gpp,amr,asf,avi,bik,d2v,dat,divx,drc,dsa,dsm,dss,dsv,flc,fli,flic,flv,ifo,ivf,m1v,m2v,m4b,m4p,m4v,mkv,mp2v,mp4,mpe,mpeg,mpg,mpv2,mov,ogm,pss,pva,qt,ram,ratdvd,rm,rmm,roq,rpm,smk,swf,tp,tpr,ts,vob,vp6,wm,wmp,wmv,rmvb"),
            new("web", "html,htm,mht,php,css,sql,js"),
            new( "exe", "exe,cmd,msi,msix"),
        ];

        internal int MaxSearchCount
        {
            get => int.TryParse(_maxSearchCount.Value, out int maxSearchCount) ? maxSearchCount : DefaultMaxSearchCount;
            set
            {
                _maxSearchCount.Value = value.ToString(CultureInfo.InvariantCulture);
                SaveSettings();
            }
        }

        internal bool MacroEnabled => _macroEnabled.Value;

        internal static string SettingsJsonPath()
        {
            var directory = Utilities.BaseSettingsPath("Microsoft.CmdPal");
            Directory.CreateDirectory(directory);

            // now, the state is just next to the exe
            return Path.Combine(directory, "settings.json");
        }
    }
}