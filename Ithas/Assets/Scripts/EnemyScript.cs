using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    //base class for all enemy scripts to inherit
    public class EnemyScript : MonoBehaviour
    {
        private static EnemyScript instance;

        public static EnemyScript Instance
        {
            get { return instance; }
        }

        public virtual void Initialize(GameController gameController) { }

        [Header("Stats")]
        public string enemyName;
        public float hp;
        public float attack;
        public float moveSpeed;
        public float exp;

        [Header("State Machine")]
        public EnemyState currentState;

        public void ChangeState(EnemyState newState)
        {
            if (currentState != newState)
            {
                currentState = newState;
            }
        }
    }

    public enum EnemyState
    {
        idle,
        move,
        attack
    }
}
