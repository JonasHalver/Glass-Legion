using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopInventory : MonoBehaviour
{
    [Serializable]
    public struct CardNumbers
    {
        public CardInfo card;
        public int number;
    }

    public List<CardNumbers> cardNumbers = new List<CardNumbers>();

    public List<CardInfo> TierOne   = new List<CardInfo>();
    public List<CardInfo> TierTwo   = new List<CardInfo>();
    public List<CardInfo> TierThree = new List<CardInfo>();
    public List<CardInfo> TierFour  = new List<CardInfo>();
    public List<CardInfo> TierFive  = new List<CardInfo>();

    // Start is called before the first frame update
    void Start()
    {
        PopulateLists();
    }

    public void PopulateLists()
    {
        foreach(CardNumbers cn in cardNumbers)
        {
            switch (cn.card.tier)
            {
                case Info.Tier.One:
                    for (int i = 0; i < cn.number; i++)
                    {
                        TierOne.Add(cn.card);
                    }
                    break;
                case Info.Tier.Two:
                    for (int i = 0; i < cn.number; i++)
                    {
                        TierTwo.Add(cn.card);
                    }
                    break;
                case Info.Tier.Three:
                    for (int i = 0; i < cn.number; i++)
                    {
                        TierThree.Add(cn.card);
                    }
                    break;
                case Info.Tier.Four:
                    for (int i = 0; i < cn.number; i++)
                    {
                        TierFour.Add(cn.card);
                    }
                    break;
                case Info.Tier.Five:
                    for (int i = 0; i < cn.number; i++)
                    {
                        TierFive.Add(cn.card);
                    }
                    break;
            }
        }
    }

    public CardInfo GenerateCard(Info.Tier shopTier)
    {
        CardInfo card;
        int list = 0;
        int item = 0;
        switch (shopTier)
        {
            case Info.Tier.One:
                list = 0;
                break;
            case Info.Tier.Two:
                list = UnityEngine.Random.Range(0, 2);
                break;
            case Info.Tier.Three:
                list = UnityEngine.Random.Range(0, 3);
                break;
            case Info.Tier.Four:
                list = UnityEngine.Random.Range(0, 4);
                break;
            case Info.Tier.Five:
                list = UnityEngine.Random.Range(0, 5);
                break;
        }
        switch (list)
        {            
            case 0:
                item = UnityEngine.Random.Range(0, TierOne.Count);
                card = TierOne[item];
                TierOne.RemoveAt(item);
                break;
            case 1:
                item = UnityEngine.Random.Range(0, TierTwo.Count);
                card = TierTwo[item];
                TierTwo.RemoveAt(item);
                break;
            case 2:
                item = UnityEngine.Random.Range(0, TierThree.Count);
                card = TierThree[item];
                TierThree.RemoveAt(item);
                break;
            case 3:
                item = UnityEngine.Random.Range(0, TierFour.Count);
                card = TierFour[item];
                TierFour.RemoveAt(item);
                break;
            case 4:
                item = UnityEngine.Random.Range(0, TierFive.Count);
                card = TierFive[item];
                TierFive.RemoveAt(item);
                break;
            default:
                card = null;
                break;
        }
        return card;
    }

    public void ReAddCard(CardInfo card)
    {
        switch (card.tier)
        {
            case Info.Tier.One:
                TierOne.Add(card);
                break;
            case Info.Tier.Two:
                TierTwo.Add(card);
                break;
            case Info.Tier.Three:
                TierThree.Add(card);
                break;
            case Info.Tier.Four:
                TierFour.Add(card);
                break;
            case Info.Tier.Five:
                TierFive.Add(card);
                break;
        }
    }
}
