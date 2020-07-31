using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/New Card")]
public class CardInfo : ScriptableObject
{
    public string cardName;
    public int basePower;
    public int baseHealth;
    public Info.Faction faction = Info.Faction.Neutral;
    public Info.Tier tier = Info.Tier.One;
    public Info.Role role = Info.Role.Any;

    public Info GenerateInfo()
    {
        return new Info(cardName, basePower, baseHealth, faction, tier, role);
    }
}
