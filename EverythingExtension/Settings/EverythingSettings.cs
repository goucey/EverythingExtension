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

        // 3g2,3gp,3gp2,3gpp,amr, asf,avi, bik,d2v,divx,drc,dsa,dsm,dss,dsv, flc,fli,flic,flv,
        // ifo,ivf,m1v,m2v, m4v,mkv,mp2v,mp4, mpe,mpeg,mpg, mpv2, mov, ogm,
        // pss,pva,qt,ram,ratdvd,rm,rmm,rmvb,roq,rpm, smk,swf,tp,tpr,ts,vob,vp6, wm,wmp,wmv dat,m4b,m4p,
        internal List<MacroSettings> Macros { get; } =
                [
            new("audio", "aac,ac3,aif,aifc,aiff,au,cda,dts,fla,flac,it,m1a,m2a,m3u,m4a,m4b,m4p,mid,midi,mka,mod,mp2,mp3,mpa,ogg,ra,rmi,snd,spc,umx,voc,wav,wma,xm") ,
            new("zip", "7z,ace,arj,bz2,cab,gz,gzip,jar,r00,r01,r02,r03,r04,r05,r06,r07,r08,r09,r10,r11,r12,r13,r14,r15,r16,r17,r18,r19,r20,r21,r22,r23,r24,r25,r26,r27,r28,r29,rar,tar,tgz,z,zip"),
            new("doc","c,chm,cpp,csv,cxx,doc,docm,docx,dot,dotm,dotx,h,hpp,htm,html,hxx,ini,java,lua,mht,mhtml,odt,pdf,potx,potm,ppam,ppsm,ppsx,pps,ppt,pptm,pptx,rtf,sldm,sldx,thmx,txt,vsd,wpd,wps,wri,xlam,xls,xlsb,xlsm,xlsx,xltm,xltx,xml"),
            new("exe", "bat,cmd,exe,msi,msi,msp,scr,msix"),
            new("pic", "ani,bmp,gif,ico,jpe,jpeg,jpg,pcx,png,psd,tga,tif,tiff,webp,wmf,wbmp,icl,jp2,mpng,raw,nef,wdp,hdp") ,
            new("video", "3g2,3gp,3gp2,3gpp,amr,amv,asf,avi,bdmv,bik,d2v,divx,drc,dsa,dsm,dss,dsv,evo,f4v,flc,fli,flic,flv,hdmov,ifo,ivf,m1v,m2p,m2t,m2ts,m2v,m4v,mkv,mp2v,mp4,mp4v,mpe,mpeg,mpg,mpls,mpv2,mpv4,mov,mts,ogm,ogv,pss,pva,qt,ram,ratdvd,rm,rmm,rmvb,roq,rpm,smil,smk,swf,tp,tpr,ts,vob,vp6,webm,wm,wmp,wmv"),
            new("web", "html,htm,css,js,svg,json,xml,scss,less,ts,mjs,vue,tsx"),
            new("font", "ttf,otf,woff,woff2,ttc"),
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