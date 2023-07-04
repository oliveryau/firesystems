using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    [CreateAssetMenu]
    public class PlayerStatsSO : ScriptableObject
    {
        public int level;
        public float hp;
        public float maxHp;
        public float movementSpeed;
        public float currentExp;
        public float maxExp;
        public float damage;
        public float attackRange;
        public float attackRate;

        public void ResetStats()
        {
            Debug.Log("Hi");
            level = 1;
            hp = 10;
            maxHp = 10;
            movementSpeed = 6;
            currentExp = 0;
            maxExp = 10;
            damage = 10;
            attackRange = 1;
            attackRate = 1;
        }
    }
}

