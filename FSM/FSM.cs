using System;
using System.Collections.Generic;
using UnityEngine;

namespace WhiteZhi
{
    public class FSM<T>
    {
        //public FSMBlackboard blackboard;
        protected Dictionary<T, IState> mStates = new Dictionary<T, IState>();
        
        public void AddState(T stateId, IState state)
        {
            mStates.Add(stateId, state);
        }

        public CustomState State(T t)
        {
            if (mStates.ContainsKey(t))
            {
                return  mStates[t] as CustomState;
            }
            var state = new CustomState();
            mStates.Add(t, state);
            return state;
        }
        
        private IState mCurrentState;
        private T mCurrentStateId;
        
        public IState CurrentState => mCurrentState;
        public T CurrentStateId => mCurrentStateId;
        
        public T PreviousStateId { get; private set; }
        
        public long FrameCountOfCurrentState = 1;
        public float SecondsOfCurrentState = 0.0f;
        
        private Action<T, T> mOnStateChanged = (_, __) => { };
        public void OnStateChanged(Action<T, T> onStateChanged)
        {
            mOnStateChanged = onStateChanged;
        }
        
        public void ChangeState(T t)
        {
            if (t.Equals(CurrentStateId)) return;
            
            if (mStates.TryGetValue(t, out var state))
            {
                if (mCurrentState != null && state.Condition())
                {
                    mCurrentState.Exit();
                    PreviousStateId = mCurrentStateId;
                    mCurrentState = state;
                    mCurrentStateId = t;
                    mOnStateChanged?.Invoke(PreviousStateId, CurrentStateId);
                    FrameCountOfCurrentState = 1;
                    SecondsOfCurrentState = 0.0f;
                    mCurrentState.Enter();
                }
            }
        }

        public void StartState(T t)
        {
            if (mStates.TryGetValue(t, out var state))
            {
                PreviousStateId = t;
                mCurrentState = state;
                mCurrentStateId = t;
                FrameCountOfCurrentState = 0;
                SecondsOfCurrentState = 0.0f;
                state.Enter();
            }
        }

        public void FixedUpdate()
        {
            mCurrentState?.FixedUpdate();
        }
        public void Update()
        {
            mCurrentState?.Update();
            SecondsOfCurrentState += Time.deltaTime;
            FrameCountOfCurrentState++;
        }
        public void OnGUI()
        {
            mCurrentState?.OnGUI();
        }

        public void Clear()
        {
            mCurrentState = null;
            mCurrentStateId = default;
            mStates.Clear();
        }

    }
}