using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace ShootingEditor2D
{
    public enum GunState
    {
        Idle,
        Shooting,
        Reload,
        EmptyBullet,
        CoolDown
    }
    public class GunInfo
    {
        public BindableProperty<int> BulletCountInGame
        {
            get => BulletCountInGame;
            set => BulletCountInGame = value;
        }
        public BindableProperty<int> BulletCountInGun;
        public BindableProperty<int> BulletCountOutGun;
        public BindableProperty<string> Name;
        public BindableProperty<GunState> State;
    }
}

