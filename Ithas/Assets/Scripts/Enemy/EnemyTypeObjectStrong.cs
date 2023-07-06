using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class EnemyTypeObjectStrong : EnemyScript
    {
        //private GameController gameController;
        private Animator animator;
        private CompletionBar completionBar;

        private void Start()
        {
            //animator = GetComponent<Animator>();
            completionBar = FindObjectOfType<CompletionBar>();

            enemyId = 98;
            ReadEnemyData();
            SetEnemyHpBar();

            ChangeState(EnemyState.idle);
        }

        private void Update()
        {
            //UpdateAnimations();
        }

        private void OnDestroy()
        {
            if (completionBar != null)
            {
                completionBar.ObjectDestroyed();
            }
        }
    }

}
