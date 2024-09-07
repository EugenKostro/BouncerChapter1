using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEndManager : MonoBehaviour
{
    public static SceneEndManager Instance;

    public GameObject fadePanel;
    public TMPro.TextMeshProUGUI resultText;
    public TMPro.TextMeshProUGUI outcomeText;
    public GameObject restartButton;
    public GameObject nextDayButton;

    private void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    else
    {
        Destroy(gameObject);  // Avoid duplicating SceneEndManager
    }
}


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
{
    if (restartButton != null)
    {
        restartButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(OnRestartButtonPressed);
    }

    if (nextDayButton != null)
    {
        nextDayButton.GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(OnNextDayButtonPressed);
    }

    SceneManager.sceneLoaded -= OnSceneLoaded;
}


    private void Start()
    {
        // Osiguraj da su svi relevantni objekti ispravno inicijalizirani
        fadePanel.SetActive(false);  // Deaktiviraj FadePanel na početku
        if (restartButton != null) restartButton.SetActive(false);
        if (nextDayButton != null) nextDayButton.SetActive(false);
    }

   private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    // Ponovno postavljanje referenci
    fadePanel = GameObject.Find("FadePanel");
    resultText = GameObject.Find("ResultText").GetComponent<TMPro.TextMeshProUGUI>();
    outcomeText = GameObject.Find("OutcomeText").GetComponent<TMPro.TextMeshProUGUI>();
    restartButton = GameObject.Find("RestartButton");
    nextDayButton = GameObject.Find("NextDayButton");

    if (fadePanel != null)
    {
        fadePanel.SetActive(false);
    }

    if (restartButton != null)
    {
        restartButton.SetActive(false);
        restartButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnRestartButtonPressed);
    }

    if (nextDayButton != null)
    {
        nextDayButton.SetActive(false);
        nextDayButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnNextDayButtonPressed);
    }
}



    public void TriggerEndScene()
    {
        if (fadePanel != null)
        {
            fadePanel.SetActive(true);  // Aktiviraj FadePanel kada završava scena
            ShowResults();
        }
        else
        {
            Debug.LogError("FadePanel is missing!");
        }
    }

    public void ShowResults()
    {
        int goodChoices = ChoiceManager.Instance.GetGoodChoicesCount();
        int badChoices = ChoiceManager.Instance.GetBadChoicesCount();

        if (resultText != null && outcomeText != null)
        {
            resultText.text = $"Good choices: {goodChoices}\nBad choices: {badChoices}";

            if (badChoices < 2)
            {
                outcomeText.text = "You did good tonight";
            }
            else if (badChoices >= 2 && badChoices <= 4)
            {
                outcomeText.text = "You did not eat today";
            }
            else if (badChoices == 5 || badChoices == 6)
            {
                outcomeText.text = "You did not have enough money for food and drink";
            }
            else if (badChoices >= 7)
            {
                outcomeText.text = "You have disappeared";
                if (nextDayButton != null)
                {
                    nextDayButton.SetActive(false);  // Sakrij gumb Next Day ako je ishod loš
                }
            }

            if (restartButton != null) restartButton.SetActive(true);
            if (nextDayButton != null) nextDayButton.SetActive(badChoices < 7);  // Prikaži gumb Next Day samo ako je ishod pozitivan
        }
        else
        {
            Debug.LogError("ResultText or OutcomeText is missing!");
        }
    }

    public void OnRestartButtonPressed()
{
    fadePanel.SetActive(false);  // Deaktiviraj FadePanel

    // Dohvati trenutni index scene
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    // Učitaj ponovno trenutnu scenu
    SceneManager.LoadScene(currentSceneIndex);
}


    public void OnNextDayButtonPressed()
{
    fadePanel.SetActive(false);  // Deactivate the FadePanel

    // Get the current active scene's build index
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    // Load the next scene by incrementing the index
    SceneManager.LoadScene(currentSceneIndex + 1);
}
}
