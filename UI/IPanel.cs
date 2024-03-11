using UnityEngine;

namespace WhiteZhi
{
    public partial interface IPanel
    {
       Transform Transform { get; }
       
       void OnUIAwake();
       void OnUIStart();
       
       void OnUIEnable();
       
       void SetData(IUIData data);
       void OnUIDisable();
       void OnUIDestroy();
       
       
    }
}