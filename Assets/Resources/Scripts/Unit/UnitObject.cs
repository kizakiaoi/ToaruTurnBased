using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Unit
{
    public abstract　class UnitObject : MonoBehaviour
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

        protected List<IItem> inventory;

        public int hit;
        public int evade;

        public Type type;
        private bool hasMoved;
        public delegate void MoveStateChangeHandler();
        public MoveStateChangeHandler OnMoveStateChange;

        public delegate void DeathEventHandler(Transform transform, UnitObject murderer);
        public DeathEventHandler OnDeath;

        public delegate void InventoryChangeEvent();
        public InventoryChangeEvent OnInventoryChange;

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

            inventory = new List<IItem>();

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
        /// adding experience and leveling up.
        /// </summary>
        /// <param name="amount"></param>
        public void AddExperience(int amount)
        {
            if(amount >= 0)
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

            else
            {
                Debug.Log("no experience set");
            }

        }

        //inventory related
        /// <summary>
        /// add item to inventory;
        /// </summary>
        /// <param name="item"></param>
        public void AddInventory(IItem item)
        {
            if (item != null)
            {
                inventory.Add(item);
                if(OnInventoryChange != null)
                {
                    OnInventoryChange();
                }

                Debug.Log(item.Name + " has been added");
            }
        }
        /// <summary>
        /// add List of item to inventory;
        /// </summary>
        /// <param name="itemList"></param>
        public void AddInventory(List<IItem> itemList)
        {
            foreach(var item in itemList)
            {
                AddInventory(item);
            }
        }
        /// <summary>
        /// remove item to inventory;
        /// </summary>
        /// <param name="item"></param>
        public void RemoveInventory(IItem item)
        {
            if(item != null)
            {
                if (inventory.Contains(item))
                {
                    inventory.Remove(item);
                    if(OnInventoryChange!= null)
                    {
                        OnInventoryChange();
                    }
                }

            }
        }
        /// <summary>
        /// return inventory
        /// </summary>
        /// <returns></returns>
        public List<IItem> GetInventory()
        {
            if (inventory != null)
            {
                return inventory;
            }
            else
            {
                Debug.Log("no inventory set");

                return null;
            }
        }
        /// <summary>
        /// return ItemCount
        /// </summary>
        /// <returns></returns>
        public int GetInventoryNum()
        {
            return inventory.Count;
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
        public virtual void TakeDamage(int amount , UnitObject damageDealer)
        {
            if (amount >= 0)
            {
                if (baseCurrentHP - amount <= 0)
                {
                    baseCurrentHP = 0;
                    Debug.Log(this.name + " got:" + amount + "damage.");
                    Death(this.transform, damageDealer);
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
        /// <summary>
        /// usual death action.
        /// </summary>
        public virtual void Death()
        {
            if (OnDeath != null)
            {
                OnDeath(this.transform, this);
            }
            IsAlive = false;
            Debug.Log(this.name + " is dead.");
            Destroy(this.gameObject);
        }
        /// <summary>
        /// usual death without Damage Source
        /// </summary>
        /// <param name="transform"></param>
        public virtual void Death(Transform transform)
        {
            if (OnDeath != null)
            {
                OnDeath(this.transform, this);
            }
            IsAlive = false;
            Debug.Log(this.name + " is dead on : " + transform);
            Destroy(this.gameObject);
        }
        /// <summary>
        /// death with Damage Source
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="murderer"></param>
        public virtual void Death(Transform transform, UnitObject murderer)
        {
            Debug.Log(murderer.name + " has killed " + this.name);

            if (OnDeath != null)
            {
                OnDeath(this.transform, murderer);
            }
            IsAlive = false;
            Debug.Log(this.name + " is dead on : " + transform);
            Destroy(this.gameObject);
        }
    }
}
