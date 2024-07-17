// Copyright © 2018 – Property of Tobii AB (publ) - All Rights Reserved

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tobii.XR
{
    /// <summary>
    /// The different states for a gaze aware button.
    /// </summary>
    public enum ButtonState
    {
        Idle,
        Focused,
        PressedDown
    }

    /// <summary>
    /// All the graphics for a gaze button, including animations.
    /// </summary>
    public class UIGazeButtonGraphics : MonoBehaviour
    {
#pragma warning disable 649
        [Header("Components")]

        public int _buttonNumber;

        [SerializeField]
        private Image _buttonImage;

        [SerializeField] private Text _label;

        [Header("Focused")]
        [SerializeField, Tooltip("The color of the button background when focused.")]
        private Color _backgroundFocusColor;

        [SerializeField, Tooltip("The color of the label when focused.")]
        private Color _labelFocusColor;

        [SerializeField, Tooltip("The button scale when focused.")]
        private float _buttonFocusScale = 1.05f;

        [SerializeField, Tooltip("The duration it takes for the visual feedback to either fully highlight or go back to the default values.")]
        private float _visualFeedbackDuration = 0.2f;

        [SerializeField, Tooltip("How the visual feedback is animated.")]
        private AnimationCurve _visualFeedbackAnimationCurve;

        [Header("Pressed")]
        [SerializeField, Tooltip("The color of the button background when the button is pressed down.")]
        private Color _backgroundPressColor;

        [SerializeField, Tooltip("The color fo the label when the button is pressed down.")]
        private Color _labelPressColor;

        [SerializeField, Tooltip("The scale of the button when pressed down.")]
        private float _buttonScaleOnPress = 0.95f;

        [SerializeField, Tooltip("The duration it takes for the button click animation.")]
        private float _buttonPressDuration = 0.1f;

        [SerializeField, Tooltip("How the button click is animated.")]
        private AnimationCurve _buttonPressAnimationCurve;

        private UITriggerGazeButton buttonGazed;
#pragma warning restore 649

        // Private fields.

        private Color32 ogColor = new Color32(105, 105, 105, 200);
        private Color32 endColor = new Color32(255, 0, 8, 255);


        private RectTransform _buttonRect;
        private Color _buttonDefaultColor;
        private Color _labelDefaultColor;
        private Vector3 _buttonDefaultScale;
        private Coroutine _buttonAnimationCoroutine;
        public ButtonState currBtnState = ButtonState.Idle;

        // Use this for initialization
        private void Awake()
        {
            // Store the button rect transform.
            _buttonRect = _buttonImage.GetComponent<RectTransform>();

            // Get the default colors and scale of the button's components.
            _buttonDefaultColor = ogColor;
            //_labelDefaultColor = _label.color;
            _buttonDefaultScale = _buttonRect.localScale;

        }

        /// <summary>
        /// Animate the button press to a new state.
        /// </summary>
        /// <param name="currentButtonState">The state the button should animate to.</param>
        public void AnimateButtonPress(ButtonState currentButtonState)
        {
            // Stop the animation if it is animating.
            if (_buttonAnimationCoroutine != null)
            {
                StopCoroutine(_buttonAnimationCoroutine);
            }

            if (!isActiveAndEnabled) return;

            // Animate the button to the new state.
            _buttonAnimationCoroutine = StartCoroutine(AnimateButton(_buttonPressDuration, _buttonPressAnimationCurve, currentButtonState));
        }

        /// <summary>
        /// Animate the visual feedback for the button.
        /// </summary>
        /// <param name="currentButtonState">The state of the button that should be animated.</param>
        public void AnimateButtonVisualFeedback(ButtonState currentButtonState)
        {
            currBtnState = currentButtonState;
            // Stop the animation if it is animating.
            if (_buttonAnimationCoroutine != null)
            {
                StopCoroutine(_buttonAnimationCoroutine);
            }

            if (!isActiveAndEnabled) return;

            // Animate the visual feedback in it's current button state.
            _buttonAnimationCoroutine = StartCoroutine(AnimateButton(_visualFeedbackDuration, _visualFeedbackAnimationCurve, currentButtonState));
        }

        /// <summary>
        /// Animates the visual feedback or the button press.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="animationCurve">How the button animates.</param>
        /// <param name="currentButtonState">The button's current state.</param>
        /// <returns></returns>
        private IEnumerator AnimateButton(float duration, AnimationCurve animationCurve, ButtonState currentButtonState)
        {
            //ogColor = this.GetComponent<Button>().colors.normalColor;
            //var colors = this.GetComponent<Button>().colors;
            //var image = this.GetComponent<Image>();
            // Sets the start values of the animation.
            var startBackgroundColor = _buttonDefaultColor;
            //var startLabelColor = _label.color;
            var startButtonScale = _buttonRect.localScale;

            // Sets the end values of the animation to the default.
            var endBackgroundColor = _buttonDefaultColor;
            //var endLabelColor = _labelDefaultColor;
            var endButtonScale = _buttonDefaultScale;

            // Updates the end values of the animation depending on the button state.
            switch (currentButtonState)
            {
                case ButtonState.Focused:
                    //Debug.Log("111111111111111111111111111111111111111----------------------------------------------------");
                    endBackgroundColor = endColor;
                    //endLabelColor = _labelFocusColor;
                    endButtonScale *= _buttonFocusScale;
                    /*colors.normalColor = endColor;
                    this.GetComponent<Button>().colors = colors;*/
                    /*if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Debug.Log("mpika");
                    }*/
                    break;
                case ButtonState.PressedDown:

                    //Debug.Log("2222222222222222222222222222222222222222222222222222----------------------------------------------------");
                    endBackgroundColor = _backgroundPressColor;
                    //endLabelColor = _labelPressColor;
                    endButtonScale *= _buttonScaleOnPress;
                    break;
            }

            // Lerp the colors and scale.
            var progress = 0f;
            while (progress < 0.05f)
            {
                //Debug.Log("3333333333333333333333333333333333333333333333----------------------------------------------------");
                progress += Time.deltaTime * (1f / duration);
                var animationProgress = animationCurve.Evaluate(progress);
                _buttonRect.localScale = Vector3.Lerp(startButtonScale, endButtonScale, animationProgress);
                //colors.normalColor = Color.Lerp(startBackgroundColor, endBackgroundColor, animationProgress); //swsto
                //colors.normalColor = Color.Lerp(ogColor, endColor, animationProgress);
                //colors = Color.Lerp(ogColor, endColor, animationProgress);

                Color buttonColor = Color.Lerp(startBackgroundColor, endBackgroundColor, animationProgress);

                //colors.normalColor = buttonColor;
                this.GetComponent<Button>().GetComponent<Image>().color = buttonColor;

                /*if (animationProgress >= 1f) {

                    colors.normalColor = ogColor;
                    this.GetComponent<Button>().colors = colors;
                }*/ //douleuei sxetikaaaaa

                //this.GetComponent<Image>().color = Color.Lerp(ogColor, endColor, animationProgress); ;
                //this.GetComponent<Button>().colors = colors;
                //colors.normalColor = Color.Lerp(startBackgroundColor, endBackgroundColor, animationProgress);
                //buttonGazed.thisButton.image.color = Color.Lerp(startBackgroundColor, endBackgroundColor, animationProgress);




                //kanonikaaaaa
                //_buttonImage.color = Color.Lerp(startBackgroundColor, endBackgroundColor, animationProgress);
                //_label.color = Color.Lerp(startLabelColor, endLabelColor, animationProgress);
                yield return null;
            }
            //episis kanonikaaaaa
            //colors.normalColor = ogColor;
            //this.GetComponent<Button>().colors = colors;


            Color afterFocus = new Color32(255, 255, 255, 200);
            if(currentButtonState!=ButtonState.Focused) this.GetComponent<Button>().GetComponent<Image>().color = afterFocus;



            /*var progress = 0f;
            while (currentButtonState == ButtonState.Focused)
            {
                //progress += Time.deltaTime * (1f / duration);
                var animationProgress = animationCurve.Evaluate(duration);
                _buttonRect.localScale = Vector3.Lerp(startButtonScale, endButtonScale, animationProgress);
                _buttonImage.color = Color.Lerp(startBackgroundColor, endBackgroundColor, animationProgress);
                _label.color = Color.Lerp(startLabelColor, endLabelColor, animationProgress);
                yield return null;
            }*/
            //_buttonImage.color = _buttonDefaultColor;
            _buttonAnimationCoroutine = null;
        }

        /*private void CkeckIfFocusLost(ButtonState currentBtnState) {
            if (currBtnState != ButtonState.Focused) { 
                
            }
        }*/

    }
}