namespace WhiteZhi
{
    public interface IModel : IBelongToArchitecture, ICanSetArchitecture, ICanGetUtility, ICanSendEvent,ICanInit
    {
    }

    public abstract class AbstractModel : IModel
    {
        private IArchitecture mArchitecturel;

        IArchitecture IBelongToArchitecture.GetArchitecture() => mArchitecturel;

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture) => mArchitecturel = architecture;

        public bool Initialized { get; set; }
        void ICanInit.Init() => OnInit();
        public void Deinit() => OnDeinit();

        protected virtual void OnDeinit(){}

        protected abstract void OnInit();
    }
}