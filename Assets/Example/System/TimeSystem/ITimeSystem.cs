using System;
using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace ShootingEditor2D
{
    public interface ITimeSystem : ISystem
    {
        float CurrentSeconds { get; }
        void AddDelayTask(float seconds, Action onFinish);
    }

    public enum DelayTaskState
    {
        NoStart,
        Started,
        Finish,
    }

    public class DelayTask
    {
        public float Seconds { get; set; }
        public Action OnFinish { get; set; }
        public float StartSeconds { get; set; }
        public float FinishSeconds { get; set; }
        public DelayTaskState State { get; set; }
    }

    public class TimeSystem : AbstractSystem, ITimeSystem
    {
        public class TimeSystemUpdateBehaviour : MonoBehaviour
        {
            public event Action OnUpdate;

            private void Update()
            {
                OnUpdate?.Invoke();
            }
        }

        public float CurrentSeconds { get; private set; } = 0.0f;
        
        public void AddDelayTask(float seconds, Action onFinish)
        {
            DelayTask delayTask = new DelayTask()
            {
                Seconds = seconds,
                OnFinish = onFinish,
                State = DelayTaskState.NoStart
            };
            m_DelayTasks.AddLast(new LinkedListNode<DelayTask>(delayTask));
        }
        protected override void OnInit()
        {
            GameObject updateBehaviourGameObj = new GameObject(nameof(TimeSystemUpdateBehaviour));
            UnityEngine.Object.DontDestroyOnLoad(updateBehaviourGameObj);
            TimeSystemUpdateBehaviour updateBehaviour = updateBehaviourGameObj.AddComponent<TimeSystemUpdateBehaviour>();
            updateBehaviour.OnUpdate += OnUpdate;
        }

        private LinkedList<DelayTask> m_DelayTasks = new LinkedList<DelayTask>();
        private void OnUpdate()
        {
            CurrentSeconds += Time.deltaTime;

            if (m_DelayTasks.Count > 0)
            {
                LinkedListNode<DelayTask> currentNode = m_DelayTasks.First;

                while (currentNode != null)
                {
                    DelayTask delayTask = currentNode.Value;
                    LinkedListNode<DelayTask> nextNode =  currentNode.Next;

                    if (delayTask.State == DelayTaskState.NoStart)
                    {
                        delayTask.State = DelayTaskState.Started;
                        delayTask.StartSeconds = CurrentSeconds;
                        delayTask.FinishSeconds = CurrentSeconds + delayTask.Seconds;
                    }
                    else if (delayTask.State == DelayTaskState.Started)
                    {
                        if (CurrentSeconds > delayTask.FinishSeconds)
                        {
                            delayTask.State = DelayTaskState.Finish;
                            delayTask.OnFinish.Invoke();
                            delayTask.OnFinish = null;
                            m_DelayTasks.Remove(currentNode);
                        }
                    }

                    currentNode = nextNode;
                }
            }
        }
        
        
    }
}

