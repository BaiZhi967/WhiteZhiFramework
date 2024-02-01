using System;
using System.Collections.Generic;

namespace WhiteZhi
{
    /// <summary>
    /// 事件处理容器
    /// </summary>
    public class EventHandler
    {
        #region 事件主体

        /// <summary>
        /// 事件Uid 用来在事件中心中做唯一标识使用
        /// </summary>
        public int Uid = -1;
        
        /// <summary>
        /// 事件列表
        /// </summary>
        private Dictionary<int, Delegate> _eventDic;
        
        

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="uid"></param>
        public EventHandler(int uid)
        {
            Uid = uid;
            _eventDic = new Dictionary<int, Delegate>();
        }


        /// <summary>
        /// 添加事件监听
        /// </summary>
        /// <param name="id">事件编号</param>
        /// <param name="wzEvent">事件</param>
        /// <exception cref="Exception">错误类型覆盖</exception>
        private void OnListenerAdding(int id,Delegate wzEvent)
        {
            if (!_eventDic.ContainsKey(id))
            {
                _eventDic.Add(id,null);
            }

            Delegate d = _eventDic[id];
            if (d != null && d.GetType() != wzEvent.GetType())
            {
                throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托，当前事件所对应的委托是{1}，要添加的委托类型为{2}", id, d.GetType(), wzEvent.GetType()));
            }
        }
        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="id">事件编号</param>
        /// <param name="wzEvent">事件</param>
        private void OnListenerRemoving(int id, Delegate wzEvent)
        {
            if (_eventDic.ContainsKey(id))
            {
                Delegate d = _eventDic[id];
                if (d == null)
                {
                    throw new Exception(string.Format("移除监听错误：事件{0}没有对应的委托", id));
                }
                else if (d.GetType() != wzEvent.GetType())
                {
                    throw new Exception(string.Format("移除监听错误：尝试为事件{0}移除不同类型的委托，当前委托类型为{1}，要移除的委托类型为{2}", id, d.GetType(), wzEvent.GetType()));
                }
            }
            else
            {
                throw new Exception(string.Format("移除监听错误：没有事件id{0}", id));
            }
        }
        private void OnListenerRemoved(int id)
        {
            if (_eventDic[id] == null)//如果事件已经被删除
            {
                _eventDic.Remove(id);
            }
        }

        #endregion

        #region 无参的事件

