using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace ShootingEditor2D
{
    public class PickGunCommand : AbstractCommand
    {
        private readonly string m_Name;
        private readonly int m_bulletInGun;
        private readonly int m_bulletOutGun;
        public PickGunCommand()
        {
        }
        
        public PickGunCommand(string name, int mBulletInGun, int mBulletOutGun)
        {
            m_Name = name;
            m_bulletInGun = mBulletInGun;
            m_bulletOutGun = mBulletOutGun;
        }
        protected override void OnExecute()
        {
            this.GetSystem<IGunSystem>().PickGun(m_Name, m_bulletInGun, m_bulletOutGun);
        }
    }

}
