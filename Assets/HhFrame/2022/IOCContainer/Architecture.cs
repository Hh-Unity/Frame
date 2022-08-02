using System;
using System.Collections;
using System.Collections.Generic;
using HhFrame.CounterApp;
using UnityEngine;
namespace HFrame2022
{
    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>, new()
    {
        private bool mInited = false;
        public static Action<T> OnRegisterPatch = architecture => { };
        private static T mArchitecture;

        public static IArchitecture Interface
        {
            get
            {
                if (mArchitecture == null)
                {
                    MakeSureArchitecture();
                }

                return mArchitecture;
            }
        }
        
        private static void MakeSureArchitecture()
        {
            if (mArchitecture == null)
            {
                mArchitecture = new T();
                mArchitecture.Init();
                OnRegisterPatch?.Invoke(mArchitecture);
                foreach (IModel model in mArchitecture.models)
                {
                    model.Init();
                }
                mArchitecture.models.Clear();
                foreach (ISystem system in mArchitecture.systems)
                {
                    system.Init();
                }
                
                mArchitecture.mInited = true;
            }
        }

        private IOCContainer mIOCContainer = new IOCContainer();

        protected abstract void Init();

        public static void Register<T>(T instance)
        {
            MakeSureArchitecture();
            mArchitecture.mIOCContainer.Register(instance);
        }

        public T GetModel<T>() where T : class,IModel
        {
            return mIOCContainer.Get<T>();
        }

        public T GetSystem<T>() where T : class, ISystem
        {
            return mIOCContainer.Get<T>();
        }
        
        private List<IModel> models = new List<IModel>();
        public void RegisterModel<T>(T model) where T : IModel
        {
            model.SetSetArchitecture(this);
            mIOCContainer.Register<T>(model);
            if (mInited)
                model.Init();
            else
                models.Add(model);
        }

        public void RegisterUtility<T>(T instance)
        {
            mIOCContainer.Register<T>(instance);
        }
        private List<ISystem> systems = new List<ISystem>();
        public void RegisterSystem<T>(T system) where T : ISystem
        {
            system.SetSetArchitecture(this);
            mIOCContainer.Register<T>(system);
            if (mInited)
                system.Init();
            else
                systems.Add(system);
        }

        public void SendCommand<T>() where T : ICommand, new()
        {
            ICommand command = new T();
            command.SetSetArchitecture(this);
            command.Execute();
        }

        public void SendCommand<T>(T command) where T : ICommand
        {
            command.SetSetArchitecture(this);
            command.Execute();
        }

        // public static T Get<T>() where T : class
        // {
        //     MakeSureArchitecture();
        //     return mArchitecture.mIOCContainer.Get<T>();
        // }

        public T GetUtility<T>() where T : class
        {
            return mIOCContainer.Get<T>();
        }

        private ITypeEvnetSystem mTypeEventSystem = new TypeEventSystem();
        public void SendEvent<T>() where T : new()
        {
            mTypeEventSystem.Send<T>();
        }

        public void SendEvent<T>(T e)
        {
            mTypeEventSystem.Send<T>(e);
        }

        public IUnRegister RegisterEvent<T>(Action<T> onEvent)
        {
            return mTypeEventSystem.Register<T>(onEvent);
        }

        public void UnRegisterEvent<T>(Action<T> onEvent)
        {
            mTypeEventSystem.UnRegister<T>(onEvent);
        }

        public T SendQuery<T>(IQuery<T> query)
        {
            query.SetSetArchitecture(this);
            return query.Do();
        }

        public static void Clear()
        {
            mArchitecture = null;
        }
    }
}

