using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WomanDayOneCorrectController3 : MonoBehaviour
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
            ShowWomanCorrectIdAndButtons();
        }
    }

    private void ShowWomanCorrectIdAndButtons()
    {
        GameObject womanCorrectId = GameObject.Find("WomanCorrectId");
        GameObject allowButton = GameObject.Find("AllowEntranceButtonWoman");
        GameObject banButton = GameObject.Find("BanButtonWoman");

        if (womanCorrectId != null)
        {
            womanCorrectId.SetActive(true);
            Animator idAnimator = womanCorrectId.GetComponent<Animator>();
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

        Debug.Log("Woman je stigla do playera, prikazujem ID i gumbe.");
    }

    public void OnAllowEntranceButtonPressed()
    {
        HideWomanCorrectIdAndButtons();
        moveToClub = true;
        animator.SetBool("isMovingToPlayer", false);
        animator.SetTrigger("MoveToClub");

        // Bilježenje bad choice jer AllowEntranceButtonWoman je loš izbor
        ChoiceManager.Instance.IncrementBadChoices();

        // Pokretanje završetka scene
        SceneEndManager.Instance.TriggerEndScene();
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
            Debug.Log("Woman reached the club.");
            gameObject.SetActive(false);
        }
    }

    public void OnBanButtonPressed()
    {
        HideWomanCorrectIdAndButtons();
        isReturning = true;
        animator.SetTrigger("TurnBack");

        // Bilježenje good choice jer BanButtonWoman je dobar izbor
        ChoiceManager.Instance.IncrementGoodChoices();

        // Pokretanje završetka scene
        SceneEndManager.Instance.TriggerEndScene();
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

    private void HideWomanCorrectIdAndButtons()
    {
        GameObject womanCorrectId = GameObject.Find("WomanCorrectId");
        GameObject allowButton = GameObject.Find("AllowEntranceButtonWoman");
        GameObject banButton = GameObject.Find("BanButtonWoman");

        if (womanCorrectId != null)
            womanCorrectId.SetActive(false);

        if (allowButton != null)
            allowButton.SetActive(false);

        if (banButton != null)
            banButton.SetActive(false);
    }
}
