// Copyright © 2018 – Property of Tobii AB (publ) - All Rights Reserved

using Tobii.G2OM;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections;
using System.Linq;
using UnityEngine.Windows.Speech;
using Helper;


namespace Tobii.XR
{
    /// <summary>
    /// A gaze aware button that is interacted with the trigger button on the Vive controller.
    /// </summary>
    [RequireComponent(typeof(UIGazeButtonGraphics))]
    public class UITriggerGazeButton : MonoBehaviour, IGazeFocusable
    {
        // Event called when the button is clicked.
        public UIButtonEvent OnButtonClicked;

        // The normalized (0 to 1) haptic strength.
        private const float HapticStrength = 0.1f;

        // The state the button is currently in.
        private ButtonState _currentButtonState = ButtonState.Idle;

        // Private fields.
        private bool _hasFocus;
        private UIGazeButtonGraphics _uiGazeButtonGraphics;
        private int newFocusedButton;
        //private int prevFocusedButton;
        public float timeElapsed = 0f;
        private float gazeThreshold = 0.8f;
        public GameObject text;
        //private float timeBtnElapsed = 0f;
        //private float gazeBtnThreshold = 1f;
        //bool btnActivator=false;
        public Button thisButton;
        //public Button phoneButton;
        //public Button messageButton;
        //private G2OM_PostTicker postTicker;
        //private int prevFocusedButton;
        //public Button btn1;

        public Canvas canvasUsed;


        private Transform oldButtonCords;
        private Transform newButtonCords;
        private GazeTriggerHelper helper;
        Boolean blinker = true;
        /*public Boolean eyeBlinkBool = true;
        public Boolean eyeDwellTimeBool = false;*/
        public GameObject uiComp;
        public BlinkCoroutine blinkScript;
        


        private void Start()
        {
            //Debug.Log(GameObject.FindGameObjectWithTag("Pressed_UI").activeSelf);
            /*if (text.activeSelf == true)
            {
                text.SetActive(false);
            }*/
            // Store the graphics class.
            //helper.oldBtnCord = this.transform;
            blinkScript = GameObject.Find("UI").GetComponent<BlinkCoroutine>();
            _uiGazeButtonGraphics = GetComponent<UIGazeButtonGraphics>();
            helper = GetComponent<GazeTriggerHelper>();

            // Initialize click event.
            if (OnButtonClicked == null)
            {
                OnButtonClicked = new UIButtonEvent();
            }
        }

        private void Update()
        {
            //Debug.Log("TRANSFORMMMM: " + this.transform);
            if (_currentButtonState == ButtonState.Focused) ClickButton(_currentButtonState, this.transform);
            /*if (postTicker.btnPressed == true)
            {
                postTicker.btnPressed = false;
                UpdateState(ButtonState.PressedDown, newFocusedButton);
            }*/
            /*if (btnActivator == true) {
                timeBtnElapsed += Time.deltaTime;
            }

            if (timeBtnElapsed >= gazeBtnThreshold && text.activeSelf == true) {
                text.SetActive(false);
                timeBtnElapsed = 0f;
                btnActivator = false;
            }*/
            //currFocusedButton = _uiGazeButtonGraphics._buttonNumber;

            //_uiGazeButtonGraphics = GetComponent<UIGazeButtonGraphics>();
            //Debug.Log("trigger button focus " + _currentButtonState);
            // When the button is being focused and the interaction button is pressed down, set the button to the PressedDown state.
            if (_currentButtonState == ButtonState.Focused &&
                ControllerManager.Instance.GetButtonPressDown(ControllerButton.Trigger))
            {
                //Debug.Log("patisa koumpi");
                UpdateState(ButtonState.PressedDown, _uiGazeButtonGraphics._buttonNumber);
            }
            // When the trigger button is released.
            else if (ControllerManager.Instance.GetButtonPressUp(ControllerButton.Trigger))
            {
                // Invoke a button click event if this button has been released from a PressedDown state.
                if (_currentButtonState == ButtonState.PressedDown)
                {
                    // Invoke click event.
                    if (OnButtonClicked != null)
                    {
                        OnButtonClicked.Invoke(gameObject);
                    }

                    ControllerManager.Instance.TriggerHapticPulse(HapticStrength);
                }

                // Set the state depending on if it has focus or not.
                UpdateState(_hasFocus ? ButtonState.Focused : ButtonState.Idle, _uiGazeButtonGraphics._buttonNumber);
            }
        }

        /// <summary>
        /// Updates the button state and starts an animation of the button.
        /// </summary>
        /// <param name="newState">The state the button should transition to.</param>
        private void UpdateState(ButtonState newState, int btnNumber)
        {
            //Button buttonUsed = canvasUsed.GetComponentInChildren
            //int prevFocusedButton = _uiGazeButtonGraphics._buttonNumber;
            var oldState = _currentButtonState;
            //newFocusedButton = btnNumber;
            _currentButtonState = newState;

            // Variables for when the button is pressed or clicked.
            var buttonPressed = newState == ButtonState.PressedDown;
            var buttonClicked = (oldState == ButtonState.PressedDown && newState == ButtonState.Focused);

            // If the button is being pressed down or clicked, animate the button click.
            if (buttonPressed || buttonClicked)
            {
                _uiGazeButtonGraphics.AnimateButtonPress(_currentButtonState);
            }
            // In all other cases, animate the visual feedback.
            else
            {
                //Debug.Log(_uiGazeButtonGraphics._buttonNumber);
                _uiGazeButtonGraphics.AnimateButtonVisualFeedback(_currentButtonState);
            }
        }

