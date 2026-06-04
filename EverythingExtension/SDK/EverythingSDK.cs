// Copyright (c) Microsoft Corporation The Microsoft Corporation licenses this file to you under the
// MIT license. See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace EverythingExtension.SDK
{
    internal sealed class EverythingSdk
    {
        #region Fields

#if ARM64
        private const string DllPath = "EverythingARM64.dll";
        private const string DllPath3 = "Everything3_ARM64.dll";

#else
        private const string DllPath = "Everything64.dll";
        private const string DllPath3 = "Everything3_x64.dll";
#endif
        private const CallingConvention CallConv = CallingConvention.StdCall;

        #endregion Fields

        #region Enums

        public enum StateCode
        {
            Ok,
            MemoryError,
            IpcError,
            RegisterClassExError,
            CreateWindowError,
            CreateThreadError,
            InvalidIndexError,
            InvalidCallError,
        }

        [Flags]
        public enum RequestFlag
        {
            FileName = 0x00000001,
            Path = 0x00000002,
            FullPathAndFileName = 0x00000004,
            Extension = 0x00000008,
            Size = 0x00000010,
            DateCreated = 0x00000020,
            DateModified = 0x00000040,
            DateAccessed = 0x00000080,
            Attributes = 0x00000100,
            FileListFileName = 0x00000200,
            RunCount = 0x00000400,
            DateRun = 0x00000800,
            DateRecentlyChanged = 0x00001000,
            HighlightedFileName = 0x00002000,
            HighlightedPath = 0x00004000,
            HighlightedFullPathAndFileName = 0x00008000,
        }

        #endregion Enums

        #region Internal Methods

        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything_GetResultFileNameW(int nIndex);

        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        internal static extern void Everything_SetSearchW(string lpSearchString);

        [DllImport(DllPath)]
        internal static extern void Everything_SetMatchPath(bool bEnable);

        [DllImport(DllPath)]
        internal static extern void Everything_SetMatchCase(bool bEnable);

        [DllImport(DllPath)]
        internal static extern void Everything_SetMatchWholeWord(bool bEnable);

        [DllImport(DllPath)]
        internal static extern void Everything_SetRegex(bool bEnable);

        [DllImport(DllPath)]
        internal static extern void Everything_SetMax(int dwMax);

        [DllImport(DllPath)]
        internal static extern void Everything_SetOffset(int dwOffset);

        [DllImport(DllPath)]
        internal static extern bool Everything_GetMatchPath();

        [DllImport(DllPath)]
        internal static extern bool Everything_GetMatchCase();

        [DllImport(DllPath)]
        internal static extern bool Everything_GetMatchWholeWord();

        [DllImport(DllPath)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything_GetRegex();

        [DllImport(DllPath)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything_GetMax();

        [DllImport(DllPath)]
        internal static extern uint Everything_GetOffset();

        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        internal static extern string Everything_GetSearchW();

        [DllImport(DllPath)]
        internal static extern StateCode Everything_GetLastError();

        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        internal static extern bool Everything_QueryW(bool bWait);

        [DllImport(DllPath)]
        internal static extern void Everything_SortResultsByPath();

        [DllImport(DllPath)]
        internal static extern int Everything_GetNumFileResults();

        [DllImport(DllPath)]
        internal static extern int Everything_GetNumFolderResults();

        [DllImport(DllPath)]
        internal static extern int Everything_GetNumResults();

        [DllImport(DllPath)]
        internal static extern int Everything_GetTotFileResults();

        [DllImport(DllPath)]
        internal static extern int Everything_GetTotFolderResults();

        [DllImport(DllPath)]
        internal static extern int Everything_GetTotResults();

        [DllImport(DllPath)]
        internal static extern bool Everything_IsVolumeResult(int nIndex);

        [DllImport(DllPath)]
        internal static extern bool Everything_IsFolderResult(int nIndex);

        [DllImport(DllPath)]
        internal static extern bool Everything_IsFileResult(int nIndex);

        [DllImport(DllPath)]
        internal static extern void Everything_Reset();

        [DllImport(DllPath)]
        internal static extern void Everything_CleanUp();

        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything_GetResultPathW(int idx);

        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        internal static extern uint Everything_IncRunCountFromFileNameW(string lpFileName);

        #region 1.0.0

        [MinVersion("1.0.0")]
        [DllImport(DllPath)]
        internal static extern int Everything_GetMajorVersion();

        [MinVersion("1.0.0")]
        [DllImport(DllPath)]
        internal static extern int Everything_GetMinorVersion();

        [MinVersion("1.0.0")]
        [DllImport(DllPath)]
        internal static extern int Everything_GetRevision();

        [MinVersion("1.0.0")]
        [DllImport(DllPath)]
        internal static extern int Everything_GetBuildNumber();

        #endregion 1.0.0

        #region 1.4.1

        [MinVersion("1.4.1")]
        [DllImport(DllPath)]
        internal static extern void Everything_SetRequestFlags(RequestFlag flag);

        [MinVersion("1.4.1")]
        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything_GetResultHighlightedPathW(int nIndex);

        [MinVersion("1.4.1")]
        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything_GetResultHighlightedFileNameW(int nIndex);

        [MinVersion("1.4.1")]
        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything_GetResultHighlightedFullPathAndFileNameW(int nIndex);

        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything_GetResultExtensionW(int nIndex);

        [MinVersion("1.4.1")]
        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        internal static extern bool Everything_GetResultSize(int nIndex, ref LargeInteger lpSize);

        [MinVersion("1.4.1")]
        [DllImport(DllPath, CharSet = CharSet.Unicode)]
        internal static extern uint Everything_GetResultFullPathNameW(int nIndex, char[] lpString, int nMaxCount);

        #endregion 1.4.1

        #region 1.5.0

        /// <summary>
        /// Everything3 错误码
        /// </summary>
        public enum EveryThing3Error : uint
        {
            Ok = 0,                                    // 无错误
            OutOfMemory = 0xE0000001,                  // 内存不足
            IpcPipeNotFound = 0xE0000002,              // IPC 管道服务器未找到(Everything 客户端未运行)
            Disconnected = 0xE0000003,                 // 与管道服务器断开连接
            InvalidParameter = 0xE0000004,             // 无效参数
            BadRequest = 0xE0000005,                   // 错误请求
            Cancelled = 0xE0000006,                    // 用户取消
            PropertyNotFound = 0xE0000007,             // 属性未找到
            Server = 0xE0000008,                       // 服务器错误(服务器内存不足)
            InvalidCommand = 0xE0000009,               // 无效命令
            BadResponse = 0xE000000A,                  // 服务器响应错误
            InsufficientBuffer = 0xE000000B,           // 缓冲区空间不足
            Shutdown = 0xE000000C,                     // 用户发起关闭
            InvalidPropertyValueType = 0xE000000D,     // 属性值类型不正确
        }

        /// <summary>
        /// 属性类型分类
        /// </summary>
        public enum EveryThing3PropertyType : uint
        {
            None = 0,               // 无
            Metadata = 1,           // 元数据
            File = 2,               // 文件
            Index = 3,              // 索引
            Content = 4,            // 内容
            Volume = 5,             // 卷
            Search = 6,             // 搜索
            PropertySystem = 7,     // Windows 属性系统
        }

        /// <summary>
        /// 属性 ID 枚举(完整列表，与 Everything3.h 一致)
        /// </summary>
        public enum EveryThing3PropertyId : uint
        {
            Invalid = 0xffffffff,                           // 无效属性
            Name = 0,                                       // 文件名
            Path = 1,                                       // 路径
            Size = 2,                                       // 文件大小(字节)
            Extension = 3,                                  // 扩展名
            Type = 4,                                       // 文件类型描述
            DateModified = 5,                               // 修改时间(FILETIME)
            DateCreated = 6,                                // 创建时间(FILETIME)
            DateAccessed = 7,                               // 访问时间(FILETIME)
            Attributes = 8,                                 // 文件属性(Windows FILE_ATTRIBUTE_*)
            DateRecentlyChanged = 9,                        // 最近变更时间(FILETIME)
            RunCount = 10,                                  // 运行次数
            DateRun = 11,                                   // 最后运行时间(FILETIME)
            FileListName = 12,                              // 文件列表名称
            Width = 13,                                     // 图片宽度(像素)
            Height = 14,                                    // 图片高度(像素)
            Dimensions = 15,                                // 图片尺寸(宽x高)
            AspectRatio = 16,                               // 宽高比(Q1K)
            BitDepth = 17,                                  // 位深度
            Length = 18,                                    // 媒体时长(100纳秒单位)
            AudioSampleRate = 19,                           // 音频采样率(Hz)
            AudioChannels = 20,                             // 音频声道数
            AudioBitsPerSample = 21,                        // 音频位深
            AudioBitRate = 22,                              // 音频比特率(bps)
            AudioFormat = 23,                               // 音频格式
            FileSignature = 24,                             // 文件签名特征
            Title = 25,                                     // 标题
            Artist = 26,                                    // 艺术家
            Album = 27,                                     // 专辑
            Year = 28,                                      // 年份
            Comment = 29,                                   // 备注
            Track = 30,                                     // 音轨号
            Genre = 31,                                     // 流派
            FrameRate = 32,                                 // 视频帧率(Q1K)
            VideoBitRate = 33,                              // 视频比特率(bps)
            VideoFormat = 34,                               // 视频格式
            Rating = 35,                                    // 评分
            Tags = 36,                                      // 标签
            Md5 = 37,                                       // MD5 哈希
            Sha1 = 38,                                      // SHA-1 哈希
            Sha256 = 39,                                    // SHA-256 哈希
            Crc32 = 40,                                     // CRC32
            SizeOnDisk = 41,                                // 磁盘占用大小(字节)
            Description = 42,                               // 描述
            Version = 43,                                   // 版本
            ProductName = 44,                               // 产品名称
            ProductVersion = 45,                            // 产品版本
            Company = 46,                                   // 公司
            Kind = 47,                                      // 种类
            NameLength = 48,                                // 文件名长度(字符数)
            PathAndNameLength = 49,                         // 完整路径长度(字符数)
            Subject = 50,                                   // 主题
            Authors = 51,                                   // 作者
            DateTaken = 52,                                 // 拍摄时间(FILETIME)
            Software = 53,                                  // 软件
            DateAcquired = 54,                              // 获取时间(FILETIME)
            Copyright = 55,                                 // 版权
            ImageId = 56,                                   // 图片ID
            HorizontalResolution = 57,                      // 水平分辨率(DPI)
            VerticalResolution = 58,                        // 垂直分辨率(DPI)
            Compression = 59,                               // 压缩方式
            ResolutionUnit = 60,                            // 分辨率单位
            ColorRepresentation = 61,                       // 色彩表示
            CompressedBitsPerPixel = 62,                    // 每像素压缩位数(Q1K)
            CameraMaker = 63,                               // 相机厂商
            CameraModel = 64,                               // 相机型号
            FStop = 65,                                     // 光圈值(Q1K)
            ExposureTime = 66,                              // 曝光时间(Q1K)
            IsoSpeed = 67,                                  // ISO 感光度
            ExposureBias = 68,                              // 曝光补偿(Q1K)
            FocalLength = 69,                               // 焦距(mm, Q1K)
            MaxAperture = 70,                               // 最大光圈(Q1M)
            MeteringMode = 71,                              // 测光模式
            SubjectDistance = 72,                           // 对焦距离(Q1K)
            FlashMode = 73,                                 // 闪光灯模式
            FlashEnergy = 74,                               // 闪光灯能量(Q1K)
            FocalLength35mm = 75,                           // 35mm等效焦距(mm)
            LensMaker = 76,                                 // 镜头厂商
            LensModel = 77,                                 // 镜头型号
            FlashMaker = 78,                                // 闪光灯厂商
            FlashModel = 79,                                // 闪光灯型号
            CameraSerialNumber = 80,                        // 相机序列号
            Contrast = 81,                                  // 对比度
            Brightness = 82,                                // 亮度(Q1M)
            LightSource = 83,                               // 光源
            ExposureProgram = 84,                           // 曝光程序
            Saturation = 85,                                // 饱和度
            Sharpness = 86,                                 // 锐度
            WhiteBalance = 87,                              // 白平衡
            PhotometricInterpretation = 88,                 // 光度解释
            DigitalZoom = 89,                               // 数字变焦(Q1K)
            ExifVersion = 90,                               // Exif 版本
            Latitude = 91,                                  // GPS 纬度(Q1M)
            Longitude = 92,                                 // GPS 经度(Q1M)
            Altitude = 93,                                  // GPS 海拔(Q1M)
            Subtitle = 94,                                  // 字幕
            TotalBitRate = 95,                              // 总比特率(bps)
            Directors = 96,                                 // 导演
            Producers = 97,                                 // 制片人
            Writers = 98,                                   // 编剧
            Publisher = 99,                                 // 发布者
            ContentDistributor = 100,                       // 内容发行商
            DateEncoded = 101,                              // 编码时间(FILETIME)
            EncodedBy = 102,                                // 编码者
            AuthorUrl = 103,                                // 作者URL
            PromotionUrl = 104,                             // 推广URL
            OfflineAvailability = 105,                      // 脱机可用性
            OfflineStatus = 106,                            // 脱机状态
            SharedWith = 107,                               // 共享对象
            Owner = 108,                                    // 所有者
            Computer = 109,                                 // 计算机名
            AlbumArtist = 110,                              // 专辑艺术家
            ParentalRatingReason = 111,                     // 家长分级原因
            Composer = 112,                                 // 作曲家
            Conductor = 113,                                // 指挥
            ContentGroupDescription = 114,                  // 内容组描述
            Mood = 115,                                     // 基调
            PartOfSet = 116,                                // 所属集合
            InitialKey = 117,                               // 初始调
            BeatsPerMinute = 118,                           // BPM(拍每分钟)
            Protected = 119,                                // 是否受保护
            PartOfACompilation = 120,                       // 是否合辑
            ParentalRating = 121,                           // 家长分级
            Period = 122,                                   // 时期
            People = 123,                                   // 人物
            Category = 124,                                 // 分类
            ContentStatus = 125,                            // 内容状态
            DocumentContentType = 126,                      // 文档内容类型
            PageCount = 127,                                // 页数
            WordCount = 128,                                // 字数
            CharacterCount = 129,                           // 字符数
            LineCount = 130,                                // 行数
            ParagraphCount = 131,                           // 段落数
            Template = 132,                                 // 模板
            Scale = 133,                                    // 缩放
            LinksDirty = 134,                               // 链接是否脏
            Language = 135,                                 // 语言
            LastAuthor = 136,                               // 最后作者
            RevisionNumber = 137,                           // 修订号
            VersionNumber = 138,                            // 版本号
            Manager = 139,                                  // 管理者
            DateContentCreated = 140,                       // 内容创建时间(FILETIME)
            DateSaved = 141,                                // 保存时间(FILETIME)
            DatePrinted = 142,                              // 打印时间(FILETIME)
            TotalEditingTime = 143,                         // 总编辑时间(FILETIME)
            OriginalFileName = 144,                         // 原始文件名
            DateReleased = 145,                             // 发布日期
            SlideCount = 146,                               // 幻灯片数
            NoteCount = 147,                                // 备注数
            HiddenSlideCount = 148,                         // 隐藏幻灯片数
            PresentationFormat = 149,                       // 演示文稿格式
            Trademarks = 150,                               // 商标
            DisplayName = 151,                              // 显示名称
            NameLengthInUtf8Bytes = 152,                    // 文件名长度(UTF-8字节)
            PathAndNameLengthInUtf8Bytes = 153,             // 完整路径长度(UTF-8字节)
            ChildCount = 154,                               // 子项数量
            ChildFolderCount = 155,                         // 子文件夹数量
            ChildFileCount = 156,                           // 子文件数量
            ChildCountFromDisk = 157,                       // 从磁盘读取的子项数量
            ChildFolderCountFromDisk = 158,                 // 从磁盘读取的子文件夹数量
            ChildFileCountFromDisk = 159,                   // 从磁盘读取的子文件数量
            FolderDepth = 160,                              // 文件夹深度
            TotalSize = 161,                                // 总大小(字节)
            TotalSizeOnDisk = 162,                          // 磁盘占用总大小(字节)
            DateChanged = 163,                              // 变更时间(FILETIME)
            HardLinkCount = 164,                            // 硬链接数
            DeletePending = 165,                            // 是否待删除
            IsDirectory = 166,                              // 是否为目录
            AlternateDataStreamCount = 167,                 // 备用数据流(ADS)数量
            AlternateDataStreamNames = 168,                 // 备用数据流名称列表
            TotalAlternateDataStreamSize = 169,             // 备用数据流总大小(字节)
            TotalAlternateDataStreamSizeOnDisk = 170,       // 备用数据流磁盘占用(字节)
            CompressedSize = 171,                           // 压缩后大小(字节)
            CompressionFormat = 172,                        // 压缩格式
            CompressionUnitShift = 173,                     // 压缩单元位移
            CompressionChunkShift = 174,                    // 压缩块位移
            CompressionClusterShift = 175,                  // 压缩簇位移
            CompressionRatio = 176,                         // 压缩率
            ReparseTag = 177,                               // 重解析点标签
            RemoteProtocol = 178,                           // 远程协议
            RemoteProtocolVersion = 179,                    // 远程协议版本
            RemoteProtocolFlags = 180,                      // 远程协议标志
            LogicalBytesPerSector = 181,                    // 每扇区逻辑字节数
            PhysicalBytesPerSectorForAtomicity = 182,       // 原子操作物理扇区字节
            PhysicalBytesPerSectorForPerformance = 183,     // 性能物理扇区字节
            EffectivePhysicalBytesPerSectorForAtomicity = 184, // 有效原子物理扇区字节
            FileStorageInfoFlags = 185,                     // 文件存储信息标志
            ByteOffsetForSectorAlignment = 186,             // 扇区对齐字节偏移
            ByteOffsetForPartitionAlignment = 187,          // 分区对齐字节偏移
            AlignmentRequirement = 188,                     // 对齐要求
            VolumeSerialNumber = 189,                       // 卷序列号
            FileId = 190,                                   // 文件ID(128位)
            FrameCount = 191,                               // 帧数
            ClusterSize = 192,                              // 簇大小(字节)
            SectorSize = 193,                               // 扇区大小(字节)
            AvailableFreeDiskSize = 194,                    // 可用磁盘空间(字节)
            FreeDiskSize = 195,                             // 空闲磁盘空间(字节)
            TotalDiskSize = 196,                            // 总磁盘空间(字节)
            MaximumComponentLength = 198,                   // 最大组件长度
            FileSystemFlags = 199,                          // 文件系统标志
            FileSystem = 200,                               // 文件系统类型
            Orientation = 201,                              // 方向
            EndOfFile = 202,                                // 文件结束偏移
            ShortName = 203,                                // 短文件名(8.3格式)
            ShortPathAndName = 204,                         // 短文件完整路径
            EncryptionStatus = 205,                         // 加密状态
            HardLinkFileNames = 206,                        // 硬链接文件名列表
            IndexType = 207,                                // 索引类型
            DriveType = 208,                                // 驱动器类型
            BinaryType = 209,                               // 二进制文件类型
            RegexMatch0 = 210,                              // 正则匹配组0
            RegexMatch1 = 211,                              // 正则匹配组1
            RegexMatch2 = 212,                              // 正则匹配组2
            RegexMatch3 = 213,                              // 正则匹配组3
            RegexMatch4 = 214,                              // 正则匹配组4
            RegexMatch5 = 215,                              // 正则匹配组5
            RegexMatch6 = 216,                              // 正则匹配组6
            RegexMatch7 = 217,                              // 正则匹配组7
            RegexMatch8 = 218,                              // 正则匹配组8
            RegexMatch9 = 219,                              // 正则匹配组9
            SiblingCount = 220,                             // 同级项数量
            SiblingFolderCount = 221,                       // 同级文件夹数量
            SiblingFileCount = 222,                         // 同级文件数量
            IndexNumber = 223,                              // 索引号
            ShortcutTarget = 224,                           // 快捷方式目标
            OutOfDate = 225,                                // 是否过期
            IncurSeekPenalty = 226,                         // 是否有寻道惩罚
            PlainTextLineCount = 227,                       // 纯文本行数
            Aperture = 228,                                 // 光圈(Q1M)
            MakerNote = 229,                                // 厂商备注
            RelatedSoundFile = 230,                         // 关联音频文件
            ShutterSpeed = 231,                             // 快门速度(Q1K)
            TranscodedForSync = 232,                        // 是否为同步转码
            CaseSensitiveDir = 233,                         // 是否区分大小写目录
            DateIndexed = 234,                              // 索引时间(FILETIME)
            NameFrequency = 235,                            // 文件名出现频率
            SizeFrequency = 236,                            // 文件大小出现频率
            ExtensionFrequency = 237,                       // 扩展名出现频率
            RegexMatches = 238,                             // 正则所有匹配
            Url = 239,                                      // URL
            PathAndName = 240,                              // 完整路径(文件或文件夹引用)
            ParentFileId = 241,                             // 父文件ID
            Sha512 = 242,                                   // SHA-512 哈希
            Sha384 = 243,                                   // SHA-384 哈希
            Crc64 = 244,                                    // CRC64
            FirstByte = 245,                                // 第一个字节
            First2Bytes = 246,                              // 前2个字节
            First4Bytes = 247,                              // 前4个字节
            First8Bytes = 248,                              // 前8个字节
            First16Bytes = 249,                             // 前16个字节
            First32Bytes = 250,                             // 前32个字节
            First64Bytes = 251,                             // 前64个字节
            First128Bytes = 252,                            // 前128个字节
            LastByte = 253,                                 // 最后一个字节
            Last2Bytes = 254,                               // 最后2个字节
            Last4Bytes = 255,                               // 最后4个字节
            Last8Bytes = 256,                               // 最后8个字节
            Last16Bytes = 257,                              // 最后16个字节
            Last32Bytes = 258,                              // 最后32个字节
            Last64Bytes = 259,                              // 最后64个字节
            Last128Bytes = 260,                             // 最后128个字节
            ByteOrderMark = 261,                            // BOM(字节顺序标记)
            VolumeLabel = 262,                              // 卷标
            FileListPathAndName = 263,                      // 文件列表路径
            DisplayPathAndName = 264,                       // 显示路径
            ParseName = 265,                                // 解析名称
            ParsePathAndName = 266,                         // 解析完整路径
            Stem = 267,                                     // 文件名主干(不含扩展名)
            ShellAttributes = 268,                          // Shell 属性
            IsFolder = 269,                                 // 是否为文件夹
            ValidUtf8 = 270,                                // 是否为有效 UTF-8
            StemLength = 271,                               // 文件名主干长度
            ExtensionLength = 272,                          // 扩展名长度
            PathPartLength = 273,                           // 路径部分长度
            DateModifiedTime = 274,                         // 修改时间(时间部分，DWORD)
            DateCreatedTime = 275,                          // 创建时间(时间部分)
            DateAccessedTime = 276,                         // 访问时间(时间部分)
            DateModifiedDate = 277,                         // 修改日期(日期部分)
            DateCreatedDate = 278,                          // 创建日期(日期部分)
            DateAccessedDate = 279,                         // 访问日期(日期部分)
            ParentName = 280,                               // 父目录名称
            ReparseTarget = 281,                            // 重解析点目标
            DescendantCount = 282,                          // 后代项数量
            DescendantFolderCount = 283,                    // 后代文件夹数量
            DescendantFileCount = 284,                      // 后代文件数量
            From = 285,                                     // 发件人
            To = 286,                                       // 收件人
            DateReceived = 287,                             // 接收时间(FILETIME)
            DateSent = 288,                                 // 发送时间(FILETIME)
            ContainerFilenames = 289,                       // 容器内文件名列表
            ContainerFileCount = 290,                       // 容器内文件数量
            CustomProperty0 = 291,                          // 自定义属性0
            CustomProperty1 = 292,                          // 自定义属性1
            CustomProperty2 = 293,                          // 自定义属性2
            CustomProperty3 = 294,                          // 自定义属性3
            CustomProperty4 = 295,                          // 自定义属性4
            CustomProperty5 = 296,                          // 自定义属性5
            CustomProperty6 = 297,                          // 自定义属性6
            CustomProperty7 = 298,                          // 自定义属性7
            CustomProperty8 = 299,                          // 自定义属性8
            CustomProperty9 = 300,                          // 自定义属性9
            AllocationSize = 301,                           // 分配大小(字节)
            SfvCrc32 = 302,                                 // SFV 文件中的 CRC32
            Md5sumMd5 = 303,                                // MD5SUM 文件中的 MD5
            Sha1sumSha1 = 304,                              // SHA1SUM 文件中的 SHA1
            Sha256sumSha256 = 305,                          // SHA256SUM 文件中的 SHA256
            SfvPass = 306,                                  // SFV 校验是否通过
            Md5sumPass = 307,                               // MD5 校验是否通过
            Sha1sumPass = 308,                              // SHA1 校验是否通过
            Sha256sumPass = 309,                            // SHA256 校验是否通过
            AlternateDataStreamAnsi = 310,                  // 备用数据流(ANSI文本)
            AlternateDataStreamUtf8 = 311,                  // 备用数据流(UTF-8)
            AlternateDataStreamUtf16Le = 312,               // 备用数据流(UTF-16LE)
            AlternateDataStreamUtf16Be = 313,               // 备用数据流(UTF-16BE)
            AlternateDataStreamTextPlain = 314,             // 备用数据流(纯文本)
            AlternateDataStreamHex = 315,                   // 备用数据流(十六进制)
            PerceivedType = 316,                            // 感知类型
            ContentType = 317,                              // 内容类型
            OpenedBy = 318,                                 // 打开方式
            TargetMachine = 319,                            // 目标机器架构
            Sha512sumSha512 = 320,                          // SHA512SUM 文件中的 SHA512
            Sha512sumPass = 321,                            // SHA512 校验是否通过
            ParentPath = 322,                               // 父目录路径
            First256Bytes = 323,                            // 前256字节
            First512Bytes = 324,                            // 前512字节
            Last256Bytes = 325,                             // 最后256字节
            Last512Bytes = 326,                             // 最后512字节
            IndexOnline = 327,                              // 索引是否在线
            Column0 = 328,                                  // 自定义列0
            Column1 = 329,                                  // 自定义列1
            Column2 = 330,                                  // 自定义列2
            Column3 = 331,                                  // 自定义列3
            Column4 = 332,                                  // 自定义列4
            Column5 = 333,                                  // 自定义列5
            Column6 = 334,                                  // 自定义列6
            Column7 = 335,                                  // 自定义列7
            Column8 = 336,                                  // 自定义列8
            Column9 = 337,                                  // 自定义列9
            ColumnA = 338,                                  // 自定义列A
            ColumnB = 339,                                  // 自定义列B
            ColumnC = 340,                                  // 自定义列C
            ColumnD = 341,                                  // 自定义列D
            ColumnE = 342,                                  // 自定义列E
            ColumnF = 343,                                  // 自定义列F
            ZoneId = 344,                                   // 区域ID(附件安全)
            ReferrerUrl = 345,                              // 来源URL
            HostUrl = 346,                                  // 主机URL
            CharacterEncoding = 347,                        // 字符编码
            RootName = 348,                                 // 根名称
            UsedDiskSize = 349,                             // 已用磁盘空间(字节)
            VolumePath = 350,                               // 卷路径
            MaxChildDepth = 351,                            // 最大子项深度
            TotalChildSize = 352,                           // 子项总大小(字节)
            Row = 353,                                      // 行号
            ChildOccurrenceCount = 354,                     // 子项出现次数
            VolumeName = 355,                               // 卷名称
            DescendantOccurrenceCount = 356,                // 后代出现次数
            ObjectId = 357,                                 // 对象ID
            BirthVolumeId = 358,                            // 创建卷ID
            BirthObjectId = 359,                            // 创建对象ID
            DomainId = 360,                                 // 域ID
            FolderDataCrc32 = 361,                          // 文件夹数据CRC32
            FolderDataCrc64 = 362,                          // 文件夹数据CRC64
            FolderDataMd5 = 363,                            // 文件夹数据MD5
            FolderDataSha1 = 364,                           // 文件夹数据SHA1
            FolderDataSha256 = 365,                         // 文件夹数据SHA256
            FolderDataSha512 = 366,                         // 文件夹数据SHA512
            FolderDataAndNamesCrc32 = 367,                  // 文件夹数据及名称CRC32
            FolderDataAndNamesCrc64 = 368,                  // 文件夹数据及名称CRC64
            FolderDataAndNamesMd5 = 369,                    // 文件夹数据及名称MD5
            FolderDataAndNamesSha1 = 370,                   // 文件夹数据及名称SHA1
            FolderDataAndNamesSha256 = 371,                 // 文件夹数据及名称SHA256
            FolderDataAndNamesSha512 = 372,                 // 文件夹数据及名称SHA512
            FolderNamesCrc32 = 373,                         // 文件夹名称CRC32
            FolderNamesCrc64 = 374,                         // 文件夹名称CRC64
            FolderNamesMd5 = 375,                           // 文件夹名称MD5
            FolderNamesSha1 = 376,                          // 文件夹名称SHA1
            FolderNamesSha256 = 377,                        // 文件夹名称SHA256
            FolderNamesSha512 = 378,                        // 文件夹名称SHA512
            FolderDataCrc32FromDisk = 379,                  // 磁盘文件夹数据CRC32
            FolderDataCrc64FromDisk = 380,                  // 磁盘文件夹数据CRC64
            FolderDataMd5FromDisk = 381,                    // 磁盘文件夹数据MD5
            FolderDataSha1FromDisk = 382,                   // 磁盘文件夹数据SHA1
            FolderDataSha256FromDisk = 383,                 // 磁盘文件夹数据SHA256
            FolderDataSha512FromDisk = 384,                 // 磁盘文件夹数据SHA512
            FolderDataAndNamesCrc32FromDisk = 385,          // 磁盘文件夹数据及名称CRC32
            FolderDataAndNamesCrc64FromDisk = 386,          // 磁盘文件夹数据及名称CRC64
            FolderDataAndNamesMd5FromDisk = 387,            // 磁盘文件夹数据及名称MD5
            FolderDataAndNamesSha1FromDisk = 388,           // 磁盘文件夹数据及名称SHA1
            FolderDataAndNamesSha256FromDisk = 389,         // 磁盘文件夹数据及名称SHA256
            FolderDataAndNamesSha512FromDisk = 390,         // 磁盘文件夹数据及名称SHA512
            FolderNamesCrc32FromDisk = 391,                 // 磁盘文件夹名称CRC32
            FolderNamesCrc64FromDisk = 392,                 // 磁盘文件夹名称CRC64
            FolderNamesMd5FromDisk = 393,                   // 磁盘文件夹名称MD5
            FolderNamesSha1FromDisk = 394,                  // 磁盘文件夹名称SHA1
            FolderNamesSha256FromDisk = 395,                // 磁盘文件夹名称SHA256
            FolderNamesSha512FromDisk = 396,                // 磁盘文件夹名称SHA512
            LongName = 397,                                 // 长文件名
            LongPathAndName = 398,                          // 长文件完整路径
            DigitalSignatureName = 399,                     // 数字签名名称
            DigitalSignatureTimestamp = 400,                // 数字签名时间(FILETIME)
            AudioTrackCount = 401,                          // 音频轨道数
            VideoTrackCount = 402,                          // 视频轨道数
            SubtitleTrackCount = 403,                       // 字幕轨道数
            NetworkIndexHost = 404,                         // 网络索引主机
            OriginalLocation = 405,                         // 原始位置
            DateDeleted = 406,                              // 删除时间(FILETIME)
            Status = 407,                                   // 状态
            VorbisComment = 408,                            // Vorbis 注释
            QuicktimeMetadata = 409,                        // QuickTime 元数据
            ParentSize = 410,                               // 父目录大小(字节)
            RootSize = 411,                                 // 根目录大小(字节)
            OpensWith = 412,                                // 打开方式
            Randomize = 413,                                // 随机化(排序用)
            Icon = 414,                                     // 图标
            Thumbnail = 415,                                // 缩略图
            Content = 416,                                  // 内容
            Separator = 417,                                // 分隔线
            BuiltinCount = 418,                             // 内置属性总数
        }

        /// <summary>
        /// 获取当前线程的最后错误码
        /// </summary>
        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_GetLastError", CallingConvention = CallConv)]
        internal static extern EveryThing3Error Everything3_GetLastError();

        /// <summary>
        /// 连接到 Everything (Unicode版本)
        /// </summary>
        /// <param name="instanceName"> 实例名称，null或空字符串表示默认实例 </param>
        /// <returns> 客户端句柄，失败返回IntPtr.Zero </returns>
        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_ConnectW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything3_ConnectW(string instanceName);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_CreateSearchState", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_CreateSearchState();

        // ---- 搜索状态管理 ----
        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_DestroySearchState", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_DestroySearchState(IntPtr searchState);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_GetMajorVersion", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetMajorVersion(IntPtr client);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_GetMinorVersion", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetMinorVersion(IntPtr client);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_GetRevision", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetRevision(IntPtr client);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_GetBuildNumber", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetBuildNumber(IntPtr client);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_SetSearchViewportOffset", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchViewportOffset(IntPtr searchState, IntPtr offset);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_SetSearchViewportCount", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchViewportCount(IntPtr searchState, IntPtr count);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_SetSearchTextW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchTextW(IntPtr searchState, string search);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_AddSearchPropertyRequest", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_AddSearchPropertyRequest(IntPtr searchState, EveryThing3PropertyId propertyId);

        /// <summary>
        /// 执行搜索(搜索+排序组合)
        /// </summary>
        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_Search", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_Search(IntPtr client, IntPtr searchState);

        // ---- 搜索执行 ----
        /// <summary>
        /// 仅获取结果(不排序)
        /// </summary>
        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_GetResults", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_GetResults(IntPtr client, IntPtr searchState);

        /// <summary>
        /// 仅排序已有结果
        /// </summary>
        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_Sort", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_Sort(IntPtr client, IntPtr searchState);

        // 搜索选项设置
        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_SetSearchMatchCase", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchMatchCase(IntPtr searchState, bool matchCase);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_GetResultListViewportCount", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_GetResultListViewportCount(IntPtr resultList);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_GetSearchMatchPath", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchMatchPath(IntPtr searchState);

        // 搜索选项读取
        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_GetSearchMatchCase", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchMatchCase(IntPtr searchState);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_GetSearchRegex", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchRegex(IntPtr searchState);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_GetSearchMatchWholeWords", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchMatchWholeWords(IntPtr searchState);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_SetSearchMatchWholeWords", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchMatchWholeWords(IntPtr searchState, bool matchWholeWords);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_SetSearchMatchPath", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchMatchPath(IntPtr searchState, bool matchPath);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_SetSearchRegex", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchRegex(IntPtr searchState, bool matchRegex);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_GetResultNameW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern int Everything3_GetResultNameW(IntPtr resultList, IntPtr resultIndex, [Out] char[] outBuf, IntPtr bufSize);

        // ---- 结果基本属性(便捷方法) ----
        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_GetResultPathW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern int Everything3_GetResultPathW(IntPtr resultList, IntPtr resultIndex, [Out] char[] outBuf, IntPtr bufSize);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_GetResultFullPathNameW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern int Everything3_GetResultFullPathNameW(IntPtr resultList, IntPtr resultIndex, [Out] char[] outBuf, IntPtr bufSize);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_GetResultSize", CallingConvention = CallConv)]
        internal static extern ulong Everything3_GetResultSize(IntPtr resultList, IntPtr resultIndex);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_GetResultExtensionW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern int Everything3_GetResultExtensionW(IntPtr resultList, IntPtr resultIndex, [Out] char[] outBuf, IntPtr bufSize);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_GetResultTypeW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern int Everything3_GetResultTypeW(IntPtr resultList, IntPtr resultIndex, [Out] char[] outBuf, IntPtr bufSize);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_IsFolderResult", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_IsFolderResult(IntPtr resultList, IntPtr resultIndex);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_GetPropertyType", CallingConvention = CallConv)]
        internal static extern EveryThing3PropertyType Everything3_GetPropertyType(IntPtr client, uint propertyId);

        /// <summary>
        /// 销毁客户端，释放资源
        /// </summary>
        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_DestroyClient", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_DestroyClient(IntPtr client);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_DestroyResultList", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_DestroyResultList(IntPtr resultList);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_IncRunCountFromFilenameW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern uint Everything3_IncRunCountFromFilenameW(IntPtr client, string filename);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_SetSearchMatchDiacritics", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchMatchDiacritics(IntPtr searchState, bool matchDiacritics);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_SetSearchMatchPrefix", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchMatchPrefix(IntPtr searchState, bool matchPrefix);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_SetSearchMatchSuffix", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchMatchSuffix(IntPtr searchState, bool matchSuffix);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_SetSearchIgnorePunctuation", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchIgnorePunctuation(IntPtr searchState, bool ignorePunctuation);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_SetSearchWhitespace", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchWhitespace(IntPtr searchState, bool ignoreWhitespace);

        [MinVersion("1.5.0")]
        [DllImport(DllPath3, EntryPoint = "Everything3_SetSearchFoldersFirst", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchFoldersFirst(IntPtr searchState, uint foldersFirstType);

        #endregion 1.5.0

        #endregion Internal Methods

        #region Structs

        [StructLayout(LayoutKind.Sequential)]
        public struct LargeInteger
        {
            public int LowPart;
            public int HighPart;
            public long QuadPart;
        }

        #endregion Structs
    }
}