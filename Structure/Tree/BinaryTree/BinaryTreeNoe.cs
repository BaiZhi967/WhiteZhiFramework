using System.Collections.Generic;

namespace WhiteZhi
{
    public class BinaryTreeNoe<T> : ITreeNode<T>
    {
        private string mName;
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        #region 子节点

        private ITreeNode<T> mLeftNode;
        public ITreeNode<T> LeftNode
        {
            get { return mLeftNode; }
            set { mLeftNode = value; }
        }
        private ITreeNode<T> mRightNode;
        public ITreeNode<T> RightNode
        {
            get { return mRightNode; }
            set { mRightNode = value; }
        }

        #endregion

        public int ChildCount => ((LeftNode is not null) ? 1 : 0) + ((RightNode is not null) ? 1 : 0);
        
        private T mValue;
        public T Value
        {
            get { return mValue; }
            set { mValue = value; }
        }

        /// <summary>
        /// 存在指定子节点
        /// </summary>
        /// <param name="index">0:左子节点 1:右子节点</param>
        /// <returns></returns>
        public bool HasChild(int index)
        {
            return (index == 0 && LeftNode is not null) || (index == 1 && RightNode is not null);
        }
        
        public bool HasChild(string name)
        {
            return LeftNode is not null && LeftNode.Name.Equals(name) || RightNode is not null && RightNode.Name.Equals(name);
        }
        
        public ITreeNode<T> GetChild(int index)
        {
            if (index == 0)
            {
                return LeftNode;
            }
            else if (index == 1)
            {
                return RightNode;
            }
            else
            {
                CommonUtils.EditorLogError($"BinaryTreeNoe.GetChild: index 超出范围!  二叉树仅存在左右子节点(0:左子节点 1:右子节点))");
                return null;
            }
        }
        public ITreeNode<T> GetChild(string name)
        {
            if (LeftNode is not null && LeftNode.Name.Equals(name))
            {
                return LeftNode;
            }
            else if (RightNode is not null && RightNode.Name.Equals(name))
            {
                return RightNode;
            }
            else
            {
                return null;
            }

           
        }
        
        public int GetChildIndex(ITreeNode<T> child)
        {
            if (child == LeftNode)
            {
                return 0;
            }
            else if (child == RightNode)
            {
                return 1;
            }
            else
            {
                return -1;
            }

            
        }

        public ITreeNode<T>[] GetAllChildRecursive()
        {
            List<ITreeNode<T>> list = new List<ITreeNode<T>>();
            GetAllChildRecursiveHelper(list, this);
            return list.ToArray();
        }
        private void GetAllChildRecursiveHelper(List<ITreeNode<T>> list, ITreeNode<T> node)
        {
            list.Add(node);
            if (LeftNode is not null)
            {
                GetAllChildRecursiveHelper(list,LeftNode);
            }
            if (RightNode is not null)
            {
                GetAllChildRecursiveHelper(list,RightNode);
            }
        }

        public ITreeNode<T>[] GetAllChild()
        {
            List<ITreeNode<T>> list = new List<ITreeNode<T>>();
            if (LeftNode is not null)
            {
                list.Add(LeftNode);
            }
            if (RightNode is not null)
            {
                list.Add(RightNode);
            }
            return list.ToArray();
        }

        public void GetAllChildNonAlloc(ref List<ITreeNode<T>> results)
        {
            results = new List<ITreeNode<T>>(GetAllChild());
        }

        public void AddChild(ITreeNode<T> child)
        {
            if (LeftNode is null)
            {
                LeftNode = child;
                child.SetParent(this);
                return;
            }
            if (RightNode is null)
            {
                RightNode = child;
                child.SetParent(this);
                return;
            }
        }
        
        public void AddChild(int index, ITreeNode<T> child)
        {
            if (index == 0)
            {
                if (LeftNode is not null)
                {
                    LeftNode.SetParent(null);
                }
                LeftNode = child;
                child.SetParent(this);
                return;
            }
            if (index == 1)
            {
                if (RightNode is not null)
                {
                    RightNode.SetParent(null);
                }
                RightNode = child;
                child.SetParent(this);
                return;
            }
            CommonUtils.EditorLogError($"BinaryTreeNoe.AddChild: index 超出范围!  二叉树仅存在左右子节点(0:左子节点 1:右子节点))");
        }
        
        private ITreeNode<T> mParent;
        public ITreeNode<T> Parent
        {
            get
            {
                return mParent;
            }
            set
            {
                mParent = value;
            }
        }

        public T GetValue()
        {
            return Value;
        }

        public void SetParent(ITreeNode<T> parent)
        {
            Parent = parent;
        }

        public void RemoveChild(int index)
        {
            if (index == 0)
            {
                if (LeftNode is not null)
                {
                    LeftNode.SetParent(null);
                    LeftNode = null;
                }
                return;
            }
            if (index == 1)
            {
                if (RightNode is not null)
                {
                    RightNode.SetParent(null);
                    RightNode = null;
                }
                return;
            }
        }
        
        public void RemoveChild(string name)
        {
            if (LeftNode is not null && LeftNode.Name.Equals(name))
            {
                LeftNode.SetParent(null);
                LeftNode = null;
                return;
            }
            if (RightNode is not null && RightNode.Name.Equals(name))
            {
                RightNode.SetParent(null);
                RightNode = null;
                return;
            }
        }

        public void ClearChild()
        {
            LeftNode = null;
            RightNode = null;
        }
        
        public void DetachFromParent()
        {
            if (Parent != null)
                Parent.RemoveChild(Parent.GetChildIndex(this));
            Parent = null;
        }
        

    }
}