using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using HhFrame.CounterApp;
using UnityEngine;

namespace HhFrame.ClickDemo
{
    public interface IGameModel : IModel
    {
        BindableProperty<int> Count { get; }
        BindableProperty<int> Gold { get; }
        BindableProperty<int> Score { get; }
        BindableProperty<int> BestScore { get; }
    }
    public class GameModel : AbstractModel,IGameModel
    {
        public BindableProperty<int> Count { get; } = new BindableProperty<int>()
        {
            Value = 0
        };

        public BindableProperty<int> Gold { get; } = new BindableProperty<int>()
        {
            Value = 0
        };

        public BindableProperty<int> Score { get; } = new BindableProperty<int>()
        {
            Value = 0
        };

        public BindableProperty<int> BestScore { get; } = new BindableProperty<int>()
        {
            Value = 0
        };

        protected override void OnInit()
        {
            IStorage storage = this.GetUtility<IStorage>();
            BestScore.Value = storage.LoadInt(nameof(BestScore), 0);
            BestScore.m_OnValueChanged += (v) => storage.SaveInt(nameof(BestScore), v);
        }
    }

}
