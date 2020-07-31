using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    [Header("Dependencies")]
    public TextMeshProUGUI money;
    public TextMeshProUGUI health;

    [Header("Info")]
    public int currentMoney;
    public int currentHealth;
    public int roundMoney;

    public List<GameObject> lineup = new List<GameObject>();
    public static int Money
    {
        get
        {
            return instance.currentMoney;
        }
        set
        {
            instance.currentMoney = value;
        }
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GameManager.instance.OnNewRecruitmentRound += NewRecruitmentRound;
        GameManager.instance.OnRecruitmentRoundEnd += RecruitmentEnd;
    }

    void Update()
    {
        money.text = Money.ToString();
        health.text = currentHealth.ToString();
        
    }

    public static void SpendMoney(int amount)
    {
        instance.currentMoney -= amount;
    }

    public static void AddMoney(int amount)
    {
        instance.currentMoney += amount;
    }

    public void NewRecruitmentRound()
    {
        roundMoney = 2 + GameManager.RoundNumber;
        Money = roundMoney;
    }

    public void RecruitmentEnd()
    {
        lineup.Clear();
        for (int i = 0; i < BattlefieldManager.instance.slots.Count; i++)
        {
            GameObject card = BattlefieldManager.instance.slots[i].occupyingCard ? BattlefieldManager.instance.slots[i].occupyingCard.gameObject : null;
            if (card)
            {
                lineup.Add(card);
            }
            else
            {
                lineup.Add(null);
            }
        }
    }
}
