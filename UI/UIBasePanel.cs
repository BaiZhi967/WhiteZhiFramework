using UnityEngine;
using UnityEngine.UI;

namespace WhiteZhi
{
    
    /// <summary>
    /// 每个UI对应各种的Data
    /// </summary>
    public interface IUIData{}
    
    public class  UIPanelData : IUIData
    {
    }
    
    /// <summary>
    /// UI面板基类
    /// </summary>
    public abstract class UIBasePanel : MonoBehaviour , IPanel
    {
        private bool _isVisible;
        public bool Visible
        {
            get => _isVisible;
            set
            {
                if (_isVisible == value)
                {
                    return;
                }

                _isVisible = value;
                gameObject.SetActive(value);
            }
        }
        public string Name
        {
            get => gameObject.name;
            set => gameObject.name = value;
        }
        private CanvasGroup _canvasGroup;
        public Transform Transform => transform;
        
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

            _canvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();
            gameObject.GetOrAddComponent<GraphicRaycaster>();
        }
        

        public virtual void OnUIStart()
        {
        }
        
        /// <summary>
        /// 传入面板参数
        /// </summary>
        /// <param name="data">参数</param>
        public virtual void SetData(IUIData data)
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