using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    private Label currencyLabel;
    private ProgressBar hungerBar;
    private ProgressBar cleanlinessBar;

    public HorseController playerHorse;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        currencyLabel = root.Q<Label>("currency-label");
        hungerBar = root.Q<ProgressBar>("hunger-bar");
        cleanlinessBar = root.Q<ProgressBar>("cleanliness-bar");
    }

    private void Update()
    {
        if (StableManager.Instance != null)
        {
            currencyLabel.text = $"Currency: {StableManager.Instance.currentCurrency}";
        }

        if (playerHorse != null && playerHorse.horseData != null)
        {
            hungerBar.value = playerHorse.horseData.hunger * 100f;
            cleanlinessBar.value = playerHorse.horseData.cleanliness * 100f;
        }
    }
}