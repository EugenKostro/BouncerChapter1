using UnityEngine;
using TMPro;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager Instance;

    public TextMeshProUGUI goodChoicesText;
    public TextMeshProUGUI badChoicesText;

    private int goodChoices = 0;
    private int badChoices = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementGoodChoices()
    {
        goodChoices++;
        UpdateUI();
    }

    public void IncrementBadChoices()
    {
        badChoices++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        goodChoicesText.text = "Good choices: " + goodChoices;
        badChoicesText.text = "Bad choices: " + badChoices;
    }

    public int GetGoodChoicesCount()
    {
        return goodChoices;
    }

    public int GetBadChoicesCount()
    {
        return badChoices;
    }
}
