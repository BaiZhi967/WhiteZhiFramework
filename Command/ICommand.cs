namespace WhiteZhi
{
    public interface ICommand : IBelongToArchitecture,ICanSetArchitecture,ICanGetModel,ICanGetSystem,ICanGetUtility,ICanSendEvent,ICanSendCommand
    {
        void Execute();
        

        
    }

    public interface ICommand<T>
    {
        T Execute();
    }
}