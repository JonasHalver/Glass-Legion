using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyBattlefield : MonoBehaviour
{
    public static EnemyBattlefield instance;
    public GameObject cardPrefab;
    public List<GameObject> lineup = new List<GameObject>();
    public List<CardInfo> testLineup = new List<CardInfo>();
    public List<CardSlot> slots = new List<CardSlot>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            lineup.Add(null);
        }
        GameManager.instance.OnRecruitmentRoundEnd += RecruitmentRoundEnd;
        for (int i = 0; i < transform.childCount; i++)
        {
            slots.Add(transform.GetChild(i).GetComponent<CardSlot>());
        }
    }

    void Awake()
    {
        instance = this;
    }

    public void RecruitmentRoundEnd()
    {
        GenerateLineup();
    }

    public void GenerateLineup()
    {
        for (int i = 0; i < testLineup.Count; i++)
        {
            if (testLineup[i] != null)
            {
                GameObject newCard = Instantiate(cardPrefab, slots[i].transform);
                newCard.transform.localPosition = Vector3.zero;
                Card card = newCard.GetComponent<Card>();
                card.cardInfo = testLineup[i];
                slots[i].occupyingCard = card;
                lineup[i] = newCard;
            }
            else
            {
                lineup[i] = null;
            }
        }
    }
}
