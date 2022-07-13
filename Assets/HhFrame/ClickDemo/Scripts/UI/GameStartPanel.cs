using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;
using UnityEngine.UI;

namespace  HhFrame.ClickDemo
{
    public class GameStartPanel : MonoBehaviour,IController
    {
        public GameObject Enemies;
        void Start()
        {
            transform.Find("btn_Click").GetComponent<Button>().onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                GetArchitecture().SendCommand<GameStartCommand>();
            });
        }

        public IArchitecture GetArchitecture()
        {
            return PointGame.Interface;
        }
    }
}

