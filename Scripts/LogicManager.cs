using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour
{

    public PlayerController playercont;
    public string p1Weapon;
    public string p2Weapon;

    public int player;

    public CanvasManager cm;

    public bool enSelected =false;

    void Start()
    {
        player = GameManager.gamePlayer;
    }

    void Update()
    {
        if (playercont.getEnWeapon() != "" && enSelected ==false && playercont.getMyWeapon() != "")
        {
            print("if in update");
            stopRound();
        }
    }


    public void stopRound()
    {
        cm.stopTimer();
        timerEnded();
        enSelected = true;
    }

    public void timerEnded()
    {
        if (player == 1)
        {
            p2Weapon = playercont.getEnWeapon();
            p1Weapon = playercont.getMyWeapon();
        }
        else if(player==2)
        {

            p1Weapon = playercont.getEnWeapon();
            p2Weapon = playercont.getMyWeapon();
        }

        CompareMoves();
        playercont.setEnemySprite();
    }

    public void CompareMoves()
    {
        switch (p1Weapon, p2Weapon)
        {
            case ("Rock", "Rock"):
            case ("Paper", "Paper"):
            case ("Scissors", "Scissors"):
                print("DRAW");
                cm.setStatus("DRAW");
                break;

            case ("Rock", "Scissors"):
            case ("Paper", "Rock"):
            case ("Scissors", "Paper"):
                print("P1 WON");
                cm.setStatus(whoWon(1));
                break;

            case ("Rock", "Paper"):
            case ("Paper", "Scissors"):
            case ("Scissors", "Rock"):
                print("P2 WON");
                cm.setStatus(whoWon(1));
                break;
        }
    }

    public string whoWon(int p)
    {
        if (player == 1)
        {
            if (p == 1)
            {
                return "You Won";
            }
            else
            {
                return "You Lost";
            }
        }
        else if (player == 2)
        {
            if (p == 1)
            {
                return "You Lost";
            }
            else
            {
                return "You Won";
            }
        }

        return "min rebah mela?";
    }



}
