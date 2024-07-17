using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Tobii.XR.Examples;
using System;
using UnityEngine.SceneManagement;

public class UIBarLogic : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public TextMeshProUGUI engineText;
    public TextMeshProUGUI activeEnginePreset;
    public TextMeshProUGUI radioSwitch;
    public TextMeshProUGUI stationPlaying;
    public AudioSource carAudio;
    public GameObject musicPlaying;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public GameObject ecoModeImage;
    public GameObject normalModeImage;
    public GameObject sportModeImage;
    public GameObject mutedImage;
    public GameObject unMutedImage;
    public TextMeshProUGUI musicVolume;
    public GameObject inboxNotification;
    public GameObject messageNotificationName;
    public GameObject messageNotificationContext;
    public GameObject messageNotificationCounter;
    public GameObject inboxMessageCounter;
    public int unreadInboxCounter=0;
    private Boolean sendMessage = true;
    public Boolean messageReceived = false;
    private Boolean messageCounterChanged = false;
    public UIPrinter printer;
    public GameObject inboxCanvas;
    public GameObject incomingCall;
    public GameObject callingLabel;
    public GameObject endCallUpperCanvasButton;
    private Boolean callAnswered = true;
    public GameObject callerName;
    public GameObject restartMenu;
    public GameObject leftTurn;
    public GameObject rightTurn;
    public GameObject slightLeftTurn;
    public GameObject slightRightTurn;
    public GameObject breaking;
    public Double temperatureDouble;
    public AudioEvents audioEvent;
    public bool AVWarningSoundBool=true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Updatetime());

        StartCoroutine(IncomingMessageEvent(sendMessage));

        StartCoroutine(IncomingCall(callAnswered));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESCAPEEEEE");
            if (restartMenu.activeSelf) restartMenu.SetActive(false);
            if (!restartMenu.activeSelf) restartMenu.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Return) && restartMenu.activeSelf) {
            restartMenu.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        if (carAudio.mute)
        {
            unMutedImage.SetActive(false);
            mutedImage.SetActive(true);
        }
        else
        {
            unMutedImage.SetActive(true);
            mutedImage.SetActive(false);
        }

        musicVolume.text = Math.Truncate((carAudio.volume) * 10).ToString();
        engineText.text = activeEnginePreset.text;
        if (engineText.text == "Eco") {
            sportModeImage.SetActive(false);
            normalModeImage.SetActive(false);
            ecoModeImage.SetActive(true);
        }
        else if (engineText.text == "Normal")
        {
            sportModeImage.SetActive(false);
            normalModeImage.SetActive(true);
            ecoModeImage.SetActive(false);
        }
        else if (engineText.text == "Sport")
        {
            sportModeImage.SetActive(true);
            normalModeImage.SetActive(false);
            ecoModeImage.SetActive(false);
        }
        if (carAudio.isPlaying)
        {
            radioSwitch.text = "ON";
            //carAudio.Play();
            musicPlaying.SetActive(true);
            if (carAudio.clip == clip1)
            {
                musicPlaying.GetComponentInChildren<TextMeshProUGUI>().text = "FM1";
            }
            else if (carAudio.clip == clip2)
            {
                musicPlaying.GetComponentInChildren<TextMeshProUGUI>().text = "FM2";
            }
            else if (carAudio.clip == clip3)
            {
                musicPlaying.GetComponentInChildren<TextMeshProUGUI>().text = "FM3";
            }
            else if (carAudio.clip == clip4)
            {
                musicPlaying.GetComponentInChildren<TextMeshProUGUI>().text = "FM4";
            }
            else if (carAudio.clip == clip5)
            {
                musicPlaying.GetComponentInChildren<TextMeshProUGUI>().text = "FM5";
            }
            /*if (stationPlaying.text != "FM1" || stationPlaying.text != "FM2" || stationPlaying.text != "FM3" || stationPlaying.text != "FM4" || stationPlaying.text != "FM5") {
                stationPlaying.text = "FM1";
            }*/
            //Debug.Log("track name: "+GameObject.Find(carAudio.clip.name).GetComponentInParent<Button>().GetComponentInChildren<TextMeshProUGUI>().text);
            //Debug.Log("track name: " + GameObject.Find(carAudio.clip.name).GetComponentInParent<UIPrinter>()); 

            //stationPlaying.text = GameObject.Find(carAudio.clip.name).GetComponentInChildren<TextMeshProUGUI>().name;

        }
        else
        {

            musicPlaying.SetActive(false);
            radioSwitch.text = "OFF";
        }

        if (messageReceived) {
            messageReceived = false;
            messageCounterChanged = true;
            unreadInboxCounter += 1;
        }
        if (unreadInboxCounter >= 1 && messageCounterChanged)
        {
            messageCounterChanged = false;
            messageNotificationCounter.GetComponentInChildren<TextMeshProUGUI>().text = unreadInboxCounter.ToString();
            inboxMessageCounter.GetComponentInChildren<TextMeshProUGUI>().text = unreadInboxCounter.ToString();
            printer.IncomingMessage(inboxCanvas, messageNotificationName, messageNotificationContext);
            messageNotificationCounter.SetActive(true);
            inboxMessageCounter.SetActive(true);
        }
        if (unreadInboxCounter >= 1)
        {
            messageNotificationCounter.SetActive(true);
            inboxMessageCounter.SetActive(true);
        }
        else
        {
            messageNotificationCounter.SetActive(false);
            inboxMessageCounter.SetActive(false);
            messageNotificationName.GetComponent<TextMeshProUGUI>().text = " ";
            messageNotificationContext.GetComponent<TextMeshProUGUI>().text = " ";
        }
    }

    public IEnumerator PopUpMessages(String where) {
        messageNotificationContext.GetComponent<TextMeshProUGUI>().text = " ";
        messageNotificationName.GetComponent<TextMeshProUGUI>().text = " ";
        leftTurn.SetActive(false);
        rightTurn.SetActive(false);
        slightLeftTurn.SetActive(false);
        slightRightTurn.SetActive(false);
        breaking.SetActive(false);
        if (where == "left")
        {
            if(AVWarningSoundBool == true) StartCoroutine(WarningAudio());
            leftTurn.SetActive(true);
        }
        else if (where == "right")
        {
            if (AVWarningSoundBool == true) StartCoroutine(WarningAudio());
            rightTurn.SetActive(true);
        }
        else if (where == "slight left")
        {
            if (AVWarningSoundBool == true) StartCoroutine(WarningAudio());
            slightLeftTurn.SetActive(true);
        }
        else if (where == "slight right")
        {
            if (AVWarningSoundBool == true) StartCoroutine(WarningAudio());
            slightRightTurn.SetActive(true);
        }
        else if (where == "breaking")
        {
            if (AVWarningSoundBool == true) StartCoroutine(WarningAudio());
            breaking.SetActive(true);
        }

        yield return new WaitForSeconds(2f);

        leftTurn.SetActive(false);
        rightTurn.SetActive(false);
        slightLeftTurn.SetActive(false);
        slightRightTurn.SetActive(false);
        breaking.SetActive(false);
    }

    IEnumerator WarningAudio()
    {
        audioEvent.audioEvents.PlayOneShot(audioEvent.warningFromAV);
        AVWarningSoundBool = false;
        yield return new WaitForSeconds(2f);
        AVWarningSoundBool = true;
    }

    IEnumerator Updatetime()
    {
        while (true)
        {
            var today = System.DateTime.Now;
            textMesh.text = today.ToString("h:mm tt");
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator IncomingMessageEvent(Boolean sendMsg)
    {
        if (sendMsg) {

            yield return new WaitForSeconds(5f);
            sendMessage = false;
            messageReceived = true;
            messageNotificationName.GetComponent<TextMeshProUGUI>().text = "markos";
            messageNotificationContext.GetComponent<TextMeshProUGUI>().text = "Hello! How are you?";

        }
        
        
    }

    IEnumerator IncomingCall(Boolean callBool) {

        if (callBool && !endCallUpperCanvasButton.activeSelf) {

            yield return new WaitForSeconds(15f);
            callAnswered = false;
            endCallUpperCanvasButton.SetActive(true);
            if (callingLabel.activeSelf) callingLabel.SetActive(false);
            incomingCall.SetActive(true);
            callerName.GetComponent<TextMeshProUGUI>().text = "Anna";
            printer.IncomingPhoneCall(callerName);

        }


    }

}
