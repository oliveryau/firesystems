using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class ButtonSceneSelector : MonoBehaviour
    {
        public int sceneToLoad;
        public DialogueManager dialogueManager;
        
        private void Awake()
        {
            dialogueManager = GameObject.FindObjectOfType<DialogueManager>();
        }
        public void RunCutscene()
        {
            dialogueManager.DisplayMessage(sceneToLoad);
        }
    }

}
