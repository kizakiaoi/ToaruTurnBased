using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Character
{
    public class PlayerCharacterObject : CharacterObject
    {
        void Awake()
        {
            base.bd = new PlayerCharacterData();
        }
    }

}
