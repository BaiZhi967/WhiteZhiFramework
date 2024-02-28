using System;

namespace WhiteZhi
{
    /// <summary>
    /// UI层级特征描述,用于标记UI的层级
    /// </summary>
    public class UILayerAttribute : Attribute
    {
        public UIPanelLayer layer { get; }

        /// <summary>
        /// 指定UI的层级
        /// </summary>
        /// <param name="layer"></param>
        public UILayerAttribute(UIPanelLayer layer)
        {
            this.layer = layer;
        }
    }
}