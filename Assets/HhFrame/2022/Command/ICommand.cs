using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HFrame2022
{
    public interface ICommand : IBelongToArchitecture,ICanSetArchitecture,ICanGetModel,ICanGetSystem,ICanGetUtility,ICanSendEvent,ICanSendCommand,ICanSendQuery
    {
        void Execute();
    }
}

