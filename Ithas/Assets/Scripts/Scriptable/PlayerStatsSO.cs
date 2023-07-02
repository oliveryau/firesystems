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
            level = 1;
            hp = maxHp;
            currentExp = 0;
        }
    }
}

