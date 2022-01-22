using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour
{

    public PlayerController playercont;
    public string p1Weapon;
    public string p2Weapon;

    public int player;

    void Start()
    {
        player = GameManager.gamePlayer;
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
    }

    public void CompareMoves()
    {
        switch (p1Weapon, p2Weapon)
        {
            case ("Rock", "Rock"):
            case ("Paper", "Paper"):
            case ("Scissors", "Scissors"):
                print("DRAW");
                break;

            case ("Rock", "Scissors"):
            case ("Paper", "Rock"):
            case ("Scissors", "Paper"):
                print("P1 WON");
                break;

            case ("Rock", "Paper"):
            case ("Paper", "Scissors"):
            case ("Scissors", "Rock"):
                print("P2 WON");
                break;


        }
    }
}
