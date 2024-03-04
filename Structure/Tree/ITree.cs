namespace WhiteZhi
{
    public interface ITree<T>
    {
        string Name
        {
            get; set;
        }

        ITreeNode<T> HeadNode
        {
            get;
        }

        /// <summary>
        /// 给定头节点构造一棵树。 每次节点更改后调用此函数以使树保持最新。
        /// </summary>
        void Construct();

        void SetHeadNode(ITreeNode<T> node);

        /// <summary>
        /// 获取树中的一个节点。 在调用该方法之前先调用Construct()。
        /// </summary>
        /// <returns></returns>
        ITreeNode<T> GetNode(string name);

        /// <summary>
        /// 删除树中的一个节点。 在调用该方法之前先调用Construct()。
        /// </summary>
        /// <returns></returns>
        ITreeNode<T> DeleteNode(string name);
    }
}