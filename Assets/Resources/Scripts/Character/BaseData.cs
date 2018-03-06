using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    [System.Serializable]
    public abstract class BaseData
    {
        public string name;
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
        public bool hasMoved;
        public bool IsAlive;

        public enum Type
        {
            Player,
            Enemy,
            Other,
        }
        public BaseData()
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
            if(baseCurrentHP <= 0)
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

            if(total > 100)
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
            //add some status to the character;
        }
        /// <summary>
        /// repeat several LevelUps.
        /// </summary>
        /// <param name="amount"></param>
        public void LevelUp(int amount)
        {
            for(var i = 0; amount <= i; i++)
            {
                LevelUp();
            }
        }
    }
}