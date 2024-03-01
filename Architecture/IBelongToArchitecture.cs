namespace WhiteZhi
{
    /// <summary>
    /// 引入IBelongToArchitecture接口解决Architecture架构初始化时递归调用的问题
    /// </summary>
    public interface IBelongToArchitecture
    {
        IArchitecture GetArchitecture();
    }
}