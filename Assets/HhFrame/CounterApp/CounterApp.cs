using HFrame2022;
using UnityEngine;

namespace HhFrame.CounterApp
{
    public class CounterApp : Architecture<CounterApp>
    {
        protected override void Init()
        {
            RegisterModel<ICounterModel>(new CounterModel());
            RegisterUtility<IStorage>(new PlayerPrefsStorage());
        }
    }
}