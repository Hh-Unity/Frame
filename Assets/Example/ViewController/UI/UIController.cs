using System;
using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace ShootingEditor2D
{
    public class UIController : MonoBehaviour,IController
    {
        private IStateSystem m_stateSystem;
        private IPlayerModel m_playerModel;
        private IGunSystem m_gunSystem;
        private int m_maxBulletCount;
        private void Awake()
        {
            m_stateSystem = this.GetSystem<IStateSystem>();
            m_playerModel = this.GetModel<IPlayerModel>();
            m_gunSystem = this.GetSystem<IGunSystem>();
            m_maxBulletCount = this.SendQuery(new MaxBulletCountQuery(m_gunSystem.CurrentGun.Name.Value));
            this.RegisterEvent<OnCurrentGunChanged>(e =>
            {
                m_maxBulletCount = this.SendQuery(new MaxBulletCountQuery(e.Name));
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private readonly Lazy<GUIStyle> mLabelStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.label)
        {
            fontSize = 40,
        });

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 300, 100), $"生命:{m_playerModel.HP.Value}/3", mLabelStyle.Value);
            GUI.Label(new Rect(10, 60, 300, 100), $"枪内子弹:{m_gunSystem.CurrentGun.BulletCountInGun.Value}/{m_maxBulletCount}", mLabelStyle.Value);
            GUI.Label(new Rect(10, 110, 300, 100), $"枪外子弹:{m_gunSystem.CurrentGun.BulletCountOutGun.Value}", mLabelStyle.Value);
            GUI.Label(new Rect(10, 160, 300, 100), $"枪械名称:{m_gunSystem.CurrentGun.Name.Value}", mLabelStyle.Value);
            GUI.Label(new Rect(10, 210, 300, 100), $"枪械状态:{m_gunSystem.CurrentGun.State.Value}", mLabelStyle.Value);
            GUI.Label(new Rect(Screen.width - 10 - 300, 10, 300, 100), $"击杀数量:{m_stateSystem.killCount.Value}", mLabelStyle.Value);
        }

        public IArchitecture GetArchitecture()
        {
            return ShootingEditor2D.Interface;
        }
    }
}

