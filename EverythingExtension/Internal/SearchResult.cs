// Copyright (c) Microsoft Corporation The Microsoft Corporation licenses this file to you under the
// MIT license. See the LICENSE file in the project root for more information.

using EverythingExtension.Properties;
using EverythingExtension.SDK;

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;

using UtfUnknown;

using Windows.Data.Html;

namespace EverythingExtension.Internal
{
    public class SearchResult
    {
        #region Fields

        private readonly string[] _textExtension = [ ".txt", ".md", ".markdown", ".ini", ".cfg", ".conf", ".config",
    ".json", ".xml", ".yaml", ".yml", ".toml", ".properties", ".env",
    ".cs", ".cshtml", ".js", ".ts", ".jsx", ".tsx", ".html", ".htm", ".css", ".scss", ".less",
    ".php", ".py", ".java", ".go", ".rb", ".lua", ".sh", ".bat", ".cmd", ".ps1", ".vbs",
    ".sql", ".log", ".csv", ".tsv", ".diff", ".patch",
    ".rtf", ".nfo", ".srt", ".ass", ".lrc",".cpp"];

        //ani,bmp,gif,ico,jpe,jpeg,jpg,pcx,png,psd,tga,tif,tiff,wmf,wbmp,icl,jp2,mpng,raw,nef,wdp,hdp
        private readonly string[] _imageExtension = [".ani", ".bmp", ".gif", ".ico", ".jpe", ".jpeg", ".jpg", ".pcx", ".png", ".tga", ".tif", ".tiff", ".wmf", ".wbmp", ".icl", ".jp2", ".mpng", ".raw", ".nef", ".wdp", ".hdp", ".svg"];

        #endregion Fields

        #region Public Constructors

        public SearchResult(string fileName, string fullPath, ResultType type, string? extension = null)
        {
            FileName = fileName;
            FullPath = fullPath;
            Type = type;
            Extension = extension;
        }

        public SearchResult(string fileName, string fullPath)
        {
            FileName = fileName;
            FullPath = fullPath;
            Type = IsDirectory() ? ResultType.Folder : ResultType.File;
            if (Type == ResultType.File && !string.IsNullOrWhiteSpace(fileName))
            {
                Extension = Path.GetExtension(fileName)[1..];
                Size = new FileInfo(fullPath).Length;
                ParentPath = Path.GetDirectoryName(fullPath);
            }
        }

        public SearchResult(string fullPath) : this(Path.GetFileName(fullPath), fullPath)
        {
        }

        #endregion Public Constructors

        #region Properties

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 全路径
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public ResultType Type { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long? Size { get; init; }

        /// <summary>
        /// 文件或文件夹父级目录
        /// </summary>
        public string? ParentPath { get; set; }

        public bool IsPreview => IsTextPreview || IsImagePreview;

        public Action? Deleted { get; set; }

        /// <summary>
        /// 文本是否可预览
        /// </summary>
        public bool IsTextPreview
            => Type == ResultType.File && !string.IsNullOrWhiteSpace(Extension) && _textExtension.Any(i => i.EndsWith(Extension, StringComparison.OrdinalIgnoreCase)) && Detection!.Detected != null;

        /// <summary>
        /// 扩展名
        /// </summary>
        public string? Extension { get; set; }

        internal DetectionResult? Detection => CharsetDetector.DetectFromFile(FullPath);

        /// <summary>
        /// 图片是否可预览
        /// </summary>
        private bool IsImagePreview
            => Type == ResultType.File && !string.IsNullOrWhiteSpace(Extension) && _imageExtension.Any(i => i.EndsWith(Extension, StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// 是否是Markdown文件
        /// </summary>
        private bool IsMarkdown => Type == ResultType.File && !string.IsNullOrWhiteSpace(Extension) && ".md".EndsWith(Extension, StringComparison.OrdinalIgnoreCase);

        #endregion Properties

        #region Public Methods

        public bool IsDirectory()
        {
            if (!Path.Exists(FullPath))
            {
                return false;
            }

            var attr = File.GetAttributes(FullPath);

            // detect whether it is a directory or file
            return (attr & FileAttributes.Directory) == FileAttributes.Directory;
        }

        #endregion Public Methods

        #region Internal Methods

        internal string? GetContent()
        {
            if (!IsTextPreview && !IsImagePreview)
                return null;

            var content = IsImagePreview ? $"![{Guid.NewGuid()}]({HttpUtility.UrlEncode(FullPath)})" : ReadFileContent(FullPath);

            return IsMarkdown ? content : ContentFormat(content);
        }

        /// <summary>
        /// 获取删除命令显示的名称
        /// </summary>
        /// <returns> </returns>
        internal string GetDeleteCommandName()
            => Type == ResultType.File ? Resources.everything_delete_file : Resources.everything_delete_folder;

        /// <summary>
        /// 获取文件或文件所在目录路径
        /// </summary>
        /// <returns> </returns>
        internal string? GetDirectoryPath()
            => Type == ResultType.File ? Path.GetDirectoryName(FullPath) : FullPath;

        internal string? GetFileSizeDisplay()
        {
            return Size switch
            {
                null => null,
                < 1024 => $"{Size} B",
                < 1024 * 1024 => $"{Size / 1024F:F1} KB",
                < 1024 * 1024 * 1024 => $"{Size / (1024F * 1024F):F1} MB",
                < 1024L * 1024L * 1024L * 1024L => $"{Size / (1024F * 1024F * 1024F):F1} GB",
                _ => null
            };
        }

        #endregion Internal Methods

        #region Private Methods

        private static string? ContentFormat(string content)
        {
            return string.IsNullOrWhiteSpace(content) ? null : string.Join("\r\n", content.Split(["\r\n", "\n"], StringSplitOptions.TrimEntries));
        }

        private string ReadFileContent(string filePath)
        {
            // 第二个参数 true：自动检测BOM编码（默认就是true，可省略）

            if (Detection == null)
            {
                return File.ReadAllText(filePath);
            }
            Encoding encoding = Detection.Detected?.Encoding ?? Encoding.Unicode;
            return File.ReadAllText(filePath, encoding);
        }

        #endregion Private Methods
    }
}