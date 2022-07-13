using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace ShootingEditor2D
{
    public class ShootingEditor2D : Architecture<ShootingEditor2D>
    {
        protected override void Init()
        {   
            this.RegisterModel<IPlayerModel>(new PlayerModel());
            this.RegisterSystem<IStateSystem>(new StateSystem());
        }
    }

}
