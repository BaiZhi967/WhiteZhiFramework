#if UNITY_EDITOR
#endif
using UnityEngine;

namespace WhiteZhi
{
    public class WhiteZhiCore : GlobalSingleton<WhiteZhiCore>
    {
        #region CommonUtils 相关配置

        public static Color Salf_Log_Color = Color.green;
        public static Color Warning_Log_Color = Color.yellow;
        public static Color Error_Log_Color = Color.red;
        public static Color Critical_Log_Color = Color.cyan;
        public static Color Normal_Log_Color = Color.white;

        public static int Salf_Log_Level = -1;
        public static int Warning_Log_Level = -1;
        public static int Error_Log_Level = -1;
        public static int Critical_Log_Level = -1;
        public static int Normal_Log_Level = -1;


        #endregion

        #region UIManager 相关配置

        /// <summary>
        /// UIRoot 路径
        /// </summary>
        public static string UIManager_UIRoot_Path = "WhiteZhi/Prefabs/UIRoot/UIRoot";

        /// <summary>
        /// UI Panel 路径
        /// </summary>
        public static string UIManager_Panel_Path = "Prefabs/UI";


        #endregion

    }
}