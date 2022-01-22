using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{

    public int score = 0;
    public int player;
    
    
    //public static int gamePlayer = 1;

    public string weapon = "";

    public GameObject PlayerObject;
    public SpriteRenderer playerSR;
    public Sprite MyIdle;
    public Sprite MyRock;
    public Sprite MyPaper;
    public Sprite MyScissors;



    // Start is called before the first frame update
    private void Start()
    {
        player = GameManager.gamePlayer;
        PlayerObject = GameObject.FindGameObjectWithTag("Player"+player);
        playerSR = PlayerObject.GetComponent<SpriteRenderer>();

        if (player == 2)
        {
            Flip();
        }
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
    }
    public void chosePaper()
    {
        weapon = "Paper";
        FirebaseController.UpdateWeapon("Paper");
        setCharacterSprite(weapon);
    }
    public void choseScissors()
    {
        weapon = "Scissors";
        FirebaseController.UpdateWeapon("Scissors");
        setCharacterSprite(weapon);

    }

    public void setCharacterSprite(string weapon)
    {
        if (weapon == "Rock")
        {
            //Rock
            playerSR.sprite = MyRock;
            
        }
        else if (weapon == "Paper")
        {
            //paper
            playerSR.sprite = MyPaper;
        }
        else if (weapon == "Scissors")
        {
            //scissors
            playerSR.sprite = MyScissors;
        }

       
    }

    
    public void Flip()
    {
        //flip
        playerSR.flipX = true;
    }
}





