using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetricEvaluationScript : MonoBehaviour
{

    public float messageTime;
    public float musicTime;
    public float carTime;
    public bool msgEval = false;
    public bool musicEval = false;
    public bool carEval = false;
    public GameObject musicCanvas;
    public GameObject carCanvas;
    public bool messageEval = false;
    public bool dwellEvalDone = false;
    public GameObject messageEvalHelper1;
    public GameObject messageEvalHelper2;
    public GameObject messageEvalHelper3;
    public GameObject messageEvalHelper4;
    public GameObject carEvalHelper1;
    public GameObject carEvalHelper2;
    public GameObject carEvalHelper3;
    public GameObject carEvalHelper4;
    public GameObject carEvalHelper5;
    public GameObject carEvalHelper6;
    public GameObject musicEvalHelper1;
    public GameObject musicEvalHelper2;
    public GameObject musicEvalHelper3;
    public GameObject musicEvalHelper4;
    public GameObject musicEvalHelper5;
    public BlinkCoroutine blinkScript;
    public bool experimentOn = true;
    Host host;
    // Start is called before the first frame update
    void Start()
    {
        experimentOn = false;
        if (experimentOn==false)
        {
            if (messageEvalHelper1.activeSelf) messageEvalHelper1.SetActive(false);
            if (messageEvalHelper2.activeSelf) messageEvalHelper2.SetActive(false);
            if (messageEvalHelper3.activeSelf) messageEvalHelper3.SetActive(false);
            if (messageEvalHelper4.activeSelf) messageEvalHelper4.SetActive(false);
        }
    }

    

    public void StartMessageTimer() {
        if (experimentOn == true)
        {
            Debug.Log("Mpikaaaaaaaaaaaaaaa");
            messageTime = Time.time;
            messageEval = false;
            msgEval = true;
        }
    }
    public void StopMessageTimer() {
        if (experimentOn == true)
        {
            Debug.Log("Message Evaluation Time: " + (Time.time - messageTime));
            if (messageEvalHelper1.activeSelf) messageEvalHelper1.SetActive(false);
            if (messageEvalHelper2.activeSelf) messageEvalHelper2.SetActive(false);
            if (messageEvalHelper3.activeSelf) messageEvalHelper3.SetActive(false);
            if (messageEvalHelper4.activeSelf) messageEvalHelper4.SetActive(false);
            //if (messageTime != 0) Debug.Log("Message Evaluation Time: "+messageTime);
            messageEval = true;
            if (blinkScript.eyeDwellTimeBool)
            {
                Debug.Log("Mpika1");
                if (!carEvalHelper6.activeSelf) carEvalHelper6.SetActive(true);
            }

            Debug.Log("Mpika2");
            if (!carEvalHelper1.activeSelf) carEvalHelper1.SetActive(true);
            if (!blinkScript.eyeDwellTimeBool)
            {
                if (!carEvalHelper2.activeSelf) carEvalHelper2.SetActive(true);
            }
            if (!carEvalHelper3.activeSelf) carEvalHelper3.SetActive(true);

            if (!carEvalHelper4.activeSelf) carEvalHelper4.SetActive(true);
            if (!carEvalHelper5.activeSelf) carEvalHelper5.SetActive(true);
        }
    }


    public void StartMusicTimer() {
        if (experimentOn == true)
        {
            musicTime = Time.time;
            musicEval = true;
        }
    }
    public void StopMusicTimer() {
        if (experimentOn == true)
        {
            Debug.Log("Music Evaluation Time: " + (Time.time - musicTime));

            if (musicEvalHelper1.activeSelf) musicEvalHelper1.SetActive(false);
            if (musicEvalHelper2.activeSelf) musicEvalHelper2.SetActive(false);
            if (musicEvalHelper3.activeSelf) musicEvalHelper3.SetActive(false);
            if (musicEvalHelper4.activeSelf) musicEvalHelper4.SetActive(false);
            if (musicEvalHelper5.activeSelf) musicEvalHelper5.SetActive(false);
            if (blinkScript.eyeDwellTimeBool) dwellEvalDone = true;
        }
        //if (musicTime != 0) Debug.Log("Message Evaluation Time: " + musicTime);
        //musicEval = false;
    }


    public void StartCarTimer()
    {
        if (experimentOn == true)
        {
            carTime = Time.time;
            carEval = true;
        }
    }
    public void StopCarTimer()
    {
        if (experimentOn == true)
        {
            Debug.Log("Car Evaluation Time: " + (Time.time - carTime));

            if (carEvalHelper1.activeSelf) carEvalHelper1.SetActive(false);
            if (carEvalHelper2.activeSelf) carEvalHelper2.SetActive(false);
            if (carEvalHelper3.activeSelf) carEvalHelper3.SetActive(false);
            if (carEvalHelper4.activeSelf) carEvalHelper4.SetActive(false);
            if (carEvalHelper5.activeSelf) carEvalHelper5.SetActive(false);
            if (carEvalHelper6.activeSelf) carEvalHelper6.SetActive(false);
            if (!musicEvalHelper1.activeSelf) musicEvalHelper1.SetActive(true);
            if (!musicEvalHelper2.activeSelf) musicEvalHelper2.SetActive(true);
            if (!musicEvalHelper3.activeSelf) musicEvalHelper3.SetActive(true);
            if (!musicEvalHelper4.activeSelf) musicEvalHelper4.SetActive(true);
            if (!musicEvalHelper5.activeSelf) musicEvalHelper5.SetActive(true);
        }
        //if (carTime != 0) Debug.Log("Message Evaluation Time: " + carTime);
        //carEval = false;
    }

}
