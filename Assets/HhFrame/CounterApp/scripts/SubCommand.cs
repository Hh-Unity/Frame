using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace HhFrame.CounterApp
{
    public class SubCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            ICounterModel model = this.GetModel<ICounterModel>();
            model.Count.Value--;
        }
    }
}

