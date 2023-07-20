//celine
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class ButtonSceneSelector : MonoBehaviour
    {
        public int sceneToLoad;
        
        public void RunCutscene()
        {
            FindObjectOfType<DialogueManager>().DisplayMessage(sceneToLoad);
        }
    }

}
