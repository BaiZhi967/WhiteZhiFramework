using System.Linq;

namespace WhiteZhi
{
    
    //TODO:未完善
    public class BinaryTree<T> : ITree<T>
    {
        private string mName;

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }
        private ITreeNode<T> mHeadNode;
        private ITreeNode<T>[] mNodeList;

        public ITreeNode<T> HeadNode
        {
            get { return mHeadNode; }
        }
        public void Construct()
        {
            if (HeadNode == null)
                return;

            mNodeList = HeadNode.GetAllChildRecursive();
        }

        public void SetHeadNode(ITreeNode<T> node)
        {
            if (node.Parent != null)
            {
                CommonUtils.EditorLogError("Tree.SetHeadNode: 有父节点的节点不能设置为头节点。");
                return;
            }

            mHeadNode = node;
        }

        public ITreeNode<T> GetNode(string name)
        {
            return mNodeList.First(x => x.Name.Equals(name));
        }

        public ITreeNode<T> DeleteNode(string name)
        {
            ITreeNode<T> node = GetNode(name);
            node.DetachFromParent();

            return node;
        }
    }
}