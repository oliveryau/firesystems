//celine
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class NPC : MonoBehaviour
    {
        [HideInInspector] public int actorId;
        private bool playerInRange;

        [Header("Others")]
        public DialogueActivator trigger;
        public DialogueManager dialogueManager;

        void Update()
        {
            if (playerInRange && Input.GetKeyDown(KeyCode.E) && !dialogueManager.isTalking) //for dialogue is not engaged yet
            {
                Message[] filteredMessages = trigger.FilterMessagesByActorId(actorId); 
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
    }
}
