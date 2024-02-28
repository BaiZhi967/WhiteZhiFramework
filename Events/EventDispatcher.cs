using System.Collections.Generic;

namespace WhiteZhi
{
    /// <summary>
    /// 事件调度中心
    /// </summary>
    public class EventDispatcher : GlobalSingleton<EventDispatcher>
    {
        /// <summary>
        /// 存放所有的事件处理器
        /// </summary>
        private static Dictionary<int, EventHandler> mHandlers = new Dictionary<int, EventHandler>();
        
        #region EventHandler 事件处理程序标识符

        public const int EH_COMMON = 0; //命令
        public const int EH_PLAYER = 1; //玩家
        public const int EH_ITEM = 2;   //物品
        public const int EH_COMBAT = 3; //战斗
        public const int EH_UI = 4;     //UI
        public const int EH_ENV = 5;    //环境
        public const int EH_FX = 6;     //特效
        public const int EH_T = 7;      //占用
        public const int EH_X = 8;      //占用
        public const int EH_Y = 9;      //占用
        
        #endregion

        
        /// <summary>
        /// 注册事件处理器
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool RegisterHandler(EventHandler handler, int id)
        {
            if (mHandlers.ContainsKey(id))
            {
                return false;
            }

            handler.Uid = id;
            return mHandlers.InsertOrUpdateKeyValue(id, handler);
        }
        
        /// <summary>
        /// 移除事件处理器
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static EventHandler UnRegisterHandler(EventHandler handler)
        {
            if (handler is not null)
            {
                return UnRegisterHandler(handler.Uid);
            }
            return null;
        }
        
        /// <summary>
        /// 移除事件处理器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static EventHandler UnRegisterHandler(int id)
        {
            if (mHandlers.TryGetValue(id,out var reHandler))
            {
                mHandlers.Remove(id);
                return reHandler;
            }

            return null;
        }

        /// <summary>
        /// 添加事件到制定容器
        /// </summary>
        /// <param name="handler"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool AddEvent<T>(EventHandler handler) where T: IWZEvent, new()
        {
            if (handler is null)
            {
                CommonUtils.EditorLogWarning("[EventDispatcher]添加事件监听 --> EventHandler 为 null");
                return false;
            }
            return handler.AddEvent<T>();
        }
        
        /// <summary>
        /// 添加事件到制定容器
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool AddEvent<T>(int id) where T : IWZEvent, new()
        {
            if (mHandlers.TryGetValue(id, out var handler))
            {
                return handler.AddEvent<T>();
            }
            CommonUtils.EditorLogWarning($"[EventDispatcher]添加事件监听 --> 不存在id为[{id}]的EventHandler");
            return false;
        }

        /// <summary>
        /// 从指定容器获取指定类型的事件实例
        /// </summary>
        /// <param name="handler"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetEvent<T>(EventHandler handler) where T : IWZEvent
        {
            if (handler is null)
            {
                CommonUtils.EditorLogWarning("[EventDispatcher]添加事件监听 --> EventHandler 为 null");
                return default;
            }
            return handler.GetEvent<T>();
        }
        
        /// <summary>
        /// 从指定容器获取指定类型的事件实例
        /// </summary>
        /// <param name="id"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetEvent<T>(int id) where T : IWZEvent
        {
            if (mHandlers.TryGetValue(id, out var handler))
            {
                return handler.GetEvent<T>();
            }

            CommonUtils.EditorLogWarning($"[EventDispatcher]添加事件监听 --> 不存在id为[{id}]的EventHandler");
            return default;
        }

        public static T GetOrAddEvent<T>(EventHandler handler) where T : IWZEvent, new()
        {
            if (handler is null)
            {
                CommonUtils.EditorLogWarning("[EventDispatcher]添加事件监听 --> EventHandler 为 null");
                return default;
            }
            return handler.GetOrAddEvent<T>();
        }
        public static T GetOrAddEvent<T>(int id) where T : IWZEvent, new()
        {
            if (mHandlers.TryGetValue(id, out var handler))
            {
                return handler.GetOrAddEvent<T>();
            }

            CommonUtils.EditorLogWarning($"[EventDispatcher]添加事件监听 --> 不存在id为[{id}]的EventHandler");
            return default;
        }


    }
}