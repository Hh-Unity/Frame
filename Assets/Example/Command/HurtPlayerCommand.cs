using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace ShootingEditor2D
{
    public class HurtPlayerCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            int hp = this.GetModel<IPlayerModel>().HP.Value;
            if (hp > 0)
                this.GetModel<IPlayerModel>().HP.Value--;
        }
    }
}

