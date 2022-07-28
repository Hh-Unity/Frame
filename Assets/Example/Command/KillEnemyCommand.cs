using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace ShootingEditor2D
{
    public class KillEnemyCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetSystem<IStateSystem>().killCount.Value++;
            int randomIndex = Random.Range(0, 100);
            if (randomIndex < 80)
                this.GetSystem<IGunSystem>().CurrentGun.BulletCountInGun.Value += Random.Range(1, 4);
        }
    }

}
