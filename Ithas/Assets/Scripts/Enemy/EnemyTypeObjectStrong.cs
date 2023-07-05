using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class EnemyTypeObjectStrong : EnemyScript
    {
        //private GameController gameController;
        private Animator animator;

        private void Start()
        {
            //animator = GetComponent<Animator>();

            enemyId = 98;
            ReadEnemyData();
            SetEnemyHpBar();

            ChangeState(EnemyState.idle);
        }

        private void Update()
        {
            //UpdateAnimations();
        }
    }

}
