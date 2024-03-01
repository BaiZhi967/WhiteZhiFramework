namespace WhiteZhi
{
    public interface ICommand : IBelongToArchitecture,ICanSetArchitecture
    {
        void Execute();
        

        
    }

    public interface ICommand<T>
    {
        T Execute();
    }
}