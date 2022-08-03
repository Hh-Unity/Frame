using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HFrame2022;
using UnityEngine;

namespace ShootingEditor2D
{
    public interface IGunSystem : ISystem
    {
        GunInfo CurrentGun { get; }
        void PickGun(string name, int bulletCountInGun, int bulletCountOutGun);
        void ShiftGun();
        Queue<GunInfo> GunInfos { get; }
    }

    public class OnCurrentGunChanged
    {
        public string Name { get; set; }
    }

    public class GunSystem : AbstractSystem, IGunSystem
    {
        protected override void OnInit()
        {
            
        }

        private Queue<GunInfo> m_GunInfos = new Queue<GunInfo>();
        
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

        public void PickGun(string name, int bulletCountInGun, int bulletCountOutGun)
        {
            if (CurrentGun.Name.Value == name)
            {
            }
            else if (m_GunInfos.Any(info => info.Name.Value == name))
            {
                GunInfo gunInfo = m_GunInfos.First(info => info.Name.Value == name);
                gunInfo.BulletCountInGun.Value = bulletCountInGun;
                gunInfo.BulletCountOutGun.Value = bulletCountOutGun;
            }
            else
            {
                GunInfo currentGunInfo = new GunInfo()
                {
                    Name = new BindableProperty<string>()
                    {
                        Value = CurrentGun.Name.Value
                    },
                    BulletCountInGun = new BindableProperty<int>()
                    {
                        Value = CurrentGun.BulletCountInGun.Value
                    },
                    BulletCountOutGun = new BindableProperty<int>()
                    {
                        Value = CurrentGun.BulletCountOutGun.Value
                    },
                    State = new BindableProperty<GunState>()
                    {
                        Value = CurrentGun.State.Value
                    }
                };
                m_GunInfos.Enqueue(currentGunInfo);
                CurrentGun.Name.Value = name;
                CurrentGun.BulletCountInGun.Value = bulletCountInGun;
                CurrentGun.BulletCountOutGun.Value = bulletCountOutGun;
                CurrentGun.State.Value = GunState.Idle;
                
                this.SendEvent(new OnCurrentGunChanged()
                {
                    Name = name
                });
            }
        }

        public void ShiftGun()
        {
            if (m_GunInfos.Count > 0)
            {
                GunInfo nextGunInfo = m_GunInfos.Dequeue();
                GunInfo currentGunInfo = new GunInfo()
                {
                    Name = new BindableProperty<string>()
                    {
                        Value = CurrentGun.Name.Value
                    },
                    BulletCountInGun = new BindableProperty<int>()
                    {
                        Value = CurrentGun.BulletCountInGun.Value
                    },
                    BulletCountOutGun = new BindableProperty<int>()
                    {
                        Value = CurrentGun.BulletCountOutGun.Value
                    },
                    State = new BindableProperty<GunState>()
                    {
                        Value = CurrentGun.State.Value
                    }
                };
                m_GunInfos.Enqueue(currentGunInfo);
                CurrentGun.Name.Value = nextGunInfo.Name.Value;
                CurrentGun.BulletCountInGun.Value = nextGunInfo.BulletCountInGun.Value;
                CurrentGun.BulletCountOutGun.Value = nextGunInfo.BulletCountOutGun.Value;
                CurrentGun.State.Value = GunState.Idle;
                this.SendEvent(new OnCurrentGunChanged()
                {
                    Name = nextGunInfo.Name.Value
                });
            }
        }

        public Queue<GunInfo> GunInfos
        {
            get { return m_GunInfos; }
        }
    }
}

