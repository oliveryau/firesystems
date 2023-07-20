//celine
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class DialogueActivator : MonoBehaviour
    {
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
                actors[i].actorId = i;
                actors[i].name = gameController.GetActorName(i);
                actors[i].sprite = gameController.GetActorSprite(i);
            }

            int noOfLines = csvReader.dialogueDataList.dialogueData.Length; 
            messages = new Message[noOfLines];

            for (int i = 0; i < noOfLines; i++)
            {
                Message toAdd = new Message();
                toAdd.actorId = gameController.GetDialogueMessages()[i].actorId;
                toAdd.cutscene = gameController.GetDialogueMessages()[i].cutscene;
                toAdd.cutsceneRef = gameController.GetDialogueMessages()[i].cutsceneRef;
                toAdd.speakerLeft = gameController.GetDialogueMessages()[i].speakerLeft;
                toAdd.speakerRight = gameController.GetDialogueMessages()[i].speakerRight;
                toAdd.currentSpeaker = gameController.GetDialogueMessages()[i].currentSpeaker;
                toAdd.message = gameController.GetDialogueMessages()[i].message;
                toAdd.choice = gameController.GetDialogueMessages()[i].choice;
                messages[i] = toAdd;
            }
        }

        public void StartDialogue(Message[] messages)
        {
            dialogue.SetActive(true);
            FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors);
        }

        public Message[] FilterMessagesByActorId(int actorId) 
        { 
            List<Message> filteredMessages = new List<Message>(); 
            for (int i = 0; i < messages.Length; i++) 
            { 
                if (messages[i].actorId == actorId) 
                { 
                    filteredMessages.Add(messages[i]); 
                } 
            } 
            return filteredMessages.ToArray(); 
        }

        public void EndDialogue()
        {
            dialogue.SetActive(false);
        }
    }

    [System.Serializable]
    public class Choice
    {
        public int dialogueId;
        public string choiceText;
        public List<string> choices = new List<string>();
        public Dictionary<string,int> choicesDict = new Dictionary<string, int>() ;
    }

    [System.Serializable]
    public class Message
    {
        public int actorId;
        public int cutscene;
        public int cutsceneRef;
        public int speakerLeft;
        public int speakerRight;
        public int currentSpeaker;
        public string message;
        public string choice;

    }
    [System.Serializable]
    public class Actor
    {
        public int actorId;
        public string name;
        public Sprite sprite;
    }
}
