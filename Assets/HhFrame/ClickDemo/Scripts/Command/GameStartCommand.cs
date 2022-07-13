using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace HhFrame.ClickDemo
{
    public class GameStartCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<GameStartEvent>();
        }
    }
}

