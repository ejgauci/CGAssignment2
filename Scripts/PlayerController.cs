using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{

    public int score = 0;
    public int player;
    public int playerEn;
    
    
    //public static int gamePlayer = 1;

    public string weapon = "";
    public static string enWeapon = "";

    public GameObject PlayerObject;
    public SpriteRenderer playerSR;

    public GameObject EnemyObject;
    public SpriteRenderer enemySR;
    

    public Sprite myRock;
    public Sprite myPaper;
    public Sprite myScissors;

    public Sprite enRock;
    public Sprite enPaper;
    public Sprite enScissors;


    public CanvasManager cm;
    public LogicManager lm;
    

    // Start is called before the first frame update
    private void Start()
    {
        player = GameManager.gamePlayer;
        if (player == 1)
        {
            playerEn = 2;
        }
        else
        {
            playerEn = 1;
        }
        
        PlayerObject = GameObject.FindGameObjectWithTag("Player"+player);
        playerSR = PlayerObject.GetComponent<SpriteRenderer>();

        EnemyObject = GameObject.FindGameObjectWithTag("Player" + playerEn);
        enemySR = EnemyObject.GetComponent<SpriteRenderer>();
        


        //if (player == 2)
        {
          //  playerSR.flipX = true;
        }

        GetEnemyWeapon_Firebase();
    }

    // Update is called once per frame
    void Update()
    {

        if (score == 2)
        {
            FirebaseController.WonGame();
            GameManager.NextScene("Win");

        }

    }

    public void choseRock()
    {
        weapon = "Rock";
        FirebaseController.UpdateWeapon("Rock");

        setCharacterSprite(weapon);
        
        cm.setStatus("Waiting for other Player");

        if(getEnWeapon()!="")
        {
            lm.stopRound();
        }
    }
    public void chosePaper()
    {
        weapon = "Paper";
        FirebaseController.UpdateWeapon("Paper");
        setCharacterSprite(weapon);
        cm.setStatus("Waiting for other Player");

        if (getEnWeapon() != "")
        {
            lm.stopRound();
        }

    }
    public void choseScissors()
    {
        weapon = "Scissors";
        FirebaseController.UpdateWeapon("Scissors");
        setCharacterSprite(weapon);
        cm.setStatus("Waiting for other Player");


        if (getEnWeapon() != "")
        {
            lm.stopRound();
        }

    }

    public void setCharacterSprite(string weapon)
    {
        if (weapon == "Rock")
        {
            //Rock
            playerSR.sprite = myRock;
            
        }
        else if (weapon == "Paper")
        {
            //paper
            playerSR.sprite = myPaper;
        }
        else if (weapon == "Scissors")
        {
            //scissors
            playerSR.sprite = myScissors;
        }
        
    }
    public void setEnemySprite()
    {
        if (enWeapon == "Rock")
        {
            //Rock
            enemySR.sprite = enRock;

        }
        else if (enWeapon == "Paper")
        {
            //paper
            enemySR.sprite = enPaper;
        }
        else if (enWeapon == "Scissors")
        {
            //scissors
            enemySR.sprite = enScissors;
        }

    }

    public void GetEnemyWeapon_Firebase()
    {
        FirebaseController.getEnemyWeapon();
        
    }

    public static void setEnWeapon(string _enweapon)
    {
        enWeapon = _enweapon;
        
    }

    public string getEnWeapon()
    {
        return enWeapon;
    }

    public string getMyWeapon()
    {
        return weapon;
    }

}





