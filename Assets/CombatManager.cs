using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public string outcome = "";
    public List<GameObject> p0Lineup = new List<GameObject>();
    public List<GameObject> p1Lineup = new List<GameObject>();
    int lastAttacker0 = 0;
    int lastAttacker1 = 0;

    void Start()
    {
        GameManager.instance.OnCombatStart += StartCombat;
    }

    public void StartCombat()
    {
        outcome = "";
        // Player is 0, enemy is 1
        p0Lineup = PlayerInfo.instance.lineup;
        p1Lineup = EnemyBattlefield.instance.lineup;
        // Have the game manager grab the PlayerInfo from each player, then pick randomly from a list of those two
        int randPlayer = Random.Range(0, 2);
        outcome += randPlayer.ToString() + " ";

        // Create a list of lists of lineups
    }

    public void Attack() // Take an int ref to lineup
    {

    }
}
