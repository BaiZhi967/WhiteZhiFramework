using System;
using System.Collections.Generic;

namespace WhiteZhi
{
    /// <summary>
    /// 事件处理容器
    /// </summary>
    public class EventHandler
    {
        #region 废弃代码

        /*/// <summary>
        /// 这是一个私有静态字段，用于存储全局的 EventHandler 实例。
        ///通过私有构造函数，确保只有一个全局实例。
        /// </summary>
        private static readonly EventHandler mGlobalEventHandler = new EventHandler();
        
        /// <summary>
        /// 获取指定类型的事件实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T> () where T : IWZEvent => mGlobalEventHandler.GetEvent<T>();

        public static void Register<T>() where T : IWZEvent, new() => mGlobalEventHandler.AddEvent<T>();*/

        #endregion

        public int Uid;
        
        private readonly Dictionary<Type, IWZEvent> mTypeEvents = new Dictionary<Type, IWZEvent>();

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public bool AddEvent<T>() where T : IWZEvent, new()
        {
            return mTypeEvents.TryAdd(typeof(T), new T());
        }

        /// <summary>
        /// 获取指定类型的事件实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetEvent<T> () where T : IWZEvent
        {
            return mTypeEvents.TryGetValue(typeof(T), out var e) ? (T)e : default;
        }
        public T GetOrAddEvent<T>() where T : IWZEvent, new()
        {
            var eType = typeof(T);
            if (mTypeEvents.TryGetValue(eType, out var e))
            {
                return (T)e;
            }

            var t = new T();
            mTypeEvents.Add(eType, t);
            return t;
        }
        
    }
}