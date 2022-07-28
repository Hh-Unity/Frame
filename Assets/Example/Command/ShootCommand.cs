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
            IGunSystem gunSystem = this.GetSystem<IGunSystem>();
            gunSystem.CurrentGun.BulletCountInGun.Value--;
            gunSystem.CurrentGun.State.Value = GunState.Shooting;

            ITimeSystem timeSystem = this.GetSystem<ITimeSystem>();
            GunConfigItem configItem = this.GetModel<IGunConfigModel>().GetItemByName(gunSystem.CurrentGun.Name.Value);
            timeSystem.AddDelayTask(1 / configItem.Frequency, () =>
            {
                gunSystem.CurrentGun.State.Value = GunState.Idle;
            });
        }
    }
}

