using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Character
{
    public class PlayerCharacterObject : CharacterObject
    {
        override internal void Awake()
        {
            base.Awake();
            base.type = Type.Player;
        }
    }

}
