using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Man3DayOneCorrectController4 : MonoBehaviour
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
            ShowMan3CorrectIdAndButtons();
        }
    }

    private void ShowMan3CorrectIdAndButtons()
    {
        GameObject man3CorrectId = GameObject.Find("Man3CorrectId");
        GameObject allowButton = GameObject.Find("AllowEntranceButton3");
        GameObject banButton = GameObject.Find("BanButton3");

        if (man3CorrectId != null)
        {
            man3CorrectId.SetActive(true);
            Animator idAnimator = man3CorrectId.GetComponent<Animator>();
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

        Debug.Log("Man3 je stigao do playera, prikazujem ID i gumbe.");
    }

    public void OnAllowEntranceButtonPressed()
    {
        HideMan3CorrectIdAndButtons();
        moveToClub = true;
        animator.SetBool("isMovingToPlayer", false);
        animator.SetTrigger("MoveToClub");

        ChoiceManager.Instance.IncrementGoodChoices();

        Man4DayOneCorrectController4 man4Controller = FindObjectOfType<Man4DayOneCorrectController4>();
        if (man4Controller != null)
        {
            man4Controller.ActivateNPC();
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
            Debug.Log("Man3 reached the club.");
            gameObject.SetActive(false);
        }
    }

    public void OnBanButtonPressed()
    {
        HideMan3CorrectIdAndButtons();
        isReturning = true;
        animator.SetTrigger("TurnBack");

        ChoiceManager.Instance.IncrementBadChoices();

        Man4DayOneCorrectController4 man4Controller = FindObjectOfType<Man4DayOneCorrectController4>();
        if (man4Controller != null)
        {
            man4Controller.ActivateNPC();
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

    private void HideMan3CorrectIdAndButtons()
    {
        GameObject man3CorrectId = GameObject.Find("Man3CorrectId");
        GameObject allowButton = GameObject.Find("AllowEntranceButton3");
        GameObject banButton = GameObject.Find("BanButton3");

        if (man3CorrectId != null)
            man3CorrectId.SetActive(false);

        if (allowButton != null)
            allowButton.SetActive(false);

        if (banButton != null)
            banButton.SetActive(false);
    }
}
