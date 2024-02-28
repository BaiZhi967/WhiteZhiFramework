namespace WhiteZhi
{
    public abstract class AbstractState<TStateId,TTarget> : IState
    {
        protected FSM<TStateId> mFSM;
        protected TTarget mTarget;

        public AbstractState(FSM<TStateId> fsm,TTarget target)
        {
            mFSM = fsm;
            mTarget = target;
        }


        bool IState.Condition()
        {
            return  OnCondition();;
        }

        void IState.Enter()
        {
            OnEnter();
        }

        void IState.Update()
        {
            OnUpdate();
        }

        void IState.FixedUpdate()
        {
            OnFixedUpdate();
        }

        public virtual void OnGUI()
        {
        }

        void IState.Exit()
        {
            OnExit();
        }

        protected virtual bool OnCondition() => true;

        protected virtual void OnEnter()
        {
        }

        protected virtual void OnUpdate()
        {
            
        }

        protected virtual void OnFixedUpdate()
        {
            
        }

        protected virtual void OnExit()
        {
            
        }
    }
}