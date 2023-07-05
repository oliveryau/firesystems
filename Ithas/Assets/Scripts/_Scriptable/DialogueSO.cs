using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    [System.Serializable]
    public class Line
    {
        public CharacterSO characterSO;
        [TextArea(2, 5)]
        public string text;
    }

    [CreateAssetMenu]
    public class DialogueSO : ScriptableObject
    {
        public CharacterSO speakerLeft;
        public CharacterSO speakerRight;
        public Line[] lines;
    }
}

