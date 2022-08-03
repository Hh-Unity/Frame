using System;
using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace ShootingEditor2D
{
    public class AttackPlayer : ShootingEditor2DController
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.CompareTag("Player"))
                this.SendCommand<HurtPlayerCommand>();
        }
        
    }
}

