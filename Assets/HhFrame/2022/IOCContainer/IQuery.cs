using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HFrame2022
{
    public interface IQuery<T> : IBelongToArchitecture, ICanSetArchitecture, ICanGetModel, ICanGetSystem, ICanGetUtility
    {
        T Do();
    }
}

