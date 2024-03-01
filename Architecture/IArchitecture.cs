namespace WhiteZhi
{
    public interface IArchitecture
    {
        
        ///<summary>
        ///注册System
        /// </summary>
        void RegisterSystem<T>(T instance) where T : ISystem;
        /// <summary>
        /// 获取System
        /// </summary>
        T GetSystem<T>() where T : class, ISystem;
        
        /// <summary>
        /// 注册 Model
        /// </summary>
        void RegisterModel<T>(T instance) where T : IModel;
        
        /// <summary>
        /// 获取 Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        T GetModel<T>() where T : class,IModel;
        
        /// <summary>
        /// 注册 Utility
        /// </summary>
        void RegisterUtility<T>(T instance) where T : IUtility;
        
        /// <summary>
        /// 获取Utility
        /// </summary>
        /// <typeparam name="T"></typeparam>
        T GetUtility<T>() where T : class, IUtility;

        void SendCommand<T>() where T : ICommand, new();
        
        void SendCommand<T>(T command) where T : ICommand;
    }
}