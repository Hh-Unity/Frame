using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HFrame2022
{
    public interface IStateSystem : ISystem
    {
        BindableProperty<int> killCount { get; }
    }

    public class StateSystem : AbstractSystem, IStateSystem
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<int> killCount { get; } = new BindableProperty<int>()
        {
            Value = 0,
        };
    }
}

