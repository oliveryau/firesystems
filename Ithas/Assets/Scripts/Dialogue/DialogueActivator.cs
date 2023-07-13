using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class DialogueActivator : MonoBehaviour
    {
        private bool playerInRange;
        public GameObject dialogue;
        public Message[] messages;
        public Actor[] actors;

        public void StartDialogue()
        {
            dialogue.SetActive(true);
            FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors);
        }

        public void EndDialogue()
        {
            dialogue.SetActive(false);
        }
    }
    [System.Serializable]
    public class Message
    {
        public int actorId;
        public string message;
    }
    [System.Serializable]
    public class Actor
    {
        public string name;
        public Sprite sprite;
    }
}
