using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Man2DayOneCorrectController3 : MonoBehaviour
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

    public TextMeshProUGUI goodChoicesText;
    public TextMeshProUGUI badChoicesText;

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
            ShowMan2CorrectIdAndButtons();
        }
    }

    private void ShowMan2CorrectIdAndButtons()
    {
        GameObject man2CorrectId = GameObject.Find("Man2CorrectId");
        GameObject allowButton = GameObject.Find("AllowEntranceButton");
        GameObject banButton = GameObject.Find("BanButton");

        if (man2CorrectId != null)
        {
            man2CorrectId.SetActive(true);
            Animator idAnimator = man2CorrectId.GetComponent<Animator>();
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

        Debug.Log("NPC je stigao do playera, prikazujem ID i gumbe.");
    }

    public void OnAllowEntranceButtonPressed()
{
    HideMan2CorrectIdAndButtons();
    moveToClub = true;
    animator.SetBool("isMovingToPlayer", false);  // Deaktivira hodanje prema playeru
    animator.SetTrigger("MoveToClub");  // Aktivira hodanje prema klubu
    ChoiceManager.Instance.IncrementBadChoices();  // Dodano za povećanje broja dobrih izbora

    // Aktiviraj Man3 NPC nakon Man2 akcije
    Man3DayOneCorrectController3 man3Controller = FindObjectOfType<Man3DayOneCorrectController3>();
    if (man3Controller != null)
    {
        man3Controller.ActivateNPC();
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
            animator.SetBool("MoveToClub", false);  // Prelazi u idle stanje kada dođe do kluba
            animator.SetTrigger("hasReachedClub");
            Debug.Log("NPC reached the club.");
            gameObject.SetActive(false);
        }
    }

    public void OnBanButtonPressed()
{
    HideMan2CorrectIdAndButtons();
    isReturning = true;
    animator.SetTrigger("TurnBack");
    ChoiceManager.Instance.IncrementGoodChoices();  // Dodano za povećanje broja loših izbora

    // Aktiviraj Man3 NPC nakon Man2 akcije
    Man3DayOneCorrectController3 man3Controller = FindObjectOfType<Man3DayOneCorrectController3>();
    if (man3Controller != null)
    {
        man3Controller.ActivateNPC();
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

    private void HideMan2CorrectIdAndButtons()
    {
        GameObject man2CorrectId = GameObject.Find("Man2CorrectId");
        GameObject allowButton = GameObject.Find("AllowEntranceButton");
        GameObject banButton = GameObject.Find("BanButton");

        if (man2CorrectId != null)
            man2CorrectId.SetActive(false);

        if (allowButton != null)
            allowButton.SetActive(false);

        if (banButton != null)
            banButton.SetActive(false);
    }
}
