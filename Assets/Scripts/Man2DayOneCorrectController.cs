using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man2DayOneCorrectController : MonoBehaviour
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
            ShowMan2CorrectIdAndButtons();
        }
    }

    private void ShowMan2CorrectIdAndButtons()
    {
        GameObject man2CorrectId = GameObject.Find("Man2CorrectId");
        GameObject allowButton = GameObject.Find("AllowEntranceButton");
        GameObject banButton = GameObject.Find("BanButton");

        if (man2CorrectId != null)
            man2CorrectId.GetComponent<Animator>().SetTrigger("ShowId");

        if (allowButton != null)
            allowButton.GetComponent<Animator>().SetTrigger("ShowButton");

        if (banButton != null)
            banButton.GetComponent<Animator>().SetTrigger("ShowButton");
    }

    public void OnAllowEntranceButtonPressed()
    {
        HideMan2CorrectIdAndButtons();
        StartCoroutine(MoveToClub());
    }

    private IEnumerator MoveToClub()
    {
        while (Vector3.Distance(transform.position, clubEntrance.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, clubEntrance.position, speed * Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
    }

    public void OnBanButtonPressed()
    {
        HideMan2CorrectIdAndButtons();
        isReturning = true;
        animator.SetTrigger("TurnBack");
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

    private void HideMan2CorrectIdAndButtons()
    {
        GameObject man2CorrectId = GameObject.Find("Man2CorrectId");
        GameObject allowButton = GameObject.Find("AllowEntranceButton");
        GameObject banButton = GameObject.Find("BanButton");

        if (man2CorrectId != null)
            man2CorrectId.GetComponent<Animator>().SetTrigger("HideId");

        if (allowButton != null)
            allowButton.GetComponent<Animator>().SetTrigger("HideButton");

        if (banButton != null)
            banButton.GetComponent<Animator>().SetTrigger("HideButton");
    }
}
