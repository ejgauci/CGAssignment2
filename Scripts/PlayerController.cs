using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{

    public int score = 0;
    
    //public static int gamePlayer = 1;

    public string weapon = "";

    // Start is called before the first frame update
    private void Start()
    {
        
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
    }
    public void chosePaper()
    {
        weapon = "Paper";
        FirebaseController.UpdateWeapon("Paper");
    }
    public void choseScissors()
    {
        weapon = "Scissors";
        FirebaseController.UpdateWeapon("Scissors");

    }

}





