using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    public Dialog playerDialog; 

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TurnRight()
    {
        transform.localScale = new Vector3(14, 11, 1); 
        animator.SetTrigger("Player-right"); 
        FindObjectOfType<DialogManager>().StartDialog(playerDialog, null);  // Pokreni dijalog za igrača, ali bez NPC-a
    }
}
