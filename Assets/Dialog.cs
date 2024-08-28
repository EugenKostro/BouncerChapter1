using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog")]
public class Dialog : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] sentences;  // Ovdje pohranjujete rečenice dijaloga
}