        /// <summary>
        /// 添加事件监听
        /// </summary>
        /// <param name="id">事件id</param>
        /// <param name="wzEvent">事件</param>
        public void AddListener(int id, WZEvent wzEvent)
        {
            OnListenerAdding(id,wzEvent);
            _eventDic[id] = (WZEvent)_eventDic[id] + wzEvent;
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="id">事件id</param>
        /// <param name="wzEvent">事件</param>
        public void RemoveListener(int id, WZEvent wzEvent)
        {
            OnListenerRemoving(id,wzEvent);
            _eventDic[id] = (WZEvent)_eventDic[id] - wzEvent;
            OnListenerRemoved(id);
        }

        /// <summary>
        /// 呼叫事件
        /// </summary>
        /// <param name="id">事件id</param>
        /// <exception cref="Exception">空事件报错</exception>
        public void Dispatch(int id)
        {
            Delegate d;
            if (_eventDic.TryGetValue(id,out d))
            {
                WZEvent wzEvent = d as WZEvent;
                if (wzEvent != null)
                {
                    wzEvent();
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", id));
                }
            }
        }

        #endregion
        
        #region 一个参的事件

        /// <summary>
        /// 添加事件监听
        /// </summary>
        /// <param name="id">事件id</param>
        /// <param name="wzEvent">事件</param>
        public void AddListener<T>(int id, WZEvent<T> wzEvent)
        {
            OnListenerAdding(id,wzEvent);
            _eventDic[id] = (WZEvent<T>)_eventDic[id] + wzEvent;
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="id">事件id</param>
        /// <param name="wzEvent">事件</param>
        public void RemoveListener<T>(int id, WZEvent<T> wzEvent)
        {
            OnListenerRemoving(id,wzEvent);
            _eventDic[id] = (WZEvent<T>)_eventDic[id] - wzEvent;
            OnListenerRemoved(id);
        }

        /// <summary>
        /// 呼叫事件
        /// </summary>
        /// <param name="id">事件id</param>
        /// <exception cref="Exception">空事件报错</exception>
        public void Dispatch<T>(int id,T arg)
        {
            Delegate d;
            if (_eventDic.TryGetValue(id,out d))
            {
                WZEvent<T> wzEvent = d as WZEvent<T>;
                if (wzEvent != null)
                {
                    wzEvent(arg);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", id));
                }
            }
        }

        #endregion
        
        #region 两个参的事件

        /// <summary>
        /// 添加事件监听
        /// </summary>
        /// <param name="id">事件id</param>
        /// <param name="wzEvent">事件</param>
        public void AddListener<T, X>(int id, WZEvent<T, X> wzEvent)
        {
            OnListenerAdding(id,wzEvent);
            _eventDic[id] = (WZEvent<T, X>)_eventDic[id] + wzEvent;
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="id">事件id</param>
        /// <param name="wzEvent">事件</param>
        public void RemoveListener<T, X>(int id, WZEvent<T, X> wzEvent)
        {
            OnListenerRemoving(id,wzEvent);
            _eventDic[id] = (WZEvent<T, X>)_eventDic[id] - wzEvent;
            OnListenerRemoved(id);
        }

        /// <summary>
        /// 呼叫事件
        /// </summary>
        /// <param name="id">事件id</param>
        /// <exception cref="Exception">空事件报错</exception>
        public void Dispatch<T, X>(int id,T arg1,X arg2)
        {
            Delegate d;
            if (_eventDic.TryGetValue(id,out d))
            {
                WZEvent<T, X> wzEvent = d as WZEvent<T, X>;
                if (wzEvent != null)
                {
                    wzEvent(arg1,arg2);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", id));
                }
            }
        }

        #endregion
        
        #region 三个参的事件

        /// <summary>
        /// 添加事件监听
        /// </summary>
        /// <param name="id">事件id</param>
        /// <param name="wzEvent">事件</param>
        public void AddListener<T, X, Y>(int id, WZEvent<T, X, Y> wzEvent)
        {
            OnListenerAdding(id,wzEvent);
            _eventDic[id] = (WZEvent<T, X, Y>)_eventDic[id] + wzEvent;
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="id">事件id</param>
        /// <param name="wzEvent">事件</param>
        public void RemoveListener<T, X, Y>(int id, WZEvent<T, X, Y> wzEvent)
        {
            OnListenerRemoving(id,wzEvent);
            _eventDic[id] = (WZEvent<T, X, Y>)_eventDic[id] - wzEvent;
            OnListenerRemoved(id);
        }

        /// <summary>
        /// 呼叫事件
        /// </summary>
        /// <param name="id">事件id</param>
        /// <exception cref="Exception">空事件报错</exception>
        public void Dispatch<T, X, Y>(int id,T arg1, X arg2, Y arg3)
        {
            Delegate d;
            if (_eventDic.TryGetValue(id,out d))
            {
                WZEvent<T, X, Y> wzEvent = d as WZEvent<T, X, Y>;
                if (wzEvent != null)
                {
                    wzEvent(arg1,arg2,arg3);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", id));
                }
            }
        }

        #endregion
        
        #region 四个参的事件

        /// <summary>
        /// 添加事件监听
        /// </summary>
        /// <param name="id">事件id</param>
        /// <param name="wzEvent">事件</param>
        public void AddListener<T, X, Y, Z>(int id, WZEvent<T, X, Y, Z> wzEvent)
        {
            OnListenerAdding(id,wzEvent);
            _eventDic[id] = (WZEvent<T, X, Y, Z>)_eventDic[id] + wzEvent;
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="id">事件id</param>
        /// <param name="wzEvent">事件</param>
        public void RemoveListener<T, X, Y, Z>(int id, WZEvent<T, X, Y, Z> wzEvent)
        {
            OnListenerRemoving(id,wzEvent);
            _eventDic[id] = (WZEvent<T, X, Y, Z>)_eventDic[id] - wzEvent;
            OnListenerRemoved(id);
        }

        /// <summary>
        /// 呼叫事件
        /// </summary>
        /// <param name="id">事件id</param>
        /// <exception cref="Exception">空事件报错</exception>
        public void Dispatch<T, X, Y, Z>(int id,T arg1, X arg2, Y arg3, Z arg4)
        {
            Delegate d;
            if (_eventDic.TryGetValue(id,out d))
            {
                WZEvent<T, X, Y, Z> wzEvent = d as WZEvent<T, X, Y, Z>;
                if (wzEvent != null)
                {
                    wzEvent(arg1,arg2,arg3,arg4);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", id));
                }
            }
        }

        #endregion
        
    }
}