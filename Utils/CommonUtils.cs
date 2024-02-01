using UnityEngine;

namespace WhiteZhi
{
    /// <summary>
    /// 常用工具类
    /// 注意：不要在这里写任何与业务相关的代码
    /// </summary>
    public static class CommonUtils
    {

        #region 属性 and 字段

        private static int[] lastFrameCounts = new int[3];

        #endregion
        
        #region Log and Debug 工具

        /// <summary>
        /// 安全Msg Log，避免在Update等频繁调用的地方打印日志
        /// </summary>
        /// <param name="message">要输出的Msg</param>
        /// <param name="detailed">是否日志详细信息</param>
        /// <param name="logLevel">log等级,低于全局设置等级的log不会被显示出来</param>
        public static void EditorLogSafe(object message, bool detailed = false, int logLevel = 0)
        {
            if (logLevel < WhiteZhiCore.Salf_Log_Level)
            {
                return;
            }
            if (detailed)
            {
                Debug.Log($"<<color=#{ColorUtility.ToHtmlStringRGB(WhiteZhiCore.Salf_Log_Color)}>安全! {message}</color> | Frame: {Time.frameCount} | 详细: {Time.frameCount - lastFrameCounts[0]}>");
            }
            else
            {
                Debug.Log($"<<color=#{ColorUtility.ToHtmlStringRGB(WhiteZhiCore.Salf_Log_Color)}>安全! {message}</color> | Frame: {Time.frameCount}>");
            }
            lastFrameCounts[0] = Time.frameCount;
        }
        
        /// <summary>
        /// 普通Msg Log，避免在Update等频繁调用的地方打印日志
        /// </summary>
        /// <param name="message">要输出的Msg</param>
        /// <param name="detailed">是否日志详细信息</param>
        /// <param name="logLevel">log等级,低于全局设置等级的log不会被显示出来</param>
        public static void EditorLogNormal(object message, bool detailed = false, int logLevel = 0)
        {
            if (logLevel < WhiteZhiCore.Normal_Log_Level)
            {
                return;
            }
            if (detailed)
            {
                Debug.Log($"<<color=#{ColorUtility.ToHtmlStringRGB(WhiteZhiCore.Normal_Log_Color)}>{message}</color> | Frame: {Time.frameCount} | 详细: {Time.frameCount - lastFrameCounts[0]}>");
            }
            else
            {
                Debug.Log($"<<color=#{ColorUtility.ToHtmlStringRGB(WhiteZhiCore.Normal_Log_Color)}>{message}</color> | Frame: {Time.frameCount}>");
            }
            lastFrameCounts[0] = Time.frameCount;
        }

        /// <summary>
        /// 警告Msg Log，避免在Update等频繁调用的地方打印日志
        /// </summary>
        /// <param name="message">要输出的Msg</param>
        /// <param name="detailed">是否日志详细信息</param>
        /// <param name="logLevel">log等级,低于全局设置等级的log不会被显示出来</param>
        public static void EditorLogWarning(object message, bool detailed = false, int logLevel = 0)
        {
            if (logLevel < WhiteZhiCore.Warning_Log_Level)
            {
                return;
            }
            if (detailed)
            {
                Debug.LogWarning($"<<color=#{ColorUtility.ToHtmlStringRGB(WhiteZhiCore.Warning_Log_Color)}>警告! {message}</color> | Frame: {Time.frameCount} | 详细: {Time.frameCount - lastFrameCounts[1]}>");
            }
            else
            {
                Debug.LogWarning($"<<color=#{ColorUtility.ToHtmlStringRGB(WhiteZhiCore.Warning_Log_Color)}>警告! {message}</color> | Frame: {Time.frameCount}>");
            }
            lastFrameCounts[1] = Time.frameCount;
        }


        /// <summary>
        /// 错误Msg Log，避免在Update等频繁调用的地方打印日志
        /// </summary>
        /// <param name="message">要输出的Msg</param>
        /// <param name="detailed">是否日志详细信息</param>
        /// <param name="logLevel">log等级,低于全局设置等级的log不会被显示出来</param>
        public static void EditorLogError(object message, bool detailed = false, int logLevel = 0)
        {
            if (logLevel < WhiteZhiCore.Error_Log_Level)
            {
                return;
            }
            if (detailed)
            {
                Debug.LogError($"<<color=#{ColorUtility.ToHtmlStringRGB(WhiteZhiCore.Error_Log_Color)}>错误!! {message}</color> | Frame: {Time.frameCount} | 详细: {Time.frameCount - lastFrameCounts[2]}>");
            }
            else
            {
                Debug.LogError($"<<color=#{ColorUtility.ToHtmlStringRGB(WhiteZhiCore.Error_Log_Color)}>错误!! {message}</color> | Frame: {Time.frameCount}>");
            }
            lastFrameCounts[2] = Time.frameCount;
        }
        
        
        /// <summary>
        /// 关键Msg Log，避免在Update等频繁调用的地方打印日志
        /// </summary>
        /// <param name="message">要输出的Msg</param>
        /// <param name="detailed">是否日志详细信息</param>
        /// <param name="logLevel">log等级,低于全局设置等级的log不会被显示出来</param>
        public static void EditorLogCritical(object message, bool detailed = false, int logLevel = 0)
        {
            if (logLevel < WhiteZhiCore.Critical_Log_Level)
            {
                return;
            }
            if (detailed)
            {
                Debug.LogError($"<<color=#{ColorUtility.ToHtmlStringRGB(WhiteZhiCore.Critical_Log_Color)}>关键!!! {message}</color> | Frame: {Time.frameCount} | 详细: {Time.frameCount - lastFrameCounts[2]}>");
            }
            else
            {
                Debug.LogError($"<<color=#{ColorUtility.ToHtmlStringRGB(WhiteZhiCore.Critical_Log_Color)}>关键!!! {message}</color> | Frame: {Time.frameCount}>");
            }
            lastFrameCounts[2] = Time.frameCount;
        }
        
        #endregion
        
        
    }
}