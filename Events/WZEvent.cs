using System;

namespace WhiteZhi
{
    public interface IWZEvent
    {
        IUnRegister Register(Action onEvent);
    }
   public class WZEvent : IWZEvent
   {
       private Action _onEvent = () => { };
        public IUnRegister Register(Action onEvent)
        {
            _onEvent += onEvent;
            return new CustomUnRegister(() => { UnRegister(onEvent); });
        }
        public void UnRegister(Action onEvent) => _onEvent -= onEvent;
        public void Trigger() => _onEvent?.Invoke(); 
        
    }
   
    public class WZEvent<T> : IWZEvent
    {
        private Action<T> _onEvent = e => { };
        
        public IUnRegister Register(Action<T> onEvent)
        {
            _onEvent += onEvent;
            return new CustomUnRegister(() => { UnRegister(onEvent); });
        }
        
        public void UnRegister(Action<T> onEvent) => _onEvent -= onEvent;
        public void Trigger(T e) => _onEvent?.Invoke(e);
        
        IUnRegister IWZEvent.Register(Action onEvent)
        {
            return Register(Action);
            void Action(T _) => onEvent();
        }
    }
    
    public class WZEvent<T, X> : IWZEvent
    {
        private Action<T, X> _onEvent = (t, x) => { };
        public IUnRegister Register(Action<T, X> onEvent)
        {
            _onEvent += onEvent;
            return new CustomUnRegister(() => { UnRegister(onEvent); });
        }
        public void UnRegister(Action<T, X> onEvent) => _onEvent -= onEvent;
        public void Trigger(T t, X x) => _onEvent?.Invoke(t, x);
        IUnRegister IWZEvent.Register(Action onEvent)
        {
            return Register(Action);
            void Action(T t, X x) => onEvent();
        }
    }
    public class WZEvent<T, X, Y> : IWZEvent
    {
        private Action<T, X, Y> _onEvent = (t, x, y) => { };
        public IUnRegister Register(Action<T, X, Y> onEvent)
        {
            _onEvent += onEvent;
            return new CustomUnRegister(() => { UnRegister(onEvent); });
        }
        public void UnRegister(Action<T, X, Y> onEvent) => _onEvent -= onEvent;
        public void Trigger(T t, X x, Y y) => _onEvent?.Invoke(t, x, y);
        IUnRegister IWZEvent.Register(Action onEvent)
        {
            return Register(Action);
            void Action(T t, X x, Y y) => onEvent();
        }
    }
    
    
}