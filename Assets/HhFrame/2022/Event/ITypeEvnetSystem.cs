using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HFrame2022
{
    public interface ITypeEvnetSystem
    {
        void Send<T>() where T: new();
        void Send<T>(T t);
        IUnRegister Register<T>(Action<T> onEvent);
        void UnRegister<T>(Action<T> onEvent);
    }

    public interface IUnRegister
    {
        void UnRegister();
    }

    public class TypeEventSystemUnRegister<T> : IUnRegister
    {
        public ITypeEvnetSystem TypeEvnetSystem { get; set; }
        public Action<T> OnEvent { get; set; }
        public void UnRegister()
        {
            TypeEvnetSystem.UnRegister(OnEvent);
            TypeEvnetSystem = null;
            OnEvent = null;
        }
    }

    public class UnRegisterOnDestroyTrigger : MonoBehaviour
    {
        private HashSet<IUnRegister> mUnRegisters = new HashSet<IUnRegister>();

        public void AddUnRegister(IUnRegister unRegister)
        {
            mUnRegisters.Add(unRegister);
        }

        private void OnDestroy()
        {
            foreach (var mUnRegister in mUnRegisters)
            {
                mUnRegister.UnRegister();
            }
            mUnRegisters.Clear();
        }
    }

    public static class UnRegisterExtension
    {
        public static void UnRegisterWhenGameObjectDestroyed(this IUnRegister unRegister,GameObject gameObject)
        {
            UnRegisterOnDestroyTrigger trigger = gameObject.GetComponent<UnRegisterOnDestroyTrigger>();
            if (!trigger)
                trigger = gameObject.AddComponent<UnRegisterOnDestroyTrigger>();
            trigger.AddUnRegister(unRegister);
        }
    }

    public class TypeEventSystem : ITypeEvnetSystem
    {
        interface IRegistrations
        {
            
        }

        class Registrations<T> : IRegistrations
        {
            public Action<T> OnEvnet = obj => { };
        }

        private Dictionary<Type, IRegistrations> mEventRegistrationsMap = new Dictionary<Type, IRegistrations>();
        public void Send<T>() where T : new()
        {
            T e = new T();
            Send<T>(e);
        }

        public void Send<T>(T e)
        {
            Type type = typeof(T);
            IRegistrations registrations = null;
            if (mEventRegistrationsMap.TryGetValue(type,out registrations))
            {
                (registrations as Registrations<T>)?.OnEvnet.Invoke(e);
            }
        }

        public IUnRegister Register<T>(Action<T> onEvent)
        {
            Type type = typeof(T);
            IRegistrations registrations = null;
            if (mEventRegistrationsMap.TryGetValue(type,out registrations))
            {
            }
            else
            {
                registrations = new Registrations<T>();
                mEventRegistrationsMap.Add(type,registrations);
            }

            (registrations as Registrations<T>).OnEvnet += onEvent;
            return new TypeEventSystemUnRegister<T>()
            {
                OnEvent = onEvent,
                TypeEvnetSystem = this
            };
        }

        public void UnRegister<T>(Action<T> onEvent)
        {
            Type type = typeof(T);
            IRegistrations registrations = null;
            if (mEventRegistrationsMap.TryGetValue(type,out registrations))
            {
                (registrations as Registrations<T>).OnEvnet -= onEvent;
            }
        }
    }
}

