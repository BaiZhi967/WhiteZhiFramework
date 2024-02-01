using System;
using System.Collections.Generic;

namespace WhiteZhi
{
    /// <summary>
    /// 泛型优先队列
    /// 泛型需实现IComparable接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class  PriorityQueue<T> where T : IComparable
    {
        private List<T> list;

        public int Count
        {
            get
            {
                return list.Count;
            }
        }
        
        /// <summary>
        /// 是否为降序
        /// </summary>
        public readonly bool IsDescending;

        public PriorityQueue()
        {
            list = new List<T>();
        }

        public PriorityQueue(bool isDescending) : this()
        {
            IsDescending = false;
        }

        public PriorityQueue(int capacity) : this(capacity, false) { }
        
        public PriorityQueue(IEnumerable<T> collection) : this(collection, false) { }
        
        public PriorityQueue(int capacity, bool isDescending)
        {
            list = new List<T>(capacity);
            IsDescending = isDescending;
        }
        public PriorityQueue(IEnumerable<T> collection, bool isdesc) : this()
        {
            IsDescending = isdesc;
            foreach (var item in collection)
            {
                Enqueue(item);
            }
        }
        
        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="x"></param>
        public void Enqueue(T x)
        {
            list.Add(x);
            int i = Count - 1;

            while (i > 0)
            {
                int p = (i - 1) / 2;
                if ((IsDescending ? -1 : 1) * list[p].CompareTo(x) <= 0) 
                    break;

                list[i] = list[p];
                i = p;
            }

            if (Count > 0) list[i] = x;
        }
        
        /// <summary>
        /// 出队
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            T target = Top();
            T root = list[Count - 1];
            list.RemoveAt(Count - 1);

            int i = 0;
            while (i * 2 + 1 < Count)
            {
                int a = i * 2 + 1;
                int b = i * 2 + 2;
                int c = b < Count && (IsDescending ? -1 : 1) * list[b].CompareTo(list[a]) < 0 ? b : a;

                if ((IsDescending ? -1 : 1) * list[c].CompareTo(root) >= 0) 
                    break;
                list[i] = list[c];
                i = c;
            }

            if (Count > 0) list[i] = root;
            return target;
        }
        
        /// <summary>
        /// 队首元素
        /// </summary>
        /// <returns>队首元素</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T Top()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("队列为空");
            }
            return list[0];
        }

        /// <summary>
        /// 清空队列
        /// </summary>
        public void Clear()
        {
            list.Clear();
        }
    }
}