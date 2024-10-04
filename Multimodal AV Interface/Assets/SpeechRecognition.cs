namespace SpeechRec {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;
    using UnityEngine.UI;
    using System.Linq;
    using UnityEngine.Windows.Speech;
    using TMPro;
    using Tobii.XR.Examples;
    using Tobii.XR;
    using UnityEditor;
    using TMPro;



    public class SpeechRecognition : MonoBehaviour
    {


        public Dictionary<string, System.Action> keywordActions = new Dictionary<string, Action>();
        //private Dictionary<string, System.Action<string>> keywordCallActions = new Dictionary<string, System.Action<string>>();
        public KeywordRecognizer keywordRecognizer;
        //private KeywordRecognizer keywordCallRecognizer;
        public DictationRecognizer dictationRecognizer;
        public UITriggerGazeButton btnTrigger;
        public UIPrinter printer;
        public AudioSource audioSource;
        public TextMeshProUGUI volumeLevel;
        public Button onButton;
        public Button offButton;
        public TextMeshProUGUI trackPlaying;
        public AudioClip clip1;
        public AudioClip clip2;
        public AudioClip clip3;
        public AudioClip clip4;
        public AudioClip clip5;
        public List<string> contactList = new List<string>();
        public GameObject callCanvas;
        public TextMeshProUGUI callName;
        public GameObject endCallButton;
        private string dictatedPhrase;
        public GameObject mutedImage;
        public GameObject unMutedImage;
        public GameObject contactTextInput;
        public GameObject newMessageTextInput;
        public Boolean voiceCommandsBool = true;
        public GameObject blinkOnBtn;
        public GameObject blinkOffBtn;
        public GameObject dwellOnBtn;
        public GameObject dwellOffBtn;
        public GameObject voiceOnBtn;
        public GameObject voiceOffBtn;
        public GameObject upperCanvasCallingLabel;
        public GameObject triggerMenuUI;
        public BlinkCoroutine blinkScript;
        public GameObject newMessageCanvas;
        public bool spRecIsOff = false;
        public AudioSource eventsAudio;
        public GameObject openMenuCanvas;
        public bool outgoingCallBool = false;
        public GameObject helpCanvas;
        public MetricEvaluationScript eval;
        Host host;

        // Start is called before the first frame update
        void Start()
        {
            keywordActions.Add("play music", StartMusic);
            keywordActions.Add("stop music", StopMusic);
            keywordActions.Add("volume up", VolumeUp);
            keywordActions.Add("volume down", VolumeDown);
            keywordActions.Add("next track", NextTrack);
            keywordActions.Add("previous track", PreviousTrack);
            keywordActions.Add("sound off", Mute);
            keywordActions.Add("sound on", UnMute);
            keywordActions.Add("end call", EndCall);
            keywordActions.Add("call", StartDict);
            keywordActions.Add("blink on", BlinkOn);
            keywordActions.Add("blink off", BlinkOff);
            keywordActions.Add("dwell on", DwellOn);
            keywordActions.Add("dwell off", DwellOff);
            keywordActions.Add("voice commands on", VoiceCommandsOn);
            keywordActions.Add("voice commands off", VoiceCommandsOff);
            keywordActions.Add("screen on", ScreenOn);
            keywordActions.Add("screen off", ScreenOff);
            keywordActions.Add("help", HelpCanvas);
            keywordActions.Add("close help", CloseHelp);
            contactList.Add("Peter");
            //keywordCallRecognizer = new KeywordRecognizer(keywordCallActions.Keys.ToArray());
            //
            //dictationRecognizer.Start();
            //keywordCallRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
            //keywordCallRecognizer.Start();

            //Initialize KeywordRecognizer
            keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
            keywordRecognizer.Start();

            
            dictationRecognizer = new DictationRecognizer();
            //Debug.Log("KEYS: " + keywordActions.Keys.Count);
            dictationRecognizer.InitialSilenceTimeoutSeconds = 30;
            dictationRecognizer.AutoSilenceTimeoutSeconds = 30;
            dictationRecognizer.DictationResult += OnDictationResult;
            dictationRecognizer.DictationComplete += OnDictationComplete;
            dictationRecognizer.DictationError += OnDictationError;
            //Debug.Log("SPEECH STATUS "+keywordRecognizer.IsRunning);
            //Debug.Log("SPEECH LIST " + keywordActions.Keys.Count);


        }

        private void Awake()
        {
            //if (spRecIsOff == true) StartDict();
        }

        // Update is called once per frame
        void Update()
        {

          
            /* //debugging
             if (Input.GetKeyDown(KeyCode.RightArrow)) NextTrack();

             if (Input.GetKeyDown(KeyCode.LeftArrow)) PreviousTrack();

             if (Input.GetKeyDown(KeyCode.Space)) StopMusic();

             if (Input.GetKeyDown(KeyCode.Return)) StartDict();
 */
        }

      

        private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
        {
            string recognizedPhrase = args.text.ToLower().Trim();
            Debug.Log("Recognized Keyword: " + recognizedPhrase);
            /*Debug.Log("Keyword: " + args.text);
            keywordActions[args.text].Invoke();*/
            if (keywordActions.ContainsKey(recognizedPhrase) && voiceCommandsBool)
            {
                if(recognizedPhrase=="call") outgoingCallBool = true;
                keywordActions[recognizedPhrase].Invoke();
            }
            else
            {
                Debug.Log("Unknown keyword: " + recognizedPhrase);
            }

        }

        public void StartDict()
        {
            //Debug.Log("Mpika start dictation");
            // Stop keyword recognizer to avoid conflicts
            if (keywordRecognizer.IsRunning)
            {
                //Debug.Log("Mpika stop");
                keywordRecognizer.Stop();
                //keywordRecognizer.Dispose();
            }

            PhraseRecognitionSystem.Shutdown();
            StartCoroutine(StartDictationRecognizer());

            // Start dictation recognizer
            /*if (!dictationRecognizer.Status.Equals(SpeechSystemStatus.Running))
            {
                dictationRecognizer.Start();
            }

            Debug.Log("Dictation started. Please say the contact name.");*/
        }

        public System.Collections.IEnumerator StartDictationRecognizer()
        {
            // Wait until the PhraseRecognitionSystem has completely shut down
            //Debug.Log("PRRIIIIIIIIIIIIIIIN");
            //spRecIsOff = false;
            yield return new WaitUntil(() => PhraseRecognitionSystem.Status == SpeechSystemStatus.Stopped);

            //spRecIsOff = true;
            //Debug.Log("METAAAAAAAAAAAAAAAAAAAAAAA");
            spRecIsOff = false;
            // Start dictation recognizer
            dictationRecognizer.Start();
            Debug.Log("Dictation started. Please say the contact name.");
        }

        private void OnDictationResult(string text, ConfidenceLevel confidence)
        {
            dictatedPhrase = text.ToLower().Trim();
            Debug.Log("Dictation Recognized Phrase: " + dictatedPhrase);

            if (contactTextInput.activeSelf) {

                //Debug.Log("MPIKA CONTACT ADDDDDDDDDDDDDDDDDDD");
                contactTextInput.GetComponent<TMP_InputField>().ActivateInputField();
                contactTextInput.GetComponent<TMP_InputField>().Select();
                contactTextInput.GetComponent<TMP_InputField>().text = dictatedPhrase.ToString();
                contactTextInput.GetComponent<TMP_InputField>().DeactivateInputField();
            }


            if (newMessageCanvas.activeSelf)
            {
                //Debug.Log("MPIKA NEW MESSAAAAGEEEEEEEEEEEEEEEE");
                newMessageTextInput.GetComponent<TMP_InputField>().ActivateInputField();
                newMessageTextInput.GetComponent<TMP_InputField>().Select();
                newMessageTextInput.GetComponent<TMP_InputField>().text = dictatedPhrase.ToString();
                newMessageTextInput.GetComponent<TMP_InputField>().DeactivateInputField();
            }
            /*if (recognizedPhrase.StartsWith("call"))
            {
                string contactName = recognizedPhrase.Substring("call".Length).Trim();
                Debug.Log("Extracted Contact Name: " + contactName);
*/
            if (contactList.Contains(dictatedPhrase) && outgoingCallBool==true)
            {
                outgoingCallBool = false;
                MakePhoneCall(dictatedPhrase);
                RestartKeywordRecognizer();
            }
            else
            {
                Debug.Log($"Contact '{dictatedPhrase}' does not exist in contacts.");
            }
            //}

            /*// Restart keyword recognizer
            if (!keywordRecognizer.IsRunning)
            {
                keywordRecognizer.Start();
            }

            // Stop dictation recognizer
            if (dictationRecognizer.Status.Equals(SpeechSystemStatus.Running))
            {
                dictationRecognizer.Stop();
            }*/
        }


        public void OnDictationComplete(DictationCompletionCause cause)
        {
            Debug.Log("Dictation completed.");
            if (dictationRecognizer.Status.Equals(SpeechSystemStatus.Running))
            {
                RestartKeywordRecognizer();
            }
            else
            {
                StartCoroutine(StartKeywordRecognizer());

            }

        }

        private void OnDictationError(string error, int hresult)
        {
            Debug.LogError($"Dictation error: {error}; HResult = {hresult}");
            RestartKeywordRecognizer();
        }

        public void RestartKeywordRecognizer()
        {
            //Debug.Log("MPIKA EDWWWWWWWWW");
            //if (!keywordRecognizer.IsRunning) //previous....working!
            if (dictationRecognizer.Status.Equals(SpeechSystemStatus.Running))
            {
                //Debug.Log("MPIKA EDWWWWWWWWW111111111111111111");
    
                    OnDisable();
                // Start the PhraseRecognitionSystem
            }
        }

        private System.Collections.IEnumerator StartKeywordRecognizer()
        {
            //Debug.Log("Keyword recognizer restarted11111111111111.");
            // Wait until the PhraseRecognitionSystem has completely started
            yield return new WaitUntil(() => PhraseRecognitionSystem.Status == SpeechSystemStatus.Running);

            //Debug.Log("Keyword recognizer restarted2222222222222222.");
            // Start the keyword recognizer
            keywordRecognizer.Start();
            //Debug.Log("Keyword recognizer restarted3333333333333333.");
        }

        private void OnDisable()
        {
            // Stop and dispose both recognizers when the object is disabled
            if (keywordRecognizer != null && keywordRecognizer.IsRunning)
            {
                //Debug.Log("mpika speechRec stop");
                keywordRecognizer.Stop();
                StartCoroutine(StopSpeechRecognition());
                //keywordRecognizer.Dispose();
            }

            if (dictationRecognizer != null && dictationRecognizer.Status.Equals(SpeechSystemStatus.Running))
            {
                //Debug.Log("mpika dictation stop");
                dictationRecognizer.Stop();
                StartCoroutine(StopDictation());
                //dictationRecognizer.Dispose();
            }
        }

        private System.Collections.IEnumerator StopSpeechRecognition()
        {
            yield return new WaitUntil(() => PhraseRecognitionSystem.Status == SpeechSystemStatus.Stopped);
        }


        private System.Collections.IEnumerator StopDictation()
        {
            //Debug.Log("mpika dictation stop!!!!!!!!!!!!!!!!!!!!!!!!!!");
            yield return new WaitUntil(() => dictationRecognizer.Status.Equals(SpeechSystemStatus.Stopped));
            //Debug.Log("mpika dictation stop%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
            PhraseRecognitionSystem.Restart();
            //Debug.Log("MPIKA EDWWWWWWWWW222222222222222222222");
            StartCoroutine(StartKeywordRecognizer());
        }


        public void ScreenOn()
        {
            if (!triggerMenuUI.activeSelf) triggerMenuUI.SetActive(true);
            if (openMenuCanvas.activeSelf) openMenuCanvas.SetActive(false);
            eventsAudio.Stop();
        }


        public void ScreenOff()
        {
            if (triggerMenuUI.activeSelf) triggerMenuUI.SetActive(false);
            if (!openMenuCanvas.activeSelf) openMenuCanvas.SetActive(true);
        }

        public void BlinkOn() {
            //Debug.Log("mpika blink on");
            blinkScript.eyeBlinkBool = true;
            blinkOnBtn.SetActive(false);
            blinkOffBtn.SetActive(true);
        }

        public void BlinkOff()
        {
            //Debug.Log("mpika blink off");
            blinkScript.eyeBlinkBool = false;
            blinkOnBtn.SetActive(true);
            blinkOffBtn.SetActive(false);
        }

        public void DwellOn()
        {
            //Debug.Log("mpika dwell on");
            blinkScript.eyeDwellTimeBool = true;
            dwellOnBtn.SetActive(false);
            dwellOffBtn.SetActive(true);

            if (!eval.dwellEvalDone && eval.experimentOn)
            {
                eval.msgEval = false;
                eval.carEval = false;
                eval.musicEval = false;
                if (!eval.messageEvalHelper1.activeSelf) eval.messageEvalHelper1.SetActive(true);
                if (!eval.messageEvalHelper2.activeSelf) eval.messageEvalHelper2.SetActive(true);
                if (!eval.messageEvalHelper3.activeSelf) eval.messageEvalHelper3.SetActive(true);
                if (!eval.messageEvalHelper4.activeSelf) eval.messageEvalHelper4.SetActive(true);
            }
        }

        public void DwellOff()
        {
            //Debug.Log("mpika dwell off");
            blinkScript.eyeDwellTimeBool = false;
            dwellOnBtn.SetActive(true);
            dwellOffBtn.SetActive(false);
        }

        public void VoiceCommandsOn()
        {
            //Debug.Log("mpika voice on");
            voiceCommandsBool = true;
            voiceOnBtn.SetActive(false);
            voiceOffBtn.SetActive(true);
        }

        public void VoiceCommandsOff()
        {
            //Debug.Log("mpika voice off");
            voiceCommandsBool = false;
            voiceOnBtn.SetActive(true);
            voiceOffBtn.SetActive(false);
        }


        public void Mute() {

             audioSource.mute = true;
             unMutedImage.SetActive(false);
             mutedImage.SetActive(true);

        }


        public void UnMute() {

            audioSource.mute = false;
            unMutedImage.SetActive(true);
            mutedImage.SetActive(false);

        }


        public void EndCall() {
            printer.EndCall();
        }



        public void MakePhoneCall(string contactName)
        {

            Debug.Log("Making phone call to:" + contactName);
            if (contactName == this.GetComponent<UIPrinter>().position1.GetComponent<UIPrinter>().positionsChild.GetComponent<ContactsType>().contactName.text)
            {
                this.GetComponent<UIPrinter>().position1.GetComponent<UIPrinter>().positionsChild.GetComponent<ContactsType>().callBtn.GetComponent<Button>().onClick.Invoke();
            }
            else if (contactName == this.GetComponent<UIPrinter>().position2.GetComponent<UIPrinter>().positionsChild.GetComponent<ContactsType>().contactName.text)
            {
                this.GetComponent<UIPrinter>().position2.GetComponent<UIPrinter>().positionsChild.GetComponent<ContactsType>().callBtn.GetComponent<Button>().onClick.Invoke();

            }
            else if (contactName == this.GetComponent<UIPrinter>().position3.GetComponent<UIPrinter>().positionsChild.GetComponent<ContactsType>().contactName.text)
            {
                this.GetComponent<UIPrinter>().position3.GetComponent<UIPrinter>().positionsChild.GetComponent<ContactsType>().callBtn.GetComponent<Button>().onClick.Invoke();
            }

        }

        public void StartMusic()
        {
            /*if (audioSource.clip == null)
            {
                audioSource.clip = GameObject.Find("Button11").GetComponent<AudioClip>();
            }*/
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                onButton.gameObject.SetActive(false);
                offButton.gameObject.SetActive(true);
            }

            //btnTrigger.timeElapsed = 0f;
        }

        public void StopMusic()
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                offButton.gameObject.SetActive(false);
                onButton.gameObject.SetActive(true);
            }


            //btnTrigger.timeElapsed = 0f;
        }

        private void VolumeUp()
        {

            if (audioSource != null)
            {
                //Debug.Log("Volume prin: " + audioSource.volume);
                if (audioSource.volume < 1)
                    audioSource.volume += 0.1f;

                //Debug.Log("Volume meta: " + audioSource.volume);
            }
            volumeLevel.text = Math.Truncate((audioSource.volume) * 10).ToString();

        }


        private void VolumeDown()
        {

            if (audioSource != null)
            {
                //Debug.Log("Volume prin: " + audioSource.volume);
                if (audioSource.volume > 0)
                    audioSource.volume -= 0.1f;

                //Debug.Log("Volume meta: " + audioSource.volume);
            }

            volumeLevel.text = Math.Truncate((audioSource.volume) * 10).ToString();

        }

        /*private void NextTrackTry()
        {
            if (audioSource != null)
            {
                //Debug.Log("mpika next trackkkkkkkkkkkkk");
                for (int i = 1; i < 6; i++) {
                    //Debug.Log("eimai edwwwwwwwwwwww");
                    //Debug.Log(GameObject.Find("Button " + (10 + i)));
                    if (audioSource.clip == GameObject.Find("Button 1" + i).GetComponent<UIPrinter>().audioClip) {
                        //Debug.Log("mpika for kai if vatheiaaaaaaaaaa");
                        if (i>=1 && i < 5)
                        {
                            audioSource.clip = GameObject.Find("Button 1" + (i + 1)).GetComponent<UIPrinter>().audioClip;
                            audioSource.Play();
                            trackPlaying.text = "FM" + (i + 1);
                            break;
                        }
                        else if (i==5){
                            audioSource.clip = GameObject.Find("Button 11").GetComponent<UIPrinter>().audioClip;
                            audioSource.Play();
                            trackPlaying.text = "FM1";
                        }
                    }

                }
            }

        }*/

        private void NextTrack()
        {
            if (audioSource.isPlaying)
            {
                if (audioSource.clip == clip1)
                {
                    audioSource.clip = clip2;
                    audioSource.Play();
                    trackPlaying.text = "FM2";
                }
                else if (audioSource.clip == clip2)
                {
                    audioSource.clip = clip3;
                    audioSource.Play();
                    trackPlaying.text = "FM3";
                }
                else if (audioSource.clip == clip3)
                {
                    audioSource.clip = clip4;
                    audioSource.Play();
                    trackPlaying.text = "FM4";
                }
                else if (audioSource.clip == clip4)
                {
                    audioSource.clip = clip5;
                    audioSource.Play();
                    trackPlaying.text = "FM5";
                }
                else if (audioSource.clip == clip5)
                {
                    audioSource.clip = clip1;
                    audioSource.Play();
                    trackPlaying.text = "FM1";
                }
            }

        }

        private void PreviousTrack()
        {

            if (audioSource.isPlaying)
            {
                if (audioSource.clip == clip1)
                {
                    audioSource.clip = clip5;
                    audioSource.Play();
                    trackPlaying.text = "FM5";
                }
                else if (audioSource.clip == clip2)
                {
                    audioSource.clip = clip1;
                    audioSource.Play();
                    trackPlaying.text = "FM1";
                }
                else if (audioSource.clip == clip3)
                {
                    audioSource.clip = clip2;
                    audioSource.Play();
                    trackPlaying.text = "FM2";
                }
                else if (audioSource.clip == clip4)
                {
                    audioSource.clip = clip3;
                    audioSource.Play();
                    trackPlaying.text = "FM3";
                }
                else if (audioSource.clip == clip5)
                {
                    audioSource.clip = clip4;
                    audioSource.Play();
                    trackPlaying.text = "FM4";
                }
            }

        }

        private void HelpCanvas() {

            if (!helpCanvas.activeSelf) helpCanvas.SetActive(true);
        }

        private void CloseHelp()
        {
            if (helpCanvas.activeSelf) helpCanvas.SetActive(false);
        }



        /*private void PreviousTrackTry()
        {
            if (audioSource != null)
            {

                for (int i = 1; i < 6; i++)
                {

                    if (audioSource.clip == GameObject.Find("Button 1" + i).GetComponent<UIPrinter>().audioClip)
                    {
                        if (i > 1 && i<=5)
                        {
                            audioSource.clip = GameObject.Find("Button 1" + (i - 1)).GetComponent<UIPrinter>().audioClip;
                            audioSource.Play();
                            trackPlaying.text = "FM" + (i - 1);
                            break;
                        }
                        else if(i==1)
                        {
                            audioSource.clip = GameObject.Find("Button 15").GetComponent<UIPrinter>().audioClip;
                            audioSource.Play();
                            trackPlaying.text = "FM5";
                            break;
                        }
                    }

                }

            }
        }*/




    }


}


