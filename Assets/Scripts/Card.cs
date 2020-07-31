using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardInfo cardInfo;
    private CardMove move;
    public Info info;
    public TextMeshProUGUI pt;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI tier;
    public Image cardImage;
    public Image cardFrame;
    public int damage;

    public List<Buff> buffs = new List<Buff>();

    public Color onyx, carnelian, amethyst, malachite, sapphire, neutral, support, offense, any;
    private Color currentColor, roleColor;
    private string tierText;

    public CardSlot currentSlot;

    public bool isInShop = true;

    public int PowerBuffs
    {
        get
        {
            if (buffs.Count > 0)
            {
                int total = 0;
                foreach(Buff b in buffs)
                {
                    total += b.powerBuff;
                }
                return total;
            }
            else
            {
                return 0;
            }
        }
    }

    public int HealthBuffs
    {
        get
        {
            if (buffs.Count > 0)
            {
                int total = 0;
                foreach(Buff b in buffs)
                {
                    total += b.healthBuff;
                }
                return total;
            }
            else
            {
                return 0;
            }
        }
    }

    public int Power
    {
        get
        {
            if (info != null)
            {
                return info.power + PowerBuffs;
            }
            else
            {
                return 0;
            }
        }
    }

    public int Health
    {
        get
        {
            if (info != null)
            {
                return info.health + HealthBuffs - damage;
            }
            else
            {
                return 0;
            }
        }
    }

    public bool IsDead
    {
        get
        {
            if  (info != null)
            {
                return Health <= 0;
            }
            else
            {
                return true;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (cardInfo)
        {
            info = cardInfo.GenerateInfo();
        }
        move = GetComponent<CardMove>();
        cardFrame = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        pt.text = Power.ToString() + "/" + Health.ToString();
        cardName.text = info != null ? info.name : "Null";
        if (info != null)
        {
            switch (info.faction)
            {
                case Info.Faction.Agency:
                    currentColor = amethyst;
                    break;
                case Info.Faction.Command:
                    currentColor = carnelian;
                    break;
                case Info.Faction.Marauders:
                    currentColor = malachite;
                    break;
                case Info.Faction.Order:
                    currentColor = onyx;
                    break;
                case Info.Faction.Syndicate:
                    currentColor = sapphire;
                    break;
                case Info.Faction.Neutral:
                    currentColor = neutral;
                    break;
            }
            switch (info.tier)
            {
                case Info.Tier.One:
                    tierText = "*";
                    break;
                case Info.Tier.Two:
                    tierText = "**";
                    break;
                case Info.Tier.Three:
                    tierText = "***";
                    break;
                case Info.Tier.Four:
                    tierText = "****";
                    break;
                case Info.Tier.Five:
                    tierText = "*****";
                    break;
            }
            switch (info.role)
            {
                case Info.Role.Any:
                    roleColor = any;
                    break;
                case Info.Role.Offense:
                    roleColor = offense;
                    break;
                case Info.Role.Support:
                    roleColor = support;
                    break;
            }
        }
        cardFrame.color = roleColor;
        tier.text = tierText;
        cardImage.color = currentColor;
    }

    public void MoveToSlot(CardSlot targetSlot)
    {
        transform.parent = targetSlot.transform;
        transform.localPosition = Vector3.zero;

        targetSlot.occupyingCard = this;
        CardSlot prevSlot = move.prevParent.GetComponent<CardSlot>();
        if (prevSlot)
        {
            prevSlot.occupyingCard = null;
        }
    }
    public void MoveToHand()
    {
        transform.parent = Hand.instance.transform;
        CardSlot prevSlot = move.prevParent.GetComponent<CardSlot>();
        if (prevSlot)
        {
            prevSlot.occupyingCard = null;
        }
    }
}

public class Info
{
    public string name;
    public enum Faction { Agency, Order, Command, Marauders, Syndicate, Neutral }
    public enum Tier { One, Two, Three, Four, Five }
    public enum Role { Offense, Support, Any }
    public Role role = Role.Any;
    public Faction faction = Faction.Neutral;
    public Tier tier = Tier.One;
    public int power;
    public int health;

    public Info(string _name, int _power, int _health, Faction _faction, Tier _tier, Role _role)
    {
        name = _name;
        faction = _faction;
        power = _power;
        health = _health;
        tier = _tier;
        role = _role;
    }
}

public class Buff
{
    public enum BuffType { Permanent, Self, Bond, Debuff, Other }
    public Card bonder;
    public BuffType buffType = BuffType.Other;
    public int powerBuff;
    public int healthBuff;
}
