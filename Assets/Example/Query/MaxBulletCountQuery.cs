using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace ShootingEditor2D
{
    public class MaxBulletCountQuery : AbstractQuery<int>
    {
        private readonly string m_GunName;

        public MaxBulletCountQuery(string gunName)
        {
            m_GunName = gunName;
        }
        protected override int OnDo()
        {
            IGunConfigModel gunConfigModel = this.GetModel<IGunConfigModel>();
            GunConfigItem gunConfigItem = gunConfigModel.GetItemByName(m_GunName);
            return gunConfigItem.BulletMaxCount;
        }
    }
}

