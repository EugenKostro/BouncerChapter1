using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Woman2DayOneCorrectController2 : MonoBehaviour
{
    public Transform player;
    public Transform clubEntrance;
    public Transform startPosition;
    public float speed = 10.0f;
    public float stopDistance = 1.0f;
    private float initialYPosition;

    private Animator animator;
    private bool isMoving = false;
    private bool isReturning = false;
    private bool moveToClub = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        initialYPosition = transform.position.y;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveToPlayer();
        }
        else if (moveToClub)
        {
            MoveToClub();
        }
        else if (isReturning)
        {
            MoveBackToStart();
        }
    }

    public void ActivateNPC()
    {
        isMoving = true;
        animator.SetBool("isMovingToPlayer", true);
    }

    private void MoveToPlayer()
    {
        Vector3 targetPosition = new Vector3(player.position.x, initialYPosition, transform.position.z);
        float distanceToPlayer = Vector2.Distance(new Vector2(transform.position.x, initialYPosition), new Vector2(targetPosition.x, initialYPosition));

        if (distanceToPlayer > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            isMoving = false;
            animator.SetBool("isMovingToPlayer", false);
            animator.SetTrigger("hasReachedPlayer");
            ShowWoman2CorrectIdAndButtons();
        }
    }

    private void ShowWoman2CorrectIdAndButtons()
    {
        GameObject woman2CorrectId = GameObject.Find("Woman2CorrectId");
        GameObject allowButton = GameObject.Find("AllowEntranceButtonWoman2");
        GameObject banButton = GameObject.Find("BanButtonWoman2");

        if (woman2CorrectId != null)
        {
            woman2CorrectId.SetActive(true);
            Animator idAnimator = woman2CorrectId.GetComponent<Animator>();
            if (idAnimator != null)
            {
                idAnimator.SetTrigger("ShowId");
            }
        }

        if (allowButton != null)
        {
            allowButton.SetActive(true);
            Animator allowButtonAnimator = allowButton.GetComponent<Animator>();
            if (allowButtonAnimator != null)
            {
                allowButtonAnimator.SetTrigger("ShowButton");
            }
        }

        if (banButton != null)
        {
            banButton.SetActive(true);
            Animator banButtonAnimator = banButton.GetComponent<Animator>();
            if (banButtonAnimator != null)
            {
                banButtonAnimator.SetTrigger("ShowButton");
            }
        }

        Debug.Log("Woman2 is at the player. Showing ID and buttons.");
    }

    public void OnAllowEntranceButtonPressed()
    {
        HideWoman2CorrectIdAndButtons();
        moveToClub = true;
        animator.SetBool("isMovingToPlayer", false);
        animator.SetTrigger("MoveToClub");

        // Record good choice because AllowEntranceButtonWoman2 is a good choice
        ChoiceManager.Instance.IncrementGoodChoices();

        // Trigger the next NPC (WomanDayOneCorrectController1) after this action
        WomanDayOneCorrectController2 nextController = FindObjectOfType<WomanDayOneCorrectController2>();
        if (nextController != null)
        {
            nextController.ActivateNPC();
        }
    }

    private void MoveToClub()
    {
        Vector3 targetPosition = new Vector3(clubEntrance.position.x, initialYPosition, transform.position.z);
        float distanceToClub = Vector2.Distance(new Vector2(transform.position.x, initialYPosition), new Vector2(targetPosition.x, initialYPosition));

        if (distanceToClub > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            moveToClub = false;
            animator.SetTrigger("hasReachedClub");
            Debug.Log("Woman2 reached the club.");
            gameObject.SetActive(false);
        }
    }

    public void OnBanButtonPressed()
    {
        HideWoman2CorrectIdAndButtons();
        isReturning = true;
        animator.SetTrigger("TurnBack");

        // Record bad choice because BanButtonWoman2 is a bad choice
        ChoiceManager.Instance.IncrementBadChoices();

        // Trigger the next NPC (WomanDayOneCorrectController1) after this action
        WomanDayOneCorrectController2 nextController = FindObjectOfType<WomanDayOneCorrectController2>();
        if (nextController != null)
        {
            nextController.ActivateNPC();
        }
    }

    private void MoveBackToStart()
    {
        Vector3 targetPosition = new Vector3(startPosition.position.x, initialYPosition, transform.position.z);
        float distanceToStart = Vector2.Distance(new Vector2(transform.position.x, initialYPosition), new Vector2(targetPosition.x, initialYPosition));

        if (distanceToStart > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            isReturning = false;
            animator.SetTrigger("hasReachedStart");
        }
    }

    private void HideWoman2CorrectIdAndButtons()
    {
        GameObject woman2CorrectId = GameObject.Find("Woman2CorrectId");
        GameObject allowButton = GameObject.Find("AllowEntranceButtonWoman2");
        GameObject banButton = GameObject.Find("BanButtonWoman2");

        if (woman2CorrectId != null)
            woman2CorrectId.SetActive(false);

        if (allowButton != null)
            allowButton.SetActive(false);

        if (banButton != null)
            banButton.SetActive(false);
    }
}
