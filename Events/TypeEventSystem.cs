using System;
using System.Collections.Generic;

namespace WhiteZhi
{
    public interface IUnRegister
    {
        void UnRegister();
    }

    public interface IUnRegisterList
    {
        List<IUnRegister> UnregisterList { get; }
    }

    public static class IUnRegisterListExtension
    {
        public static void AddToUnregisterList(this IUnRegister self, IUnRegisterList unRegisterList) =>
            unRegisterList.UnregisterList.Add(self);

        public static void UnRegisterAll(this IUnRegisterList self)
        {
            foreach (var unRegister in self.UnregisterList)
            {
                unRegister.UnRegister();
            }

            self.UnregisterList.Clear();
        }
    }

    public struct CustomUnRegister : IUnRegister
    {
        private Action OnUnRegister { get; set; }
        public CustomUnRegister(Action onUnRegister) => OnUnRegister = onUnRegister;

        public void UnRegister()
        {
            OnUnRegister.Invoke();
            OnUnRegister = null;
        }
    }
#if UNITY_5_6_OR_NEWER
    public abstract class UnRegisterTrigger : UnityEngine.MonoBehaviour
    {
        private readonly HashSet<IUnRegister> mUnRegisters = new HashSet<IUnRegister>();

        public void AddUnRegister(IUnRegister unRegister) => mUnRegisters.Add(unRegister);

        public void RemoveUnRegister(IUnRegister unRegister) => mUnRegisters.Remove(unRegister);

        public void UnRegister()
        {
            foreach (var unRegister in mUnRegisters)
            {
                unRegister.UnRegister();
            }

            mUnRegisters.Clear();
        }
    }

    public class UnRegisterOnDestroyTrigger : UnRegisterTrigger
    {
        private void OnDestroy()
        {
            UnRegister();
        }
    }

    public class UnRegisterOnDisableTrigger : UnRegisterTrigger
    {
        private void OnDisable()
        {
            UnRegister();
        }
    }
#endif

    public static class UnRegisterExtension
    {
#if UNITY_5_6_OR_NEWER
        public static IUnRegister UnRegisterWhenGameObjectDestroyed(this IUnRegister unRegister,
            UnityEngine.GameObject gameObject)
        {
            var trigger = gameObject.GetComponent<UnRegisterOnDestroyTrigger>();

            if (!trigger)
            {
                trigger = gameObject.AddComponent<UnRegisterOnDestroyTrigger>();
            }

            trigger.AddUnRegister(unRegister);

            return unRegister;
        }

        public static IUnRegister UnRegisterWhenGameObjectDestroyed<T>(this IUnRegister self, T component)
            where T : UnityEngine.Component =>
            self.UnRegisterWhenGameObjectDestroyed(component.gameObject);

        public static IUnRegister UnRegisterWhenDisabled<T>(this IUnRegister self, T component)
            where T : UnityEngine.Component =>
            self.UnRegisterWhenDisabled(component.gameObject);

        public static IUnRegister UnRegisterWhenDisabled(this IUnRegister unRegister,
            UnityEngine.GameObject gameObject)
        {
            var trigger = gameObject.GetComponent<UnRegisterOnDisableTrigger>();

            if (!trigger)
            {
                trigger = gameObject.AddComponent<UnRegisterOnDisableTrigger>();
            }

            trigger.AddUnRegister(unRegister);

            return unRegister;
        }
#endif
    }
    public class TypeEventSystem
    {
        private readonly EventHandler mEvents = new EventHandler();

        public static readonly TypeEventSystem Global = new TypeEventSystem();

        public void Send<T>() where T : new() => mEvents.GetEvent<WZEvent<T>>()?.Trigger(new T());

        public void Send<T>(T e) => mEvents.GetEvent<WZEvent<T>>()?.Trigger(e);

        public IUnRegister Register<T>(Action<T> onEvent) => mEvents.GetOrAddEvent<WZEvent<T>>().Register(onEvent);

        public void UnRegister<T>(Action<T> onEvent)
        {
            var e = mEvents.GetEvent<WZEvent<T>>();
            e?.UnRegister(onEvent);
        }
    }
}