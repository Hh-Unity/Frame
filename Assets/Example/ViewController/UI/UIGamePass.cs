using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootingEditor2D
{
    public class UIGamePass : MonoBehaviour
    {
        private Lazy<GUIStyle> m_lableStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.label)
        {
            fontSize = 80,
            alignment = TextAnchor.MiddleCenter,
        });

        private Lazy<GUIStyle> m_buttonStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.button)
        {
            fontSize = 40,
            alignment = TextAnchor.MiddleCenter,
        });

        private void OnGUI()
        {
            int labelWidth = 400;
            int labelHeight = 100;
            Vector2 labelPos = new Vector2(Screen.width - labelWidth, Screen.height - labelHeight) * 0.5f;
            Vector2 labelSize = new Vector2(labelWidth, labelHeight);
            Rect labelRect = new Rect(labelPos, labelSize);
            GUI.Label(labelRect, "游戏通关", m_lableStyle.Value);

            int btnWidth = 200;
            int btnHeight = 100;
            Vector2 btnPos = new Vector2(Screen.width - btnWidth, Screen.height - btnHeight + 300) * 0.5f;
            Vector2 btnSize = new Vector2(btnWidth, btnHeight);
            Rect btnRect = new Rect(btnPos, btnSize);
            if ( GUI.Button(btnRect, "回到首页", m_buttonStyle.Value))
            {
                SceneManager.LoadScene("GameStart");
            }
        }
    }
}

