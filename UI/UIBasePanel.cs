using UnityEngine;

namespace WhiteZhi
{
    /// <summary>
    /// UI面板基类
    /// </summary>
    public abstract class UIBasePanel : MonoBehaviour
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected UIBasePanel()
        {
        }

        /// <summary>
        /// 第一次打开时触发
        /// </summary>
        public virtual void OnUIAwake()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }
        

        public virtual void OnUIStart()
        {
        }
        
        /// <summary>
        /// 传入面板参数
        /// </summary>
        /// <param name="data">参数</param>
        public virtual void SetData(object data)
        {
        }

        public virtual void OnUIEnable()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnUIDisable()
        {
            gameObject.SetActive(false);
        }

        public virtual void OnUIDestroy()
        {
            Destroy(gameObject);
        }
    }
}