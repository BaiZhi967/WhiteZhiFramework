using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace WhiteZhi
{
    public class WZSceneManager : GlobalSingleton<WZSceneManager>
    {
        private static AsyncOperation async = null;
        private static float actualAsyncProgress = 0;
        
        [Header("Async 设置")]
        [Tooltip("异步加载延迟")]
        public float asyncLoadLag = 3f;
        [Tooltip("异步加载完延迟")]
        public float asyncAfterFullLag = 1.5f;
        [Tooltip("用于设置异步加载的延迟进度的随机范围")]
        public float asyncLagLowerThreshold = 0.5f, asyncLagUpperThreshold = 0.8f;

        [Header("Async 事件")]
        public UnityEvent onStartLoad;
        public UnityEvent onLoadingSceneLoaded,onNextSceneLoaded, onLoadingBarFull, onAwake;

        protected override void Awake()
        {
            base.Awake();
            onAwake.Invoke();
        }

        #region 方法

        public void LoadSceneSync(string sceneName,LoadSceneMode loadSceneMode= LoadSceneMode.Single)
        {
            SceneManager.LoadScene(sceneName,loadSceneMode);
        }
        

        #endregion
    }
}