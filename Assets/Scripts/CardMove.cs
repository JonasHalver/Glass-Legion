using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardMove : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Vector3 pos;
    public bool pickedUp = false;
    public bool mousedOver;
    public Transform prevParent;
    public Card card;
    
    void Start()
    {
        card = GetComponent<Card>();
    }

    void Update()
    {
        if (pickedUp)
        {
            pos = Input.mousePosition;
            transform.position = pos;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mousedOver = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        mousedOver = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (mousedOver)
        {
            if ((card.isInShop && PlayerInfo.Money >= Shop.instance.cardCost) || !card.isInShop)
            {
                pickedUp = true;
                prevParent = transform.parent;
                transform.parent = CardCanvasManager.instance.transform;
                CardCanvasManager.CardSelection(gameObject);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (mousedOver && pickedUp)
        {
            pickedUp = false;
            CardCanvasManager.CardDeselection();
        }        
    }    
}
