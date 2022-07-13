using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace HhFrame.ClickDemo
{
    public class KillEnemyCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            IGameModel model = this.GetModel<IGameModel>();
            model.Count.Value++;
            this.SendEvent<KillEnemyEvent>();
            if (model.Count.Value == 10)
                this.SendEvent<GamePassEvent>();
        }
    }
}

