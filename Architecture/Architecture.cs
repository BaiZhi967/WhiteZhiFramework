using System;
using System.Collections.Generic;
using System.Linq;

namespace WhiteZhi
{
    /// <summary>
    /// 架构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>, new()
    {
        #region 单例 但仅在内部访问

        /// <summary>
        /// 是否已经初始化
        /// </summary>
        private bool mInited = false;
        
        /// <summary>
        /// 增加注册
        /// </summary>
        public static Action<T> OnRegisterPatch = architecture => { };

        private static T mArchitecture = null;
        
        public static IArchitecture Interface
        {
            get
            {
                if (mArchitecture is null)
                {
                    MakeSureArchitecture();
                }

                return mArchitecture;
            }
        }
        
        static void MakeSureArchitecture()
        {
            if (mArchitecture is null)
            {
                mArchitecture = new T();
                mArchitecture.Init();
                
                //调用
                OnRegisterPatch?.Invoke(mArchitecture);
                
                //初始化 Model
                foreach (var model in mArchitecture.mContainer.GetInstancesByType<IModel>().Where(m=>!m.Initialized))
                {
                    model.Init();
                    model.Initialized = true;
                }
                
                
                foreach (var system in mArchitecture.mContainer.GetInstancesByType<ISystem>().Where(m=>!m.Initialized))
                {
                    system.Init();
                    system.Initialized = true;
                }

                mArchitecture.mInited = true;
            }
        }
        

        #endregion
        
        /// <summary>
        /// 留给子类自行注册模块
        /// </summary>
        protected abstract void Init();
        
        public void Deinit()
        {
            OnDeinit();
            foreach (var system in mContainer.GetInstancesByType<ISystem>().Where(s=>s.Initialized)) system.Deinit();
            foreach (var model in mContainer.GetInstancesByType<IModel>().Where(m=>m.Initialized)) model.Deinit();
            mContainer.Clear();
            mArchitecture = null;
        }

        protected virtual void OnDeinit() { }
        
        /// <summary>
        /// 注册模块的容器
        /// </summary>
        private IOCContainer mContainer = new IOCContainer();

        #region 注册 API

        /// <summary>
        /// 注册 System 的API
        /// </summary>
        public void RegisterSystem<TSystem>(TSystem system) where TSystem : ISystem
        {
            system.SetArchitecture(this);
            mContainer.Register<TSystem>(system);

            if (mInited)
            {
                system.Init();
                system.Initialized = true;
            }
        }
        
        /// <summary>
        /// 注册 Model 的API
        /// </summary>
        public void RegisterModel<TModel>(TModel model) where TModel : IModel
        {
            model.SetArchitecture(this);
            mContainer.Register<TModel>(model);

            if (mInited)
            {
                model.Init();
                model.Initialized = true;
            }
        }
        
        /// <summary>
        /// 注册 Utility 的API
        /// </summary>
        public void RegisterUtility<TUtility>(TUtility utility) where TUtility : IUtility =>
            mContainer.Register<TUtility>(utility);

       #endregion
       
        #region 获取API

        public TSystem GetSystem<TSystem>() where TSystem : class, ISystem => mContainer.Get<TSystem>();

        public TModel GetModel<TModel>() where TModel : class, IModel => mContainer.Get<TModel>();

        public TUtility GetUtility<TUtility>() where TUtility : class, IUtility => mContainer.Get<TUtility>();

        #endregion

        #region Command相关

        public TResult SendCommand<TResult>(ICommand<TResult> command) => ExecuteCommand(command);

        public void SendCommand<TCommand>(TCommand command) where TCommand : ICommand => ExecuteCommand(command);

        protected virtual TResult ExecuteCommand<TResult>(ICommand<TResult> command)
        {
            command.SetArchitecture(this);
            return command.Execute();
        }

        protected virtual void ExecuteCommand(ICommand command)
        {
            command.SetArchitecture(this);
            command.Execute();
        }

        #endregion

        #region Query相关

        public TResult SendQuery<TResult>(IQuery<TResult> query) => DoQuery<TResult>(query);

        protected virtual TResult DoQuery<TResult>(IQuery<TResult> query)
        {
            query.SetArchitecture(this);
            return query.Do();
        }
        
        #endregion


        #region Event相关

        private TypeEventSystem mTypeEventSystem = new TypeEventSystem();

        public void SendEvent<TEvent>() where TEvent : new() => mTypeEventSystem.Send<TEvent>();

        public void SendEvent<TEvent>(TEvent e) => mTypeEventSystem.Send<TEvent>(e);

        public IUnRegister RegisterEvent<TEvent>(Action<TEvent> onEvent) => mTypeEventSystem.Register<TEvent>(onEvent);

        public void UnRegisterEvent<TEvent>(Action<TEvent> onEvent) => mTypeEventSystem.UnRegister<TEvent>(onEvent);

        #endregion
    }
}