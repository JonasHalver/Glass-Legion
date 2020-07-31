using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public static Shop instance;

    [Header("Dependencies")]
    public Toggle autoLevel;
    public Button levelUpButton;
    public Button buyXPButton;
    public TextMeshProUGUI xpButtonText;
    public Image lockIcon;
    public Sprite locked, unlocked;
    public GameObject lockBG;
    public Button rerollButton;
    public GameObject cardPrefab;
    public Slider xpBar;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI shopTierText;

    private Transform shopArea;
    [HideInInspector]
    public ShopInventory inventory;    
    
    [Header("Info")]
    public Info.Tier shopTier = Info.Tier.One;
    public List<CardInfo> currentDisplay = new List<CardInfo>();
    private List<Card> prevDisplay = new List<Card>();
    public int cardCost = 3;
    public int rerollCost = 1;
    public int sellValue = 1;
    public int currentXP = 0;
    public int nextLevelXP;
    public bool lockShop = false;

    [Header("Cards per Tier")]
    public int tierOne   = 3;
    public int tierTwo   = 4;
    public int tierThree = 4;
    public int tierFour  = 5;
    public int tierFive  = 5;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        shopArea = transform.GetChild(1);
        inventory = GetComponent<ShopInventory>();
        GameManager.instance.OnNewRecruitmentRound += NewRecruitmentRound;
        xpButtonText = buyXPButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        ExperienceManagement();
        ShopTierText();
    }

    public void ExperienceManagement()
    {
        nextLevelXP = MaxXP();
        currentXP = Mathf.Clamp(currentXP, 0, nextLevelXP);
        xpBar.maxValue = nextLevelXP;
        xpBar.value = currentXP;
        xpText.text = currentXP + "/" + nextLevelXP;
        buyXPButton.interactable = (currentXP != nextLevelXP && shopTier != Info.Tier.Five && PlayerInfo.Money >= 2) || (currentXP == nextLevelXP && shopTier != Info.Tier.Five);
        rerollButton.interactable = PlayerInfo.Money >= rerollCost;
        if (autoLevel.isOn && currentXP == nextLevelXP)
        {
            xpButtonText.text = "Buy 2 XP";
            IncreaseTier();
        }
        else if (!autoLevel.isOn && currentXP == nextLevelXP)
        {
            xpButtonText.text = "Level Up";
        }
        else
        {
            xpButtonText.text = "Buy 2 XP";
        }
    }

    public void ShopTierText()
    {
        string output = "";
        switch (shopTier)
        {
            case Info.Tier.One:
                output = "*";
                break;
            case Info.Tier.Two:
                output = "**";
                break;
            case Info.Tier.Three:
                output = "***";
                break;
            case Info.Tier.Four:
                output = "****";
                break;
            case Info.Tier.Five:
                output = "*****";
                break;
        }
        shopTierText.text = output;
    }

    public int MaxXP()
    {
        int output = 0;
        switch (shopTier)
        {
            case Info.Tier.One:
                output = 4;
                break;
            case Info.Tier.Two:
                output = 6;
                break;
            case Info.Tier.Three:
                output = 10;
                break;
            case Info.Tier.Four:
                output = 12;
                break;
            case Info.Tier.Five:
                output = 12;
                break;
        }
        return output;
    }

    public void BuyXP()
    {
        if (currentXP != nextLevelXP)
        {
            currentXP += 2;
            PlayerInfo.SpendMoney(2);
        }
        else
        {
            IncreaseTier();
        }
    }

    public void Reroll()
    {
        PlayerInfo.SpendMoney(rerollCost);
        CreateNewShopDisplay();
    }

    public void CreateNewShopDisplay()
    {
        int amount = 0;
        
        if (!lockShop)
        {
            for (int i = 0; i < prevDisplay.Count; i++)
            {
                inventory.ReAddCard(prevDisplay[i].cardInfo);
                Destroy(prevDisplay[i].gameObject);
            }
            prevDisplay.Clear();            
        }

        switch (shopTier)
        {
            case Info.Tier.One:
                amount = tierOne;
                break;
            case Info.Tier.Two:
                amount = tierTwo;
                break;
            case Info.Tier.Three:
                amount = tierThree;
                break;
            case Info.Tier.Four:
                amount = tierFour;
                break;
            case Info.Tier.Five:
                amount = tierFive;
                break;
        }
        amount -= prevDisplay.Count;
        
        for (int i = 0; i < amount; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, shopArea);
            Card card = newCard.GetComponent<Card>();
            prevDisplay.Add(card);
            card.cardInfo = inventory.GenerateCard(shopTier);
        }

        
    }

    public void IncreaseTier()
    {
        currentXP = 0;
        if (shopTier != Info.Tier.Five)
            shopTier += 1;
    }

    public void OnPurchase(Card card)
    {
        prevDisplay.Remove(card);
        PlayerInfo.SpendMoney(cardCost);
    }

    public void NewRecruitmentRound()
    {
        if (GameManager.RoundNumber != 1)
            currentXP += 1;
        CreateNewShopDisplay();
        LockShop(false);
    }

    public void LockShop(bool flag)
    {
        if (flag)
        {
            lockShop = !lockShop;
        }
        else
        { 
            lockShop = false;
        }
        lockBG.SetActive(lockShop);
        lockIcon.sprite = lockShop ? locked : unlocked;
    }
}
