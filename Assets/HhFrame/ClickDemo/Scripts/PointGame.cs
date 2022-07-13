using HFrame2022;
using HhFrame.CounterApp;
using UnityEngine;

namespace HhFrame.ClickDemo
{
    public class PointGame : Architecture<PointGame>
    {
        protected override void Init()
        {
            RegisterSystem<ISystem>(new ScoreSystem());
            RegisterModel<IGameModel>(new GameModel());
            RegisterUtility<IStorage>(new PlayerPrefsStorage());
        }
        
    }
}