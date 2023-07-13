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
        public GameController gameController;
        public CsvReader csvReader;

        private void Start()
        {
            actors = new Actor[csvReader.actorDataList.actorData.Length];

            for (int i = 0; i < actors.Length; i++)
            {
                actors[i] = new Actor();
                actors[i].name = gameController.GetActorName(i);
                actors[i].sprite = gameController.GetActorSprite(i);
                //Debug.Log(actors[i].name);
            }

            messages = new Message[csvReader.dialogueDataList.dialogueData.Length];

            for (int i = 0; i < messages.Length; i++)
            {
                messages[i] = new Message();
                messages[i].message = gameController.GetDialogueMessages(i)[i].message;
                Debug.Log(messages[i].message);
            }
        }

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
        public int dialogueId;
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
