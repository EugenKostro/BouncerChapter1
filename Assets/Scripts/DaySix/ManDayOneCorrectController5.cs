using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManDayOneCorrectController5 : MonoBehaviour
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
            ShowManCorrectIdAndButtons();
        }
    }

    private void ShowManCorrectIdAndButtons()
    {
        GameObject manCorrectId = GameObject.Find("ManCorrectId");
        GameObject allowButton = GameObject.Find("AllowEntranceButton1");
        GameObject banButton = GameObject.Find("BanButton1");

        if (manCorrectId != null)
        {
            manCorrectId.SetActive(true);
            Animator idAnimator = manCorrectId.GetComponent<Animator>();
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

        Debug.Log("ManCorrect je stigao do playera, prikazujem ID i gumbe.");
    }

    public void OnAllowEntranceButtonPressed()
    {
        HideManCorrectIdAndButtons();
        moveToClub = true;
        animator.SetBool("isMovingToPlayer", false);
        animator.SetTrigger("MoveToClub");

        // Bilježenje good choice
        ChoiceManager.Instance.IncrementGoodChoices();

        // Aktiviraj sljedeći NPC (ManDayOneIncorrectController1) nakon ove akcije
        ManDayOneIncorrectController5 nextController = FindObjectOfType<ManDayOneIncorrectController5>();
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
            Debug.Log("ManCorrect reached the club.");
            gameObject.SetActive(false);
        }
    }

    public void OnBanButtonPressed()
    {
        HideManCorrectIdAndButtons();
        isReturning = true;
        animator.SetTrigger("TurnBack");

        // Bilježenje bad choice
        ChoiceManager.Instance.IncrementBadChoices();

        // Aktiviraj sljedeći NPC (ManDayOneIncorrectController1) nakon ove akcije
        ManDayOneIncorrectController5 nextController = FindObjectOfType<ManDayOneIncorrectController5>();
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

    private void HideManCorrectIdAndButtons()
    {
        GameObject manCorrectId = GameObject.Find("ManCorrectId");
        GameObject allowButton = GameObject.Find("AllowEntranceButton1");
        GameObject banButton = GameObject.Find("BanButton1");

        if (manCorrectId != null)
            manCorrectId.SetActive(false);

        if (allowButton != null)
            allowButton.SetActive(false);

        if (banButton != null)
            banButton.SetActive(false);
    }
}
