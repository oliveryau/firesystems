using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    //base class for all player scripts to inherit
    public class PlayerScript : MonoBehaviour
    {
        public virtual void Initialize(GameController gameController) { }

        [HideInInspector] public PlayerState currentState;

        public void ChangeState(PlayerState newState)
        {
            if (currentState != newState)
            {
                currentState = newState;
            }
        }
    }

    public enum PlayerState
    {
        idle,
        move,
        attack
    }
}
