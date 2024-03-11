using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WhiteZhi
{
    
    /// <summary>
    /// UI管理类，负责UI的加载，显示，隐藏，回收等操作
    /// 注意：该类为单例模式
    /// </summary>
    public class UIManager : GlobalSingleton<UIManager>
    {

        /// <summary>
        /// UI面板集合 记录当前界面的所有面板
        /// </summary>
        public static Dictionary<Type, UIBasePanel> PanelDic = null;

        /// <summary>
        /// UI画布
        /// </summary>
        public static Canvas Canvas { get; private set; } = null;
        
        /// <summary>
        /// UI事件系统
        /// </summary>
        public static EventSystem EventSystem { get; private set; } = null;
        
        //public static Camera Camera  { get; private set; } = null;

        /// <summary>
        /// UIRoot层级集合
        /// </summary>
        private static Dictionary<UIPanelLayer, RectTransform> layers = null;

        /// <summary>
        /// 获取指定的UI层级
        /// </summary>
        
        public static RectTransform GetLayer(UIPanelLayer layer)
        {
            return layers[layer];
        }

        /// <summary>
        /// UI框架初始化
        /// </summary>
        /// <param name="gameObject"></param>
        protected override void Init(GameObject gameObject)
        {
            PanelDic = new Dictionary<Type, UIBasePanel>();

            //加载UIRoot 
            //TODO:等待ResourcesManager 类后进行优化
            ResourcesManager.Instance.LoadAsync<GameObject>(WhiteZhiCore.UIManager_UIRoot_Path,
                obj =>
                {
                    obj.transform.SetParent(gameObject.transform);
                    Canvas = obj.GetComponentInChildren<Canvas>();
                    EventSystem = obj.GetComponentInChildren<EventSystem>();
                    layers = new Dictionary<UIPanelLayer, RectTransform>();
                    foreach (UIPanelLayer layer in Enum.GetValues(typeof(UIPanelLayer)))
                    {
                        layers.Add(layer,Canvas.transform.Find(layer.ToString()) as RectTransform);
                    }
                }, ResourcesLoadMod.AssetBundleLoad);
            
            //Camera = obj.GetComponentInChildren<Camera>();
            
            //初始化UIRoot层级
            
        }

        /// <summary>
        /// 打开一个面板
        /// </summary>
        /// <param name="data">面板所需的数据</param>
        /// <typeparam name="T">面板</typeparam>
        public static void Open<T>(IUIData data = null,Action<T> openCallback = null) where T : UIBasePanel
        {
            void OpenPanel(UIBasePanel panel)
            {
                //将面板舍设置到最底层 即显示到屏幕最上层
                panel.transform.SetAsLastSibling();
                
                panel.SetData(data);
                
                panel.OnUIEnable();
                
                openCallback?.Invoke(panel as T);
            }

            void ClonePanel(Action<UIBasePanel> callback)
            {
                //面板路径
                string panelPath = $"{WhiteZhiCore.UIManager_Panel_Path}/{typeof(T).Name}/{typeof(T).Name}";
                ResourcesManager.Instance.LoadAsync<GameObject>(panelPath, obj =>
                {
                    if (obj == null)
                    {
                        CommonUtils.EditorLogError($"在{panelPath}路径下未找到UI面板:{typeof(T).Name}");
                    }
                    //查找面板所在层级
                    RectTransform layer = UIManager.GetLayer(UIPanelLayer.Normal); // 默认显示在Normal层

                    var objects = typeof(T).GetCustomAttributes(typeof(UILayerAttribute),true);
                    if (objects.Length > 0)
                    {
                        var layerAttr = objects[0] as UILayerAttribute;
                        layer = UIManager.GetLayer(layerAttr.layer);
                    }
                    //克隆面板
                    obj.transform.SetParent(layer);
                    obj.name = typeof(T).Name;
                
                    var newPanel = obj.GetOrAddComponent<T>();
                    callback?.Invoke(newPanel);

                },ResourcesLoadMod.AssetBundleLoad);
                
            }

            if (PanelDic.TryGetValue(typeof(T),out UIBasePanel panel))
            {
                //打开面板
                OpenPanel(panel);
            }
            else
            {
                //克隆面板
                ClonePanel(panel =>
                {
                    
                    //将面板添加到字典中
                    PanelDic.Add(typeof(T),panel);
                
                    panel.OnUIAwake();
                    //延迟一帧去执行 Start
                    Instance.StartCoroutine(Invoke(() =>
                    {
                        panel.OnUIStart();
                    }));
                    //打开面板
                    OpenPanel(panel);
                });
                
            }
        }

        
        /// <summary>
        /// 关闭面板
        /// </summary>
        /// <typeparam name="T">需要关闭的面板</typeparam>
        public static void Close<T>() where T : UIBasePanel
        {
            if (!PanelDic.TryGetValue(typeof(T),out UIBasePanel panel))
            {
                CommonUtils.EditorLogError($"场景中不存在面板:{typeof(T).Name}");
            }
            else
            {
                Close(panel);
            }
        }

        public static void Close(UIBasePanel panel)
        {
            panel?.OnUIDisable();
        }

        /// <summary>
        /// 关闭所有的面板
        /// </summary>
        public static void CloseAll()
        {
            foreach (var panel in PanelDic.Values)
            {
                Close(panel);
            }
        }

        /// <summary>
        /// 删除面板
        /// </summary>
        /// <typeparam name="T">要删除的面板</typeparam>
        public static void Destroy<T>() where T : UIBasePanel
        {
            if (!PanelDic.TryGetValue(typeof(T),out UIBasePanel panel))
            {
                CommonUtils.EditorLogError($"场景中不存在面板:{typeof(T).Name}");
            }
            else
            {
                Destroy(panel);
            }
        }
        
        public static void Destroy(UIBasePanel panel)
        {
            panel?.OnUIDisable();
            panel?.OnUIDestroy();
            if (PanelDic.ContainsKey(panel.GetType()))
            {
                PanelDic.Remove(panel.GetType());
            }
        }

        /// <summary>
        /// 删除所有面板
        /// </summary>
        public static void DestroyAll()
        {
            List<UIBasePanel> panels = new List<UIBasePanel>(PanelDic.Values);
            for (int i = 0; i < panels.Count; i++)
            {
                 Destroy(panels[i]);
            }
            PanelDic.Clear();
            panels.Clear();
            
        }

        /// <summary>
        /// 获取场景中的面板
        /// </summary>
        /// <typeparam name="T">要获取的面板</typeparam>
        /// <returns></returns>
        public static T Get<T>() where T : UIBasePanel
        {
            if (PanelDic.TryGetValue(typeof(T),out UIBasePanel panel))
            {
                return panel as T;
            }
            else
            {
                CommonUtils.EditorLogError($"场景中不存在面板:{typeof(T).Name}");
                return default(T);
            }
            
        }

        private static IEnumerator Invoke(Action callback)
        {
            yield return new WaitForEndOfFrame();
            callback?.Invoke();
        }

    }
}