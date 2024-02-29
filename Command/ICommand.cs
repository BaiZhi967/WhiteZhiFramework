namespace WhiteZhi
{
    public interface ICommand
    {
        void Execute();
        
        void Revert();
        
    }

    public interface ICommand<T>
    {
        T Execute();
        T Revert();
    }
}