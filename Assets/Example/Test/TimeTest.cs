using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingEditor2D
{
    public class TimeTest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(Time.time);
            ShootingEditor2D.Interface.GetSystem<ITimeSystem>().AddDelayTask(3, () =>
            {
                Debug.Log(Time.time);
            });
        }

    }
}

