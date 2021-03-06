using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace ShootingEditor2D
{
    public interface IGunSystem : ISystem
    {
        GunInfo CurrentGun { get; }
    }

    public class GunSystem : AbstractSystem, IGunSystem
    {
        protected override void OnInit()
        {
            
        }

        public GunInfo CurrentGun { get; } = new GunInfo()
        {
            // BulletCountInGame = new BindableProperty<int>()
            // {
            //     Value = 3,
            // },
            
            BulletCountInGun = new BindableProperty<int>()
            {
                Value = 3,
            },
            
            BulletCountOutGun = new BindableProperty<int>()
            {
                Value = 1,
            },
            
            Name = new BindableProperty<string>()
            {
                Value = "手枪",
            },
            
            State = new BindableProperty<GunState>()
            {
                Value = GunState.Idle
            }
        };
    }
}

