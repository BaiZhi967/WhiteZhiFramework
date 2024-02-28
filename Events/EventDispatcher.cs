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
        
        public static bool UnRegisterHandler(EventHandler handler)
        {
            return mHandlers.Remove(handler.Uid);
        }
        
        public static bool UnRegisterHandler(int id)
        {
            return mHandlers.Remove(id);
        }
        

    }
}