using UnityEngine;

namespace WhiteZhi
{

    
    public static partial class Utils
    {
        /// <summary>
        /// 随机数相关函数
        /// </summary>
        public static partial class Random
        {
            private static System.Random rd = new System.Random();
            
            /// <summary>
            /// 获取一个随机数[start,end)
            /// </summary>
            /// <param name="start">最小值</param>
            /// <param name="end">最大值（不包括）</param>
            /// <returns>[start,end)中的一个随机整数</returns>
            public static int RandomInt(int start, int end)
            {
                int re = rd.Next(start, end); 
                return re;
            }

            /// <summary>
            /// 随机生成一个颜色
            /// </summary>
            /// <returns></returns>
            public static Color RandomColor()
            {
                //随机生成颜色
                return new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
            }
        }
    }
}