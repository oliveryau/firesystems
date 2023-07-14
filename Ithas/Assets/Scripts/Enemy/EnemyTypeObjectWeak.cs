using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class EnemyTypeObjectWeak : EnemyScript
    {
        private CompletionBar completionBar;

        private void Start()
        {
            completionBar = FindObjectOfType<CompletionBar>();

            ChangeState(EnemyState.idle);
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
