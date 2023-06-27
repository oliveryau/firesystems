using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    //base class for all player scripts to inherit
    public class PlayerScript : MonoBehaviour
    {
        private static PlayerScript instance;

        public static PlayerScript Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject); //persist throughout different scenes
        }

        public virtual void Initialize(GameController gameController) { }
    }
}