using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
namespace HFrame2022
{
    public class Singleton<T> where T : class
    {
        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    ConstructorInfo[] ctors = typeof(T).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
                    ConstructorInfo ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                    if (ctor == null)
                    {
                        throw new Exception("Non-Public Constructor() not found in" + typeof(T));
                    }
                    mInstance = ctor.Invoke(null) as T;
                }
                return mInstance;
            }
        }

        private static T mInstance;
    }
}

