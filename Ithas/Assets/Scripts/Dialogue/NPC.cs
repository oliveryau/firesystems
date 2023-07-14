using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class NPC : MonoBehaviour
    {
        public DialogueActivator trigger;
        public int actorId;
        private bool playerInRange;

        //boolean check
        public DialogueManager dialogueManager;
        void Update()
        {
            if (playerInRange && Input.GetKeyDown(KeyCode.E) && !dialogueManager.isTalking)
            {
                 
                Message[] filteredMessages = trigger.FilterMessagesByActorId(actorId); 
                Debug.Log(actorId);
                trigger.StartDialogue(filteredMessages);    
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