        /// <summary>
        /// Method called by Tobii XR when the gaze focus changes by implementing <see cref="IGazeFocusable"/>.
        /// </summary>
        /// <param name="hasFocus"></param>
        public void GazeFocusChanged(bool hasFocus)
        {
            // If the component is disabled, do nothing.
            if (!enabled) return;

            _hasFocus = hasFocus;
            if (hasFocus == true) {
                this.helper.timeElapsed = 0f;
            }

            // Return if the trigger button is pressed down, meaning, when the user has locked on any element, this element shouldn't be highlighted when gazed on.
            if (ControllerManager.Instance.GetButtonPress(ControllerButton.Trigger)) return;

            UpdateState(hasFocus ? ButtonState.Focused : ButtonState.Idle, _uiGazeButtonGraphics._buttonNumber);
        }

        private void ClickButton(ButtonState currBtnState, Transform btnCords) {

            //int prevFocusedButton = _uiGazeButtonGraphics._buttonNumber;

            //newFocusedButton = btnNumber;
            newButtonCords = btnCords;
            //Debug.Log("MPIKAAAAAAAAAAA");
            //Debug.Log("Prev: " + OnButtonClicked.prevFocusedButton + " New: " + newFocusedButton + "---------------");
            //if (OnButtonClicked.prevFocusedButton == newFocusedButton)




            //if(newButtonCords == oldButtonCords) //swsto
            //helper.oldBtnCord = btnCords;

            //Debug.Log("OLDDDDDDDDDDDDD: " + helper.oldBtnCord);



            //douleuei kalaaaaaaaaaaaaaaaa 
            /// <summary>
            /// Implements dwell time selection technique
            /// </summary>
            /// uiComp.GetComponent<UITriggerGazeButton>().eyeDwellTimeBool
            if (blinkScript.eyeDwellTimeBool && helper.oldBtnCord != null)
            {
                if (newButtonCords == helper.oldBtnCord && currBtnState == ButtonState.Focused)
                {
                    //timeElapsed += Time.deltaTime; //swsto
                    helper.timeElapsed += Time.deltaTime;
                    //Debug.Log(timeElapsed);
                    //if (timeElapsed >= gazeThreshold) //swsto
                    if (helper.timeElapsed >= gazeThreshold)
                    {
                        //thisButton = GameObject.Find("Button " + btnNumber).GetComponent<Button>();
                        OnButtonClicked.Invoke(this.gameObject);
                        thisButton = this.gameObject.GetComponent<Button>();
                        helper.timeElapsed = 0f;
                        blinkScript.audioEvents.PlayOneShot(blinkScript.buttonClickAudio);
                        thisButton.onClick.Invoke();

                        //phoneButton.onClick.Invoke();
                        //Debug.Log("mpika");
                        //btn1.onClick.Invoke();



                        //thisButton = GameObject.Find("Trigger Button " + _uiGazeButtonGraphics._buttonNumber).GetComponent<Button>();
                        //thisButton.onClick.Invoke();
                        //btnActivator = true;
                        //Debug.Log("mpika koumpi " + _uiGazeButtonGraphics._buttonNumber);
                        //_uiGazeButtonGraphics.AnimateButtonVisualFeedback(ButtonState.PressedDown);
                        //canvasUsed.onClick.Invoke();
                        //this.gameObject.
                        //Debug.Log(newFocusedButton);
                        //text.SetActive(true);
                        //UpdateState(ButtonState.PressedDown, btnNumber);
                    }
                }
                else
                {
                    //Debug.Log("bgikaaaaaaaaa");
                    timeElapsed = 0f;
                }
            }


            /// <summary>
            /// Implements eye pinch selection technique
            /// </summary>
            //uiComp.GetComponent<UITriggerGazeButton>().eyeBlinkBool
            if (blinkScript.eyeBlinkBool && currBtnState == ButtonState.Focused && (TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World).IsLeftEyeBlinking || TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World).IsRightEyeBlinking)) {

                thisButton = this.gameObject.GetComponent<Button>();
                StartCoroutine(blinkScript.BlinkStallCoroutine(thisButton,blinkScript.blinker));
            }
            
            OnButtonClicked.prevFocusedButton = newFocusedButton;
            //oldButtonCords = newButtonCords; //swsto
            helper.oldBtnCord = newButtonCords;
            //Debug.Log("OLDDDDDDDDDDDDD: " + helper.oldBtnCord);

        }


        /*IEnumerator BlinkCoroutine(Button pressedButton, Boolean blinkOrNotBlink)
        {
            if (blinkOrNotBlink == true)
            {
                pressedButton.onClick.Invoke();
            }
            blinker = false;
            //if (maintenanceUI.activeSelf==true) maintenanceUI.SetActive(false);

            //Debug.Log("prin");

            yield return new WaitForSecondsRealtime(0.2f);
            blinker = true;

            *//*maintenanceBoolean = false;
            time = 0;*//*
            //Debug.Log("meta");
            //if (maintenanceUI.activeSelf == false) maintenanceUI.SetActive(true);
        }*/

    }
}
