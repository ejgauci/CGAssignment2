using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinSceneScript : MonoBehaviour
{

    [SerializeField] public TMPro.TMP_Text WinnerName;

    public string winnerText="";
    public int player;
    public static int p1score;
    public static int p2score;


    void Start()
    {
        player = GameManager.gamePlayer;
        getPoints();
    }

    public void getPoints()
    {
        FirebaseController.getP1Score();
        FirebaseController.getP2Score();

        StartCoroutine(ExampleCoroutine());

    }

    public static void setP1Score(string _p1score)
    {
        p1score = int.Parse(_p1score);

        //print("p1Set: "+p1score +", "+_p1score);
    }

    public static void setP2Score(string _p2score)
    {
        p2score = int.Parse(_p2score);
        //print("p2Set: " + p2score + ", " + _p2score);
    }

   

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        if (p1score > p2score)
        {
            //p1 won
            if (player == 1)
            {
                winnerText = "You Won";
            }
            else
            {
                winnerText = "You Lost";
            }
        }
        else if (p1score < p2score)
        {
            //p2 won
            if (player == 1)
            {
                winnerText = "You Lost";
            }
            else
            {
                winnerText = "You Won";
            }
        }
        else
        {
            winnerText = "kif gejt aw?";
        }

        WinnerName.GetComponent<TextMeshProUGUI>().text = winnerText;
    }

}
