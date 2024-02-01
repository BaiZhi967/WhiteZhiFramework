using System.Collections.Generic;

namespace WhiteZhi
{
    /// <summary>
    /// 状态机系统
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FSMSystem<T>
    {
        public FSMBlackboard fsmBlackboard;
        protected FSMState<T> currentState;
        protected Dictionary<int, FSMState<T>> states;
        public T Owner { get; private set; }

        public FSMSystem(T owner,FSMBlackboard fsmBlackboard)
        {
            Owner = owner;
            this.fsmBlackboard = fsmBlackboard;
            states = new Dictionary<int, FSMState<T>>();
        }
        
        

        /// <summary>
        /// 添加状态到状态机
        /// </summary>
        /// <param name="stateId">状态Id</param>
        /// <param name="state">状态</param>
        public bool AddState(int stateId, FSMState<T> state)
        {
            bool res = states.TryAdd(stateId, state);
            if (!res)
            {
                CommonUtils.EditorLogWarning("[状态机]状态添加失败，状态Id重复："+ stateId);
            }

            state.system = this;
            return res;
        }

        /// <summary>
        /// 强制添加状态到状态机
        /// </summary>
        /// <param name="stateId">状态Id</param>
        /// <param name="state">状态</param>
        public void ForceAddState(int stateId, FSMState<T> state)
        {
            states.InsertOrUpdateKeyValue(stateId, state);
            state.system = this;
        }
        
        
        
        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="stateId">目标状态id</param>
        public bool SwitchState(int stateId)
        {
            if (!states.ContainsKey(stateId))
            {
                CommonUtils.EditorLogWarning("[状态机]状态切换失败，状态Id不存在："+ stateId);
                 return false;
            }
            //当前状态非空
            if (currentState is not null)
            {
                currentState.OnExit();
            }

            currentState = states[stateId];
            currentState.OnEnter();
            return true;
        }

        public void OnUpdate()
        {
            currentState.OnUpdate();
        }
    }
}