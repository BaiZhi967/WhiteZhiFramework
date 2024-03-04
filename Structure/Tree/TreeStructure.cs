using System.Linq;
namespace WhiteZhi
{
    public class TreeStructure<T> : ITree<T>
    {
        private string name;
        private ITreeNode<T> headNode;
        private ITreeNode<T>[] nodeList;

        public string Name { get => name; set => name = value; }

        public ITreeNode<T> HeadNode => headNode;

        public TreeStructure() { }
        public TreeStructure(string name)
        {
            this.name = name;
        }

        public TreeStructure(string name, ITreeNode<T> headNode)
        {
            this.name = name;
            this.headNode = headNode;
        }

        public void SetHeadNode(ITreeNode<T> node)
        {
            if (node.Parent != null)
            {
                CommonUtils.EditorLogError("Tree.SetHeadNode: 有父节点的节点不能设置为头节点。");
                return;
            }

            headNode = node;
        }
        
        public void Construct()
        {
            if (headNode == null)
                return;

            nodeList = headNode.GetAllChildRecursive();
        }
        
        /// <summary>
        /// 通过名称获取树中的节点。 在调用该方法之前先调用Construct()。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ITreeNode<T> GetNode(string name)
        {
            return nodeList.First(x => x.Name.Equals(name));
        }

        /// <summary>
        /// 按名称删除树中的节点。 在调用该方法之前先调用Construct()。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ITreeNode<T> DeleteNode(string name)
        {
            ITreeNode<T> node = GetNode(name);
            node.DetachFromParent();

            return node;
        }
    }
}