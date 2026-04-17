using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;

public class ShopController : MonoBehaviour
{
    [SerializeField] private List<HorseShopData> horses;
    private ScrollView horseList;
    
    [Serializable]
    public class HorseShopData
    {
        public string name;
        public Sprite icon;
        public int price;
        public bool isCoins;
        public bool isBought;
    }

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        horseList = root.Q<ScrollView>("horse-list");
        
        if (horseList != null)
        {
            PopulateShop();
        }
    }

    private void PopulateShop()
    {
        horseList.Clear();
        
        foreach (var horse in horses)
        {
            var item = CreateHorseItem(horse);
            horseList.Add(item);
        }
    }

    private VisualElement CreateHorseItem(HorseShopData horse)
    {
        var item = new VisualElement();
        item.AddToClassList("horse-item");

        var icon = new VisualElement();
        icon.AddToClassList("horse-icon");
        if (horse.icon != null) icon.style.backgroundImage = new StyleBackground(horse.icon);
        item.Add(icon);

        var info = new VisualElement();
        info.AddToClassList("horse-info");
        
        var nameLabel = new Label(horse.name);
        nameLabel.AddToClassList("horse-name");
        info.Add(nameLabel);

        var priceContainer = new VisualElement();
        priceContainer.AddToClassList("price-container");
        
        var priceIcon = new VisualElement();
        priceIcon.AddToClassList("price-icon");
        // Using common classes for icons
        priceIcon.AddToClassList(horse.isCoins ? "coin-icon" : "gem-icon");
        priceContainer.Add(priceIcon);

        var priceLabel = new Label(horse.price.ToString());
        priceLabel.AddToClassList("price-text");
        priceContainer.Add(priceLabel);
        
        info.Add(priceContainer);
        item.Add(info);

        var checkmark = new VisualElement();
        checkmark.AddToClassList("bought-checkmark");
        item.Add(checkmark);

        var buyButton = new Button();
        buyButton.text = "BUY";
        buyButton.AddToClassList("buy-button");
        
        buyButton.clicked += () => {
            if (horse.isBought) return;
            
            bool success = false;
            if (horse.isCoins) success = CurrencyManager.Instance.SpendCoins(horse.price);
            else success = CurrencyManager.Instance.SpendGems(horse.price);

            if (success)
            {
                horse.isBought = true;
                item.AddToClassList("is-bought");
            }
        };

        if (horse.isBought)
        {
            item.AddToClassList("is-bought");
        }

        item.Add(buyButton);
        return item;
    }
}