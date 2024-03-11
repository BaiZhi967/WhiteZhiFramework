﻿using System;
using System.Collections.Generic;

namespace WhiteZhi
{
    public abstract class Pool<T> : IPool<T>
    {
        /// <summary>
        /// 存储相关数据的栈
        /// </summary>
        protected readonly Stack<T> mCacheStack = new Stack<T>();
        public int CurCount
        {
            get { return mCacheStack.Count;; }
        }
        protected IObjectFactory<T> mFactory;
        public void SetObjectFactory(IObjectFactory<T> factory)
        {
            mFactory = factory;
        }

        public void SetFactoryMethod(Func<T> factoryMethod)
        {
            mFactory = new CustomObjectFactory<T>(factoryMethod);
        }
        

        public void Clear(Action<T> onClearItem = null)
        {
            if (onClearItem != null)
            {
                foreach (var poolObject in mCacheStack)
                {
                    onClearItem(poolObject);
                }
            }
            
            mCacheStack.Clear();
        }
        
        /// <summary>
        /// 默认上限
        /// </summary>
        protected int mMaxCount = 12;

        public virtual T Allocate()
        {
            return mCacheStack.Count == 0
                ? mFactory.Create()
                : mCacheStack.Pop();
        }

        public abstract bool Recycle(T obj);
    }
}