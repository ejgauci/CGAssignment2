using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{

    public int score = 0;
    public int points = 0;

    public int posX = 0;
    public int posY = 0;


    public static int gamePlayer = 1;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (GameManager.gamePlayer == 1)
            {
                if (posX > -4)
                {
                    transform.position -= new Vector3(1f, 0); //Change position
                    score++;

                    posX -= 1;

                    FirebaseController.UpdatePostion(posX, posY);
                }
            }
            else if (GameManager.gamePlayer == 2)
            {
                if (posX > -12)
                {
                    transform.position -= new Vector3(1f, 0); //Change position
                    score++;

                    posX -= 1;

                    FirebaseController.UpdatePostion(posX, posY);
                }
            }
            

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            if (GameManager.gamePlayer == 1)
            {
                if (posX < 12)
                {
                    transform.position += new Vector3(1f, 0);
                    score++;

                    posX += 1;

                    FirebaseController.UpdatePostion(posX, posY);
                }
            }
            else if (GameManager.gamePlayer == 2)
            {
                if (posX < 4)
                {
                    transform.position += new Vector3(1f, 0);
                    score++;

                    posX += 1;

                    FirebaseController.UpdatePostion(posX, posY);
                }
            }

            

        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (GameManager.gamePlayer == 1)
            {
                if (posY < 4)
                {
                    transform.position += new Vector3(0, 1f);
                    score++;

                    posY += 1;

                    FirebaseController.UpdatePostion(posX, posY);
                }
            }
            else if (GameManager.gamePlayer == 2)
            {
                if (posY < 4)
                {
                    transform.position += new Vector3(0, 1f);
                    score++;

                    posY += 1;

                    FirebaseController.UpdatePostion(posX, posY);
                }
            }
            
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (posY > -4)
            {
                if (GameManager.gamePlayer == 1)
                {

                    transform.position -= new Vector3(0, 1f);
                    score++;

                    posY -= 1;

                    FirebaseController.UpdatePostion(posX, posY);

                }
                else if (GameManager.gamePlayer == 2)
                {

                    transform.position -= new Vector3(0, 1f);
                    score++;

                    posY -= 1;

                    FirebaseController.UpdatePostion(posX, posY);

                }
            }

            
            
        }

        if (score == 10)
        {
            points++;

            score = 0;

        }

        if (points == 10)
        {
            points++;
            FirebaseController.WonGame();
            GameManager.NextScene("Win");

        }

    }
}