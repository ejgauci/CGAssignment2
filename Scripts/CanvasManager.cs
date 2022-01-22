using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    public int seconds = 5;
    public LogicManager lmanager;

    [SerializeField] public TMPro.TMP_Text statusText;
    [SerializeField] public TMPro.TMP_Text player1Pionts;
    [SerializeField] public TMPro.TMP_Text player2Points;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    // Update is called once per frame
    void Update()
    {
        
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


    public void setStatus(String status)
    {
        statusText.GetComponent<TextMeshProUGUI>().text = status;
    }

}
