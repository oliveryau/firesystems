using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Experimental.GraphView;
using TMPro;

namespace Ithas
{
    public class Dialogue : MonoBehaviour
    {
        [SerializeField]
        private string[] sentences1;
        [SerializeField]
        private string[] sentences2;
        public GameObject[] answers;
        public int index;
        private bool canCont;
        public int option;
        public GameObject dialogueScreen;
        public TextMeshProUGUI dialogueText;

        //void Start()
        //{
        //    dialogueScreen.SetActive(true);
        //    for (int i = 0; i < answers.Length; i++)
        //    {
        //        answers[i].SetActive(true);
        //    }
        //    index = 0;
        //}

        void Update()
        {
            if (canCont && Input.GetMouseButtonDown(0))
            {
                index += 1;
                if (option == 1 && sentences1.Length > index)
                {
                    dialogueText.text = sentences1[index];
                }
                else if (option == 2 && sentences2.Length > index)
                {
                    dialogueText.text = sentences2[index];
                }

                if (option == 1 && sentences1.Length == index)
                {
                    this.gameObject.SetActive(false);
                    dialogueScreen.SetActive(false);
                    //index = 0;
                    option = 0;
                    //canCont = false;
                }
                //if (option == 2 && sentences2.Length == index)
                //{
                //    this.gameObject.SetActive(false);
                //    dialogueScreen.SetActive(false);
                //    index = 0;
                //    option = 0;
                //    canCont = false;
                //}
            }
        }

        public void StartDialogue()
        {
            dialogueScreen.SetActive(true);
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i].SetActive(true);
            }
            index = 0;
        }

        public void DialogueOption1()
        {
            option = 1;
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i].SetActive(false);
            }
            canCont = true;
            dialogueText.gameObject.SetActive(true);
            Debug.Log("You have chosen option 1");
            dialogueText.text = (sentences1[index]);
        }

        public void DialogueOption2()
        {
            option = 2;
            dialogueText.gameObject.SetActive(true);
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i].SetActive(false);
            }
            canCont = true;
            dialogueText.gameObject.SetActive(true);
            Debug.Log("You have chosen option 2");
            dialogueText.text = (sentences2[index]);
        }
    }
}