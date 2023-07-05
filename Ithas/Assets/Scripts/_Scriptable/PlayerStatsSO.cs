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

        [Header("Initial Stats")]
        public int initialLevel;
        public float initialHp;
        public float initialMaxHp;
        public float initialMovementSpeed;
        public float initialCurrentExp;
        public float initialMaxExp;
        public float initialDamage;
        public float initialAttackRange;
        public float initialAttackRate;

        public void ResetStats()
        {
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

        public void ResetToInitialStats()
        {
            level = initialLevel;
            hp = initialHp;
            maxHp = initialMaxHp;
            movementSpeed = initialMovementSpeed;
            currentExp = initialCurrentExp;
            maxExp = initialMaxExp;
            damage = initialDamage;
            attackRange = initialAttackRange;
            attackRate = initialAttackRate;
        }
    }
}

