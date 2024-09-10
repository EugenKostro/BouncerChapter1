using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager1 : MonoBehaviour
{
    public TextMeshProUGUI dialogText;  
    private Queue<Dialog.DialogLine> dialogLines;
    public Man2DayOneCorrectController1 man2NPC; 

    void Start()
    {
        dialogLines = new Queue<Dialog.DialogLine>();
        dialogText = GameObject.Find("DialogText").GetComponent<TextMeshProUGUI>();

        if (dialogText == null)
        {
            Debug.LogError("DialogText UI element nije pronađen!");
        }
    }

    public void StartDialog(Dialog dialog, MafiaNPCController1 npc)
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

    public void DisplayNextSentence(MafiaNPCController1 npc)
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

    public void EndDialog(MafiaNPCController1 npc)
    {
        if (npc == null || !npc.gameObject.activeInHierarchy)
        {
            Debug.LogError("MafiaNPCController nije pronađen ili je deaktiviran!");
            return;
        }

        npc.EndDialog();  

        dialogText.gameObject.SetActive(false); 

        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.TurnLeft();
        }

        if (man2NPC != null)
        {
            man2NPC.ActivateNPC();  
        }
    }

    void Update()
    {
        MafiaNPCController1 npc = FindObjectOfType<MafiaNPCController1>();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence(npc);
        }
    }
}
