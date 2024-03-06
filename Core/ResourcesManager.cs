using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

namespace WhiteZhi
{

    /// <summary>
    ///  Resources加载方式
    /// </summary>
    public enum ResourcesLoadMod
    {
        [InspectorName("Resources加载")]
        ResourcesLoad,
        [InspectorName("AssetBundle加载")]
        AssetBundleLoad,
    }

    public interface IResourcesManager
    {
        public  T Load<T>(string assetName,ResourcesLoadMod mod = ResourcesLoadMod.ResourcesLoad) where T : Object;
        public void LoadAsync<T>(string assetName, UnityAction<T> callback) where T : Object;
    }
    
    public class ResourcesManager : GlobalSingleton<ResourcesManager>
    {
        /// <summary>
        /// 同步资源加载
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <typeparam name="T">资源类型</typeparam>
        /// <returns>如果类型是GameObject将实例化之后再返回</returns>
        public T Load<T>(string assetName,ResourcesLoadMod mod = ResourcesLoadMod.ResourcesLoad) where T : Object
        {
            if (mod == ResourcesLoadMod.ResourcesLoad)
            {
                T resource = Resources.Load<T>(assetName);
                return resource is GameObject ? Instantiate(resource) : resource;
            }else if (mod == ResourcesLoadMod.AssetBundleLoad)
            {
                T resource = null;
                bool isDone = false;
                LoadAsync<T>(assetName, (res) =>
                {
                    resource = res;
                    CommonUtils.EditorLogSafe("资源加载完成");
                    isDone = true;
                });
                

            }

            return null;
        }


        /// <summary>
        /// 异步资源加载
        /// </summary>
        /// <param name="assetName">资源名称</param>
        /// <param name="callback">回调</param>
        /// <typeparam name="T">资源类型</typeparam>
        public void LoadAsync<T>(string assetName, UnityAction<T> callback,ResourcesLoadMod mod = ResourcesLoadMod.ResourcesLoad) where T : Object
        {
            //开枪异步资源加载协程
            if (mod == ResourcesLoadMod.ResourcesLoad)
            {
                StartCoroutine(LoadFromResAsyncCoroutine(assetName, callback));
            }else if (mod == ResourcesLoadMod.AssetBundleLoad)
            {
                StartCoroutine(LoadAsyncFromCoroutine(assetName, callback));
            }
        }

        /// <summary>
        /// 异步资源加载 内部协程实现
        /// </summary>
        private IEnumerator LoadAsyncFromCoroutine<T>(string assetName, UnityAction<T> callback) where T : Object
        {
            
            var handle = Addressables.LoadAssetAsync<T>(assetName);
            while (!handle.IsDone)
            {
                yield return null;
            }

            var res = handle.Result;
            if (res is GameObject)
            {
                callback(Instantiate(res) as T);
            }
            else
            {
                callback(res);
            }
        }

        private IEnumerator LoadFromResAsyncCoroutine<T>(string assetName, UnityAction<T> callback) where T : Object
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