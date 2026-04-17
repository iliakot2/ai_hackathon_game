using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    private Label coinsLabel;
    private Label gemsLabel;
    private VisualElement shopPopup;
    private Button horseTabButton;
    private Button shopCloseButton;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        // Counters
        coinsLabel = root.Q<VisualElement>("coins-counter")?.Q<Label>();
        gemsLabel = root.Q<VisualElement>("gems-counter")?.Q<Label>();
        
        // Shop
        shopPopup = root.Q<VisualElement>("shop-root");
        horseTabButton = root.Q<Button>("tab-horses");
        shopCloseButton = root.Q<Button>("close-button");

        if (horseTabButton != null)
        {
            horseTabButton.clicked += () => {
                if (shopPopup != null) shopPopup.style.display = DisplayStyle.Flex;
            };
        }

        if (shopCloseButton != null)
        {
            shopCloseButton.clicked += () => {
                if (shopPopup != null) shopPopup.style.display = DisplayStyle.None;
            };
        }

        CurrencyManager.OnCurrencyChanged += UpdateUI;
        UpdateUI();
    }

    private void OnDisable()
    {
        CurrencyManager.OnCurrencyChanged -= UpdateUI;
    }

    private void UpdateUI()
    {
        if (CurrencyManager.Instance == null) return;
        
        if (coinsLabel != null) coinsLabel.text = CurrencyManager.Instance.Coins.ToString("N0");
        if (gemsLabel != null) gemsLabel.text = CurrencyManager.Instance.Gems.ToString("N0");
    }
}