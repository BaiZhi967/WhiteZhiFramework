using System.Collections.Generic;

namespace WhiteZhi.Events
{
    /// <summary>
    /// 事件调度中心
    /// </summary>
    public class EventDispatcher : GlobalSingleton<EventDispatcher>
    {
        /// <summary>
        /// 所有的EventHandler
        /// </summary>
        public static Dictionary<int, EventHandler> Handlers = new Dictionary<int, EventHandler>();

        #region EventHandler 事件处理程序标识符

        /// <summary>
        /// 命令
        /// </summary>
        public const int EH_COMMON = 0;
        /// <summary>
        /// 玩家
        /// </summary>
        public const int EH_PLAYER = 1; 
        /// <summary>
        /// 物品
        /// </summary>
        public const int EH_ITEM = 2;   // 物品
        /// <summary>
        /// 战斗
        /// </summary>
        public const int EH_COMBAT = 3; 
        /// <summary>
        /// UI
        /// </summary>
        public const int EH_UI = 4;    
        /// <summary>
        /// 环境
        /// </summary>
        public const int EH_ENV = 5;    
        /// <summary>
        /// 特效
        /// </summary>
        public const int EH_FX = 6;    
        /// <summary>
        /// 占用
        /// </summary>
        public const int EH_T = 7;      
        /// <summary>
        /// 占用
        /// </summary>
        public const int EH_X = 8;      
        /// <summary>
        /// 占用
        /// </summary>
        public const int EH_Y = 9;      

        
        #endregion

        #region 默认 EventHandler

        public static EventHandler Common { get { return m_GetHandler(EH_COMMON); } }
        public static EventHandler Player { get { return m_GetHandler(EH_PLAYER); } }
        public static EventHandler Item { get { return m_GetHandler(EH_ITEM); } }
        public static EventHandler Combat { get { return m_GetHandler(EH_COMBAT); } }
        public static EventHandler UI { get { return m_GetHandler(EH_UI); } }
        public static EventHandler Env { get { return m_GetHandler(EH_ENV); } }
        public static EventHandler Fx { get { return m_GetHandler(EH_FX); } }
        public static EventHandler T { get { return m_GetHandler(EH_T); } }
        public static EventHandler X { get { return m_GetHandler(EH_X); } }
        public static EventHandler Y { get { return m_GetHandler(EH_Y); } }

        #endregion


        #region Handlers相关处理

        /// <summary>
        /// 在处理程序dict中注册一个EventHandler。
        /// </summary>
        public static bool RegisterHandler(EventHandler handler, int id)
        {
            if (Handlers.ContainsKey(id))
            {
                return false;
            }

            handler.Uid = id;
            return Handlers.InsertOrUpdateKeyValue(id, handler);
        }

        private static EventHandler m_GetHandler(int type)
        {
            if (!Handlers.ContainsKey(type))
            {
                RegisterHandler(new EventHandler(type), type);
            }

            return Handlers.GetValueInDictionary(type);
        }

        #endregion

        #region 无参数

        /// <summary>
        /// 在指定EventHandler中添加指定id的事件
        /// </summary>
        /// <param name="handler">指定EventHandler</param>
        /// <param name="id">事件id</param>
        /// <param name="wzEvent">事件</param>
        /// <returns></returns>
        public static bool AddListener(EventHandler handler, int id, WZEvent wzEvent)
        {
            if (handler == null)
            {
                CommonUtils.EditorLogWarning("[事件调度中心]添加事件监听 -- EventHandler 为 null");
                return false;
            }
            handler.AddListener(id,wzEvent);
            return true;
        }
        

        /// <summary>
        /// 在指定EventHandler中呼叫指定id的事件
        /// </summary>
        /// <param name="handler">指定EventHandler</param>
        /// <param name="id">事件id</param>
        public static void Dispatch(EventHandler handler, int id)
        {
            handler.Dispatch(id);
        }

        

        /// <summary>
        /// 移除指定EventHandler中指定id的事件
        /// </summary>
        /// <param name="handler">指定EventHandler</param>
        /// <param name="id">事件id</param>
        public static bool RemoveListener(EventHandler handler, int id, WZEvent wzEvent)
        {
            if (handler == null)
            {
                CommonUtils.EditorLogWarning("[事件调度中心]移除事件监听 -- EventHandler 为 null");
                return false;
            }
            handler.RemoveListener(id, wzEvent);
            return true;
        }

        #endregion

        #region 一参数

        /// <summary>
        /// 在指定EventHandler中添加指定id的事件
        /// </summary>
        /// <param name="handler">指定EventHandler</param>
        /// <param name="id">事件id</param>
        /// <param name="wzEvent">事件</param>
        /// <returns></returns>
        public static bool AddListener<T>(EventHandler handler, int id, WZEvent<T> wzEvent)
        {
            if (handler == null)
            {
                CommonUtils.EditorLogWarning("[事件调度中心]添加事件监听 -- EventHandler 为 null");
                return false;
            }
            handler.AddListener<T>(id,wzEvent);
            return true;
        }
        

        /// <summary>
        /// 在指定EventHandler中呼叫指定id的事件
        /// </summary>
        /// <param name="handler">指定EventHandler</param>
        /// <param name="id">事件id</param>
        public static void Dispatch<T>(EventHandler handler, int id,T arg)
        {
            handler.Dispatch(id,arg);
        }

        

