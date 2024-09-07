using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManDayOneIncorrectController5 : MonoBehaviour
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
            ShowManIncorrectIdAndButtons();
        }
    }

    private void ShowManIncorrectIdAndButtons()
    {
        GameObject manIncorrectId = GameObject.Find("ManIncorrectId");
        GameObject allowButton = GameObject.Find("AllowEntranceButton1Incorrect");
        GameObject banButton = GameObject.Find("BanButton1Incorrect");

        if (manIncorrectId != null)
        {
            manIncorrectId.SetActive(true);
            Animator idAnimator = manIncorrectId.GetComponent<Animator>();
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

        Debug.Log("ManIncorrect je stigao do playera, prikazujem ID i gumbe.");
    }

    public void OnAllowEntranceButtonPressed()
    {
        HideManIncorrectIdAndButtons();
        moveToClub = true;
        animator.SetBool("isMovingToPlayer", false);
        animator.SetTrigger("MoveToClub");

        // Bilježenje bad choice jer AllowEntranceButton1Incorrect je loš izbor
        ChoiceManager.Instance.IncrementBadChoices();

        // Aktiviraj sljedeći NPC (Woman2DayOneCorrectController1) nakon ove akcije
        Woman2DayOneCorrectController5 nextController = FindObjectOfType<Woman2DayOneCorrectController5>();
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
            Debug.Log("ManIncorrect reached the club.");
            gameObject.SetActive(false);
        }
    }

    public void OnBanButtonPressed()
    {
        HideManIncorrectIdAndButtons();
        isReturning = true;
        animator.SetTrigger("TurnBack");

        // Bilježenje good choice jer BanButton1Incorrect je dobar izbor
        ChoiceManager.Instance.IncrementGoodChoices();

        // Aktiviraj sljedeći NPC (Woman2DayOneCorrectController1) nakon ove akcije
        Woman2DayOneCorrectController5 nextController = FindObjectOfType<Woman2DayOneCorrectController5>();
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

    private void HideManIncorrectIdAndButtons()
    {
        GameObject manIncorrectId = GameObject.Find("ManIncorrectId");
        GameObject allowButton = GameObject.Find("AllowEntranceButton1Incorrect");
        GameObject banButton = GameObject.Find("BanButton1Incorrect");

        if (manIncorrectId != null)
            manIncorrectId.SetActive(false);

        if (allowButton != null)
            allowButton.SetActive(false);

        if (banButton != null)
            banButton.SetActive(false);
    }
}
