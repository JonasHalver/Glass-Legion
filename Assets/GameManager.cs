using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public event Action OnNewRecruitmentRound;
    public event Action OnRecruitmentRoundEnd;
    public event Action OnCombatStart;
    
    public static int RoundNumber
    {
        get
        {
            return instance.currentRoundNumber;
        }
        set
        {
            instance.currentRoundNumber += value;
        }
    }

    [Header("Info")]
    public int currentRoundNumber = 0;


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NewRecruitmentRound();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            EndRecruitment();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCombat();
        }
    }

    public void NewRecruitmentRound()
    {
        RoundNumber = 1;
        OnNewRecruitmentRound.Invoke();
    }

    public void EndRecruitment()
    {
        OnRecruitmentRoundEnd.Invoke();
    }

    public void StartCombat()
    {
        OnCombatStart.Invoke();
    }
}
