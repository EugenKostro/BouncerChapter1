using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialogText;  // Referenca na Text UI element

    private Queue<string> sentences;

    void Start()
{
    sentences = new Queue<string>();
    dialogText = GameObject.Find("DialogText").GetComponent<Text>();

    if (dialogText == null)
    {
        Debug.LogError("DialogText UI element nije pronađen!");
    }
}


    public void StartDialog(Dialog dialog)
    {
        sentences.Clear();

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogText.text = sentence;
    }

    void EndDialog()
    {
        Debug.Log("End of conversation.");
        // Ovdje možeš dodati logiku koja će se dogoditi nakon završetka dijaloga
    }
}
