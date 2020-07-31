using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattlefieldManager : MonoBehaviour
{
    public static BattlefieldManager instance;
    public GameObject slot;
    public int numberOfOffenseSlots = 3, numberOfSupportSlots = 3;
    public List<CardSlot> slots = new List<CardSlot>();

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateBattlefield();
    }

    public void GenerateBattlefield()
    {
        for (int i = 0; i < numberOfOffenseSlots; i++)
        {
            GameObject newSlot = Instantiate(slot, transform);
            slots.Add(newSlot.GetComponent<CardSlot>());
            newSlot.GetComponent<CardSlot>().role = Info.Role.Offense;
        }
        for (int i = 0; i < numberOfSupportSlots; i++)
        {
            GameObject newSlot = Instantiate(slot, transform);
            slots.Add(newSlot.GetComponent<CardSlot>());
            newSlot.GetComponent<CardSlot>().role = Info.Role.Support;
        }
    }
}
