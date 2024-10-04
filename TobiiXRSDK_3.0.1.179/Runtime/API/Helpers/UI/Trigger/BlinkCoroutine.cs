
using Tobii.G2OM;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections;
using System.Linq;
using UnityEngine.Windows.Speech;
using Helper;

public class BlinkCoroutine : MonoBehaviour
{
    public Boolean blinker=true;
    public Boolean blinkAudio;
    public Boolean eyeBlinkBool = true;
    public Boolean eyeDwellTimeBool = false;
    public Boolean dictationOn = false;
    public AudioSource audioEvents;
    public AudioClip buttonClickAudio;
    public int errorCounter;
    // Start is called before the first frame update
    void Start()
    {
        errorCounter = 0;
        blinker = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            errorCounter++;
            Debug.Log(errorCounter);
        }
    }

    public IEnumerator BlinkStallCoroutine(Button pressedButton, Boolean blinkOrNotBlink)
    {
        if (blinker == true)
        {
            //blinker = false;
            //audioEvents.clip = buttonClickAudio;
            audioEvents.PlayOneShot(buttonClickAudio);
            StartCoroutine(BlinkStallTimer());
            pressedButton.onClick.Invoke();
            yield return new WaitForSecondsRealtime(0.4f);
            //blinker = true;
        }
        //Debug.Log("prin");

        //blinkAudio = true;

        /*maintenanceBoolean = false;
        time = 0;*/
        //Debug.Log("meta");
        //if (maintenanceUI.activeSelf == false) maintenanceUI.SetActive(true);
    }


    public IEnumerator BlinkStallTimer() {

        blinker = false;
        yield return new WaitForSecondsRealtime(0.4f);
        blinker = true;
    }

}
