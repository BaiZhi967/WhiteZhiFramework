﻿namespace WhiteZhi
{
    public interface ISystem : IBelongToArchitecture,ICanSetArchitecture,ICanGetModel,ICanGetUtility
    {
        void Init();
    }
    public abstract class AbstractSystem : ISystem
    {
        private IArchitecture mArchitecture = null;
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return mArchitecture;
        }

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }

        void ISystem.Init()
        {
            OnInit();
        }

        protected abstract void OnInit();
    }
}