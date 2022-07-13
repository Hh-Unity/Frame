using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace HhFrame.ClickDemo
{
    public class MissCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<MissEvent>();
        }
    }
}

