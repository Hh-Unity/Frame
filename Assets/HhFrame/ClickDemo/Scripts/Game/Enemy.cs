using System;
using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace HhFrame.ClickDemo
{
    public class Enemy : MonoBehaviour,IController
    {
        private void OnMouseDown()
        {
            Destroy(gameObject);
            GetArchitecture().SendCommand<KillEnemyCommand>();
        }

        public IArchitecture GetArchitecture()
        {
            return PointGame.Interface;
        }
    }
}
