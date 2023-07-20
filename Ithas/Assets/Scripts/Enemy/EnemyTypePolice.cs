//oliver
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class EnemyTypePolice : EnemyScript
    {
        private void Start()
        {
            player = GameObject.FindWithTag("Player").transform;

            ChangeState(EnemyState.idle); //set initial state
        }

        private void Update()
        {
            UpdateAnimationsAndMovementAttack();
        }
    }
}
