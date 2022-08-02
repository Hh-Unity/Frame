using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace ShootingEditor2D
{
    public class ReloadCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            GunInfo currentGun = this.GetSystem<IGunSystem>().CurrentGun;
            GunConfigItem gunConfigItem = this.GetModel<IGunConfigModel>().GetItemByName(currentGun.Name.Value);
            int needBulletCount = gunConfigItem.BulletMaxCount - currentGun.BulletCountInGun.Value;
            if (needBulletCount > 0)
            {
                if (currentGun.BulletCountOutGun.Value > 0)
                {
                    currentGun.State.Value = GunState.Reload;
                    this.GetSystem<ITimeSystem>().AddDelayTask(gunConfigItem.ReloadSeconds, () =>
                    {
                        if (currentGun.BulletCountOutGun.Value >= needBulletCount)
                        {
                            currentGun.BulletCountOutGun.Value -= needBulletCount;
                            currentGun.BulletCountInGun.Value += needBulletCount;
                        }
                        else
                        {
                            currentGun.BulletCountInGun.Value += currentGun.BulletCountOutGun.Value;
                            currentGun.BulletCountOutGun.Value = 0;
                        }
                        currentGun.State.Value = GunState.Idle;
                    });
                }
            }
        }
    }
}

