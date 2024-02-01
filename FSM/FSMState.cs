namespace WhiteZhi
{
    /// <summary>
    /// 泛型状态基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class FSMState<T>
    {
        public FSMSystem<T> system;
        public T owner { get; private set; }
        public FSMState(T owner)
        {
            this.owner = owner;
        }
        public virtual void OnEnter()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}