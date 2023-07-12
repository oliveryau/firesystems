using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ithas
{
    public class DialogueManager : MonoBehaviour
    {
        public Image actorImage;
        //public Image speakerRImage;
        public TextMeshProUGUI actorName;
        public TextMeshProUGUI messageText;
        public RectTransform backgroundBox;
        public DialogueActivator trigger;

        Message[] currentMessages;
        Actor[] currentActors;
        int activeMessage = 0;
        public static bool isActive = false;

        public bool isTalking = false; //boolean check

        public event Action OnDialogueEnd; // Event for ending the dialogue

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && isActive == true)
            {
                NextMessage();
            }
        }

        public void OpenDialogue(Message[] messages, Actor[] actors)
        {
            currentMessages = messages;
            currentActors = actors;
            activeMessage = 0;
            isActive = true;

            Debug.Log("Started conversation! Loaded messages: " + messages.Length);
            DisplayMessage();
        }

        void DisplayMessage()
        {
            Message messageToDisplay = currentMessages[activeMessage];
            messageText.text = messageToDisplay.message;

            Actor actorToDisplay = currentActors[messageToDisplay.actorId];
            actorName.text = actorToDisplay.name;
            actorImage.sprite = actorToDisplay.sprite;
        }

        public void NextMessage()
        {
            activeMessage++;
            if (activeMessage < currentMessages.Length)
            {
                DisplayMessage();
                isTalking = true;
            }
            else
            {
                Debug.Log("Convo ended!");
                isActive = false;
                OnDialogueEnd?.Invoke(); // Invoke the dialogue end event
                isTalking = false;
            }
        }
        
    }
}
