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
        public string speaker;  
        [TextArea(3, 10)]
        public string sentence; 
    }

    public DialogLine[] dialogLines;  
}
