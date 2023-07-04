using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class Dialogue : MonoBehaviour
    {
        public DialogueSO conversation;

        public GameObject speakerLeft;
        public GameObject speakerRight;

        private DialogueSpeaker speakerUiLeft;
        private DialogueSpeaker speakerUiRight;
        private GameObject player;

        private int activeLineIndex = 0;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");

            speakerUiLeft = speakerLeft.GetComponent<DialogueSpeaker>();
            speakerUiRight = speakerRight.GetComponent<DialogueSpeaker>();

            speakerUiLeft.Speaker = conversation.speakerLeft;
            speakerUiRight.Speaker = conversation.speakerRight;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AdvanceConversation();
            }
        }

        private void AdvanceConversation()
        {
            if (activeLineIndex < conversation.lines.Length)
            {
                Time.timeScale = 0;
                player.GetComponent<Animator>().enabled = false;

                DisplayLine();
                activeLineIndex += 1;
            }
            else
            {
                Time.timeScale = 1;
                player.GetComponent<Animator>().enabled = true;

                speakerUiLeft.Hide();
                speakerUiRight.Hide();
                activeLineIndex = 0;
            }
        }

        private void DisplayLine()
        {
            Line line = conversation.lines[activeLineIndex];
            CharacterSO character = line.characterSO;

            if (speakerUiLeft.SpeakerIs(character))
            {
                SetDialogue(speakerUiLeft, speakerUiRight, line.text);
            }
            else
            {
                SetDialogue(speakerUiRight, speakerUiLeft, line.text);
            }
        }

        private void SetDialogue(DialogueSpeaker activeSpeaker, DialogueSpeaker inactiveSpeaker, string text)
        {
            activeSpeaker.Dialogue = text;
            activeSpeaker.Show();
            inactiveSpeaker.Hide();
        }
    }
}