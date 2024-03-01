namespace WhiteZhi
{
    public interface ICommand : IBelongToArchitecture,ICanSetArchitecture,ICanGetModel,ICanGetSystem,ICanGetUtility
    {
        void Execute();
        

        
    }

    public interface ICommand<T>
    {
        T Execute();
    }
}