using System;
using System.Collections.Generic;

namespace WhiteZhi
{
    public interface IUnRegister
    {
        void UnRegister();
    }
    public interface IUnRegisterList
    {
        List<IUnRegister> UnregisterList { get; }
    }
    
    public struct CustomUnRegister : IUnRegister
    {
        private Action OnUnRegister { get; set; }
        public CustomUnRegister(Action onUnRegister) => OnUnRegister = onUnRegister;

        public void UnRegister()
        {
            OnUnRegister.Invoke();
            OnUnRegister = null;
        }
    }
}