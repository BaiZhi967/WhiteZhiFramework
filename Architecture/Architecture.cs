using System;

namespace WhiteZhi
{
    /// <summary>
    /// 架构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Architecture<T> where T : Architecture<T>, new()
    {
        #region 单例 但仅在内部访问

        private static T mArchitecture = null;

        static void MakeSureArchitecture()
        {
            if (mArchitecture is null)
            {
                mArchitecture = new T();
                mArchitecture.Init();
            }
        }

        #endregion

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

        public static T Get<T>() where T : class
        {
            MakeSureArchitecture();//确保架构已经初始化
            return mArchitecture.mContainer.Get<T>();
        }
    }
}