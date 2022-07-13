using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HFrame2022
{
    public class BindableProperty<T> where T : IEquatable<T>
    {
        public T mValue;
        public Action<T> m_OnValueChanged = (v) => { };

        public T Value 
        {
            get
            {
                return mValue;
            }
            set
            {
                if (!mValue.Equals(value))
                {
                    mValue = value;
                    m_OnValueChanged?.Invoke(mValue);
                }
            }
        }

        public IUnRegister RegisterOnValueChanged(Action<T> onValueChanged)
        {
            m_OnValueChanged += onValueChanged;
            return new BindablePropertyUnRegister<T>()
            {
                BindableProperty = this,
                OnValueChanged = onValueChanged
            };
        }

        public void UnRegisterOnValueChanged(Action<T> onValueChanged)
        {
            m_OnValueChanged -= onValueChanged;
        }
    }

    public class BindablePropertyUnRegister<T> : IUnRegister where T : IEquatable<T>
    {
        public BindableProperty<T> BindableProperty { get; set; }
        public Action<T> OnValueChanged { get; set; }

        public void UnRegister()
        {
            BindableProperty.UnRegisterOnValueChanged(OnValueChanged);
        }
    }

}
