using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace ShootingEditor2D
{
    public class ShootingEditor2DController : MonoBehaviour,IController
    {
        public IArchitecture GetArchitecture()
        {
            return ShootingEditor2D.Interface;
        }
    }
}
