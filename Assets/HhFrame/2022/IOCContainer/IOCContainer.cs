using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace HFrame2022
{
    public class IOCContainer
    {
        public Dictionary<Type, object> mInstances = new Dictionary<Type, object>();

        public void Register<T>(T instance)
        {
            Type key = typeof(T);
            if (mInstances.ContainsKey(key))
                mInstances[key] = instance;
            else
                mInstances.Add(key,instance);
            
        }

        public T Get<T>() where T : class
        {
            Type key = typeof(T);
            object retObj;
            if (mInstances.TryGetValue(key, out retObj))
                return retObj as T;
            return null;
        }
    }
}

