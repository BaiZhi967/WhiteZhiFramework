namespace WhiteZhi
{
    public delegate void WZEvent();
    public delegate void WZEvent<T>(T arg1);
    public delegate void WZEvent<T, X>(T arg1, X arg2);
    public delegate void WZEvent<T, X, Y>(T arg1, X arg2, Y arg3);
    public delegate void WZEvent<T, X, Y, Z>(T arg1, X arg2, Y arg3, Z arg4);
}