using System;
using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;
using UnityEngine.UI;

namespace HhFrame.CounterApp
{
    public interface ICounterModel : IModel
    {
        BindableProperty<int> Count { get; }
    }
    public class CounterModel : AbstractModel,ICounterModel
    {
        protected override void OnInit()
        {
            IStorage storage = this.GetUtility<IStorage>();
            Count.Value = storage.LoadInt("COUNTER_COUNT",0);
            Count.m_OnValueChanged += count =>
            {
                storage.SaveInt("COUNTER_COUNT",count);
            };
        }

        public BindableProperty<int> Count { get; } = new BindableProperty<int>()
        {
            Value = 0
        };

        
    }
    public class CounterViewController : MonoBehaviour,IController
    {
        private ICounterModel model;
        void Start()
        {
            model = this.GetModel<ICounterModel>();
            model.Count.m_OnValueChanged += OnCountChanged;
            transform.Find("btnAdd").GetComponent<Button>().onClick.AddListener(() =>
            {
                this.SendCommand<AddCommand>();
            });
            transform.Find("btnSub").GetComponent<Button>().onClick.AddListener(() =>
            {
                this.SendCommand<SubCommand>();
            });
            OnCountChanged(model.Count.Value);
        }

        void OnCountChanged(int count)
        {
            transform.Find("countText").GetComponent<Text>().text = count.ToString();
        }

        private void OnDestroy()
        {
            model.Count.m_OnValueChanged -= OnCountChanged;
        }
        
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }
}

