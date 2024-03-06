using System;
using System.Collections.Generic;

namespace WhiteZhi
{
    public class WZEvents
    {
        private static readonly WZEvents mGlobalEvents = new WZEvents();

        public static T Get<T>() where T : IWZEvent => mGlobalEvents.GetEvent<T>();

        public static void Register<T>() where T : IWZEvent, new() => mGlobalEvents.AddEvent<T>();

        private readonly Dictionary<Type, IWZEvent> mTypeEvents = new Dictionary<Type, IWZEvent>();

        public void AddEvent<T>() where T : IWZEvent, new() => mTypeEvents.Add(typeof(T), new T());

        public T GetEvent<T>() where T : IWZEvent
        {
            return mTypeEvents.TryGetValue(typeof(T), out var e) ? (T)e : default;
        }

        public T GetOrAddEvent<T>() where T : IWZEvent, new()
        {
            var eType = typeof(T);
            if (mTypeEvents.TryGetValue(eType, out var e))
            {
                return (T)e;
            }

            var t = new T();
            mTypeEvents.Add(eType, t);
            return t;
        }
    }
}