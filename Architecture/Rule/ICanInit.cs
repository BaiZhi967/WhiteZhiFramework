namespace WhiteZhi
{
    public interface ICanInit
    {
        bool Initialized { get; set; }
        void Init();
        void Deinit();
    }
}