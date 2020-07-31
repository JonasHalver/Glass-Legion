using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSlot : MonoBehaviour
{
    public Color offense, support, highlightOffense, highlightSupport;
    public Info.Role role = Info.Role.Offense;
    private Image img;
    private Highlight highlight;
    public Card occupyingCard;
    public bool Occupied
    {
        get
        {
            return occupyingCard;
        }
    }

    void Start()
    {
        highlight = GetComponent<Highlight>();
        img = GetComponent<Image>();
        highlight.highlightColor = role == Info.Role.Offense ? highlightOffense : highlightSupport;
        img.color = role == Info.Role.Offense ? offense : support;
        highlight.originalColor = img.color;
    }

    void Update()
    {
        if (transform.childCount == 0 && occupyingCard)
        {
            occupyingCard = null;
        }
        else if (transform.childCount != 0 && !occupyingCard)
        {
            occupyingCard = transform.GetChild(0).GetComponent<Card>();
        }
    }
}
