using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;  // Referenca na TextMeshProUGUI UI element
    private Queue<Dialog.DialogLine> dialogLines;

    void Start()
    {
        dialogLines = new Queue<Dialog.DialogLine>();
        dialogText = GameObject.Find("DialogText").GetComponent<TextMeshProUGUI>();

        if (dialogText == null)
        {
            Debug.LogError("DialogText UI element nije pronađen!");
        }
    }

    public void StartDialog(Dialog dialog, MafiaNPCController npc)
    {
        dialogLines.Clear();

        foreach (var line in dialog.dialogLines)
        {
            dialogLines.Enqueue(line);
        }

        DisplayNextSentence(npc);
    }

    public void DisplayNextSentence(MafiaNPCController npc)
    {
        if (dialogLines.Count == 0)
        {
            EndDialog(npc);
            return;
        }

        var dialogLine = dialogLines.Dequeue();
        dialogText.text = dialogLine.sentence;
        Debug.Log($"Prikazujem rečenicu: {dialogLine.speaker}: {dialogLine.sentence}");
    }

    public void EndDialog(MafiaNPCController npc)
    {
        if (npc != null)
        {
            Debug.Log("Kraj razgovora.");
            npc.EndDialog();
        }
        else
        {
            Debug.LogError("MafiaNPCController nije pronađen!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var npc = FindObjectOfType<MafiaNPCController>();
            if (npc != null)
            {
                DisplayNextSentence(npc);
            }
            else
            {
                Debug.LogError("MafiaNPCController nije pronađen!");
            }
        }
    }
}
