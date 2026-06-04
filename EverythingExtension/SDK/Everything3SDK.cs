using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EverythingExtension.SDK
{
    /*

    #region 常量定义

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
    /// 目标机器架构类型
    /// </summary>
    public enum EveryThing3TargetMachine : uint
    {
        Unknown = 0,    // 未知
        X86 = 1,        // x86 32位
        X64 = 2,        // x64 64位
        Arm = 3,        // ARM 32位
        Arm64 = 4,      // ARM64
    }

    /// <summary>
    /// 搜索结果中文件夹优先显示模式
    /// </summary>
    public enum EveryThing3SearchFoldersFirst : uint
    {
        Ascending = 0,  // 升序排序时文件夹排在前面
        Always = 1,     // 始终文件夹排在前面
        Never = 2,      // 文件夹排在最后
        Descending = 3, // 降序排序时文件夹排在前面
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
    /// 属性值类型
    /// </summary>
    public enum EveryThing3PropertyValueType : uint
    {
        Null = 0,                                   // 空
        Byte = 1,                                   // 单字节
        Word = 2,                                   // 双字节
        Dword = 3,                                  // 四字节
        DwordFixedQ1K = 4,                          // 四字节定点数(Q1K, 除以1000)
        Uint64 = 5,                                 // 64位无符号整数
        Uint128 = 6,                                // 128位无符号整数
        Dimensions = 7,                             // 尺寸(宽/高)
        PString = 8,                                // 字符串指针
        PStringMultiString = 9,                     // 多字符串指针(以\0分隔，双\0结尾)
        PStringStringReference = 10,                // 字符串引用指针
        SizeT = 11,                                 // SIZE_T(指针大小的整数)
        Int32FixedQ1K = 12,                         // 32位定点数(Q1K, 除以1000)
        Int32FixedQ1M = 13,                         // 32位定点数(Q1M, 除以1000000)
        PStringFolderReference = 14,                // 文件夹路径字符串引用
        PStringFileOrFolderReference = 15,          // 文件或文件夹路径字符串引用
        Blob8 = 16,                                 // 8字节二进制数据
        DwordGetText = 17,                          // DWORD 值(可通过 GetText 转为字符串)
        WordGetText = 18,                           // WORD 值(可通过 GetText 转为字符串)
        Blob16 = 19,                                // 16字节二进制数据
        ByteGetText = 20,                           // BYTE 值(可通过 GetText 转为字符串)
        PropVariant = 21,                           // PROPVARIANT 类型
    }

    /// <summary>
    /// PROPVARIANT 值子类型
    /// </summary>
    public enum EveryThing3PropertyVariantType : uint
    {
        Empty = 0,                          // 空
        Null = 1,                           // 空值
        ByteUi1 = 2,                        // 无符号字节(UI1)
        WordUi2 = 3,                        // 无符号双字节(UI2)
        DwordUi4 = 4,                       // 无符号四字节(UI4)
        DwordUint = 5,                      // 无符号四字节(UINT)
        Uint64Ui8 = 6,                      // 无符号64位(UI8)
        Uint64Filetime = 7,                 // FILETIME 64位时间
        CharI1 = 8,                         // 有符号字节(I1)
        Int16I2 = 9,                        // 有符号16位(I2)
        Int16Bool = 10,                     // 16位布尔值
        Int32I4 = 11,                       // 有符号32位(I4)
        Int32Int = 12,                      // 有符号32位(INT)
        Int32Error = 13,                    // 32位错误码
        Int64I8 = 14,                       // 有符号64位(I8)
        Int64Cy = 15,                       // 64位货币值
        FloatR4 = 16,                       // 单精度浮点(R4)
        DoubleR8 = 17,                      // 双精度浮点(R8)
        DoubleDate = 18,                    // 双精度日期
        PointerClsid = 19,                  // CLSID 指针
        StringBstr = 20,                    // BSTR 字符串
        StringLpwstr = 21,                  // LPWSTR 宽字符串
        StringLpstr = 22,                   // LPSTR 多字节字符串
        Blob = 23,                          // 二进制大对象
        ArrayByteUi1 = 128 | 2,             // BYTE 数组
        ArrayWordUi2 = 128 | 3,             // WORD 数组
        ArrayDwordUi4 = 128 | 4,            // DWORD 数组
        ArrayUint64Ui8 = 128 | 6,           // UINT64 数组
        ArrayUint64Filetime = 128 | 7,      // FILETIME 数组
        ArrayCharI1 = 128 | 8,              // CHAR 数组
        ArrayInt16I2 = 128 | 9,             // INT16 数组
        ArrayInt16Bool = 128 | 10,          // BOOL 数组
        ArrayInt32I4 = 128 | 11,            // INT32 数组
        ArrayInt32Error = 128 | 13,         // 错误码数组
        ArrayInt64I8 = 128 | 14,            // INT64 数组
        ArrayInt64Cy = 128 | 15,            // 货币值数组
        ArrayFloatR4 = 128 | 16,            // 浮点数组
        ArrayDoubleR8 = 128 | 17,           // 双精度数组
        ArrayDoubleDate = 128 | 18,         // 日期数组
        ArrayClsid = 128 | 19,              // CLSID 数组
        ArrayStringBstr = 128 | 20,         // BSTR 字符串数组
        ArrayStringLpwstr = 128 | 21,       // LPWSTR 字符串数组
        ArrayStringLpstr = 128 | 22,        // LPSTR 字符串数组
    }

    /// <summary>
    /// 日志变更类型
    /// </summary>
    public enum EveryThing3JournalChangeType : byte
    {
        FolderCreate = 1,   // 文件夹创建
        FolderDelete = 2,   // 文件夹删除
        FolderRename = 3,   // 文件夹重命名
        FolderMove = 4,     // 文件夹移动
        FolderModify = 5,   // 文件夹修改
        FileCreate = 6,     // 文件创建
        FileDelete = 7,     // 文件删除
        FileRename = 8,     // 文件重命名
        FileMove = 9,       // 文件移动
        FileModify = 10,    // 文件修改
    }

    /// <summary>
    /// 读取日志的标志位
    /// </summary>
    [Flags]
    public enum EveryThing3ReadJournal : uint
    {
        ChangeId = 0x00000001,              // 返回变更ID
        Timestamp = 0x00000002,             // 返回时间戳
        SourceTimestamp = 0x00000004,       // 返回源时间戳(USN日志时间戳)
        OldParentDateModified = 0x00000008, // 返回旧父目录修改时间
        OldPath = 0x00000010,               // 返回旧路径
        OldName = 0x00000020,               // 返回旧名称
        Size = 0x00000040,                  // 返回文件大小
        DateCreated = 0x00000080,           // 返回创建时间
        DateModified = 0x00000100,          // 返回修改时间
        DateAccessed = 0x00000200,          // 返回访问时间
        Attributes = 0x00000400,            // 返回文件属性
        NewParentDateModified = 0x00000800, // 返回新父目录修改时间
        NewPath = 0x00001000,               // 返回新路径
        NewName = 0x00002000,               // 返回新名称
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

    #endregion 常量定义

    #region 结构体定义

    /// <summary>
    /// 图片尺寸(宽 x 高)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EveryThing3Dimensions
    {
        public uint Width;      // 宽度(像素)
        public uint Height;     // 高度(像素)
    }

    /// <summary>
    /// 128位无符号整数
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EveryThing3UInt128
    {
        public ulong LoUInt64;  // 低64位
        public ulong HiUInt64;  // 高64位
    }

    /// <summary>
    /// 日志信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EveryThing3JournalInfo
    {
        public ulong JournalId;          // 日志ID
        public ulong FirstItemIndex;     // 第一个可用变更的索引(从0开始)
        public ulong NextItemIndex;      // 下一个变更的索引(从0开始)
        public ulong Size;               // 日志当前大小(字节)
        public ulong MaxSize;            // 日志最大大小(字节)
    }

    /// <summary>
    /// 日志变更记录(Unicode版本)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EveryThing3JournalChangeW
    {
        public ulong JournalId;                  // 当前日志ID(不会变化)
        public ulong ChangeId;                   // 当前变更ID
        public ulong Timestamp;                  // 变更时间戳
        public ulong SourceTimestamp;            // 源时间戳(USN日志时间戳)
        public ulong OldParentDateModified;      // 旧父目录修改时间(UINT64_MAX表示未知)
        public ulong NewParentDateModified;      // 新父目录修改时间(UINT64_MAX表示未知)
        public ulong Size;                       // 文件大小(字节, UINT64_MAX表示未知)
        public ulong DateCreated;                // 创建时间(UINT64_MAX表示未知)
        public ulong DateModified;               // 修改时间(UINT64_MAX表示未知)
        public ulong DateAccessed;               // 访问时间(UINT64_MAX表示未知)
        public IntPtr OldPath;                   // 旧路径(空字符串表示未知)
        public IntPtr OldPathLen;                // 旧路径长度
        public IntPtr OldName;                   // 旧文件名(空字符串表示未知)
        public IntPtr OldNameLen;                // 旧文件名长度
        public IntPtr NewPath;                   // 新路径(空字符串表示未知)
        public IntPtr NewPathLen;                // 新路径长度
        public IntPtr NewName;                   // 新文件名(空字符串表示未知)
        public IntPtr NewNameLen;                // 新文件名长度
        public uint Attributes;                  // Windows 文件属性(FILE_ATTRIBUTE_DIRECTORY始终有效)
        public byte Type;                        // 变更类型(EveryThing3JournalChangeType)
    }

    /// <summary>
    /// 日志变更记录(UTF-8版本)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EveryThing3JournalChangeUtf8
    {
        public ulong JournalId;                  // 当前日志ID
        public ulong ChangeId;                   // 当前变更ID
        public ulong Timestamp;                  // 变更时间戳
        public ulong SourceTimestamp;            // 源时间戳
        public ulong OldParentDateModified;      // 旧父目录修改时间
        public ulong NewParentDateModified;      // 新父目录修改时间
        public ulong Size;                       // 文件大小
        public ulong DateCreated;                // 创建时间
        public ulong DateModified;               // 修改时间
        public ulong DateAccessed;               // 访问时间
        public IntPtr OldPath;                   // 旧路径(UTF-8)
        public IntPtr OldPathLen;                // 旧路径长度
        public IntPtr OldName;                   // 旧文件名(UTF-8)
        public IntPtr OldNameLen;                // 旧文件名长度
        public IntPtr NewPath;                   // 新路径(UTF-8)
        public IntPtr NewPathLen;                // 新路径长度
        public IntPtr NewName;                   // 新文件名(UTF-8)
        public IntPtr NewNameLen;                // 新文件名长度
        public uint Attributes;                  // Windows 文件属性
        public byte Type;                        // 变更类型
    }

    #endregion 结构体定义

    #region 回调委托

    /// <summary>
    /// 读取日志的回调函数(Unicode版本)
    /// </summary>
    /// <param name="userData">      用户自定义数据指针 </param>
    /// <param name="journalChange"> 日志变更记录 </param>
    /// <returns> 返回true继续读取，返回false停止 </returns>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate bool EveryThing3ReadJournalCallbackW(IntPtr userData, ref EveryThing3JournalChangeW journalChange);

    /// <summary>
    /// 读取日志的回调函数(UTF-8版本)
    /// </summary>
    /// <param name="userData">      用户自定义数据指针 </param>
    /// <param name="journalChange"> 日志变更记录 </param>
    /// <returns> 返回true继续读取，返回false停止 </returns>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate bool EveryThing3ReadJournalCallbackUtf8(IntPtr userData, ref EveryThing3JournalChangeUtf8 journalChange);

    #endregion 回调委托

    #region 平台相关DLL加载

    /// <summary>
    /// 获取当前进程架构对应的DLL名称
    /// </summary>
    internal static class NativeLibrary
    {
        #region Internal Methods

        internal static string GetDllName()
        {
            if (Environment.Is64BitProcess)
                return "Everything3_x64.dll";
            else
                return "Everything3_x86.dll";
        }

        #endregion Internal Methods
    }

    #endregion 平台相关DLL加载

    #region 原生 P/Invoke 方法定义

    /// <summary>
    /// Everything3 原生函数 P/Invoke 绑定(Unicode W版本)
    /// </summary>
    internal static class Everything3SDK
    {
        #region Public Constructors

        static Everything3SDK()
        {
            string dllPath = NativeLibrary.GetDllName();
            _loadedDll = LoadLibrary(dllPath);
            if (_loadedDll == IntPtr.Zero)
                throw new DllNotFoundException(
                    $"无法加载 {dllPath}。请确保DLL在应用程序目录或PATH中。");
        }

        #endregion Public Constructors

        #region Fields

        private const string DllName = "Everything3_x64.dll";
        private const CallingConvention CallConv = CallingConvention.StdCall;

        // 静态构造函数中预加载对应架构的DLL
        private static readonly IntPtr _loadedDll;

        #endregion Fields

        #region Internal Methods

        /// <summary>
        /// 获取当前线程的最后错误码
        /// </summary>
        [DllImport(DllName, EntryPoint = "Everything3_GetLastError", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetLastError();

        /// <summary>
        /// 连接到 Everything (Unicode版本)
        /// </summary>
        /// <param name="instanceName"> 实例名称，null或空字符串表示默认实例 </param>
        /// <returns> 客户端句柄，失败返回IntPtr.Zero </returns>
        [DllImport(DllName, EntryPoint = "Everything3_ConnectW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything3_ConnectW(string instanceName);

        // ---- 错误处理 ----
        // ---- 连接管理 ----
        /// <summary>
        /// 关闭客户端连接(可从中止阻塞操作)
        /// </summary>
        [DllImport(DllName, EntryPoint = "Everything3_ShutdownClient", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_ShutdownClient(IntPtr client);

        /// <summary>
        /// 销毁客户端，释放资源
        /// </summary>
        [DllImport(DllName, EntryPoint = "Everything3_DestroyClient", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_DestroyClient(IntPtr client);

        [DllImport(DllName, EntryPoint = "Everything3_GetIPCPipeVersion", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetIPCPipeVersion(IntPtr client);

        // ---- 版本信息 ----
        [DllImport(DllName, EntryPoint = "Everything3_GetMajorVersion", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetMajorVersion(IntPtr client);

        [DllImport(DllName, EntryPoint = "Everything3_GetMinorVersion", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetMinorVersion(IntPtr client);

        [DllImport(DllName, EntryPoint = "Everything3_GetRevision", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetRevision(IntPtr client);

        [DllImport(DllName, EntryPoint = "Everything3_GetBuildNumber", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetBuildNumber(IntPtr client);

        [DllImport(DllName, EntryPoint = "Everything3_GetTargetMachine", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetTargetMachine(IntPtr client);

        [DllImport(DllName, EntryPoint = "Everything3_IsDBLoaded", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_IsDBLoaded(IntPtr client);

        [DllImport(DllName, EntryPoint = "Everything3_GetRunCountFromFilenameW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern uint Everything3_GetRunCountFromFilenameW(IntPtr client, string filename);

        // ---- 文件/运行计数操作 ----
        [DllImport(DllName, EntryPoint = "Everything3_SetRunCountFromFilenameW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetRunCountFromFilenameW(IntPtr client, string filename, uint runCount);

        [DllImport(DllName, EntryPoint = "Everything3_IncRunCountFromFilenameW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern uint Everything3_IncRunCountFromFilenameW(IntPtr client, string filename);

        [DllImport(DllName, EntryPoint = "Everything3_GetFolderSizeFromFilenameW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern ulong Everything3_GetFolderSizeFromFilenameW(IntPtr client, string filename);

        [DllImport(DllName, EntryPoint = "Everything3_GetFileAttributesW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern uint Everything3_GetFileAttributesW(IntPtr client, string filename);

        [DllImport(DllName, EntryPoint = "Everything3_CreateSearchState", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_CreateSearchState();

        // ---- 搜索状态管理 ----
        [DllImport(DllName, EntryPoint = "Everything3_DestroySearchState", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_DestroySearchState(IntPtr searchState);

        // 搜索选项设置
        [DllImport(DllName, EntryPoint = "Everything3_SetSearchMatchCase", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchMatchCase(IntPtr searchState, bool matchCase);

        [DllImport(DllName, EntryPoint = "Everything3_SetSearchMatchDiacritics", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchMatchDiacritics(IntPtr searchState, bool matchDiacritics);

        [DllImport(DllName, EntryPoint = "Everything3_SetSearchMatchWholeWords", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchMatchWholeWords(IntPtr searchState, bool matchWholeWords);

        [DllImport(DllName, EntryPoint = "Everything3_SetSearchMatchPath", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchMatchPath(IntPtr searchState, bool matchPath);

        [DllImport(DllName, EntryPoint = "Everything3_SetSearchMatchPrefix", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchMatchPrefix(IntPtr searchState, bool matchPrefix);

        [DllImport(DllName, EntryPoint = "Everything3_SetSearchMatchSuffix", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchMatchSuffix(IntPtr searchState, bool matchSuffix);

        [DllImport(DllName, EntryPoint = "Everything3_SetSearchIgnorePunctuation", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchIgnorePunctuation(IntPtr searchState, bool ignorePunctuation);

        [DllImport(DllName, EntryPoint = "Everything3_SetSearchWhitespace", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchWhitespace(IntPtr searchState, bool ignoreWhitespace);

        [DllImport(DllName, EntryPoint = "Everything3_SetSearchRegex", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchRegex(IntPtr searchState, bool matchRegex);

        [DllImport(DllName, EntryPoint = "Everything3_SetSearchFoldersFirst", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchFoldersFirst(IntPtr searchState, uint foldersFirstType);

        [DllImport(DllName, EntryPoint = "Everything3_SetSearchRequestTotalSize", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchRequestTotalSize(IntPtr searchState, bool requestTotalSize);

        [DllImport(DllName, EntryPoint = "Everything3_SetSearchHideResultOmissions", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchHideResultOmissions(IntPtr searchState, bool hideResultOmissions);

        [DllImport(DllName, EntryPoint = "Everything3_SetSearchSortMix", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchSortMix(IntPtr searchState, bool sortMix);

        [DllImport(DllName, EntryPoint = "Everything3_SetSearchTextW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchTextW(IntPtr searchState, string search);

        [DllImport(DllName, EntryPoint = "Everything3_AddSearchSort", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_AddSearchSort(IntPtr searchState, uint propertyId, bool ascending);

        [DllImport(DllName, EntryPoint = "Everything3_ClearSearchSorts", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_ClearSearchSorts(IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_AddSearchPropertyRequest", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_AddSearchPropertyRequest(IntPtr searchState, uint propertyId);

        [DllImport(DllName, EntryPoint = "Everything3_AddSearchPropertyRequestFormatted", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_AddSearchPropertyRequestFormatted(IntPtr searchState, uint propertyId);

        [DllImport(DllName, EntryPoint = "Everything3_AddSearchPropertyRequestHighlighted", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_AddSearchPropertyRequestHighlighted(IntPtr searchState, uint propertyId);

        [DllImport(DllName, EntryPoint = "Everything3_ClearSearchPropertyRequests", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_ClearSearchPropertyRequests(IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_SetSearchViewportOffset", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchViewportOffset(IntPtr searchState, IntPtr offset);

        [DllImport(DllName, EntryPoint = "Everything3_SetSearchViewportCount", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchViewportCount(IntPtr searchState, IntPtr count);

        [DllImport(DllName, EntryPoint = "Everything3_SetSearchSort", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_SetSearchSort(IntPtr searchState, uint propertyId, bool ascending);

        // 搜索选项读取
        [DllImport(DllName, EntryPoint = "Everything3_GetSearchMatchCase", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchMatchCase(IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchMatchDiacritics", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchMatchDiacritics(IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchMatchWholeWords", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchMatchWholeWords(IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchMatchPath", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchMatchPath(IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchMatchPrefix", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchMatchPrefix(IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchMatchSuffix", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchMatchSuffix(IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchIgnorePunctuation", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchIgnorePunctuation(IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchWhitespace", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchWhitespace(IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchRegex", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchRegex(IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchFoldersFirst", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetSearchFoldersFirst(IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchRequestTotalSize", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchRequestTotalSize(IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchHideResultOmissions", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchHideResultOmissions(IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchSortMix", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchSortMix(IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchTextW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything3_GetSearchTextW(IntPtr searchState, StringBuilder outBuf, IntPtr bufSize);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchSortCount", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_GetSearchSortCount(IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchSortPropertyId", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetSearchSortPropertyId(IntPtr searchState, IntPtr sortIndex);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchSortAscending", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchSortAscending(IntPtr searchState, IntPtr sortIndex);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchPropertyRequestCount", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_GetSearchPropertyRequestCount(IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchPropertyRequestPropertyId", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetSearchPropertyRequestPropertyId(IntPtr searchState, IntPtr index);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchPropertyRequestHighlight", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchPropertyRequestHighlight(IntPtr searchState, IntPtr index);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchPropertyRequestFormat", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetSearchPropertyRequestFormat(IntPtr searchState, IntPtr index);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchViewportOffset", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_GetSearchViewportOffset(IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_GetSearchViewportCount", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_GetSearchViewportCount(IntPtr searchState);

        /// <summary>
        /// 执行搜索(搜索+排序组合)
        /// </summary>
        [DllImport(DllName, EntryPoint = "Everything3_Search", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_Search(IntPtr client, IntPtr searchState);

        // ---- 搜索执行 ----
        /// <summary>
        /// 仅获取结果(不排序)
        /// </summary>
        [DllImport(DllName, EntryPoint = "Everything3_GetResults", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_GetResults(IntPtr client, IntPtr searchState);

        /// <summary>
        /// 仅排序已有结果
        /// </summary>
        [DllImport(DllName, EntryPoint = "Everything3_Sort", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_Sort(IntPtr client, IntPtr searchState);

        [DllImport(DllName, EntryPoint = "Everything3_IsResultListChange", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_IsResultListChange(IntPtr client);

        [DllImport(DllName, EntryPoint = "Everything3_WaitForResultListChange", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_WaitForResultListChange(IntPtr client);

        [DllImport(DllName, EntryPoint = "Everything3_DestroyResultList", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_DestroyResultList(IntPtr resultList);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultListFolderCount", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_GetResultListFolderCount(IntPtr resultList);

        // ---- 结果列表查询 ----
        [DllImport(DllName, EntryPoint = "Everything3_GetResultListFileCount", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_GetResultListFileCount(IntPtr resultList);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultListCount", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_GetResultListCount(IntPtr resultList);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultListTotalSize", CallingConvention = CallConv)]
        internal static extern ulong Everything3_GetResultListTotalSize(IntPtr resultList);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultListViewportOffset", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_GetResultListViewportOffset(IntPtr resultList);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultListViewportCount", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_GetResultListViewportCount(IntPtr resultList);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultListSortCount", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_GetResultListSortCount(IntPtr resultList);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultListSortPropertyId", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetResultListSortPropertyId(IntPtr resultList, IntPtr sortIndex);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultListSortAscending", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetResultListSortAscending(IntPtr resultList, IntPtr sortIndex);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultListPropertyRequestCount", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_GetResultListPropertyRequestCount(IntPtr resultList);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultListPropertyRequestPropertyId", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetResultListPropertyRequestPropertyId(IntPtr resultList, IntPtr propertyIndex);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultListPropertyRequestValueType", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetResultListPropertyRequestValueType(IntPtr resultList, IntPtr propertyIndex);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultListPropertyRequestOffset", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_GetResultListPropertyRequestOffset(IntPtr resultList, IntPtr propertyIndex);

        [DllImport(DllName, EntryPoint = "Everything3_IsFolderResult", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_IsFolderResult(IntPtr resultList, IntPtr resultIndex);

        [DllImport(DllName, EntryPoint = "Everything3_IsRootResult", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_IsRootResult(IntPtr resultList, IntPtr resultIndex);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultPropertyTextW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything3_GetResultPropertyTextW(IntPtr resultList, IntPtr resultIndex, uint propertyId, [Out] char[] outBuf, IntPtr bufSize);

        // ---- 结果属性读取(文本) ----
        [DllImport(DllName, EntryPoint = "Everything3_GetResultPropertyTextFormattedW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything3_GetResultPropertyTextFormattedW(IntPtr resultList, IntPtr resultIndex, uint propertyId, [Out] char[] outBuf, IntPtr bufSize);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultPropertyTextHighlightedW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything3_GetResultPropertyTextHighlightedW(IntPtr resultList, IntPtr resultIndex, uint propertyId, [Out] char[] outBuf, IntPtr bufSize);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultPropertyBYTE", CallingConvention = CallConv)]
        internal static extern byte Everything3_GetResultPropertyBYTE(IntPtr resultList, IntPtr resultIndex, uint propertyId);

        // ---- 结果属性读取(数值) ----
        [DllImport(DllName, EntryPoint = "Everything3_GetResultPropertyWORD", CallingConvention = CallConv)]
        internal static extern ushort Everything3_GetResultPropertyWORD(IntPtr resultList, IntPtr resultIndex, uint propertyId);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultPropertyDWORD", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetResultPropertyDWORD(IntPtr resultList, IntPtr resultIndex, uint propertyId);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultPropertyUINT64", CallingConvention = CallConv)]
        internal static extern ulong Everything3_GetResultPropertyUINT64(IntPtr resultList, IntPtr resultIndex, uint propertyId);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultPropertyUINT128", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetResultPropertyUINT128(IntPtr resultList, IntPtr resultIndex, uint propertyId, out EveryThing3UInt128 value);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultPropertyDIMENSIONS", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetResultPropertyDIMENSIONS(IntPtr resultList, IntPtr resultIndex, uint propertyId, out EveryThing3Dimensions dimensions);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultPropertySIZE_T", CallingConvention = CallConv)]
        internal static extern IntPtr Everything3_GetResultPropertySIZE_T(IntPtr resultList, IntPtr resultIndex, uint propertyId);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultPropertyINT32", CallingConvention = CallConv)]
        internal static extern int Everything3_GetResultPropertyINT32(IntPtr resultList, IntPtr resultIndex, uint propertyId);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultPropertyBlob", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetResultPropertyBlob(IntPtr resultList, IntPtr resultIndex, uint propertyId, [Out] byte[] outBuf, ref IntPtr bufSize);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultNameW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything3_GetResultNameW(IntPtr resultList, IntPtr resultIndex, [Out] byte[] outBuf, IntPtr bufSize);

        // ---- 结果基本属性(便捷方法) ----
        [DllImport(DllName, EntryPoint = "Everything3_GetResultPathW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything3_GetResultPathW(IntPtr resultList, IntPtr resultIndex, [Out] byte[] outBuf, IntPtr bufSize);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultFullPathNameW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything3_GetResultFullPathNameW(IntPtr resultList, IntPtr resultIndex, [Out] byte[] outBuf, IntPtr bufSize);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultSize", CallingConvention = CallConv)]
        internal static extern ulong Everything3_GetResultSize(IntPtr resultList, IntPtr resultIndex);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultExtensionW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything3_GetResultExtensionW(IntPtr resultList, IntPtr resultIndex, [Out] byte[] outBuf, IntPtr bufSize);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultTypeW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything3_GetResultTypeW(IntPtr resultList, IntPtr resultIndex, [Out] byte[] outBuf, IntPtr bufSize);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultDateModified", CallingConvention = CallConv)]
        internal static extern ulong Everything3_GetResultDateModified(IntPtr resultList, IntPtr resultIndex);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultDateCreated", CallingConvention = CallConv)]
        internal static extern ulong Everything3_GetResultDateCreated(IntPtr resultList, IntPtr resultIndex);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultDateAccessed", CallingConvention = CallConv)]
        internal static extern ulong Everything3_GetResultDateAccessed(IntPtr resultList, IntPtr resultIndex);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultAttributes", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetResultAttributes(IntPtr resultList, IntPtr resultIndex);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultDateRecentlyChanged", CallingConvention = CallConv)]
        internal static extern ulong Everything3_GetResultDateRecentlyChanged(IntPtr resultList, IntPtr resultIndex);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultRunCount", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetResultRunCount(IntPtr resultList, IntPtr resultIndex);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultDateRun", CallingConvention = CallConv)]
        internal static extern ulong Everything3_GetResultDateRun(IntPtr resultList, IntPtr resultIndex);

        [DllImport(DllName, EntryPoint = "Everything3_GetResultFilelistFilenameW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything3_GetResultFilelistFilenameW(IntPtr resultList, IntPtr resultIndex, StringBuilder outBuf, IntPtr bufSize);

        [DllImport(DllName, EntryPoint = "Everything3_FindPropertyW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern uint Everything3_FindPropertyW(IntPtr client, string canonicalName);

        // ---- 属性系统查询 ----
        [DllImport(DllName, EntryPoint = "Everything3_GetPropertyNameW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything3_GetPropertyNameW(IntPtr client, uint propertyId, StringBuilder outBuf, IntPtr bufSize);

        [DllImport(DllName, EntryPoint = "Everything3_GetPropertyCanonicalNameW", CallingConvention = CallConv, CharSet = CharSet.Unicode)]
        internal static extern IntPtr Everything3_GetPropertyCanonicalNameW(IntPtr client, uint propertyId, StringBuilder outBuf, IntPtr bufSize);

        [DllImport(DllName, EntryPoint = "Everything3_GetPropertyType", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetPropertyType(IntPtr client, uint propertyId);

        [DllImport(DllName, EntryPoint = "Everything3_IsPropertyIndexed", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_IsPropertyIndexed(IntPtr client, uint propertyId);

        [DllImport(DllName, EntryPoint = "Everything3_IsPropertyFastSort", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_IsPropertyFastSort(IntPtr client, uint propertyId);

        [DllImport(DllName, EntryPoint = "Everything3_IsPropertyRightAligned", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_IsPropertyRightAligned(IntPtr client, uint propertyId);

        [DllImport(DllName, EntryPoint = "Everything3_IsPropertySortDescending", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_IsPropertySortDescending(IntPtr client, uint propertyId);

        [DllImport(DllName, EntryPoint = "Everything3_GetPropertyDefaultWidth", CallingConvention = CallConv)]
        internal static extern uint Everything3_GetPropertyDefaultWidth(IntPtr client, uint propertyId);

        [DllImport(DllName, EntryPoint = "Everything3_GetJournalInfo", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_GetJournalInfo(IntPtr client, out EveryThing3JournalInfo info);

        // ---- 日志(Journal)功能 ----
        [DllImport(DllName, EntryPoint = "Everything3_ReadJournalW", CallingConvention = CallConv)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool Everything3_ReadJournalW(IntPtr client, ulong journalId, ulong changeId, uint flags, IntPtr userData, IntPtr callback);

        #endregion Internal Methods

        #region Private Methods

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        #endregion Private Methods
    }

    #endregion 原生 P/Invoke 方法定义

    */
    /*

    #region 托管封装类

    /// <summary>
    /// Everything 客户端连接封装
    /// </summary>
    public class Everything3Client : IDisposable
    {
        #region Public Constructors

        /// <summary>
        /// 连接到 Everything
        /// </summary>
        /// <param name="instanceName"> 实例名称，null或空字符串连接默认实例 </param>
        /// <exception cref="InvalidOperationException"> 连接失败时抛出 </exception>
        public Everything3Client(string? instanceName = null)
        {
            _client = Everything3SDK.Everything3_ConnectW(instanceName ?? string.Empty);
            if (_client == IntPtr.Zero)
            {
                throw new InvalidOperationException(
                    $"连接到 Everything 失败。错误: {(EveryThing3Error)GetLastError()}");
            }
        }

        #endregion Public Constructors

        #region Internal Constructors

        internal Everything3Client(IntPtr clientHandle)
        {
            _client = clientHandle;
        }

        #endregion Internal Constructors

        #region Fields

        private IntPtr _client;
        private bool _disposed;

        #endregion Fields

        #region Properties

        /// <summary>
        /// 获取原生客户端句柄
        /// </summary>
        public IntPtr Handle => _client;

        /// <summary>
        /// IPC 管道协议版本
        /// </summary>
        public uint IPCPipeVersion => Everything3SDK.Everything3_GetIPCPipeVersion(_client);

        // ---- 版本信息属性 ----
        /// <summary>
        /// 主版本号
        /// </summary>
        public uint MajorVersion => Everything3SDK.Everything3_GetMajorVersion(_client);

        /// <summary>
        /// 次版本号
        /// </summary>
        public uint MinorVersion => Everything3SDK.Everything3_GetMinorVersion(_client);

        /// <summary>
        /// 修订号
        /// </summary>
        public uint Revision => Everything3SDK.Everything3_GetRevision(_client);

        /// <summary>
        /// 构建号
        /// </summary>
        public uint BuildNumber => Everything3SDK.Everything3_GetBuildNumber(_client);

        /// <summary>
        /// 目标机器架构
        /// </summary>
        public EveryThing3TargetMachine TargetMachine
            => (EveryThing3TargetMachine)Everything3SDK.Everything3_GetTargetMachine(_client);

        /// <summary>
        /// 数据库是否已加载
        /// </summary>
        public bool IsDbLoaded => Everything3SDK.Everything3_IsDBLoaded(_client);

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// 释放客户端资源
        /// </summary>
        public void Dispose()
        {
            if (!_disposed && _client != IntPtr.Zero)
            {
                Everything3SDK.Everything3_ShutdownClient(_client);
                Everything3SDK.Everything3_DestroyClient(_client);
                _client = IntPtr.Zero;
                _disposed = true;
            }
        }

        /// <summary>
        /// 获取最后一次操作的错误码
        /// </summary>
        public uint GetLastError() => Everything3SDK.Everything3_GetLastError();

        // ---- 文件操作 ----

        /// <summary>
        /// 获取文件运行次数
        /// </summary>
        public uint GetRunCountFromFilename(string filename)
            => Everything3SDK.Everything3_GetRunCountFromFilenameW(_client, filename);

        /// <summary>
        /// 设置文件运行次数
        /// </summary>
        public bool SetRunCountFromFilename(string filename, uint runCount)
            => Everything3SDK.Everything3_SetRunCountFromFilenameW(_client, filename, runCount);

        /// <summary>
        /// 递增文件运行次数并返回新值
        /// </summary>
        public uint IncRunCountFromFilename(string filename)
            => Everything3SDK.Everything3_IncRunCountFromFilenameW(_client, filename);

        /// <summary>
        /// 获取文件夹总大小(字节)
        /// </summary>
        public ulong GetFolderSizeFromFilename(string filename)
            => Everything3SDK.Everything3_GetFolderSizeFromFilenameW(_client, filename);

        /// <summary>
        /// 获取文件属性
        /// </summary>
        public uint GetFileAttributes(string filename)
            => Everything3SDK.Everything3_GetFileAttributesW(_client, filename);

        // ---- 属性系统 ----

        /// <summary>
        /// 根据规范名称查找属性ID
        /// </summary>
        public uint FindProperty(string canonicalName)
            => Everything3SDK.Everything3_FindPropertyW(_client, canonicalName);

        /// <summary>
        /// 获取属性显示名称
        /// </summary>
        public string? GetPropertyName(uint propertyId)
            => GetStringViaBuffer((sb, size) =>
                Everything3SDK.Everything3_GetPropertyNameW(_client, propertyId, sb, size));

        /// <summary>
        /// 获取属性规范名称
        /// </summary>
        public string? GetPropertyCanonicalName(uint propertyId)
            => GetStringViaBuffer((sb, size) =>
                Everything3SDK.Everything3_GetPropertyCanonicalNameW(_client, propertyId, sb, size));

        /// <summary>
        /// 获取属性类型
        /// </summary>
        public EveryThing3PropertyType GetPropertyType(uint propertyId)
            => (EveryThing3PropertyType)Everything3SDK.Everything3_GetPropertyType(_client, propertyId);

        /// <summary>
        /// 属性是否已索引
        /// </summary>
        public bool IsPropertyIndexed(uint propertyId)
            => Everything3SDK.Everything3_IsPropertyIndexed(_client, propertyId);

        /// <summary>
        /// 属性是否支持快速排序
        /// </summary>
        public bool IsPropertyFastSort(uint propertyId)
            => Everything3SDK.Everything3_IsPropertyFastSort(_client, propertyId);

        /// <summary>
        /// 属性是否右对齐
        /// </summary>
        public bool IsPropertyRightAligned(uint propertyId)
            => Everything3SDK.Everything3_IsPropertyRightAligned(_client, propertyId);

        /// <summary>
        /// 属性默认是否降序排序
        /// </summary>
        public bool IsPropertySortDescending(uint propertyId)
            => Everything3SDK.Everything3_IsPropertySortDescending(_client, propertyId);

        /// <summary>
        /// 获取属性默认列宽
        /// </summary>
        public uint GetPropertyDefaultWidth(uint propertyId)
            => Everything3SDK.Everything3_GetPropertyDefaultWidth(_client, propertyId);

        // ---- 日志(Journal) ----

        /// <summary>
        /// 获取日志信息
        /// </summary>
        public bool GetJournalInfo(out EveryThing3JournalInfo info)
            => Everything3SDK.Everything3_GetJournalInfo(_client, out info);

        /// <summary>
        /// 读取日志变更记录
        /// </summary>
        public bool ReadJournal(ulong journalId, ulong changeId, EveryThing3ReadJournal flags,
            EveryThing3ReadJournalCallbackW callback, object userData)
        {
            var gch = GCHandle.Alloc(userData);
            var callbackPtr = Marshal.GetFunctionPointerForDelegate(callback);
            try
            {
                return Everything3SDK.Everything3_ReadJournalW(_client, journalId, changeId, (uint)flags,
                    GCHandle.ToIntPtr(gch), callbackPtr);
            }
            finally
            {
                gch.Free();
            }
        }

        // ---- 结果变更检测 ----

        /// <summary>
        /// 检测结果集是否已变更
        /// </summary>
        public bool IsResultListChange() => Everything3SDK.Everything3_IsResultListChange(_client);

        /// <summary>
        /// 等待结果集变更(阻塞)
        /// </summary>
        public bool WaitForResultListChange() => Everything3SDK.Everything3_WaitForResultListChange(_client);

        /// <summary>
        /// 创建搜索状态对象
        /// </summary>
        public SearchState CreateSearchState() => new SearchState(this);

        /// <summary>
        /// 执行搜索并返回结果集
        /// </summary>
        public ResultList? Search(SearchState searchState)
        {
            var ptr = Everything3SDK.Everything3_Search(_client, searchState.Handle);
            return ptr != IntPtr.Zero ? new ResultList(ptr) : null;
        }

        /// <summary>
        /// 获取结果(不排序)
        /// </summary>
        public ResultList? GetResults(SearchState searchState)
        {
            var ptr = Everything3SDK.Everything3_GetResults(_client, searchState.Handle);
            return ptr != IntPtr.Zero ? new ResultList(ptr) : null;
        }

        /// <summary>
        /// 对已有结果进行排序
        /// </summary>
        public ResultList? Sort(SearchState searchState)
        {
            var ptr = Everything3SDK.Everything3_Sort(_client, searchState.Handle);
            return ptr != IntPtr.Zero ? new ResultList(ptr) : null;
        }

        #endregion Public Methods

        #region Internal Methods

        /// <summary>
        /// 辅助方法：调用原生函数获取字符串(自动调整缓冲区大小)
        /// </summary>
        internal static string? GetStringViaBuffer(Func<StringBuilder, IntPtr, IntPtr> func)
        {
            int size = 512;
            while (true)
            {
                var sb = new StringBuilder(size);
                var ret = func(sb, (IntPtr)sb.Capacity);
                if ((long)ret == 0)
                    return null;
                if ((long)ret <= sb.Capacity)
                    return sb.ToString(0, (int)ret);
                size = (int)ret;
            }
        }

        #endregion Internal Methods
    }

    /// <summary>
    /// 搜索状态封装(配置搜索条件)
    /// </summary>
    public class SearchState : IDisposable
    {
        #region Internal Constructors

        internal SearchState(Everything3Client client)
        {
            _client = client;
            _state = Everything3SDK.Everything3_CreateSearchState();
            if (_state == IntPtr.Zero)
                throw new InvalidOperationException("创建搜索状态失败。");
        }

        #endregion Internal Constructors

        #region Fields

        private readonly Everything3Client _client;
        private IntPtr _state;

        #endregion Fields

        #region Properties

        /// <summary>
        /// 是否区分大小写
        /// </summary>
        public bool MatchCase
        {
            set => Everything3SDK.Everything3_SetSearchMatchCase(_state, value);
            get => Everything3SDK.Everything3_GetSearchMatchCase(_state);
        }

        /// <summary>
        /// 是否区分变音符号
        /// </summary>
        public bool MatchDiacritics
        {
            set => Everything3SDK.Everything3_SetSearchMatchDiacritics(_state, value);
            get => Everything3SDK.Everything3_GetSearchMatchDiacritics(_state);
        }

        /// <summary>
        /// 是否全字匹配
        /// </summary>
        public bool MatchWholeWords
        {
            set => Everything3SDK.Everything3_SetSearchMatchWholeWords(_state, value);
            get => Everything3SDK.Everything3_GetSearchMatchWholeWords(_state);
        }

        /// <summary>
        /// 是否匹配路径(包括路径部分)
        /// </summary>
        public bool MatchPath
        {
            set => Everything3SDK.Everything3_SetSearchMatchPath(_state, value);
            get => Everything3SDK.Everything3_GetSearchMatchPath(_state);
        }

        /// <summary>
        /// 是否前缀匹配
        /// </summary>
        public bool MatchPrefix
        {
            set => Everything3SDK.Everything3_SetSearchMatchPrefix(_state, value);
            get => Everything3SDK.Everything3_GetSearchMatchPrefix(_state);
        }

        /// <summary>
        /// 是否后缀匹配
        /// </summary>
        public bool MatchSuffix
        {
            set => Everything3SDK.Everything3_SetSearchMatchSuffix(_state, value);
            get => Everything3SDK.Everything3_GetSearchMatchSuffix(_state);
        }

        /// <summary>
        /// 是否忽略标点符号
        /// </summary>
        public bool IgnorePunctuation
        {
            set => Everything3SDK.Everything3_SetSearchIgnorePunctuation(_state, value);
            get => Everything3SDK.Everything3_GetSearchIgnorePunctuation(_state);
        }

        /// <summary>
        /// 是否忽略空白字符
        /// </summary>
        public bool IgnoreWhitespace
        {
            set => Everything3SDK.Everything3_SetSearchWhitespace(_state, value);
            get => Everything3SDK.Everything3_GetSearchWhitespace(_state);
        }

        /// <summary>
        /// 是否使用正则表达式搜索
        /// </summary>
        public bool Regex
        {
            set => Everything3SDK.Everything3_SetSearchRegex(_state, value);
            get => Everything3SDK.Everything3_GetSearchRegex(_state);
        }

        /// <summary>
        /// 文件夹优先显示模式
        /// </summary>
        public EveryThing3SearchFoldersFirst FoldersFirst
        {
            set => Everything3SDK.Everything3_SetSearchFoldersFirst(_state, (uint)value);
            get => (EveryThing3SearchFoldersFirst)Everything3SDK.Everything3_GetSearchFoldersFirst(_state);
        }

        /// <summary>
        /// 是否请求计算总大小
        /// </summary>
        public bool RequestTotalSize
        {
            set => Everything3SDK.Everything3_SetSearchRequestTotalSize(_state, value);
            get => Everything3SDK.Everything3_GetSearchRequestTotalSize(_state);
        }

        /// <summary>
        /// 是否隐藏结果省略信息
        /// </summary>
        public bool HideResultOmissions
        {
            set => Everything3SDK.Everything3_SetSearchHideResultOmissions(_state, value);
            get => Everything3SDK.Everything3_GetSearchHideResultOmissions(_state);
        }

        /// <summary>
        /// 是否混合排序(文件夹和文件混合)
        /// </summary>
        public bool SortMix
        {
            set => Everything3SDK.Everything3_SetSearchSortMix(_state, value);
            get => Everything3SDK.Everything3_GetSearchSortMix(_state);
        }

        /// <summary>
        /// 搜索文本
        /// </summary>
        public string? SearchText
        {
            set => Everything3SDK.Everything3_SetSearchTextW(_state, value ?? string.Empty);
            get => Everything3Client.GetStringViaBuffer((sb, size) =>
                Everything3SDK.Everything3_GetSearchTextW(_state, sb, size));
        }

        /// <summary>
        /// 视口偏移(用于分页)
        /// </summary>
        public IntPtr ViewportOffset
        {
            set => Everything3SDK.Everything3_SetSearchViewportOffset(_state, value);
            get => Everything3SDK.Everything3_GetSearchViewportOffset(_state);
        }

        /// <summary>
        /// 视口大小(每页条数)
        /// </summary>
        public IntPtr ViewportCount
        {
            set => Everything3SDK.Everything3_SetSearchViewportCount(_state, value);
            get => Everything3SDK.Everything3_GetSearchViewportCount(_state);
        }

        internal IntPtr Handle => _state;

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// 释放搜索状态
        /// </summary>
        public void Dispose()
        {
            if (_state != IntPtr.Zero)
            {
                Everything3SDK.Everything3_DestroySearchState(_state);
                _state = IntPtr.Zero;
            }
        }

        /// <summary>
        /// 设置单级排序(会清除已有排序)
        /// </summary>
        public void SetSearch(uint propertyId, bool ascending)
            => Everything3SDK.Everything3_SetSearchSort(_state, propertyId, ascending);

        /// <summary>
        /// 添加多级排序
        /// </summary>
        public bool AddSort(uint propertyId, bool ascending)
            => Everything3SDK.Everything3_AddSearchSort(_state, propertyId, ascending);

        /// <summary>
        /// 清除所有排序
        /// </summary>
        public void ClearSorts()
            => Everything3SDK.Everything3_ClearSearchSorts(_state);

        /// <summary>
        /// 添加属性请求(原始值)
        /// </summary>
        public bool AddPropertyRequest(uint propertyId)
            => Everything3SDK.Everything3_AddSearchPropertyRequest(_state, propertyId);

        /// <summary>
        /// 添加属性请求(格式化值)
        /// </summary>
        public bool AddPropertyRequestFormatted(uint propertyId)
            => Everything3SDK.Everything3_AddSearchPropertyRequestFormatted(_state, propertyId);

        /// <summary>
        /// 添加属性请求(高亮值)
        /// </summary>
        public bool AddPropertyRequestHighlighted(uint propertyId)
            => Everything3SDK.Everything3_AddSearchPropertyRequestHighlighted(_state, propertyId);

        /// <summary>
        /// 清除所有属性请求
        /// </summary>
        public void ClearPropertyRequests()
            => Everything3SDK.Everything3_ClearSearchPropertyRequests(_state);

        /// <summary>
        /// 获取排序条件的数量
        /// </summary>
        public long GetSortCount() => (long)Everything3SDK.Everything3_GetSearchSortCount(_state);

        /// <summary>
        /// 获取指定排序的属性ID
        /// </summary>
        public uint GetSortPropertyId(long index) => Everything3SDK.Everything3_GetSearchSortPropertyId(_state, (IntPtr)index);

        /// <summary>
        /// 获取指定排序是否升序
        /// </summary>
        public bool GetSortAscending(long index) => Everything3SDK.Everything3_GetSearchSortAscending(_state, (IntPtr)index);

        /// <summary>
        /// 获取属性请求数量
        /// </summary>
        public long GetPropertyRequestCount() => (long)Everything3SDK.Everything3_GetSearchPropertyRequestCount(_state);

        /// <summary>
        /// 获取指定属性请求的属性ID
        /// </summary>
        public uint GetPropertyRequestPropertyId(long index) => Everything3SDK.Everything3_GetSearchPropertyRequestPropertyId(_state, (IntPtr)index);

        /// <summary>
        /// 获取指定属性请求是否高亮
        /// </summary>
        public bool GetPropertyRequestHighlight(long index) => Everything3SDK.Everything3_GetSearchPropertyRequestHighlight(_state, (IntPtr)index);

        /// <summary>
        /// 获取指定属性请求是否格式化
        /// </summary>
        public bool GetPropertyRequestFormat(long index) => Everything3SDK.Everything3_GetSearchPropertyRequestFormat(_state, (IntPtr)index);

        /// <summary>
        /// 执行搜索(使用当前搜索状态)
        /// </summary>
        public ResultList? Search() => _client.Search(this);

        /// <summary>
        /// 获取结果(不排序)
        /// </summary>
        public ResultList? GetResults() => _client.GetResults(this);

        /// <summary>
        /// 排序结果
        /// </summary>
        public ResultList? Sort() => _client.Sort(this);

        #endregion Public Methods
    }

    /// <summary>
    /// 搜索结果集封装
    /// </summary>
    public class ResultList : IDisposable
    {
        #region Internal Constructors

        internal ResultList(IntPtr list)
        {
            _list = list;
        }

        #endregion Internal Constructors

        #region Fields

        private IntPtr _list;

        #endregion Fields

        #region Properties

        /// <summary>
        /// 结果总数
        /// </summary>
        public long Count => (long)Everything3SDK.Everything3_GetResultListCount(_list);

        /// <summary>
        /// 文件夹数量
        /// </summary>
        public long FolderCount => (long)Everything3SDK.Everything3_GetResultListFolderCount(_list);

        /// <summary>
        /// 文件数量
        /// </summary>
        public long FileCount => (long)Everything3SDK.Everything3_GetResultListFileCount(_list);

        /// <summary>
        /// 总大小(字节)
        /// </summary>
        public ulong TotalSize => Everything3SDK.Everything3_GetResultListTotalSize(_list);

        /// <summary>
        /// 视口偏移
        /// </summary>
        public long ViewportOffset => (long)Everything3SDK.Everything3_GetResultListViewportOffset(_list);

        /// <summary>
        /// 视口条数
        /// </summary>
        public long ViewportCount => (long)Everything3SDK.Everything3_GetResultListViewportCount(_list);

        /// <summary>
        /// 排序条件数量
        /// </summary>
        public long SortCount => (long)Everything3SDK.Everything3_GetResultListSortCount(_list);

        /// <summary>
        /// 属性请求数量
        /// </summary>
        public long PropertyRequestCount => (long)Everything3SDK.Everything3_GetResultListPropertyRequestCount(_list);

        internal IntPtr Handle => _list;

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// 释放结果集
        /// </summary>
        public void Dispose()
        {
            if (_list != IntPtr.Zero)
            {
                Everything3SDK.Everything3_DestroyResultList(_list);
                _list = IntPtr.Zero;
            }
        }

        /// <summary>
        /// 获取指定排序的属性ID
        /// </summary>
        public uint GetSortPropertyId(long index) => Everything3SDK.Everything3_GetResultListSortPropertyId(_list, (IntPtr)index);

        /// <summary>
        /// 获取指定排序是否升序
        /// </summary>
        public bool GetSortAscending(long index) => Everything3SDK.Everything3_GetResultListSortAscending(_list, (IntPtr)index);

        /// <summary>
        /// 获取指定属性请求的属性ID
        /// </summary>
        public uint GetPropertyRequestPropertyId(long index) => Everything3SDK.Everything3_GetResultListPropertyRequestPropertyId(_list, (IntPtr)index);

        /// <summary>
        /// 获取指定属性请求的值类型
        /// </summary>
        public uint GetPropertyRequestValueType(long index) => Everything3SDK.Everything3_GetResultListPropertyRequestValueType(_list, (IntPtr)index);

        /// <summary>
        /// 获取指定属性请求的数据偏移
        /// </summary>
        public long GetPropertyRequestOffset(long index) => (long)Everything3SDK.Everything3_GetResultListPropertyRequestOffset(_list, (IntPtr)index);

        /// <summary>
        /// 判断指定结果是否为文件夹
        /// </summary>
        public bool IsFolder(long resultIndex) => Everything3SDK.Everything3_IsFolderResult(_list, (IntPtr)resultIndex);

        /// <summary>
        /// 判断指定结果是否为根目录
        /// </summary>
        public bool IsRoot(long resultIndex) => Everything3SDK.Everything3_IsRootResult(_list, (IntPtr)resultIndex);

        /// <summary>
        /// 获取结果文件名
        /// </summary>
        public string? GetName(long resultIndex)
            => Everything3Client.GetStringViaBuffer((sb, size) =>
                Everything3SDK.Everything3_GetResultNameW(_list, (IntPtr)resultIndex, sb, size));

        /// <summary>
        /// 获取结果路径(不含文件名)
        /// </summary>
        public string? GetPath(long resultIndex)
            => Everything3Client.GetStringViaBuffer((sb, size) =>
                Everything3SDK.Everything3_GetResultPathW(_list, (IntPtr)resultIndex, sb, size));

        /// <summary>
        /// 获取结果完整路径(含文件名)
        /// </summary>
        public string? GetFullPathName(long resultIndex)
            => Everything3Client.GetStringViaBuffer((sb, size) =>
                Everything3SDK.Everything3_GetResultFullPathNameW(_list, (IntPtr)resultIndex, sb, size));

        /// <summary>
        /// 获取文件大小(字节)
        /// </summary>
        public ulong GetSize(long resultIndex)
            => Everything3SDK.Everything3_GetResultSize(_list, (IntPtr)resultIndex);

        /// <summary>
        /// 获取扩展名
        /// </summary>
        public string? GetExtension(long resultIndex)
            => Everything3Client.GetStringViaBuffer((sb, size) =>
                Everything3SDK.Everything3_GetResultExtensionW(_list, (IntPtr)resultIndex, sb, size));

        /// <summary>
        /// 获取文件类型描述
        /// </summary>
        public string? GetTypeName(long resultIndex)
            => Everything3Client.GetStringViaBuffer((sb, size) =>
                Everything3SDK.Everything3_GetResultTypeW(_list, (IntPtr)resultIndex, sb, size));

        /// <summary>
        /// 获取文件列表文件名
        /// </summary>
        public string? GetFilelistFilename(long resultIndex)
            => Everything3Client.GetStringViaBuffer((sb, size) =>
                Everything3SDK.Everything3_GetResultFilelistFilenameW(_list, (IntPtr)resultIndex, sb, size));

        /// <summary>
        /// 获取修改时间(FILETIME)
        /// </summary>
        public ulong GetDateModified(long resultIndex)
            => Everything3SDK.Everything3_GetResultDateModified(_list, (IntPtr)resultIndex);

        /// <summary>
        /// 获取创建时间(FILETIME)
        /// </summary>
        public ulong GetDateCreated(long resultIndex)
            => Everything3SDK.Everything3_GetResultDateCreated(_list, (IntPtr)resultIndex);

        /// <summary>
        /// 获取访问时间(FILETIME)
        /// </summary>
        public ulong GetDateAccessed(long resultIndex)
            => Everything3SDK.Everything3_GetResultDateAccessed(_list, (IntPtr)resultIndex);

        /// <summary>
        /// 获取文件属性标志
        /// </summary>
        public uint GetAttributes(long resultIndex)
            => Everything3SDK.Everything3_GetResultAttributes(_list, (IntPtr)resultIndex);

        /// <summary>
        /// 获取最近变更时间(FILETIME)
        /// </summary>
        public ulong GetDateRecentlyChanged(long resultIndex)
            => Everything3SDK.Everything3_GetResultDateRecentlyChanged(_list, (IntPtr)resultIndex);

        /// <summary>
        /// 获取运行次数
        /// </summary>
        public uint GetRunCount(long resultIndex)
            => Everything3SDK.Everything3_GetResultRunCount(_list, (IntPtr)resultIndex);

        /// <summary>
        /// 获取最后运行时间(FILETIME)
        /// </summary>
        public ulong GetDateRun(long resultIndex)
            => Everything3SDK.Everything3_GetResultDateRun(_list, (IntPtr)resultIndex);

        /// <summary>
        /// 获取结果的指定属性文本
        /// </summary>
        public string? GetPropertyText(long resultIndex, uint propertyId)
            => Everything3Client.GetStringViaBuffer((sb, size) =>
                Everything3SDK.Everything3_GetResultPropertyTextW(_list, (IntPtr)resultIndex, propertyId, sb, size));

        /// <summary>
        /// 获取结果的指定属性文本(格式化)
        /// </summary>
        public string? GetPropertyTextFormatted(long resultIndex, uint propertyId)
            => Everything3Client.GetStringViaBuffer((sb, size) =>
                Everything3SDK.Everything3_GetResultPropertyTextFormattedW(_list, (IntPtr)resultIndex, propertyId, sb, size));

        /// <summary>
        /// 获取结果的指定属性文本(高亮)
        /// </summary>
        public string? GetPropertyTextHighlighted(long resultIndex, uint propertyId)
            => Everything3Client.GetStringViaBuffer((sb, size) =>
                Everything3SDK.Everything3_GetResultPropertyTextHighlightedW(_list, (IntPtr)resultIndex, propertyId, sb, size));

        /// <summary>
        /// 获取BYTE类型的属性值
        /// </summary>
        public byte GetPropertyByte(long resultIndex, uint propertyId)
            => Everything3SDK.Everything3_GetResultPropertyBYTE(_list, (IntPtr)resultIndex, propertyId);

        /// <summary>
        /// 获取WORD(ushort)类型的属性值
        /// </summary>
        public ushort GetPropertyWord(long resultIndex, uint propertyId)
            => Everything3SDK.Everything3_GetResultPropertyWORD(_list, (IntPtr)resultIndex, propertyId);

        /// <summary>
        /// 获取DWORD(uint)类型的属性值
        /// </summary>
        public uint GetPropertyDword(long resultIndex, uint propertyId)
            => Everything3SDK.Everything3_GetResultPropertyDWORD(_list, (IntPtr)resultIndex, propertyId);

        /// <summary>
        /// 获取UINT64(ulong)类型的属性值
        /// </summary>
        public ulong GetPropertyUInt64(long resultIndex, uint propertyId)
            => Everything3SDK.Everything3_GetResultPropertyUINT64(_list, (IntPtr)resultIndex, propertyId);

        /// <summary>
        /// 获取UINT128类型的属性值
        /// </summary>
        public bool GetPropertyUInt128(long resultIndex, uint propertyId, out EveryThing3UInt128 value)
            => Everything3SDK.Everything3_GetResultPropertyUINT128(_list, (IntPtr)resultIndex, propertyId, out value);

        /// <summary>
        /// 获取尺寸(宽/高)类型的属性值
        /// </summary>
        public bool GetPropertyDimensions(long resultIndex, uint propertyId, out EveryThing3Dimensions dimensions)
            => Everything3SDK.Everything3_GetResultPropertyDIMENSIONS(_list, (IntPtr)resultIndex, propertyId, out dimensions);

        /// <summary>
        /// 获取SIZE_T(long)类型的属性值
        /// </summary>
        public long GetPropertySizeT(long resultIndex, uint propertyId)
            => (long)Everything3SDK.Everything3_GetResultPropertySIZE_T(_list, (IntPtr)resultIndex, propertyId);

        /// <summary>
        /// 获取INT32(int)类型的属性值
        /// </summary>
        public int GetPropertyInt32(long resultIndex, uint propertyId)
            => Everything3SDK.Everything3_GetResultPropertyINT32(_list, (IntPtr)resultIndex, propertyId);

        /// <summary>
        /// 获取二进制(BLOB)类型的属性值
        /// </summary>
        public bool GetPropertyBlob(long resultIndex, uint propertyId, byte[] buffer, ref long bufferSize)
        {
            var size = (IntPtr)bufferSize;
            var result = Everything3SDK.Everything3_GetResultPropertyBlob(_list, (IntPtr)resultIndex, propertyId, buffer, ref size);
            bufferSize = (long)size;
            return result;
        }

        #endregion Public Methods
    }

    #endregion 托管封装类

    */
}