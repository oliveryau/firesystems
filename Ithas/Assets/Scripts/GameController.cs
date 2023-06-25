using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class GameController : MonoBehaviour
    {
        public InputHandler inputHandler;
        public GameObject player;

        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            if (playerScript != null)
            {
                playerScript.Initialize(this);
                inputHandler.SetInputReceiver((InputReceiver)playerScript); //cast playerScript as inputReceiver
            }
        }
    }
}
