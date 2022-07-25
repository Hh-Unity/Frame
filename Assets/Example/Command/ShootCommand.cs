using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace ShootingEditor2D
{
    public class ShootCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetSystem<IGunSystem>().CurrentGun.BulletCount.Value--;
        }
    }
}

