using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager6 : MonoBehaviour
{
    public TextMeshProUGUI dialogText;  // Referenca na TextMeshProUGUI UI element
    private Queue<Dialog.DialogLine> dialogLines;
    public Man2DayOneCorrectController6 man2NPC; // Referenca na Man2DayOneCorrectController

    void Start()
    {
        dialogLines = new Queue<Dialog.DialogLine>();
        dialogText = GameObject.Find("DialogText").GetComponent<TextMeshProUGUI>();

        if (dialogText == null)
        {
            Debug.LogError("DialogText UI element nije pronađen!");
        }
    }

    public void StartDialog(Dialog dialog, MafiaNPCController6 npc)
    {
        if (npc == null || !npc.gameObject.activeInHierarchy)
        {
            Debug.LogError("MafiaNPCController nije pronađen ili je deaktiviran!");
            return;
        }

        dialogLines.Clear();

        foreach (var line in dialog.dialogLines)
        {
            dialogLines.Enqueue(line);
        }

        DisplayNextSentence(npc);
    }

    public void DisplayNextSentence(MafiaNPCController6 npc)
    {
        if (dialogLines.Count == 0)
        {
            EndDialog(npc);
            return;
        }

        var dialogLine = dialogLines.Dequeue();
        dialogText.text = dialogLine.sentence;
        Debug.Log($"Displaying sentence: {dialogLine.speaker}: {dialogLine.sentence}");
    }

    public void EndDialog(MafiaNPCController6 npc)
    {
        if (npc == null || !npc.gameObject.activeInHierarchy)
        {
            Debug.LogError("MafiaNPCController nije pronađen ili je deaktiviran!");
            return;
        }

        npc.EndDialog();  // Završava dijalog za MafiaNPC

        dialogText.gameObject.SetActive(false); // Sakrij dijalog

        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.TurnLeft();
        }

        if (man2NPC != null)
        {
            man2NPC.ActivateNPC();  // Aktivira kretanje drugog NPC-a
        }
    }

    void Update()
    {
        MafiaNPCController6 npc = FindObjectOfType<MafiaNPCController6>();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence(npc);
        }
    }
}
