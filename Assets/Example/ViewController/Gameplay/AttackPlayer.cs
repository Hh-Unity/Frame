using System;
using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace ShootingEditor2D
{
    public class AttackPlayer : MonoBehaviour,IController
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.CompareTag("Player"))
                this.SendCommand<HurtPlayerCommand>();
        }

        public IArchitecture GetArchitecture()
        {
            return ShootingEditor2D.Interface;
        }
    }
}

