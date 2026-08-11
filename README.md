# EverythingExtension
> 切换语言：[English](./README_en.md)

EverythingExtension 是一个用于 Windows 的命令面板扩展，它允许您通过命令面板快速访问 Everything 数据库中的文件和文件夹。

[![Build Status](https://git.okmn.cc/goucey/EverythingExtension/actions/workflows/release-auto.yml/badge.svg?branch={branch}&event={event}&style={style})](https://git.okmn.cc/goucey/EverythingExtension/actions/workflows/release-auto.yml)

![1](./doc/PixPin_2025-04-05_08-48-56.png)

## 使用方法
1. 输入 `Everything`（或选择 `Everything 搜索`）以打开 Everything 命令面板。
2. 在命令面板中输入 `搜索关键字` 进行搜索。
3. 在搜索结果中，选择要打开的文件或文件夹，按 Enter 键进行打开。
4. 您还可以使用其他快捷键和功能(Ctrl+K)来操作搜索结果。
5. 搜索中可以使用内置 `宏` （设置中需要开启宏搜索）具有针对性的搜索，例如：`doc:、audio:、zip:、pic:、video:、web:、exe:`等。

## `宏` 搜索说明（支持的文件类型）
- `doc:`：搜索文档文件，如 .doc、.docx、.pdf 等。
  > asm,c,cc,chm,cpp,csv,cxx,doc,docm,docx,dot,dotm,dotx,efu,epub,h,hpp,htm,html,hxx,ini,java,js,json,lua,md,mht,mhtml,mobi,odp,ods,odt,ofd,pdf,php,pl,potm,potx,ppam,pps,ppsm,ppsx,ppt,pptm,pptx,ps1xml,pssc,pub,py,rtf,sldm,sldx,sql,tsv,txt,vb,vsd,wpd,wps,wri,xlam,xls,xlsb,xlsm,xlsx,xltm,xltx,xml,xsl

- `audio:`：搜索音频文件，如 .mp3、.wav、.flac 等。
  > aac,ac3,adt,adts,aif,aifc,aiff,amr,ape,au,cda,dts,ec3,fla,flac,it,lpcm,m1a,m2a,m3u,m3u8,m4a,m4b,m4p,mid,midi,mka,mod,mp2,mp3,mpa,mpc,oga,ogg,opus,ra,rmi,snd,spc,umx,voc,wav,wax,weba,wma,xm

- `zip:`：搜索压缩文件，如 .zip、.rar、.7z 等。
  > 7z,ace,arj,bz2,cab,gz,gzip,jar,r00,r01,r02,r03,r04,r05,r06,r07,r08,r09,r10,r11,r12,r13,r14,r15,r16,r17,r18,r19,r20,r21,r22,r23,r24,r25,r26,r27,r28,r29,rar,tar,tgz,z,zip

- `pic:`：搜索图片文件，如 .jpg、.png、.gif 等。
  > ani,apng,avif,avifs,bmp,bpg,cur,dds,gif,heic,heics,heif,heifs,hif,ico,jfi,jfif,jif,jpe,jpeg,jpg,jxl,jxr,pcx,png,psb,psd,svg,tga,tif,tiff,wdp,webp,wmf,wbmp,icl,jp2,mpng,raw,nef,hdp

- `video:`：搜索视频文件，如 .mp4、.avi、.mkv 等。
  > 3g2,3gp,3gp2,3gpp,amr,amv,asf,asx,avi,bdmv,bik,d2v,divx,drc,dsa,dsm,dss,dsv,evo,f4v,flc,fli,flic,flv,hdmov,ifo,ivf,m1v,m2p,m2t,m2ts,m2v,m4v,mkv,mod,mov,mp2v,mp4,mp4v,mpe,mpeg,mpg,mpls,mpv2,mpv4,mts,ogm,ogv,ogx,pss,pva,qt,ram,ratdvd,rm,rmm,rmvb,roq,rpm,smil,smk,swf,tod,tp,tpr,ts,tts,uvu,vob,vp6,webm,wm,wmp,wmv,wmx,wvx

- `web:`：搜索网页文件，如 .html、.htm、.php 等。
  > html,htm,css,js,svg,json,xml,scss,sass,less,styl,ts,mjs,vue,tsx,jsx

- `exe:`：搜索可执行文件，如 .exe、.msi、.bat 等。
  > bat,cmd,exe,msi,msp,msu,ps1,scr,msix,vbs

- `font:`：搜索字体文件，如 .ttf、.otf 等。
  > ttf,otf,woff,woff2,ttc,ttf

## 搜索参数
- 常规搜索
  > 输入关键词即可搜索

- 宏搜索
  > 宏标记:搜索关键词

  例如:

  ```
  pic:1212
  ```

- 自定义搜索结果数量（数量最大取值100，超过100会导致崩毁）
  > 用于自定义返回搜索结果；搜索规则：搜索关键词 count:数量；

  例如：
  ```
  hello count:10
  ```

- 开启正则搜索
   用 `@` 开头则启用正则搜索，需要注意的是，启用正则搜索时，无法启用`宏`搜索 和 `自定义返回结果数量`

## ? 安装下载
<a href="https://get.microsoft.com/installer/download/9pnd4pgfp6km?referrer=appbadge" target="_self" >
	<img src="https://get.microsoft.com/images/zh-cn%20dark.svg" width="200"/>
</a>

### 常规搜索
![1](./doc/PixPin_2025-04-05_08-49-25.png)

### 宏搜索
![1](./doc/PixPin_2025-04-05_08-51-36.png)

### 宏搜索带自定义返回数量
![1](./doc/PixPin_2025-04-05_08-52-28.png)

### 正则搜索
![1](./doc/PixPin_2025-04-05_08-54-57.png)

### 正则搜索带自定义返回数量
![1](./doc/PixPin_2025-04-05_08-53-59.png)
