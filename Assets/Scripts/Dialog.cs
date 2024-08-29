using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog")]
public class Dialog : ScriptableObject
{
    [System.Serializable]
    public struct DialogLine
    {
        public string speaker;  // Tko govori, npr. "Player" ili "MafiaNPC"
        [TextArea(3, 10)]
        public string sentence;  // Što govore
    }

    public DialogLine[] dialogLines;  // Ovdje pohranjujemo sve linije dijaloga
}
