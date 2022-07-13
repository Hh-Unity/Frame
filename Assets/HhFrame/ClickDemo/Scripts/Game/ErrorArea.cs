using System;
using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace  HhFrame.ClickDemo
{
    public class ErrorArea : MonoBehaviour,IController
    {
        private void OnMouseDown()
        {
            this.SendCommand<MissCommand>();
        }

        public IArchitecture GetArchitecture()
        {
            return PointGame.Interface;
        }
    }
}

