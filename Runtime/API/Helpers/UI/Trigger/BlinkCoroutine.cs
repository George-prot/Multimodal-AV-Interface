
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
    public Boolean blinker;
    public Boolean eyeBlinkBool = true;
    public Boolean eyeDwellTimeBool = false;
    public Boolean dictationOn = false;
    public AudioSource audioEvents;
    public AudioClip buttonClickAudio;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator BlinkStallCoroutine(Button pressedButton, Boolean blinkOrNotBlink)
    {
        if (blinkOrNotBlink == true)
        {
            blinker = false;
            //audioEvents.clip = buttonClickAudio;
            audioEvents.PlayOneShot(buttonClickAudio);
            pressedButton.onClick.Invoke();
        }
        //if (maintenanceUI.activeSelf==true) maintenanceUI.SetActive(false);

        //Debug.Log("prin");

        yield return new WaitForSecondsRealtime(0.4f);
        blinker = true;

        /*maintenanceBoolean = false;
        time = 0;*/
        //Debug.Log("meta");
        //if (maintenanceUI.activeSelf == false) maintenanceUI.SetActive(true);
    }
}
