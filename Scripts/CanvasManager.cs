using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    public int seconds = 10;
    public int countdownRound;
    public LogicManager lmanager;

    [SerializeField] public TMPro.TMP_Text statusText;
    [SerializeField] public TMPro.TMP_Text roundCount;
    [SerializeField] public TMPro.TMP_Text player1Points;
    [SerializeField] public TMPro.TMP_Text player2Points;

    // Start is called before the first frame update
    void Start()
    {
        startTimer();
        setRoundCount("Round 1");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void startTimer()
    {
        seconds = 10;
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {

        while (seconds > 0)
        {
            transform.Find("CountdownTimer").GetComponent<TextMeshProUGUI>().text = formatTime(seconds);
            yield return new WaitForSeconds(1f);
            seconds--;
        }

        transform.Find("CountdownTimer").GetComponent<TextMeshProUGUI>().text = "00:00";
        lmanager.timerEnded();


    }

    static string formatTime(int secs)
    {
        int mins = (secs % 3600) / 60;
        secs = secs % 60;
        return string.Format("{0:D2}:{1:D2}", mins, secs);
    }


    public void stopTimer()
    {
        seconds = 0;
    }


    public void setStatus(String status)
    {
        statusText.GetComponent<TextMeshProUGUI>().text = status;
    }

    public void setP1Points(int points)
    {
        player1Points.GetComponent<TextMeshProUGUI>().text = "P1: " + points;
        startRoundCountdown();
    }

    public void setP2Points(int points)
    {
        player2Points.GetComponent<TextMeshProUGUI>().text = "P2: " + points;
        startRoundCountdown();
    }


    public void setRoundCount(string round)
    {
        roundCount.GetComponent<TextMeshProUGUI>().text = round;
    }


    public void startRoundCountdown()
    {

        countdownRound = 3;
        StartCoroutine(RoundCountdown());
    }

    IEnumerator RoundCountdown()
    {

        while (countdownRound > 0)
        {
            setRoundCount(countdownRound.ToString());
            yield return new WaitForSeconds(1f);
            countdownRound--;
        }

        setRoundCount("Round "+lmanager.getRoundNumber());
        setStatus("VS");
        lmanager.resetRound();
        startTimer();

    }
}
