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

        //void Update()
        //{
        //    if (playerInRange && Input.GetKeyDown(KeyCode.E))
        //    {
        //        StartDialogue();
        //    }
        //}

        public void StartDialogue()
        {
            dialogue.SetActive(true);
            FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors);
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
