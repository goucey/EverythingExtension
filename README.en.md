# EverythingExtension
EverythingExtension is a command palette extension for Windows. It allows you to quickly access files and folders stored in the Everything database via the command palette.

![1](./doc/PixPin_2025-04-05_08-48-56.png)

## Usage
1. Type `Everything` (or select `Everything Search`) to open the Everything command palette.
2. Enter search keywords in the command palette to perform searches.
3. Select the file or folder you want to open from the search results, then press Enter to open it.
4. You can also operate search results with other shortcut keys (Ctrl+K).
5. Built-in macros are available for targeted searches (Macro Search must be enabled in Settings). Available macros include: `doc:`, `audio:`, `zip:`, `pic:`, `video:`, `web:`, `exe:`, `font:`, etc.

## Macro Search Reference (Supported File Types)
- `doc:`: Search document files such as .doc, .docx, .pdf, etc.
  > asm,c,cc,chm,cpp,csv,cxx,doc,docm,docx,dot,dotm,dotx,efu,epub,h,hpp,htm,html,hxx,ini,java,js,json,lua,md,mht,mhtml,mobi,odp,ods,odt,ofd,pdf,php,pl,potm,potx,ppam,pps,ppsm,ppsx,ppt,pptm,pptx,ps1xml,pssc,pub,py,rtf,sldm,sldx,sql,tsv,txt,vb,vsd,wpd,wps,wri,xlam,xls,xlsb,xlsm,xlsx,xltm,xltx,xml,xsl

- `audio:`: Search audio files such as .mp3, .wav, .flac, etc.
  > aac,ac3,adt,adts,aif,aifc,aiff,amr,ape,au,cda,dts,ec3,fla,flac,it,lpcm,m1a,m2a,m3u,m3u8,m4a,m4b,m4p,mid,midi,mka,mod,mp2,mp3,mpa,mpc,oga,ogg,opus,ra,rmi,snd,spc,umx,voc,wav,wax,weba,wma,xm

- `zip:`: Search archive files such as .zip, .rar, .7z, etc.
  > 7z,ace,arj,bz2,cab,gz,gzip,jar,r00,r01,r02,r03,r04,r05,r06,r07,r08,r09,r10,r11,r12,r13,r14,r15,r16,r17,r18,r19,r20,r21,r22,r23,r24,r25,r26,r27,r28,r29,rar,tar,tgz,z,zip

- `pic:`: Search image files such as .jpg, .png, .gif, etc.
  > ani,apng,avif,avifs,bmp,bpg,cur,dds,gif,heic,heics,heif,heifs,hif,ico,jfi,jfif,jif,jpe,jpeg,jpg,jxl,jxr,pcx,png,psb,psd,svg,tga,tif,tiff,wdp,webp,wmf,wbmp,icl,jp2,mpng,raw,nef,hdp

- `video:`: Search video files such as .mp4, .avi, .mkv, etc.
  > 3g2,3gp,3gp2,3gpp,amr,amv,asf,asx,avi,bdmv,bik,d2v,divx,drc,dsa,dsm,dss,dsv,evo,f4v,flc,fli,flic,flv,hdmov,ifo,ivf,m1v,m2p,m2t,m2ts,m2v,m4v,mkv,mod,mov,mp2v,mp4,mp4v,mpe,mpeg,mpg,mpls,mpv2,mpv4,mts,ogm,ogv,ogx,pss,pva,qt,ram,ratdvd,rm,rmm,rmvb,roq,rpm,smil,smk,swf,tod,tp,tpr,ts,tts,uvu,vob,vp6,webm,wm,wmp,wmv,wmx,wvx

- `web:`: Search web-related files such as .html, .htm, .php, etc.
  > html,htm,css,js,svg,json,xml,scss,sass,less,styl,ts,mjs,vue,tsx,jsx

- `exe:`: Search executable files such as .exe, .msi, .bat, etc.
  > bat,cmd,exe,msi,msp,msu,ps1,scr,msix,vbs

- `font:`: Search font files such as .ttf, .otf, etc.
  > ttf,otf,woff,woff2,ttc,ttf

## Search Parameters
- Regular Search
  > Simply input keywords to perform a search

- Macro Search
  > Syntax: `macro_tag:search_keyword`

  Example:
  ```
  pic:1212
  ```

- Custom Result Count (Maximum value: 100; exceeding this limit will cause crashes)
  > Customize the number of returned results. Syntax: `search_keyword count:number`

  Example:
  ```
  hello count:10
  ```

- Regex Search
  > Start your query with `@` to enable regular expression search.
  Note: Macro search and custom result count functions will be unavailable when regex mode is enabled.

## Installation & Download
<a href="https://get.microsoft.com/installer/download/9pnd4pgfp6km?referrer=appbadge" target="_self" >
	<img src="https://get.microsoft.com/images/en-us%20dark.svg" width="200"/>
</a>

### Regular Search
![1](./doc/PixPin_2025-04-05_08-49-25.png)

### Macro Search
![1](./doc/PixPin_2025-04-05_08-51-36.png)

### Macro Search with Custom Result Count
![1](./doc/PixPin_2025-04-05_08-52-28.png)

### Regex Search
![1](./doc/PixPin_2025-04-05_08-54-57.png)

### Regex Search with Custom Result Count
![1](./doc/PixPin_2025-04-05_08-53-59.png)