using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class EnemyCharacterObject :  CharacterObject{
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
        public override void Death(Transform transform, CharacterObject murderer)
        {
            base.Death(transform, murderer);
            GiveDropItem(murderer);
            GiveExperience(murderer);
        }
        private void GiveExperience(CharacterObject targetCharacter)
        {
            targetCharacter.AddExperience(base.totalExp);
        }
        private void GiveDropItem(CharacterObject targetCharacter)
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


