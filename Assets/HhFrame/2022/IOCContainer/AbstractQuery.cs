using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HFrame2022
{
    public abstract class AbstractQuery<T> : IQuery<T>
    {
        public T Do()
        {
            return OnDo();
        }

        protected abstract T OnDo();

        private IArchitecture m_Architecture;
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return m_Architecture;
        }

        void ICanSetArchitecture.SetSetArchitecture(IArchitecture architecture)
        {
            m_Architecture = architecture;
        }
    }
}

