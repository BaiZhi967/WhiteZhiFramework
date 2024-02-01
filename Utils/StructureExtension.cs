using System.Collections.Generic;

namespace WhiteZhi
{
    /// <summary>
    /// 数据结构扩展方法
    /// </summary>
    public static class StructureExtension
    {
        #region 字典相关扩展


        /// <summary>
        /// 尝试从字典中获取值，如果不存在则添加默认值
        /// </summary>
        /// <param name="dictionary">目标字典</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="def">默认值</param>
        /// <typeparam name="T">键的类型</typeparam>
        /// <typeparam name="K">值的类型</typeparam>
        /// <returns></returns>
        public static bool TryGetOrSetValue<T, K>(this Dictionary<T, K> dictionary,T key,out K val, K def)
        {
            if (!dictionary.TryGetValue(key, out K value))
            {
                value = def;
                dictionary.Add(key,value);
                val = def;
                return false;
            }

            val = value;
            return true;
        }

        /// <summary>
        /// 从字典中获取值
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <returns></returns>
        public static K GetValueInDictionary<T, K>(this Dictionary<T, K> dictionary, T key)
        {
            if (dictionary is null)
            {
                CommonUtils.EditorLogError("[GetValueInDictionary] 字典为空");
                return default(K);
            }

            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            CommonUtils.EditorLogError($"[GetValueInDictionary]字典中不存在键：{key}");
            return default(K);
        }

        /// <summary>
        /// 尝试从字典中移除值
        /// </summary>
        /// <param name="dictionary">目标字典</param>
        /// <param name="key">键</param>
        /// <typeparam name="T">键的类型</typeparam>
        /// <typeparam name="K">值的类型</typeparam>
        /// <returns>是否成功移除</returns>
        public static bool TryRemoveKey<T, K>(this Dictionary<T, K> dictionary, T key)
        {
            if (dictionary == null)
            {
                return false;
            }

            if (dictionary.ContainsKey(key))
            {
                dictionary.Remove(key);
                return true;
            }

            return false;
        }

        
        /// <summary>
        /// 在字典中尝试添加键值对，如果键已经存在则更新值
        /// </summary>
        /// <param name="dictionary">目标字典</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <typeparam name="T">键的类型</typeparam>
        /// <typeparam name="K">值的类型</typeparam>
        /// <returns>是否插入或更新成功</returns>
        public static bool InsertOrUpdateKeyValue<T, K>(this Dictionary<T, K> dictionary, T key, K val)
        {
            if (dictionary == null)
            {
                return false;
            }

            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = val;
                return true;
            }
            else
            {
                return dictionary.TryAdd(key,val);
            }
        }

        public static void PrintAllKeyValues<T, K>(this Dictionary<T, K> dictionary)
        {
            if (dictionary == null)
            {
                CommonUtils.EditorLogWarning("字典为null!");
                return;
            }

            foreach (var kv in dictionary)
            {
                CommonUtils.EditorLogNormal($"Key = {kv.Key}, Value = {kv.Value}");
            }
        }
        
        #endregion

        #region List 相关扩展

        /// <summary>
        /// 打印所有元素
        /// </summary>
        /// <param name="list"></param>
        /// <param name="showIndex">是否展示元素下标</param>
        /// <typeparam name="T"></typeparam>
        public static void PrintAllElements<T>(this List<T> list,bool showIndex = true)
        {
            if (list is null)
            {
                CommonUtils.EditorLogWarning("列表为null!");
                return;
            }

            int idx = 0;
            
            foreach (var e in list)
            {
                if (showIndex)
                {
                    CommonUtils.EditorLogNormal($"List[{idx}] = {e}");
                }
                else
                {
                    CommonUtils.EditorLogNormal(e);
                }

                idx++;
            }
        }

        #endregion
    }
}