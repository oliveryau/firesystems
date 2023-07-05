using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ithas
{
    public class DialogueSpeaker : MonoBehaviour
    {
        public Image portrait;
        public TextMeshProUGUI fullName;
        public TextMeshProUGUI dialogue;

        private CharacterSO speaker;
        public CharacterSO Speaker
        {
            get { return speaker; }
            set
            {
                speaker = value;
                portrait.sprite = speaker.portrait;
                fullName.text = speaker.fullName;
            }
        }

        public string Dialogue
        {
            set { dialogue.text = value; }
        }

        public bool HasSpeaker()
        {
            return speaker != null;
        }

        public bool SpeakerIs(CharacterSO character)
        {
            return speaker == character;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}