using System;
using System.Collections.Generic;

namespace WhiteZhi
{
    public static partial class Utils
    {
        /// <summary>
        /// 随机数相关函数
        /// </summary>
        public static class Algorithm
        {
            /// <summary>
            /// 获得传入元素某个符合条件的所有对象
            /// </summary>
            public static T[] FindAll<T>(IList<T> array, Predicate<T> handler)
            {
                var dstArray = new T[array.Count];
                int idx = 0;
                for (int i = 0; i < array.Count; i++)
                {
                    if (handler(array[i]))
                    {
                        dstArray[idx] = array[i];
                        idx++;
                    }
                }
                Array.Resize(ref dstArray, idx);
                return dstArray;
            }
        }
    }
}