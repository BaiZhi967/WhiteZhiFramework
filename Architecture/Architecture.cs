using System;
using System.Collections.Generic;

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
        /// 增加注册
        /// </summary>
        public static Action<T> OnRegisterPatch = architecture => { };

        private static T mArchitecture = null;
        
        /// <summary>
        /// 是否已经初始化
        /// </summary>
        private bool mInited = false;

        /// <summary>
        /// 用于缓存初始化的 Model
        /// </summary>
        private List<IModel> mModels = new List<IModel>();
        
        /// <summary>
        /// 用于缓存初始化的 System
        /// </summary>
        private List<ISystem> mSystems = new List<ISystem>();

        static void MakeSureArchitecture()
        {
            if (mArchitecture is null)
            {
                mArchitecture = new T();
                mArchitecture.Init();
                
                //调用
                OnRegisterPatch?.Invoke(mArchitecture);
                
                //初始化 Model
                foreach (var model in mArchitecture.mModels)
                {
                    model.Init();
                }
                //清空 Models
                mArchitecture.mModels.Clear();
                
                //初始化 System
                foreach (var system in mArchitecture.mSystems)
                {
                    system.Init();
                }
                //清空 Systems
                mArchitecture.mSystems.Clear();
                
                mArchitecture.mInited = true;
            }
        }

        #endregion

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

        /// <summary>
        /// 注册模块的容器
        /// </summary>
        private IOCContainer mContainer = new IOCContainer();

        /// <summary>
        /// 留给子类自行注册模块
        /// </summary>
        protected abstract void Init();

        /// <summary>
        /// 注册模块的API
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        public void Register<T>(T instance)
        {
            MakeSureArchitecture();//确保架构已经初始化
            mArchitecture.mContainer.Register<T>(instance);
        }

        /// <summary>
        /// 获取模块的API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>() where T : class
        {
            MakeSureArchitecture();//确保架构已经初始化
            return mArchitecture.mContainer.Get<T>();
        }
        /// <summary>
        /// 注册 Model 的API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public void RegisterModel<T>(T model) where T : IModel
        {
            
            //给Model赋值
            model.SetArchitecture(this);
            mContainer.Register<T>(model);

            if (mInited)
            {
                //如果初始化过了
                model.Init();
            }
            else
            {
                //缓存到mModels中 用于初始化
                mModels.Add(model);
            }
        }

        /// <summary>
        /// 获取Model的API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetModel<T>() where T : class, IModel
        {
            return mContainer.Get<T>();
        }
        
        /// <summary>
        /// 注册 System 的API
        /// </summary>
        /// <param name="system"></param>
        /// <typeparam name="T"></typeparam>
        public void RegisterSystem<T>(T system) where T : ISystem
        {
            
            //给System赋值
            system.SetArchitecture(this);
            mContainer.Register<T>(system);

            if (mInited)
            {
                //如果初始化过了
                system.Init();
            }
            else
            {
                //缓存到mSystems中 用于初始化
                mSystems.Add(system);
            }
        }
        /// <summary>
        /// 获取 System 的API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetSystem<T>() where T : class, ISystem
        {
            return mContainer.Get<T>();
        }

        /// <summary>
        /// 注册 Utility 的API
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="T>"></typeparam>
        public void RegisterUtility<T>(T instance) where T : IUtility
        {
            mContainer.Register<T>(instance);
        }

        /// <summary>
        /// 获取 Utility 的API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUtility<T>() where T : class,IUtility
        {
            return mContainer.Get<T>();
        }
        
        public void SendCommand<T>() where T : ICommand, new()
        {
            var command = new T();
            command.SetArchitecture(this);
            command.Execute();
            command.SetArchitecture(null);
        }
        
        public void SendCommand<T>(T command) where T : ICommand
        {
            command.SetArchitecture(this);
            command.Execute();
            command.SetArchitecture(null);
        }
    }
}