using System.Collections;
using UnityEngine;
using UnityEngine.Events;
namespace WhiteZhi
{
    public class ResourcesManager : GlobalSingleton<ResourcesManager>
    {
        /// <summary>
        /// 同步资源加载
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <typeparam name="T">资源类型</typeparam>
        /// <returns>如果类型是GameObject将实例化之后再返回</returns>
        public T Load<T>(string assetName) where T : Object
        {
            T resource = Resources.Load<T>(assetName);
            return resource is GameObject ? Instantiate(resource) : resource;
        }


        /// <summary>
        /// 异步资源加载
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <param name="callback">回调</param>
        /// <typeparam name="T">资源类型</typeparam>
        public void LoadAsync<T>(string assetName, UnityAction<T> callback) where T : Object
        {
            //开枪异步资源加载协程
            StartCoroutine(LoadAsyncCoroutine(assetName, callback));
        }

        /// <summary>
        /// 异步资源加载 内部协程实现
        /// </summary>
        private IEnumerator LoadAsyncCoroutine<T>(string assetName, UnityAction<T> callback) where T : Object
        {
            ResourceRequest resourceRequest = Resources.LoadAsync<T>(assetName);
            //CommonUtils.EditorLogSafe(assetName);
            yield return resourceRequest;
            if (resourceRequest.asset is GameObject)
            {
                callback(Instantiate(resourceRequest.asset) as T);
            }
            else
            {
                callback(resourceRequest.asset as T);
            }
        }
        
    }
}