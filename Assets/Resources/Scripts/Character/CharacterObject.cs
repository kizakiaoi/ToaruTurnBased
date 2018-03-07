using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Character
{
    public abstract　class CharacterObject : MonoBehaviour
    {
        public int level;
        public int totalExp;
        public int exp;
        public int baseMaxHP;
        public int baseCurrentHP;
        public int baseAttack;
        public int baseDefence;
        public int maxActionPoint;
        public int currentActionPoint;

        public int hit;
        public int evade;

        public Type type;
        private bool hasMoved;
        public delegate void MoveStateChangeHandler();
        public MoveStateChangeHandler OnMoveStateChange;
        public bool IsAlive = true;

        public enum Type
        {
            Player,
            Enemy,
            Other,
        }

        internal virtual void Awake()
        {
            name = "default";
            level = 1;
            totalExp = 0;
            exp = 0;

            baseMaxHP = 10;
            baseCurrentHP = baseMaxHP;
            baseAttack = 5;
            baseDefence = 5;

            maxActionPoint = 2;
            currentActionPoint = maxActionPoint;

            hit = 5;
            evade = 5;

            type = Type.Other;

            hasMoved = false;
            IsAlive = true;
        }
        /// <summary>
        /// check for character's health.
        /// </summary>
        public void Die()
        {
            if (baseCurrentHP <= 0)
            {
                IsAlive = false;
            }
        }
        /// <summary>
        /// adding experience and leveling up.
        /// </summary>
        /// <param name="amount"></param>
        public void AddExperience(int amount)
        {
            var total = exp + amount;
            //adding it to the total exp;
            totalExp += amount;

            if (total > 100)
            {
                while (total >= 100)
                {
                    LevelUp();
                    total -= 100;
                }
            }

            else
            {
                exp += amount;
            }
        }
        /// <summary>
        /// leveling up.
        /// </summary>
        protected void LevelUp()
        {
            level++;
            Debug.Log("levelup");
            //do some effect with the character;
            HPFullRecovery();
            //add some status to the character;
        }
        /// <summary>
        /// repeat several LevelUps.
        /// </summary>
        /// <param name="amount"></param>
        public void LevelUp(int amount)
        {
            for (var i = 0; amount <= i; i++)
            {
                LevelUp();
            }
        }
        /// <summary>
        /// recover full hp or uses when character's HP when leveling up.
        /// </summary>
        protected void HPFullRecovery()
        {
            this.baseCurrentHP = this.baseMaxHP;
            Debug.Log("hp has be restored");
        }
        /// <summary>
        /// for healing hp.
        /// </summary>
        /// <param name="amount"></param>
        public void Heal(int amount)
        {
            //negative check.
            if (amount >= 0)
            {
                if (baseCurrentHP + amount < baseMaxHP)
                {
                    baseCurrentHP += amount;
                }

                else
                {
                    baseCurrentHP = baseMaxHP;
                }
            }
        }
        /// <summary>
        /// use for solving damage.
        /// </summary>
        /// <param name="amount"></param>
        public void TakeDamage(int amount)
        {
            if (amount >= 0)
            {
                if (baseCurrentHP - amount <= 0)
                {
                    baseCurrentHP = 0;
                    this.Die();
                    Debug.Log(this.name + " got:" + amount + "damage.");
                    Debug.Log(this.name + " is dead.");
                }

                else
                {
                    baseCurrentHP -= amount;
                    Debug.Log(this.name + " got:" + amount + "damage.");
                }
            }
        }
        /// <summary>
        /// reset character's action at beginning of the turn.
        /// </summary>
        public void ResetCharacterActionPoint()
        {
            this.currentActionPoint = this.maxActionPoint;
            SetMoveStateTo(false);
            Debug.Log("action point is restored");
        }
        /// <summary>
        /// use when changing character's movement refreshment
        /// </summary>
        /// <param name="movementstate"></param>
        public void SetMoveStateTo(bool movementstate)
        {
            this.hasMoved = movementstate;
            if (this.OnMoveStateChange != null)
            {
                OnMoveStateChange();
            }
        }
        /// <summary>
        /// get movestate from the character by bool;
        /// </summary>
        public bool GetMoveState()
        {
            return this.hasMoved;
        }
        internal virtual void Update()
        {
        }
        /// <summary>
        /// for checking character is still alive or not.
        /// </summary>
        void CheckDeath()
        {
            if (!IsAlive)
            {
                //destroy this character if dead.
                Destroy(this.gameObject);
            }
        }
    }
}
