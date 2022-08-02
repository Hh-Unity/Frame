using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

namespace HFrame2022
{
    public interface IArchitecture
    {
        //提供一个获取Utility的API
        T GetUtility<T>() where T : class;
        T GetModel<T>() where T : class,IModel;
        T GetSystem<T>() where T : class, ISystem;
        void RegisterModel<T>(T model) where T : IModel;
        void  RegisterUtility<T>(T instance);
        void RegisterSystem<T>(T system) where T : ISystem;
        void SendCommand<T>() where T : ICommand, new();
        void SendCommand<T>(T command) where T : ICommand;
        void SendEvent<T>() where T : new();
        void SendEvent<T>(T e);
        IUnRegister RegisterEvent<T>(Action<T> onEvent);
        void UnRegisterEvent<T>(Action<T> onEvent);
        T SendQuery<T>(IQuery<T> query);
    }
}

