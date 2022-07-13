using System;
using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEditor;
using UnityEngine;

namespace HhFrame.CounterApp
{
    public class EditorCounterApp : EditorWindow,IController
    {
        [MenuItem("EditorCounterApp/Open")]
        static void Open()
        {
            CounterApp.Clear();
            CounterApp.OnRegisterPatch += app =>
            {
                app.RegisterUtility<IStorage>(new EditorPrefsStorage());
            };
             var editorCounterApp = GetWindow<EditorCounterApp>();
             editorCounterApp.name = nameof(EditorCounterApp);
             editorCounterApp.position = new Rect(100, 100, 400, 600);
             editorCounterApp.Show();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("+"))
            {
                GetArchitecture().SendCommand<AddCommand>();
            }
            
            // ICounterModel model = CounterApp.Get<ICounterModel>();
            ICounterModel model = this.GetModel<ICounterModel>();
            GUILayout.Label(model.Count.Value.ToString());
            if (GUILayout.Button("-"))
            {
                GetArchitecture().SendCommand<SubCommand>();
            }
        }

        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }
}

