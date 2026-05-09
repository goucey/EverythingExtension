/*************************************************************************************
    *
    * 描    述:  暂无
    *
    * 版    本：  V1.0
    * 创 建 者：  Goucey（高希）
    * ======================================
    *
*************************************************************************************/
using System.Drawing;
using System.IO;
using System.Text;

namespace System
{
    public static partial class ByteExtensions
    {
        #region Public Methods

        /// <summary>
        /// 字节数组转成字符串
        /// </summary>
        /// <param name="bytes">    </param>
        /// <param name="encoding"> </param>
        /// <returns> </returns>
        public static string GetString(this byte[] bytes, string encoding = "utf-8") => Encoding.GetEncoding(encoding).GetString(bytes);

        /// <summary>
        /// 导出base64
        /// </summary>
        /// <param name="bytes"> </param>
        /// <returns> </returns>
        public static string ToBase64(this byte[] bytes) => Convert.ToBase64String(bytes);

        /// <summary>
        /// 字节数组转图片
        /// </summary>
        /// <param name="bytes"> </param>
        /// <returns> </returns>
        public static Bitmap ToBitmap(this byte[] bytes)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream(bytes))
                    return new Bitmap(stream);
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// 字节转16进制字符串
        /// </summary>
        /// <param name="bytes"> 字节数组 </param>
        /// <returns> </returns>
        public static string ToHexString(this byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException("bytes 不能为Null");

            StringBuilder sb = new StringBuilder();
            //逐个字符变为16进制字节数据
            foreach (byte letter in bytes)
            {
                string hexOutput = letter.ToString("X2");
                sb.AppendFormat(hexOutput);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 字节数组转图片
        /// </summary>
        /// <param name="bytes"> </param>
        /// <returns> </returns>
        public static Image ToImage(this byte[] bytes)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream(bytes))
                    return Image.FromStream(stream);
            }
            catch
            {
                return default;
            }
        }

        #endregion Public Methods
    }
}