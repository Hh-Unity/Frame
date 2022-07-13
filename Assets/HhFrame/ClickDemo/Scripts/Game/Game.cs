using System;
using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using HhFrame.ClickDemo;
using UnityEngine;

public class Game : MonoBehaviour,IController
{
    void Start()
    {
        this.RegisterEvent<GameStartEvent>(OnGameStart);
    }

    void OnGameStart(GameStartEvent e)
    {
        transform.Find("Enemies").gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        this.UnRegisterEvent<GameStartEvent>(OnGameStart);
    }

    public IArchitecture GetArchitecture()
    {
        return PointGame.Interface;
    }
}
