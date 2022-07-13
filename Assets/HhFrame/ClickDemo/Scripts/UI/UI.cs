using System;
using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace HhFrame.ClickDemo
{
    public class UI : MonoBehaviour,IController
    {
        // Start is called before the first frame update
        void Start()
        {
            this.RegisterEvent<GamePassEvent>(OnGamePass);
        }

        void OnGamePass(GamePassEvent e)
        {
            transform.Find("OverPanel").gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            this.UnRegisterEvent<GamePassEvent>(OnGamePass);
        }

        public IArchitecture GetArchitecture()
        {
            return PointGame.Interface;
        }
    }
}

