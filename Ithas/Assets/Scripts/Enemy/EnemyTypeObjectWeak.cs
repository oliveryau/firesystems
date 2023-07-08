using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class EnemyTypeObjectWeak : EnemyScript
    {
        private Animator animator;
        private CompletionBar completionBar;

        private void Start()
        {
            //animator = GetComponent<Animator>();
            completionBar = FindObjectOfType<CompletionBar>();

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
