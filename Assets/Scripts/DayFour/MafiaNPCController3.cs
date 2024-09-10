using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MafiaNPCController3 : MonoBehaviour
{
    public Transform player;  
    public Transform exitPoint;  
    public float speed = 2.0f;  
    public float stopDistance = 1.0f;  
    public Dialog dialog;

    private Animator animator;
    private bool hasExitedBuilding = false;
    private float initialYPosition; 
    private bool dialogStarted = false;
    private bool dialogFinished = false;

    void Start()
    {
        animator = GetComponent<Animator>();  
        initialYPosition = transform.position.y; 
    }

    void Update()
    {
        if (!hasExitedBuilding)
        {
            MoveToExitPoint();  
        }
        else if (!dialogFinished)
        {
            MoveToPlayer();  
        }
        else
        {
            MoveToExitAfterDialog();
        }
    }

    void MoveToExitPoint()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, initialYPosition); 
        Vector2 targetPosition = new Vector2(exitPoint.position.x, initialYPosition); 

        float distanceToExit = Vector2.Distance(currentPosition, targetPosition);

        if (distanceToExit > 0.1f)
        {
            transform.position = new Vector3(
                Mathf.MoveTowards(transform.position.x, exitPoint.position.x, speed * Time.deltaTime),
                initialYPosition, 
                0); 

            animator.SetBool("MafiaNPCLeftWalk", true);  
        }
        else
        {
            hasExitedBuilding = true;
            animator.SetBool("MafiaNPCLeftWalk", false);  
        }
    }

    void MoveToPlayer()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, initialYPosition); 
        Vector2 playerPosition = new Vector2(player.position.x, initialYPosition); 

        float distanceToPlayer = Vector2.Distance(currentPosition, playerPosition);

        if (distanceToPlayer > stopDistance)
        {
            transform.position = new Vector3(
                Mathf.MoveTowards(transform.position.x, player.position.x, speed * Time.deltaTime),
                initialYPosition, 
                0); 
                

            animator.SetBool("MafiaNPCLeftWalk", true);  
        }
        else
        {
            animator.SetBool("MafiaNPCLeftWalk", false);  
            animator.SetTrigger("MafiaNPCLeftIdle");  

            if (!dialogStarted)
            {
                dialogStarted = true;
                player.GetComponent<PlayerController>().TurnRight(); 
                FindObjectOfType<DialogManager3>().StartDialog(dialog, this);  
            }
        }
    }

    public void EndDialog()
    {
        dialogFinished = true;
        animator.SetTrigger("MafiaNPCRightIdle");  
    }

    void MoveToExitAfterDialog()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, initialYPosition); 
        Vector2 exitPosition = new Vector2(exitPoint.position.x, initialYPosition); 

        float distanceToExit = Vector2.Distance(currentPosition, exitPosition);

        if (distanceToExit > 0.1f)
        {
            transform.position = new Vector3(
                Mathf.MoveTowards(transform.position.x, exitPoint.position.x, speed * Time.deltaTime),
                initialYPosition, 
                0); 

            animator.SetBool("MafiaNPCRightWalk", true);  
        }
        else
        {
            animator.SetBool("MafiaNPCRightWalk", false);  
            gameObject.SetActive(false);  
        }
    }
}
