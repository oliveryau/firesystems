using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class DialogueActivator : MonoBehaviour
    {
        [SerializeField]
        private GameObject dialogueManager;
        //private Rigidbody2D rb;
        private bool playerInRange;
        public Dialogue dialogue;

        void Update()
        {
            if (playerInRange && Input.GetKeyDown(KeyCode.E))
            {
                dialogueManager.SetActive(true);
                dialogue.StartDialogue();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                playerInRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                playerInRange = false;
            }
        }
    }

}
