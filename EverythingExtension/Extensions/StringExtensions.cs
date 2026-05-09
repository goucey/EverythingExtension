/*************************************************************************************
    *
    * 描    述:  暂无
    *
    * 版    本：  V1.0
    * 创 建 者：  Goucey（高希）
    * ======================================
    *
*************************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System
{
    public static partial class StringExtensions
    {
        #region Public Methods

        /// <summary>
        /// Hex 转 Color
        /// </summary>
        /// <param name="str"> </param>
        /// <returns> </returns>
        public static Color HexToColor(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return Color.Empty;

            if (!str.StartsWith("#", StringComparison.OrdinalIgnoreCase) || str.Length != 4 && str.Length != 5 && str.Length != 7 && str.Length != 9)
                throw new ArgumentException("颜色格式不正确，应为：#000、#0000、#000000、#00000000", nameof(str));

            string temp = str[1..];

            return temp.Length switch
            {
                3 => Color.FromArgb(Convert.ToInt32($"0{temp[0]}", 16), Convert.ToInt32($"0{temp[1]}", 16), Convert.ToInt32($"0{temp[2]}", 16)),
                4 => Color.FromArgb(Convert.ToInt32($"0{temp[0]}", 16), Convert.ToInt32($"0{temp[1]}", 16), Convert.ToInt32($"0{temp[2]}", 16), Convert.ToInt32($"0{temp[3]}", 16)),
                6 => Color.FromArgb(Convert.ToInt32($"{temp[0]}{temp[1]}", 16), Convert.ToInt32($"{temp[2]}{temp[3]}", 16), Convert.ToInt32($"{temp[4]}{temp[5]}", 16)),
                _ => Color.FromArgb(Convert.ToInt32($"{temp[0]}{temp[1]}", 16), Convert.ToInt32($"{temp[2]}{temp[3]}", 16), Convert.ToInt32($"{temp[4]}{temp[5]}", 16), Convert.ToInt32($"{temp[6]}{temp[7]}", 16)),
            };
        }

        /// <summary>
        /// 字符串转Base64
        /// </summary>
        /// <param name="str">      </param>
        /// <param name="encoding"> </param>
        /// <returns> </returns>
        public static string ToBase64(this string str, string encoding = "utf-8")
        {
            byte[] buffer = Encoding.GetEncoding(encoding).GetBytes(str);
            return buffer.ToBase64();
        }

        /// <summary>
        /// base64 字符串转 byte数组
        /// </summary>
        /// <param name="str"> </param>
        /// <returns> </returns>
        public static byte[]? FromBase64(this string str)
        {
            try
            {
                return Convert.FromBase64String(str);
            }
            catch (FormatException)
            {
                //_logger.LogError(ex, "Base64字符串解密异常");
                return default;
            }
        }

        /// <summary>
        /// 判断字符串是 null 或者 空白
        /// </summary>
        /// <param name="str"> </param>
        /// <returns> </returns>
        public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);

        /// <summary>
        /// 字符串转成指定编码的字节数组
        /// </summary>
        /// <param name="str">      </param>
        /// <param name="encoding"> </param>
        /// <returns> </returns>
        public static byte[]? ToBytes(this string str, string encoding = "utf-8")
        {
            if (str.IsNullOrWhiteSpace())
                return default;

            return Encoding.GetEncoding(encoding)?.GetBytes(str);
        }

        /// <summary>
        /// 字符串分割成数组
        /// </summary>
        /// <param name="str">       </param>
        /// <param name="separator"> </param>
        /// <returns> </returns>
        public static string[]? ToArray(this string str, params string[] separator) => str.Split(separator, StringSplitOptions.RemoveEmptyEntries);

        #endregion Public Methods
    }
}