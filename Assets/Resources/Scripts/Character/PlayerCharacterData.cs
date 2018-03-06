using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class PlayerCharacterData : BaseData 
    {
        public PlayerCharacterData() : base()
        {
            base.type = Type.Player;
        }
    }
}


