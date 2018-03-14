using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class EnemyUnitObject : UnitObject
    {
        internal override void Awake()
        {
            base.Awake();
            //setting the character's type to enemy.
            base.type = Type.Enemy;
        }
        /// <summary>
        /// give experiance and drop item to the murderer.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="murderer"></param>
        public override void Death(Transform transform, UnitObject murderer)
        {
            base.Death(transform, murderer);
            GiveDropItem(murderer);
            GiveExperience(murderer);
        }
        private void GiveExperience(UnitObject targetCharacter)
        {
            targetCharacter.AddExperience(base.totalExp);
        }
        private void GiveDropItem(UnitObject targetCharacter)
        {
            if (targetCharacter.GetInventory() != null && this.GetInventory() != null)
            {
                targetCharacter.AddInventory(this.GetInventory());
            }
            else
            {
                Debug.Log("character's inventory is null");
            }
        }
    }
}


