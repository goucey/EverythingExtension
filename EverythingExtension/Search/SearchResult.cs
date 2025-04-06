// Copyright (c) Microsoft Corporation The Microsoft Corporation licenses this file to you under the
// MIT license. See the LICENSE file in the project root for more information.

using EverythingExtension.Properties;
using EverythingExtension.SDK;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

using Windows.Storage.Streams;

namespace EverythingExtension.Search
{
    public class SearchResult
    {
        #region Fields

        private readonly string[] textExtension = [".txt", ".ini", ".log", ".md"];

        //ani,bmp,gif,ico,jpe,jpeg,jpg,pcx,png,psd,tga,tif,tiff,wmf,wbmp,icl,jp2,mpng,raw,nef,wdp,hdp
        private readonly string[] imageExtension = [".ani", ".bmp", ".gif", ".ico", ".jpe", ".jpeg", ".jpg", ".pcx", ".png", ".tga", ".tif", ".tiff", ".wmf", ".wbmp", ".icl", ".jp2", ".mpng", ".raw", ".nef", ".wdp", ".hdp", ".svg"];

        #endregion Fields

        #region Public Constructors

        public SearchResult(string fileName, string fullPath, ResultType type, int serialNumber, string? extension = null)
        {
            FileName = fileName;
            FullPath = fullPath;
            Type = type;
            Extension = extension;
            SerialNumber = serialNumber;
        }

        public SearchResult(string fileName, string fullPath, int serialNumber)
        {
            FileName = fileName;
            FullPath = fullPath;
            SerialNumber = serialNumber;
            Type = IsDirectory() ? ResultType.Folder : ResultType.File;
            if (Type == ResultType.File && !string.IsNullOrWhiteSpace(fileName))
                Extension = Path.GetExtension(fileName);
        }

        public SearchResult(string fullPath, int serialNumber)
        {
            FileName = Path.GetFileName(fullPath);
            FullPath = fullPath;
            SerialNumber = serialNumber;
            Type = IsDirectory() ? ResultType.Folder : ResultType.File;
            if (Type == ResultType.File && !string.IsNullOrWhiteSpace(FileName))
                Extension = Path.GetExtension(FileName);
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
        /// 扩展名
        /// </summary>
        public string? Extension { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int SerialNumber { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long? Size { get; set; }

        /// <summary>
        /// 文件或文件夹父级目录
        /// </summary>
        public string? ParentPath { get; set; }

        public bool IsPreviewable => IsTextPreviewable || IsImagePreviewable;

        /// <summary>
        /// 是否是Markdown文件
        /// </summary>
        public bool IsMarkdown => Type == ResultType.File && !string.IsNullOrWhiteSpace(Extension) && Extension.Equals(".md", StringComparison.OrdinalIgnoreCase);

        public Action? Deleted { get; set; }

        /// <summary>
        /// 文本是否可预览
        /// </summary>
        protected bool IsTextPreviewable
            => Type == ResultType.File && !string.IsNullOrWhiteSpace(Extension) && textExtension.Any(i => i.Equals(Extension, StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// 图片是否可预览
        /// </summary>
        protected bool IsImagePreviewable
            => Type == ResultType.File && !string.IsNullOrWhiteSpace(Extension) && imageExtension.Any(i => i.Equals(Extension, StringComparison.OrdinalIgnoreCase));

        #endregion Properties

        #region Internal Methods

        internal string? GetContent()
        {
            if (!IsTextPreviewable && !IsImagePreviewable)
                return default;

            string content;
            if (IsImagePreviewable)
            {
                content = $"![{FileName}]({FullPath})";
            }
            else
            {
                content = File.ReadAllText(FullPath, Encoding.UTF8);
            }

            if (IsMarkdown)
                return content;
            else
                return ContentFormat(content);
        }

        internal bool IsDirectory()
        {
            if (!Path.Exists(FullPath))
            {
                return false;
            }

            var attr = File.GetAttributes(FullPath);

            // detect whether it is a directory or file
            return (attr & FileAttributes.Directory) == FileAttributes.Directory;
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
            if (Size == null)
                return default;

            if (Size < 1024)
                return $"{Size} B";
            else if (Size < 1024 * 1024)
            {
                return $"{(Size / 1024F):F1} KB";
            }
            else if (Size < 1024 * 1024 * 1024)
            {
                return $"{(Size / (1024F * 1024F)):F1} MB";
            }
            else if (Size < (1024L * 1024L * 1024L * 1024L))
            {
                return $"{(Size / (1024F * 1024F * 1024F)):F1} GB";
            }
            else
                return default;
        }

        #endregion Internal Methods

        #region Private Methods

        private static string? ContentFormat(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return default;

            return string.Join("\r\n", content.Split(["\r\n", "\n"], StringSplitOptions.TrimEntries));
        }

        #endregion Private Methods
    }
}