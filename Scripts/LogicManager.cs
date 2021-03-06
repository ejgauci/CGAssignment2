using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicManager : MonoBehaviour
{

    public PlayerController playercont;
    public string p1Weapon;
    public int p1Points=0;
    public string p2Weapon;
    public int p2Points=0;

    public int roundNumber = 1;

    public int player;

    public CanvasManager cm;

    bool enSelected =false;
    bool roundFinished = false;

    
    public float startedTime;
    public float endTime;
    public float totalTime;


    void Start()
    {
        p1Points = 0;
        p2Points = 0;
        player = GameManager.gamePlayer;
        playercont.increasePointFB(p1Points);
        playercont.increasePointFB(p2Points);
        cm.setP1Points(p1Points);
        cm.setP2Points(p2Points);

        startedTime = System.DateTime.Now.Second;
    }

    void Update()
    {
        if (playercont.getEnWeapon() != "" && enSelected ==false && playercont.getMyWeapon() != "")
        {
            print("player liehor ghazel weapon");
            stopRound();
        }
    }
    public void Surrender()
    {
        endTime = System.DateTime.Now.Second;
        totalTime = endTime - startedTime;
        FirebaseController.setTotalTime(totalTime.ToString());
        print("Surrendered");
        //print("total time: "+ totalTime);
        SceneManager.LoadScene("Win");

    }


    public void setEnSelected(bool _enSeleted)
    {
        enSelected = _enSeleted;
    }
    public void setRoundFinished(bool _roundFinished)
    {
        roundFinished = _roundFinished;
    }

    public void stopRound()
    {
        cm.stopTimer();
        timerEnded();
        enSelected = true;
    }

    public void timerEnded()
    {
        if (roundFinished == false)
        {
            if (player == 1)
            {
                p2Weapon = playercont.getEnWeapon();
                p1Weapon = playercont.getMyWeapon();
            }
            else if (player == 2)
            {

                p1Weapon = playercont.getEnWeapon();
                p2Weapon = playercont.getMyWeapon();
            }

            CompareMoves();
            playercont.setEnemySprite();
            roundFinished = true;
        }
        
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
                newRound(0);
                break;

            case ("Rock", "Scissors"):
            case ("Paper", "Rock"):
            case ("Scissors", "Paper"):
                print("P1 WON");
                cm.setStatus(whoWon(1));
                newRound(1);
                break;

            case ("Rock", "Paper"):
            case ("Paper", "Scissors"):
            case ("Scissors", "Rock"):
                print("P2 WON");
                cm.setStatus(whoWon(2));
                newRound(2);
                break;
            case ("Rock", ""):
            case ("Paper", ""):
            case ("Scissors", ""):
                print("P1 WON");
                cm.setStatus(whoWon(1));
                newRound(1);
                break;

            case ("", "Paper"):
            case ("", "Scissors"):
            case ("", "Rock"):
                print("P2 WON");
                cm.setStatus(whoWon(2));
                newRound(2);
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


    public string getRoundNumber()
    {
        return roundNumber.ToString();
    }
    public void newRound(int playerWinner)
    {
        roundNumber++;

        if (roundNumber == 6)
        {
            if (playerWinner == 1)
            {
                addPointsToPlayer(1);

            }
            else if (playerWinner == 2)
            {
                addPointsToPlayer(2);
            }



            endTime = System.DateTime.Now.Second;
            totalTime = endTime - startedTime;
            FirebaseController.setTotalTime(totalTime.ToString());
            print("winner scene");
            //print("total time: "+ totalTime);
            SceneManager.LoadScene("Win");
        }
        else
        {
            

            if (playerWinner == 0)
            {
                //Draw
                cm.startRoundCountdown();
            }
            else if (playerWinner == 1)
            {
                addPointsToPlayer(1);

            }
            else if (playerWinner == 2)
            {
                addPointsToPlayer(2);
            }
        }
        
    }

    void addPointsToPlayer(int playerWinner)
    {
        if (playerWinner == 1)
        {
            p1Points++;
            if (roundNumber != 6)
            {
                cm.setP1Points(p1Points);
            }
            
        }
        else
        {
            p2Points++;
            if (roundNumber != 6)
            {
                cm.setP2Points(p2Points);
            }
        }

        if (playerWinner == player)
        {
            if (player == 1)
            {
                playercont.increasePointFB(p1Points);
            }
            else if (player == 2)
            {
                playercont.increasePointFB(p2Points);
            }
            
        }
    }


    public void resetRound()
    {
        playercont.resetRoundPC();
    }

    

}
