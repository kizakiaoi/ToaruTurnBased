using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Character
{
    public abstract　class CharacterObject : MonoBehaviour
    {
        internal BaseData bd;

        public int level;
        public int exp;
        public int baseMaxHP;
        public int baseCurrentHP;
        public int baseAttack;
        public int baseDefence;
        public int maxActionPoint;
        public int currentActionPoint;

        public int hit;
        public int evade;

        public bool hasMoved;
        public bool IsAlive;
        public bool IsSelected;

        internal virtual void Update()
        {
            UpdateData();
            CheckDeath();
        }
        /// <summary>
        /// for updating character's ui.
        /// </summary>
        internal virtual void UpdateData()
        {
            this.name = bd.name;
            this.exp = bd.exp;
            this.level = bd.level;
            this.baseMaxHP = bd.baseMaxHP;
            this.baseCurrentHP = bd.baseCurrentHP;
            this.baseAttack = bd.baseAttack;
            this.baseDefence = bd.baseDefence;
            this.maxActionPoint = bd.maxActionPoint;
            this.currentActionPoint = bd.currentActionPoint;
            this.hit = bd.hit;
            this.evade = bd.evade;
            this.hasMoved = bd.hasMoved;
            this.IsAlive = bd.IsAlive;

        }
        /// <summary>
        /// for checking character is still alive or not.
        /// </summary>
        void CheckDeath()
        {
            if (!bd.IsAlive)
            {
                //destroy this character if dead.
                Destroy(this.gameObject);
            }
        }
    }
}
