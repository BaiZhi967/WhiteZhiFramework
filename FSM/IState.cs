namespace WhiteZhi
{
    /// <summary>
    /// 状态机接口
    /// </summary>
    public interface IState
    {
        bool Condition();
        void Enter();
        void Update();
        void FixedUpdate();
        void OnGUI();
        void Exit();
    }
}