        /// <summary>
        /// 移除指定EventHandler中指定id的事件
        /// </summary>
        /// <param name="handler">指定EventHandler</param>
        /// <param name="id">事件id</param>
        public static bool RemoveListener<T>(EventHandler handler, int id, WZEvent<T> wzEvent)
        {
            if (handler == null)
            {
                CommonUtils.EditorLogWarning("[事件调度中心]移除事件监听 -- EventHandler 为 null");
                return false;
            }
            handler.RemoveListener<T>(id, wzEvent);
            return true;
        }

        #endregion
        
        #region 两参数

        /// <summary>
        /// 在指定EventHandler中添加指定id的事件
        /// </summary>
        /// <param name="handler">指定EventHandler</param>
        /// <param name="id">事件id</param>
        /// <param name="wzEvent">事件</param>
        /// <returns></returns>
        public static bool AddListener<T, X>(EventHandler handler, int id, WZEvent<T, X> wzEvent)
        {
            if (handler == null)
            {
                CommonUtils.EditorLogWarning("[事件调度中心]添加事件监听 -- EventHandler 为 null");
                return false;
            }
            handler.AddListener<T, X>(id,wzEvent);
            return true;
        }
        

        /// <summary>
        /// 在指定EventHandler中呼叫指定id的事件
        /// </summary>
        /// <param name="handler">指定EventHandler</param>
        /// <param name="id">事件id</param>
        public static void Dispatch<T, X>(EventHandler handler, int id,T arg1,X arg2)
        {
            handler.Dispatch(id,arg1,arg2);
        }

        

        /// <summary>
        /// 移除指定EventHandler中指定id的事件
        /// </summary>
        /// <param name="handler">指定EventHandler</param>
        /// <param name="id">事件id</param>
        public static bool RemoveListener<T, X>(EventHandler handler, int id, WZEvent<T, X> wzEvent)
        {
            if (handler == null)
            {
                CommonUtils.EditorLogWarning("[事件调度中心]移除事件监听 -- EventHandler 为 null");
                return false;
            }
            handler.RemoveListener<T, X>(id, wzEvent);
            return true;
        }

        #endregion
        
        #region 三参数

        /// <summary>
        /// 在指定EventHandler中添加指定id的事件
        /// </summary>
        /// <param name="handler">指定EventHandler</param>
        /// <param name="id">事件id</param>
        /// <param name="wzEvent">事件</param>
        /// <returns></returns>
        public static bool AddListener<T, X, Y>(EventHandler handler, int id, WZEvent<T, X, Y> wzEvent)
        {
            if (handler == null)
            {
                CommonUtils.EditorLogWarning("[事件调度中心]添加事件监听 -- EventHandler 为 null");
                return false;
            }
            handler.AddListener<T, X, Y>(id,wzEvent);
            return true;
        }
        

        /// <summary>
        /// 在指定EventHandler中呼叫指定id的事件
        /// </summary>
        /// <param name="handler">指定EventHandler</param>
        /// <param name="id">事件id</param>
        public static void Dispatch<T, X, Y>(EventHandler handler, int id,T arg1,X arg2,Y arg3)
        {
            handler.Dispatch(id,arg1,arg2,arg3);
        }

        

        /// <summary>
        /// 移除指定EventHandler中指定id的事件
        /// </summary>
        /// <param name="handler">指定EventHandler</param>
        /// <param name="id">事件id</param>
        public static bool RemoveListener<T, X, Y>(EventHandler handler, int id, WZEvent<T, X, Y> wzEvent)
        {
            if (handler == null)
            {
                CommonUtils.EditorLogWarning("[事件调度中心]移除事件监听 -- EventHandler 为 null");
                return false;
            }
            handler.RemoveListener<T, X, Y>(id, wzEvent);
            return true;
        }

        #endregion
        
        #region 四参数

        /// <summary>
        /// 在指定EventHandler中添加指定id的事件
        /// </summary>
        /// <param name="handler">指定EventHandler</param>
        /// <param name="id">事件id</param>
        /// <param name="wzEvent">事件</param>
        /// <returns></returns>
        public static bool AddListener<T, X, Y, Z>(EventHandler handler, int id, WZEvent<T, X, Y, Z> wzEvent)
        {
            if (handler == null)
            {
                CommonUtils.EditorLogWarning("[事件调度中心]添加事件监听 -- EventHandler 为 null");
                return false;
            }
            handler.AddListener<T, X, Y, Z>(id,wzEvent);
            return true;
        }
        

        /// <summary>
        /// 在指定EventHandler中呼叫指定id的事件
        /// </summary>
        /// <param name="handler">指定EventHandler</param>
        /// <param name="id">事件id</param>
        public static void Dispatch<T, X, Y, Z>(EventHandler handler, int id,T arg1, X arg2, Y arg3, Z arg4)
        {
            handler.Dispatch(id,arg1,arg2,arg3,arg4);
        }

        

        /// <summary>
        /// 移除指定EventHandler中指定id的事件
        /// </summary>
        /// <param name="handler">指定EventHandler</param>
        /// <param name="id">事件id</param>
        public static bool RemoveListener<T, X, Y, Z>(EventHandler handler, int id, WZEvent<T, X, Y, Z> wzEvent)
        {
            if (handler == null)
            {
                CommonUtils.EditorLogWarning("[事件调度中心]移除事件监听 -- EventHandler 为 null");
                return false;
            }
            handler.RemoveListener<T, X, Y, Z>(id, wzEvent);
            return true;
        }

        #endregion
        
    }
}