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
            BulletCount = new BindableProperty<int>()
            {
                Value = 3,
            }
        };
    }
}

