using System;
using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace ShootingEditor2D
{
    public class Gun : MonoBehaviour,IController
    {
        private Bullet m_Bullet;
        private GunInfo m_gunInfo;
        void Start()
        {
            m_Bullet = transform.Find("bullet").GetComponent<Bullet>();
            m_gunInfo = this.GetSystem<IGunSystem>().CurrentGun;
        }

        public void Shoot()
        {
            if (m_gunInfo.BulletCountInGun.Value > 0 && m_gunInfo.State.Value == GunState.Idle)
            {
                Transform bullet = Instantiate(m_Bullet.transform, m_Bullet.transform.position, m_Bullet.transform.rotation);
                bullet.localScale = m_Bullet.transform.lossyScale;
                bullet.gameObject.SetActive(true);
                this.SendCommand<ShootCommand>();
            }
        }

        public IArchitecture GetArchitecture()
        {
            return ShootingEditor2D.Interface;
        }

        private void OnDestroy()
        {
            m_gunInfo = null;
        }
    }
}

