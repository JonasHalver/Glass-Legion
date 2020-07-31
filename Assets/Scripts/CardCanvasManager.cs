using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CardCanvasManager : MonoBehaviour
{
    public static CardCanvasManager instance;
    [HideInInspector]
    public GraphicRaycaster raycaster;

    public static GameObject selectedCard;
    private static List<RaycastResult> results = new List<RaycastResult>();
    private static List<GameObject> prevResults = new List<GameObject>();
    private static List<GameObject> newResults = new List<GameObject>();

    public bool locationIsValid = false;
    public GameObject validLocation;

    public event Action OnPurchase;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedCard)
        {
            Card card = selectedCard.GetComponent<Card>();
            CheckHoverWhileCardSelected();
            if (prevResults.Count > 0)
            {
                foreach (GameObject location in prevResults)
                {
                    CardSlot slot = location.GetComponent<CardSlot>();
                    if (slot && !card.isInShop)
                    {
                        if (CardAndSlotMatch(card, slot))
                        {
                            if (slot.Occupied && slot.occupyingCard != card)
                            {
                                if (OccupantCanMoveLeft(slot))
                                {

                                }
                                else if (OccupantCanMoveRight(slot))
                                {

                                }
                                else
                                {
                                    validLocation = null;
                                    locationIsValid = false;
                                }
                            }
                            else
                            {
                                locationIsValid = CardAndSlotMatch(card, slot);
                                validLocation = locationIsValid ? location : null;
                            }
                        }                        
                    }
                    else if (location.CompareTag("HandArea"))
                    {
                        validLocation = location;
                        locationIsValid = true;
                    }
                    else if (location.CompareTag("Sell") && !card.isInShop)
                    {
                        validLocation = location;
                        locationIsValid = true;
                    }
                }
            }
            else
            {
                validLocation = null;
                locationIsValid = false;
            }
        }
    }

    public static void CardSelection(GameObject card)
    {
        selectedCard = card;
    }

    public static void CardDeselection()
    {
        if (instance.locationIsValid)
        {
            CardSlot slot = instance.validLocation.GetComponent<CardSlot>();
            Card card = selectedCard.GetComponent<Card>();

            if (card.isInShop)
            {
                if (!slot)
                {
                    card.MoveToHand();
                    //instance.OnPurchase.Invoke();
                    Shop.instance.OnPurchase(card);
                    card.isInShop = false;
                }
            }
            else
            {
                if (slot)
                {
                    card.MoveToSlot(slot);
                }
                else if (instance.validLocation.CompareTag("Sell"))
                {
                    Shop.instance.inventory.ReAddCard(card.cardInfo);
                    Destroy(card.gameObject);
                    PlayerInfo.AddMoney(Shop.instance.sellValue);
                }
                else
                {
                    card.MoveToHand();
                }
            }
        }
        else
        {
            selectedCard.transform.parent = selectedCard.GetComponent<CardMove>().prevParent;
            selectedCard.transform.localPosition = Vector3.zero;
        }
        selectedCard = null;
        for (int i = 0; i < prevResults.Count; i++)
        {
            prevResults[i].gameObject.GetComponent<Highlight>().OnHoverExit();
            prevResults.RemoveAt(i);
            i--;
        }
    }

    public void CheckHoverWhileCardSelected()
    {
        prevResults.AddRange(newResults);
        newResults.Clear();
        results.Clear();
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.position = Input.mousePosition;
        raycaster.Raycast(ped, results);
        if (results.Count > 0)
        {
            foreach (RaycastResult hit in results)
            {
                if (hit.gameObject.GetComponent<Highlight>())
                {
                    newResults.Add(hit.gameObject);
                    hit.gameObject.GetComponent<Highlight>().OnHoverEnter();
                }
            }
        }
        for (int i = 0; i < prevResults.Count; i++)
        {
            if (!newResults.Contains(prevResults[i]))
            {
                prevResults[i].gameObject.GetComponent<Highlight>().OnHoverExit();
                prevResults.RemoveAt(i);
                i--;
            }
        }
    }

    public bool CardAndSlotMatch(Card card, CardSlot slot)
    {
        bool output = false;

        output = (card.info.role == Info.Role.Any) || (slot.role == card.info.role);

        return output;
    }

    public bool OccupantCanMoveLeft(CardSlot current)
    {
        bool output = false;
        Card card = current.occupyingCard;
        int slotIndex = BattlefieldManager.instance.slots.IndexOf(current);

        if (slotIndex - 1 >= 0)
        {
            CardSlot leftSlot = BattlefieldManager.instance.slots[slotIndex - 1];
            if (!leftSlot.Occupied)
            {
                if (CardAndSlotMatch(card, leftSlot))
                {
                    output = true;
                    if (leftSlot.transform.childCount == 0)
                        card.MoveToSlot(leftSlot);
                }
                else
                {
                    output = false;
                }                
            }
            else
            {
                if (OccupantCanMoveLeft(leftSlot))
                {
                    output = true;
                    //card.MoveToSlot(leftSlot);
                }
                else
                {
                    output = false;
                }
            }
        }
        else
        {
            output = false;
        }

        return output;
    }
    public bool OccupantCanMoveRight(CardSlot current)
    {
        bool output = false;
        Card card = current.occupyingCard;
        int slotIndex = BattlefieldManager.instance.slots.IndexOf(current);

        if (slotIndex + 1 < BattlefieldManager.instance.slots.Count)
        {
            CardSlot rightSlot = BattlefieldManager.instance.slots[slotIndex + 1];
            if (!rightSlot.Occupied)
            {
                if (CardAndSlotMatch(card, rightSlot))
                {
                    output = true;
                    if (rightSlot.transform.childCount == 0)
                        card.MoveToSlot(rightSlot);
                }
                else
                {
                    output = false;
                }
            }
            else
            {
                if (OccupantCanMoveLeft(rightSlot))
                {
                    output = true;
                    //card.MoveToSlot(rightSlot);
                }
                else
                {
                    output = false;
                }
            }
        }
        else
        {
            output = false;
        }

        return output;
    }

    public void ShiftCardsLeft(CardSlot current)
    {

        int slotIndex = BattlefieldManager.instance.slots.IndexOf(current);

        if (slotIndex - 1 >= 0)
        {
            CardSlot leftSlot = BattlefieldManager.instance.slots[slotIndex - 1];
            if (!leftSlot.Occupied)
            {

            }
        }
    }
    public void ShiftCardsRight(CardSlot current)
    {

    }
}
