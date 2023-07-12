using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class NPC : MonoBehaviour
    {
        public DialogueActivator trigger;
        private bool playerInRange;

        //boolean check
        public DialogueManager dialogueManager;

        private void Start()
        {
            FindObjectOfType<DialogueManager>().OnDialogueEnd += EndDialogue;
        }

        void Update()
        {
            if (playerInRange && Input.GetKeyDown(KeyCode.E) && !dialogueManager.isTalking)
            {
                trigger.StartDialogue();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player") == true)
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

        private void EndDialogue()
        {
            trigger.EndDialogue();
        }
    }
}