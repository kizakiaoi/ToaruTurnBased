using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Unit
{
    public class PlayerUnitObject : UnitObject
    {
        override internal void Awake()
        {
            base.Awake();
            base.type = Type.Player;
        }
    }

}
