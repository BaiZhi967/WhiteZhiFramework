using System;

namespace WhiteZhi
{
    /// <summary>
    /// 自定义状态
    /// </summary>
    public class CustomState : IState
    {
        private Func<bool> mOnCondition;
        private Action mOnEnter;
        private Action mOnUpdate;
        private Action mOnFixedUpdate;
        private Action mOnGUI;
        private Action mOnExit;

        public CustomState OnCondition(Func<bool> onCondition)
        {
            mOnCondition = onCondition;
            return this;
        }
        public CustomState OnEnter(Action onEnter)
        {
            mOnEnter = onEnter;
            return this;
        }
        public CustomState OnUpdate(Action onUpdate)
        {
            mOnUpdate = onUpdate;
            return this;
        }
        public CustomState OnFixedUpdate(Action onFixedUpdate)
        {
            mOnFixedUpdate = onFixedUpdate;
            return this;
        }
        public CustomState OnGUI(Action onGUI)
        {
            mOnGUI = onGUI;
            return this;
        }
        public CustomState OnExit(Action onExit)
        {
            mOnExit = onExit;
            return this;
        }
        public bool Condition()
        {
            var result = mOnCondition?.Invoke();
            return result == null || result.Value;
        }

        public void Enter()
        {
            mOnEnter?.Invoke();
        }
        

        public void Update()
        {
            mOnUpdate?.Invoke();

        }

        public void FixedUpdate()
        {
            mOnFixedUpdate?.Invoke();
        }

        
        public void OnGUI()
        {
            mOnGUI?.Invoke();
        }

        public void Exit()
        {
            mOnExit?.Invoke();
        }

    }
}