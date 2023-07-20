//celine
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
        [Header("Dialogue Objects")]
        public Image actorLImage;
        public Image actorRImage;
        public TextMeshProUGUI actorName;
        public TextMeshProUGUI messageText;
        public Button firstOption;
        public Button secondOption;

        [Header("Others")]
        public DialogueActivator trigger;
        public GameObject inputHandler;

        Message[] currentMessages;
        Actor[] currentActors;
        int activeMessage = 0;
        public static bool isActive = false;
        private int nextCutscene;
        public bool isTalking = false; //boolean check
        public bool hasChoice = false;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && isActive == true && !hasChoice)
            {
                NextMessage();
            }
        }

        public void OpenDialogue(Message[] messages, Actor[] actors)
        {
            inputHandler.SetActive(false);
            currentMessages = messages;
            currentActors = actors;
            activeMessage = 0;
            isActive = true;
            if (currentMessages[activeMessage].cutsceneRef == 0)
            {
                
                Choice currentChoice = new Choice();
                currentChoice.choiceText = currentMessages[activeMessage].choice;
                string[] splits = currentChoice.choiceText.Split("*");
                foreach (string split in splits)
                {
                    string[] secondSplit = split.Split("#");
                    currentChoice.choicesDict.Add(secondSplit[0],Int32.Parse(secondSplit[1]));
                }
            }
            else
            {
                nextCutscene = currentMessages[activeMessage].cutsceneRef;
            }

            DisplayMessage(currentMessages[activeMessage].cutscene);
        }

        public void DisplayMessage(int cutsceneToGo)
        {
            foreach (Message message in currentMessages) // runs thru all messages
            {
                if (message.cutscene == cutsceneToGo) // find right cutscene to go to
                {
                    isTalking = true;
                    hasChoice = false;
                    messageText.text = message.message;

                    Actor actorToDisplayName = currentActors[message.currentSpeaker]; // for name change
                    actorName.text = actorToDisplayName.name;
                    Actor actorToDisplayLeft = currentActors[message.speakerLeft]; // to show on dialogue box
                    actorLImage.sprite = actorToDisplayLeft.sprite;
                    Actor actorToDisplayRight = currentActors[message.speakerRight]; // to show on dialogue box
                    actorRImage.sprite = actorToDisplayRight.sprite;

                    if (message.currentSpeaker == message.speakerLeft)
                    {
                        actorLImage.GetComponent<Image>().color = Color.white;
                        actorRImage.GetComponent<Image>().color = Color.grey;
                    }
                    else if (message.currentSpeaker == message.speakerRight)
                    {
                        actorLImage.GetComponent<Image>().color = Color.grey;
                        actorRImage.GetComponent<Image>().color = Color.white;
                    }
                    else
                    {
                        Debug.Log("Current Speakers do not match left/right speakers!");
                    }

                    nextCutscene = message.cutsceneRef;
                    if (nextCutscene == -1) // choice checker based on cutscene ref no
                    {
                        hasChoice = true;
                        Choice currentChoice = new Choice();
                        currentChoice.choiceText = message.choice;
                        string[] splits = currentChoice.choiceText.Split("*");
                        foreach (string split in splits)
                        {
                            string[] secondSplit = split.Split("#");
                            currentChoice.choicesDict.Add(secondSplit[0],Int32.Parse(secondSplit[1]));
                        }
                        firstOption.gameObject.SetActive(true);
                        secondOption.gameObject.SetActive(true);
                        List<string> choices = new List<string>();
                        List<int> cutsceneOptions = new List<int>();
                        foreach (var choiceOption in currentChoice.choicesDict)
                        {
                            choices.Add(choiceOption.Key);
                            cutsceneOptions.Add(choiceOption.Value);
                        }
                        firstOption.gameObject.GetComponent<TMP_Text>().text = choices[0];
                        secondOption.gameObject.GetComponent<TMP_Text>().text = choices[1];
                        firstOption.gameObject.GetComponent<ButtonSceneSelector>().sceneToLoad = cutsceneOptions[0];
                        secondOption.gameObject.GetComponent<ButtonSceneSelector>().sceneToLoad = cutsceneOptions[1];
                    }
                    else
                    {
                        firstOption.gameObject.SetActive(false);
                        secondOption.gameObject.SetActive(false);
                    }
                    return;
                }
            }
            isActive = false;
            trigger.EndDialogue(); // when dialogue finishes for one NPC, end dialogue
            isTalking = false;
            inputHandler.SetActive(true);
        }        
            

        public void NextMessage()
        {
            DisplayMessage(nextCutscene);
        }
        
    }
}
