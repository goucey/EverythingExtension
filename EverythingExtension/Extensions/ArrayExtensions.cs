/*************************************************************************************
    *
    * 描    述:  暂无
    *
    * 版    本：  V1.0
    * 创 建 者：  Goucey（高希）
    * ======================================
    *
*************************************************************************************/
using System.Linq;

namespace System
{
    public static partial class ArrayExtensions
    {
        #region Public Methods

        /// <summary>
        /// 合并两个数组
        /// </summary>
        /// <param name="source"> 源数据 </param>
        /// <param name="target"> 目标 </param>
        /// <returns> </returns>
        public static T[] Merge<T>(this T[] source, T[] target)
        {
            if (target == null || target.Length < 1)
                return source;

            if (source == null)
                source = new T[0];

            T[] tempBuffer = new T[source.Length + target.Length];
            source.CopyTo(tempBuffer, 0);
            target.CopyTo(tempBuffer, source.Length);
            return tempBuffer;
        }

        /// <summary>
        /// 合并多个数组
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="source"> 源数据 </param>
        /// <param name="args">   待合并数组 </param>
        /// <returns> </returns>
        public static T[] Merges<T>(this T[] source, params T[][] args)
        {
            if (!args.Any())
                return source;

            T[] temp = source;

            foreach (var item in args)
                temp = temp.Merge(item);

            return source;
        }

        #endregion Public Methods
    }
}