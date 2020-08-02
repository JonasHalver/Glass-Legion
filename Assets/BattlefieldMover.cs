using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldMover : MonoBehaviour
{
    public Transform shop;
    public Transform enemyBattlefield;
    public Vector3 farLeft, farRight, midShop, midBF;
    // READD TEST VALUES, SHITS FUCKED
    //public Vector3 shopPos, bfPos;

    public AnimationCurve speedCurve;
    public float t = 0;

    public float moveTime = 1;

    public bool moveLeft = false, moveRight = false;

    // Start is called before the first frame update
    void Start()
    {
        //farLeft = enemyBattlefield.position;
        //midShop = shop.position;
    }

    // Update is called once per frame
    void Update()
    {
        //bfPos = enemyBattlefield.position;
        //shopPos = shop.position;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveRight = false;
            moveLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveLeft = false;
            moveRight = true;
        }
        if (moveLeft || moveRight)
        {
            Move(moveLeft);
        }

        t = Mathf.Clamp01(t);

        if (t == 1 || t == 0)
        {
            moveLeft = false;
            moveRight = false;
        }
    }

    public void Move(bool moveLeft)
    {
        float tt = speedCurve.Evaluate(t);
        shop.position = Vector3.Lerp(midShop, farRight, tt);
        enemyBattlefield.position = Vector3.Lerp(farLeft, midBF, tt);
        t += moveLeft ? -Time.deltaTime : Time.deltaTime;
    }
}
