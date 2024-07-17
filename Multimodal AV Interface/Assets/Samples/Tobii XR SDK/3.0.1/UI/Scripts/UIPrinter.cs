// Copyright © 2018 – Property of Tobii AB (publ) - All Rights Reserved

using UnityEngine;
using Tobii.XR;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.Speech;
using SpeechRec;
using UnityEngine.SceneManagement;

namespace Tobii.XR.Examples
{
    /// <summary>
    /// Prints out messages.
    /// </summary>
    public class UIPrinter : MonoBehaviour
    {

        public GameObject originalCanvas;
        public GameObject buttonCanvas;
        //private UIGazeButtonGraphics btnGraphics;
        public UITriggerGazeButton btnTrigger;
        public AudioSource audioSource;
        public AudioClip audioClip;
        public Button onButton;
        public Button offButton;
        public GameObject tirePressureImage;
        public GameObject volume;
        public TextMeshProUGUI volumeLevel;
        public TextMeshProUGUI activeEnginePreset;
        public Boolean isPresetActive;
        public GameObject enginePresetsContent;
        public GameObject thisGameObject;
        public Button btn23;
        public Button btn24;
        public Button btn25;
        public GameObject presetText;
        public GameObject maintenanceUI;
        public GameObject detectionAnimation;
        public GameObject problemDetectionIndicator;
        public GameObject recentCallsButton;
        public GameObject contactsButton;
        public GameObject contactPrefab;
        public GameObject recentCallPrefab;
        public GameObject contactsCanvas;
        public GameObject position1;
        public GameObject position2;
        public GameObject position3;
        public GameObject phoneCallCanvas;
        public GameObject phoneCallName;
        public GameObject recentCallsCanvas;
        public GameObject positionsChild;
        public GameObject callPosition1;
        public GameObject callPosition2;
        public GameObject callPosition3;
        public GameObject callPosition4;
        public Boolean speechInitializer = true;
        public TextMeshProUGUI trackPlaying;
        public TextMeshProUGUI toolBarTrackPlaying;
        public GameObject addContactButton;
        public GameObject contactName;
        public GameObject saveContactButton;
        public TextMeshProUGUI nameInput;
        public SpeechRecognition spRec;
        public Boolean volumeUp = true;
        public Boolean volumeDown = true;
        public Boolean tempUp = true;
        public Boolean tempDown = true;
        public GameObject inboxCanvas;
        public GameObject outboxCanvas;
        public GameObject newMessageCanvas;
        public GameObject receiverPosition1;
        public GameObject receiverPosition2;
        public GameObject receiverPosition3;
        public GameObject chosenReceiver;
        public GameObject receiverOptionPrefab;
        public GameObject newMessageButton;
        public GameObject sendMessageButton;
        public GameObject messageTextInput;
        public GameObject outboxPosition1;
        public GameObject outboxPosition2;
        public GameObject outboxPosition3;
        public GameObject outboxMessagePrefab;
        public GameObject emptyMessageWarning;
        public GameObject noContactsWarning;
        public GameObject messageCanvas;
        public GameObject phoneCanvas;
        public GameObject openMessageCanvas;
        public GameObject forwardPosition1;
        public GameObject forwardPosition2;
        public GameObject forwardPosition3;
        public GameObject inboxPosition1;
        public GameObject inboxPosition2;
        public GameObject inboxPosition3;
        public GameObject mutedImage;
        public GameObject unMutedImage;
        public GameObject dwellTimeOn;
        public GameObject dwellTimeOff;
        public GameObject eyeBlinkOn;
        public GameObject eyeBlinkOff;
        public GameObject voiceCommandsOn;
        public GameObject voiceCommandsOff;
        public GameObject uiComponent;
        public GameObject inboxMessagePrefab;
        public UIBarLogic barLogic;
        public GameObject callingLabelCallCanvas;
        public GameObject incomingLabelCallCanvas;
        public GameObject uiTriggerMenu;
        public GameObject openTriggerMenuButton;
        public GameObject restartMenu;
        public BlinkCoroutine blinkScript;
        public CarCanvasSprites canvasSpritesOn;
        public PhoneSpritesScript phoneSprites;
        public MessageSpritesScript messageSprites;
        public GameObject noInboxMessagesAvailable;
        public GameObject noOutboxMessagesAvailable;
        public GameObject noContactsAvailable;
        public GameObject noRecentCallsAvailable;
        public GameObject exitOpenMessageButton;
        public GameObject openedMessage;
        public GameObject speedText;
        public Text speed;
        private Speedometer speedometer;
        public GameObject temperature;
        public GameObject detectingText;
        public GameObject unreadMessageSign;
        public GameObject unreadInboxMessageSign;
        public Color afterFocus = new Color32(255, 255, 255, 200);
        //public Double temperatureDouble;



        //public int randomNumber;



        //Voice Recognition Parameters

        /*private Dictionary<string, Action> keywordActions = new Dictionary<string, Action>();
        private KeywordRecognizer keywordRecognizer;*/
        //public float time;
        //public Boolean maintenanceBoolean = false;
        //findUI = GameObject.Find("UI").GetComponent<UIPrinter>();


        private void Update()
        {
            if (GameObject.Find("UI").GetComponent<UIPrinter>().inboxPosition1.GetComponent<UIPrinter>().positionsChild.activeSelf)
            {
                //findUI.noInboxMessagesAvailable.SetActive(false);
                GameObject.Find("UI").GetComponent<UIPrinter>().noInboxMessagesAvailable.SetActive(false);
            }
            else { GameObject.Find("UI").GetComponent<UIPrinter>().noInboxMessagesAvailable.SetActive(true); }

            if (GameObject.Find("UI").GetComponent<UIPrinter>().outboxPosition1.GetComponent<UIPrinter>().positionsChild.activeSelf)
            {
                GameObject.Find("UI").GetComponent<UIPrinter>().noOutboxMessagesAvailable.SetActive(false);
            }
            else { GameObject.Find("UI").GetComponent<UIPrinter>().noOutboxMessagesAvailable.SetActive(true); }

            if (GameObject.Find("UI").GetComponent<UIPrinter>().position1.GetComponent<UIPrinter>().positionsChild.activeSelf)
            {
                GameObject.Find("UI").GetComponent<UIPrinter>().noContactsAvailable.SetActive(false);
            }
            else { GameObject.Find("UI").GetComponent<UIPrinter>().noContactsAvailable.SetActive(true); }

            if (GameObject.Find("UI").GetComponent<UIPrinter>().callPosition1.GetComponent<UIPrinter>().positionsChild.activeSelf)
            {
                GameObject.Find("UI").GetComponent<UIPrinter>().noRecentCallsAvailable.SetActive(false);
            }
            else { GameObject.Find("UI").GetComponent<UIPrinter>().noRecentCallsAvailable.SetActive(true); }

            //Debug.Log(speedometer.speedometerText.ToString());

            GameObject.Find("UI").GetComponent<UIPrinter>().speedText.GetComponent<TextMeshProUGUI>().text = (Convert.ToInt32(GameObject.Find("UI").GetComponent<UIPrinter>().speed.text)*1.6).ToString();
        }


        void Start()
        {

            volumeUp = true;
            volumeDown = true;
            contactsCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().contactsCanvas;
            blinkScript = GameObject.Find("UI").GetComponent<BlinkCoroutine>();
        }
        /*
                private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
                {
                    Debug.Log("Keyword: " + args.text);
                    keywordActions[args.text].Invoke();
                }*/

        /*private void VolumeUp()
        {
            if (audioSource != null)
            {
                if (audioSource.volume < 1)
                {
                    audioSource.volume += 0.1f;
                    printer.volumeLevel.text = Math.Truncate((audioSource.volume) * 10).ToString();
                }
            }

            Debug.Log("ANEVIKE ENTASIIIIIIIIII" + audioSource.volume);

        }*/

        /*private void NewWord()
        {
            Debug.Log("AKOUSAAAAAAAA NEW WORD");
        }*/

        public void ReloadScene() {
            Debug.Log("MPIKA RELOAD");
            if (restartMenu.activeSelf) restartMenu.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void MinimizeUI() {

            if (!openTriggerMenuButton.activeSelf) openTriggerMenuButton.SetActive(true);
            if (uiTriggerMenu.activeSelf) uiTriggerMenu.SetActive(false);
        }
        public void MaximizeUI()
        {

            if (!uiTriggerMenu.activeSelf) uiTriggerMenu.SetActive(true);
            if (openTriggerMenuButton.activeSelf) openTriggerMenuButton.SetActive(false);
            if (buttonCanvas.activeSelf) buttonCanvas.SetActive(false);
            if (!originalCanvas.activeSelf) originalCanvas.SetActive(true);
            uiComponent.GetComponent<AudioEvents>().audioEvents.Stop();
        }

        public void DwellTimeSelectionTechnique() {
            if(blinkScript.eyeDwellTimeBool)
            {
                blinkScript.eyeDwellTimeBool = false;
                dwellTimeOff.SetActive(false);
                dwellTimeOff.GetComponent<Button>().GetComponent<Image>().color = afterFocus;
                dwellTimeOn.SetActive(true);
            }
            else
            {
                blinkScript.eyeDwellTimeBool = true;
                dwellTimeOff.SetActive(true);
                dwellTimeOn.GetComponent<Button>().GetComponent<Image>().color = afterFocus;
                dwellTimeOn.SetActive(false);
            }
        }

        public void EyeBlinkSelectionTechnique() {
            if (blinkScript.eyeBlinkBool)
            {
                blinkScript.eyeBlinkBool = false;
                eyeBlinkOff.GetComponent<Button>().GetComponent<Image>().color = afterFocus;
                eyeBlinkOff.SetActive(false);
                eyeBlinkOn.SetActive(true);
            }
            else
            {
                blinkScript.eyeBlinkBool = true;
                eyeBlinkOff.SetActive(true);
                eyeBlinkOn.GetComponent<Button>().GetComponent<Image>().color = afterFocus;
                eyeBlinkOn.SetActive(false);
            }
        }

        public void VoiceCommandsEnabler() {
            if (spRec.voiceCommandsBool)
            {
                spRec.voiceCommandsBool = false;
                voiceCommandsOff.GetComponent<Button>().GetComponent<Image>().color = afterFocus;
                voiceCommandsOff.SetActive(false);
                voiceCommandsOn.SetActive(true);
            }
            else
            {
                spRec.voiceCommandsBool = true;
                voiceCommandsOff.SetActive(true);
                voiceCommandsOn.GetComponent<Button>().GetComponent<Image>().color = afterFocus;
                voiceCommandsOn.SetActive(false);
            }
        }

        public void OpenInboxCanvas() {
            messageSprites.inboxSpriteOn.SetActive(true);
            messageSprites.outboxSpriteOn.SetActive(false);
            if (outboxCanvas.activeSelf) outboxCanvas.SetActive(false);
            if (newMessageCanvas.activeSelf) newMessageCanvas.SetActive(false);
            if (!newMessageButton.activeSelf) newMessageButton.SetActive(true);
            if (!inboxCanvas.activeSelf) inboxCanvas.SetActive(true);
            if (!spRec.keywordRecognizer.IsRunning) spRec.RestartKeywordRecognizer();

        }


        public void OpenOutboxCanvas()
        {
            messageSprites.inboxSpriteOn.SetActive(false);
            messageSprites.outboxSpriteOn.SetActive(true);
            if (newMessageCanvas.activeSelf) newMessageCanvas.SetActive(false);
            if (!newMessageButton.activeSelf) newMessageButton.SetActive(true);
            if (inboxCanvas.activeSelf) inboxCanvas.SetActive(false);
            if (!outboxCanvas.activeSelf) outboxCanvas.SetActive(true);
            if (!spRec.keywordRecognizer.IsRunning) spRec.RestartKeywordRecognizer();
        }

        public void ExitOpenMessageCanvas() {

            if (!messageCanvas.activeSelf) messageCanvas.SetActive(true);
            if (openMessageCanvas.activeSelf) openMessageCanvas.SetActive(false);
            if (!spRec.keywordRecognizer.IsRunning) spRec.RestartKeywordRecognizer();
            //if (!openedMessage.GetComponent<OutboxMessage>().openButton.activeSelf) openedMessage.GetComponent<OutboxMessage>().openButton.SetActive(true);

        }

        public void OpenMessageCanvas()
        {
            if (GameObject.Find("UI").GetComponent<UIPrinter>().inboxCanvas.activeSelf && !this.GetComponent<MessageRead>().messageIsRead) {
                this.GetComponent<MessageRead>().messageIsRead = true;
                //this.GetComponent<MessageRead>().unreadSign.SetActive(false);
                barLogic.unreadInboxCounter -= 1;
                if (unreadMessageSign.activeSelf) unreadMessageSign.SetActive(false);
                if (this.GetComponent<OutboxMessage>().unreadInboxMessageSign.activeSelf) this.GetComponent<OutboxMessage>().unreadInboxMessageSign.SetActive(false);
            }
            GameObject.Find("UI").GetComponent<UIPrinter>().exitOpenMessageButton.GetComponent<UIPrinter>().openedMessage = GameObject.Find("Sprite Mask - Open Message");
            if (noContactsWarning.activeSelf) noContactsWarning.SetActive(false);
            if (messageCanvas.activeSelf) messageCanvas.SetActive(false);
            if (!openMessageCanvas.activeSelf) openMessageCanvas.SetActive(true);
            if (openMessageCanvas.activeSelf && GameObject.Find("Sprite Mask - Open Message").GetComponent<OutboxMessage>().openButton.activeSelf) GameObject.Find("Sprite Mask - Open Message").GetComponent<OutboxMessage>().openButton.SetActive(false);
            if (!openMessageCanvas.GetComponent<OpenMessage>().forwardButton.activeSelf) openMessageCanvas.GetComponent<OpenMessage>().forwardButton.SetActive(true);
            if (!openMessageCanvas.GetComponent<OpenMessage>().deleteButton.activeSelf) openMessageCanvas.GetComponent<OpenMessage>().deleteButton.SetActive(true);
            openMessageCanvas.GetComponent<OpenMessage>().deleteButton.GetComponent<UIPrinter>().contactPrefab = this.GetComponent<UIPrinter>().contactPrefab;
            if (sendMessageButton.activeSelf) sendMessageButton.SetActive(false);
            openMessageCanvas.GetComponent<OpenMessage>().messageName.text = this.GetComponent<OutboxMessage>().receiverName.text;
            openMessageCanvas.GetComponent<OpenMessage>().messageContent.text = this.GetComponent<OutboxMessage>().messageContent.text;
        }

        public void ForwardMessage() {

            openMessageCanvas.GetComponent<OpenMessage>().forwardButton.SetActive(false);
            openMessageCanvas.GetComponent<OpenMessage>().deleteButton.SetActive(false);
            if (noContactsWarning.activeSelf) noContactsWarning.SetActive(false);
            this.GetComponent<OutboxMessage>().receiverName.text = "";


            if (spRec.contactList.Count != 0)
            {
                foreach (string s in spRec.contactList)
                {
                    GameObject newContact;
                    if (forwardPosition1.GetComponent<UIPrinter>().positionsChild == null)
                    {
                        newContact = Instantiate(receiverOptionPrefab, forwardPosition1.transform, worldPositionStays: false);
                        forwardPosition1.GetComponent<UIPrinter>().positionsChild = newContact;
                        //Debug.Log("listaaaaaaa: " + s);
                        //Debug.Log("gameobjectttttt: " + newContact.GetComponent<ReceiverName>().receiverName.GetComponent<TextMeshProUGUI>().text);
                        newContact.GetComponent<ReceiverName>().receiverName.GetComponent<Text>().text = s.ToString();
                        newContact.gameObject.transform.localScale -= new Vector3(0.35f, 0.75f, 0);
                        newContact.GetComponent<UIPrinter>().openMessageCanvas = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UIPrinter>().openMessageCanvas;
                        newContact.GetComponent<UIPrinter>().sendMessageButton = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UIPrinter>().sendMessageButton;
                        newContact.GetComponent<UIPrinter>().forwardPosition1 = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UIPrinter>().forwardPosition1;
                        newContact.GetComponent<UIPrinter>().forwardPosition2 = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UIPrinter>().forwardPosition2;
                        newContact.GetComponent<UIPrinter>().forwardPosition3 = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UIPrinter>().forwardPosition3;
                        newContact.GetComponent<UITriggerGazeButton>().uiComp = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UITriggerGazeButton>().uiComp;
                        newContact.GetComponent<UIPrinter>().spRec = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UIPrinter>().spRec;

                    }
                    else if (forwardPosition1.GetComponent<UIPrinter>().positionsChild != null && forwardPosition2.GetComponent<UIPrinter>().positionsChild == null)
                    {
                        if (forwardPosition1.GetComponent<UIPrinter>().positionsChild.GetComponent<ReceiverName>().receiverName.GetComponent<Text>().text == s) continue;

                        newContact = Instantiate(receiverOptionPrefab, forwardPosition2.transform, worldPositionStays: false);
                        forwardPosition2.GetComponent<UIPrinter>().positionsChild = newContact;
                        newContact.GetComponent<ReceiverName>().receiverName.GetComponent<Text>().text = s.ToString();
                        newContact.gameObject.transform.localScale -= new Vector3(0.35f, 0.75f, 0);
                        newContact.GetComponent<UIPrinter>().openMessageCanvas = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UIPrinter>().openMessageCanvas;
                        newContact.GetComponent<UIPrinter>().sendMessageButton = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UIPrinter>().sendMessageButton;
                        newContact.GetComponent<UIPrinter>().forwardPosition1 = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UIPrinter>().forwardPosition1;
                        newContact.GetComponent<UIPrinter>().forwardPosition2 = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UIPrinter>().forwardPosition2;
                        newContact.GetComponent<UIPrinter>().forwardPosition3 = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UIPrinter>().forwardPosition3;
                        newContact.GetComponent<UITriggerGazeButton>().uiComp = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UITriggerGazeButton>().uiComp;
                        newContact.GetComponent<UIPrinter>().spRec = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UIPrinter>().spRec;

                    }
                    else if (forwardPosition1.GetComponent<UIPrinter>().positionsChild != null && forwardPosition2.GetComponent<UIPrinter>().positionsChild != null && forwardPosition3.GetComponent<UIPrinter>().positionsChild == null)
                    {

                        if (forwardPosition2.GetComponent<UIPrinter>().positionsChild.GetComponent<ReceiverName>().receiverName.GetComponent<Text>().text == s || forwardPosition1.GetComponent<UIPrinter>().positionsChild.GetComponent<ReceiverName>().receiverName.GetComponent<Text>().text == s) continue;

                        newContact = Instantiate(receiverOptionPrefab, forwardPosition3.transform, worldPositionStays: false);
                        forwardPosition3.GetComponent<UIPrinter>().positionsChild = newContact;
                        newContact.GetComponent<ReceiverName>().receiverName.GetComponent<Text>().text = s.ToString();
                        newContact.gameObject.transform.localScale -= new Vector3(0.35f, 0.75f, 0);
                        newContact.GetComponent<UIPrinter>().openMessageCanvas = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UIPrinter>().openMessageCanvas;
                        newContact.GetComponent<UIPrinter>().sendMessageButton = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UIPrinter>().sendMessageButton;
                        newContact.GetComponent<UIPrinter>().forwardPosition1 = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UIPrinter>().forwardPosition1;
                        newContact.GetComponent<UIPrinter>().forwardPosition2 = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UIPrinter>().forwardPosition2;
                        newContact.GetComponent<UIPrinter>().forwardPosition3 = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UIPrinter>().forwardPosition3;
                        newContact.GetComponent<UITriggerGazeButton>().uiComp = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UITriggerGazeButton>().uiComp;
                        newContact.GetComponent<UIPrinter>().spRec = GameObject.Find("Button 8 - Exit Open Canvas").GetComponent<UIPrinter>().spRec;

                    }
                }
            }
            else
            {
                if (!noContactsWarning.activeSelf) noContactsWarning.SetActive(true);

            }




        }


        public void ChooseReceiverForward() {

            openMessageCanvas.GetComponent<OpenMessage>().messageName.text = this.GetComponent<ReceiverName>().receiverName.GetComponent<Text>().text;
            if (!sendMessageButton.activeSelf) sendMessageButton.SetActive(true);
            if (forwardPosition1.GetComponent<UIPrinter>().positionsChild != null) Destroy(forwardPosition1.GetComponent<UIPrinter>().positionsChild.gameObject);
            if (forwardPosition2.GetComponent<UIPrinter>().positionsChild != null) Destroy(forwardPosition2.GetComponent<UIPrinter>().positionsChild.gameObject);
            if (forwardPosition3.GetComponent<UIPrinter>().positionsChild != null) Destroy(forwardPosition3.GetComponent<UIPrinter>().positionsChild.gameObject);


            if (spRec.keywordRecognizer.IsRunning)
            {
                spRec.keywordRecognizer.Stop();
            }
            PhraseRecognitionSystem.Shutdown();


            StartCoroutine(spRec.StartDictationRecognizer());

        }


        public void DeleteMessage() {

            //Debug.Log(position1.GetComponent<UIPrinter>().positionsChild);
            if (this.contactPrefab == outboxPosition1.GetComponent<UIPrinter>().positionsChild && outboxPosition2.GetComponent<UIPrinter>().positionsChild != null)
            {
                //Debug.Log("MPIKAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                outboxPosition2.GetComponent<UIPrinter>().positionsChild.transform.SetParent(outboxPosition1.transform, false);
                outboxPosition1.GetComponent<UIPrinter>().positionsChild = outboxPosition2.GetComponent<UIPrinter>().positionsChild;
                outboxPosition2.GetComponent<UIPrinter>().positionsChild = null;
                if (outboxPosition3.GetComponent<UIPrinter>().positionsChild != null)
                {
                    outboxPosition3.GetComponent<UIPrinter>().positionsChild.transform.SetParent(outboxPosition2.transform, false);
                    outboxPosition2.GetComponent<UIPrinter>().positionsChild = outboxPosition3.GetComponent<UIPrinter>().positionsChild;
                    outboxPosition3.GetComponent<UIPrinter>().positionsChild = null;
                }
                //position1.GetComponent<UIPrinter>().positionsChild.transform.position = position1.transform.position;
            }
            else if (this.contactPrefab == outboxPosition2.GetComponent<UIPrinter>().positionsChild && outboxPosition3.GetComponent<UIPrinter>().positionsChild != null)
            {

                outboxPosition3.GetComponent<UIPrinter>().positionsChild.transform.SetParent(outboxPosition2.transform, false);
                outboxPosition2.GetComponent<UIPrinter>().positionsChild = outboxPosition3.GetComponent<UIPrinter>().positionsChild;
                outboxPosition3.GetComponent<UIPrinter>().positionsChild = null;
            }

            if (this.contactPrefab == inboxPosition1.GetComponent<UIPrinter>().positionsChild && inboxPosition2.GetComponent<UIPrinter>().positionsChild != null)
            {
                //Debug.Log("MPIKAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                inboxPosition2.GetComponent<UIPrinter>().positionsChild.transform.SetParent(inboxPosition1.transform, false);
                inboxPosition1.GetComponent<UIPrinter>().positionsChild = inboxPosition2.GetComponent<UIPrinter>().positionsChild;
                inboxPosition2.GetComponent<UIPrinter>().positionsChild = null;
                if (inboxPosition3.GetComponent<UIPrinter>().positionsChild != null)
                {
                    inboxPosition3.GetComponent<UIPrinter>().positionsChild.transform.SetParent(inboxPosition2.transform, false);
                    inboxPosition2.GetComponent<UIPrinter>().positionsChild = inboxPosition3.GetComponent<UIPrinter>().positionsChild;
                    inboxPosition3.GetComponent<UIPrinter>().positionsChild = null;
                }
                //position1.GetComponent<UIPrinter>().positionsChild.transform.position = position1.transform.position;
            }
            else if (this.contactPrefab == inboxPosition2.GetComponent<UIPrinter>().positionsChild && inboxPosition3.GetComponent<UIPrinter>().positionsChild != null)
            {

                inboxPosition3.GetComponent<UIPrinter>().positionsChild.transform.SetParent(inboxPosition2.transform, false);
                inboxPosition2.GetComponent<UIPrinter>().positionsChild = inboxPosition3.GetComponent<UIPrinter>().positionsChild;
                inboxPosition3.GetComponent<UIPrinter>().positionsChild = null;
            }



            //Debug.Log(spRec.contactList.Count);
            Destroy(this.contactPrefab);
            this.positionsChild = null;
            if (openMessageCanvas.activeSelf) openMessageCanvas.SetActive(false);
            if (!messageCanvas.activeSelf) messageCanvas.SetActive(true);


        }

        public void OpenNewMessageCanvas()
        {
            if (inboxCanvas.activeSelf) inboxCanvas.SetActive(false);
            if (outboxCanvas.activeSelf) outboxCanvas.SetActive(false);
            if (!newMessageCanvas.activeSelf) newMessageCanvas.SetActive(true);
            if (this.GetComponent<MessageSpritesScript>().inboxSpriteOn.activeSelf) this.GetComponent<MessageSpritesScript>().inboxSpriteOn.SetActive(false);
            if (this.GetComponent<MessageSpritesScript>().outboxSpriteOn.activeSelf) this.GetComponent<MessageSpritesScript>().outboxSpriteOn.SetActive(false);
            if ((receiverPosition1.GetComponent<UIPrinter>().positionsChild == null && !chosenReceiver.activeSelf) || !chosenReceiver.activeSelf)
            {
                if (noContactsWarning.activeSelf) noContactsWarning.SetActive(false);

                if (spRec.contactList.Count != 0)
                {
                    foreach (string s in spRec.contactList)
                    {
                        GameObject newContact;
                        if (receiverPosition1.GetComponent<UIPrinter>().positionsChild == null)
                        {
                            newContact = Instantiate(receiverOptionPrefab, receiverPosition1.transform, worldPositionStays: false);
                            PrefabInitializer(newContact);
                            receiverPosition1.GetComponent<UIPrinter>().positionsChild = newContact;
                            //Debug.Log("listaaaaaaa: " + s);
                            //Debug.Log("gameobjectttttt: " + newContact.GetComponent<ReceiverName>().receiverName.GetComponent<TextMeshProUGUI>().text);
                            newContact.GetComponent<ReceiverName>().receiverName.GetComponent<Text>().text = s.ToString();
                        }
                        else if (receiverPosition1.GetComponent<UIPrinter>().positionsChild != null && receiverPosition2.GetComponent<UIPrinter>().positionsChild == null)
                        {
                            if (receiverPosition1.GetComponent<UIPrinter>().positionsChild.GetComponent<ReceiverName>().receiverName.GetComponent<Text>().text == s) continue;

                            newContact = Instantiate(receiverOptionPrefab, receiverPosition2.transform, worldPositionStays: false);
                            PrefabInitializer(newContact);
                            receiverPosition2.GetComponent<UIPrinter>().positionsChild = newContact;
                            newContact.GetComponent<ReceiverName>().receiverName.GetComponent<Text>().text = s.ToString();

                        }
                        else if (receiverPosition1.GetComponent<UIPrinter>().positionsChild != null && receiverPosition2.GetComponent<UIPrinter>().positionsChild != null && receiverPosition3.GetComponent<UIPrinter>().positionsChild == null)
                        {

                            if (receiverPosition2.GetComponent<UIPrinter>().positionsChild.GetComponent<ReceiverName>().receiverName.GetComponent<Text>().text == s || receiverPosition1.GetComponent<UIPrinter>().positionsChild.GetComponent<ReceiverName>().receiverName.GetComponent<Text>().text == s) continue;

                            newContact = Instantiate(receiverOptionPrefab, receiverPosition3.transform, worldPositionStays: false);
                            PrefabInitializer(newContact);
                            receiverPosition3.GetComponent<UIPrinter>().positionsChild = newContact;
                            newContact.GetComponent<ReceiverName>().receiverName.GetComponent<Text>().text = s.ToString();

                        }
                    }
                }
                else
                {
                    if (!noContactsWarning.activeSelf) noContactsWarning.SetActive(true);

                }


            }
            else {

                messageTextInput.GetComponent<TMP_InputField>().characterLimit = 50;
                //contactName.GetComponent<UIPrinter>().speechInitializer = false;
                messageTextInput.GetComponent<TMP_InputField>().ActivateInputField();
                messageTextInput.GetComponent<TMP_InputField>().Select();
                /*
                foreach (char c in Input.inputString)
                {
                    *//*if (nameInput.text.Length <= 8)
                    {
                        nameInput.text += c;
                    }
                    else { break; }*//*
                    if (messageTextInput.GetComponent<TMP_InputField>().text.Length <= 8)
                    {
                        messageTextInput.GetComponent<TMP_InputField>().text += c;
                    }
                    else { break; }

                }*/
                messageTextInput.GetComponent<TMP_InputField>().text = "";
                /*if (spRec.keywordRecognizer.IsRunning)
                {
                    spRec.keywordRecognizer.Stop();
                }
                PhraseRecognitionSystem.Shutdown();
                StartCoroutine(spRec.StartDictationRecognizer());*/
                spRec.spRecIsOff = true;
                spRec.StartDict();
            }

            if (newMessageButton.activeSelf) newMessageButton.SetActive(false);
        }


        public void PrefabInitializer(GameObject thisPrefab) {

            thisPrefab.GetComponent<UIPrinter>().chosenReceiver = newMessageCanvas.GetComponent<UIPrinter>().chosenReceiver;
            thisPrefab.GetComponent<UIPrinter>().sendMessageButton = newMessageCanvas.GetComponent<UIPrinter>().sendMessageButton;
            thisPrefab.GetComponent<UIPrinter>().messageTextInput = newMessageCanvas.GetComponent<UIPrinter>().messageTextInput;
            thisPrefab.GetComponent<UIPrinter>().receiverPosition1 = newMessageCanvas.GetComponent<UIPrinter>().receiverPosition1;
            thisPrefab.GetComponent<UIPrinter>().receiverPosition2 = newMessageCanvas.GetComponent<UIPrinter>().receiverPosition2;
            thisPrefab.GetComponent<UIPrinter>().receiverPosition3 = newMessageCanvas.GetComponent<UIPrinter>().receiverPosition3;
            thisPrefab.GetComponent<UITriggerGazeButton>().uiComp = newMessageCanvas.GetComponent<UIPrinter>().uiComponent;

        }

        public void ChooseMessageReceiver()
        {
            //Debug.Log(chosenReceiver.GetComponent<TextMeshProUGUI>().text);
            chosenReceiver.GetComponentInChildren<TextMeshProUGUI>().text = this.GetComponent<ReceiverName>().receiverName.GetComponent<Text>().text;
            if (!chosenReceiver.activeSelf) chosenReceiver.SetActive(true);
            if (!sendMessageButton.activeSelf) sendMessageButton.SetActive(true);
            messageTextInput.GetComponent<TMP_InputField>().characterLimit = 50;
            //contactName.GetComponent<UIPrinter>().speechInitializer = false;
            chosenReceiver.GetComponent<UIPrinter>().WriteMessage();
            //WriteMessage();

            

        }

        public void WriteMessage() {


            if (receiverPosition1.GetComponent<UIPrinter>().positionsChild != null)
            {
                receiverPosition1.GetComponent<UIPrinter>().positionsChild = null;
                Transform children = receiverPosition1.GetComponentInChildren<Transform>();
                foreach (Transform child in children)
                {
                    Destroy(child.gameObject);
                }
                if (receiverPosition2.GetComponent<UIPrinter>().positionsChild != null)
                {
                    Transform children1 = receiverPosition2.GetComponentInChildren<Transform>();
                    foreach (Transform child1 in children1)
                    {
                        Destroy(child1.gameObject);
                        receiverPosition2.GetComponent<UIPrinter>().positionsChild = null;
                    }
                    if (receiverPosition3.GetComponent<UIPrinter>().positionsChild != null)
                    {
                        Transform children2 = receiverPosition3.GetComponentInChildren<Transform>();
                        foreach (Transform child2 in children2)
                        {
                            Destroy(child2.gameObject);
                            receiverPosition3.GetComponent<UIPrinter>().positionsChild = null;
                        }
                    }
                }
            }


            /*if (spRec.keywordRecognizer.IsRunning)
            {
                spRec.keywordRecognizer.Stop();
            }
            PhraseRecognitionSystem.Shutdown();*/


            messageTextInput.GetComponent<TMP_InputField>().ActivateInputField();
            messageTextInput.GetComponent<TMP_InputField>().Select();
            messageTextInput.GetComponent<TMP_InputField>().text = "";
            spRec.spRecIsOff = true;
            spRec.StartDict();
            //StartCoroutine(spRec.StartDictationRecognizer());

            //messageTextInput.GetComponent<TMP_InputField>().characterLimit = 8;
            //contactName.GetComponent<UIPrinter>().ContactNameWriter();
            /*foreach (char c in Input.inputString) {
                *//*if (nameInput.text.Length <= 8)
                {
                    nameInput.text += c;
                }
                else { break; }*//*
                if (contactName.GetComponent<TMP_InputField>().text.Length <= 8)
                {
                    contactName.GetComponent<TMP_InputField>().text += c;
                }
                else { break; }

            }*/


            /*foreach (char c in Input.inputString)
            {
                *//*if (nameInput.text.Length <= 8)
                {
                    nameInput.text += c;
                }
                else { break; }*//*
                if (messageTextInput.GetComponent<TMP_InputField>().text.Length <= 8)
                {
                    messageTextInput.GetComponent<TMP_InputField>().text += c;
                }
                else { break; }

            }*/


        }

        public void ContactMessage()
        {
            //chosenReceiver.GetComponentInChildren<TextMeshProUGUI>().text = this.GetComponent<ContactsType>().contactName.text;
            newMessageButton.GetComponent<UIPrinter>().SendMessageFromContacts();
        }

        public void SendMessageFromContacts() {

            //if (phoneCallCanvas.activeSelf) phoneCallCanvas.SetActive(false);
            //if (contactsCanvas.activeSelf) contactsCanvas.SetActive(false);
            if (inboxCanvas.activeSelf) inboxCanvas.SetActive(false);
            if (outboxCanvas.activeSelf) outboxCanvas.SetActive(false);
            if (newMessageButton.activeSelf) newMessageButton.SetActive(false);
            if (!sendMessageButton.activeSelf) sendMessageButton.SetActive(true);
            if (!chosenReceiver.activeSelf) chosenReceiver.SetActive(true);
            if (noContactsWarning.activeSelf) noContactsWarning.SetActive(false);
            chosenReceiver.GetComponentInChildren<TextMeshProUGUI>().text = this.GetComponentInParent<ContactsType>().contactName.text;

            messageTextInput.GetComponent<TMP_InputField>().characterLimit = 50;
            messageTextInput.GetComponent<TMP_InputField>().text = " ";
            //contactName.GetComponent<UIPrinter>().speechInitializer = false;
            /*if (spRec.keywordRecognizer.IsRunning)
            {
                spRec.keywordRecognizer.Stop();
            }
            PhraseRecognitionSystem.Shutdown();*/
            spRec.spRecIsOff = true;
            messageTextInput.GetComponent<TMP_InputField>().ActivateInputField();
            messageTextInput.GetComponent<TMP_InputField>().Select();
            spRec.StartDict();

            //while (spRec.spRecIsOff == false) StartCoroutine(WaitCoroutine());
            if (!newMessageCanvas.activeSelf) newMessageCanvas.SetActive(true);
            if (phoneCanvas.activeSelf) phoneCanvas.SetActive(false);
            if (!messageCanvas.activeSelf) messageCanvas.SetActive(true);
        }

        IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(1);

        }

        public void SendForwardMessage() {
            GameObject sendNewForwardMessage;
            if (outboxPosition1.GetComponent<UIPrinter>().positionsChild != null)
            {
                if (outboxPosition2.GetComponent<UIPrinter>().positionsChild != null)
                {
                    if (outboxPosition3.GetComponent<UIPrinter>().positionsChild != null)
                    {
                        Destroy(outboxPosition3.GetComponent<UIPrinter>().positionsChild);
                    }
                    outboxPosition2.GetComponent<UIPrinter>().positionsChild.transform.SetParent(outboxPosition3.transform, false);
                    outboxPosition3.GetComponent<UIPrinter>().positionsChild = outboxPosition2.GetComponent<UIPrinter>().positionsChild;
                }

                outboxPosition1.GetComponent<UIPrinter>().positionsChild.transform.SetParent(outboxPosition2.transform, false);
                outboxPosition2.GetComponent<UIPrinter>().positionsChild = outboxPosition1.GetComponent<UIPrinter>().positionsChild;
            }
            sendNewForwardMessage = Instantiate(outboxMessagePrefab, outboxPosition1.transform, worldPositionStays: false);
            outboxPosition1.GetComponent<UIPrinter>().positionsChild = sendNewForwardMessage;
            sendNewForwardMessage.GetComponent<OutboxMessage>().receiverName.text = this.GetComponent<OutboxMessage>().receiverName.text;
            sendNewForwardMessage.GetComponent<OutboxMessage>().messageContent.text = messageTextInput.GetComponent<TextMeshProUGUI>().text;

            //sendNewMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().noContactsWarning = sendMessageButton.GetComponent<UIPrinter>().noContactsWarning;
            sendNewForwardMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().contactPrefab = sendNewForwardMessage;
            sendNewForwardMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().openMessageCanvas = sendMessageButton.GetComponent<UIPrinter>().openMessageCanvas;
            sendNewForwardMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().messageCanvas = sendMessageButton.GetComponent<UIPrinter>().messageCanvas;
            sendNewForwardMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().sendMessageButton = sendMessageButton.GetComponent<UIPrinter>().sendMessageButton;
            sendNewForwardMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().noContactsWarning = sendMessageButton.GetComponent<UIPrinter>().noContactsWarning;
            sendNewForwardMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UITriggerGazeButton>().uiComp = sendMessageButton.GetComponent<UITriggerGazeButton>().uiComp;

            if (openMessageCanvas.activeSelf) openMessageCanvas.SetActive(false);
            if (inboxCanvas.activeSelf) inboxCanvas.SetActive(false);
            if (!outboxCanvas.activeSelf) outboxCanvas.SetActive(true);
            if (!messageCanvas.activeSelf) messageCanvas.SetActive(true);
            if (!this.GetComponent<OutboxMessage>().openButton.activeSelf) this.GetComponent<OutboxMessage>().openButton.SetActive(true);
            messageSprites.inboxSpriteOn.SetActive(false);
            messageSprites.outboxSpriteOn.SetActive(true);
            /*sendNewMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().openMessageCanvas = GameObject.Find("Canvas - Outbox").GetComponent<UIPrinter>().openMessageCanvas;
            sendNewMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().messageCanvas = GameObject.Find("Canvas - Outbox").GetComponent<UIPrinter>().messageCanvas;*/
            //chosenReceiver.GetComponentInChildren<TextMeshProUGUI>().text = null;


        }

        public void SendNewMessage() {
            if (messageTextInput.GetComponent<TMP_InputField>().text.Length >= 0)
            {
                GameObject sendNewMessage;
                if (outboxPosition1.GetComponent<UIPrinter>().positionsChild != null)
                {
                    if (outboxPosition2.GetComponent<UIPrinter>().positionsChild != null)
                    {
                        if (outboxPosition3.GetComponent<UIPrinter>().positionsChild != null)
                        {
                            Destroy(outboxPosition3.GetComponent<UIPrinter>().positionsChild);
                        }
                        outboxPosition2.GetComponent<UIPrinter>().positionsChild.transform.SetParent(outboxPosition3.transform, false);
                        outboxPosition3.GetComponent<UIPrinter>().positionsChild = outboxPosition2.GetComponent<UIPrinter>().positionsChild;
                    }

                    outboxPosition1.GetComponent<UIPrinter>().positionsChild.transform.SetParent(outboxPosition2.transform, false);
                    outboxPosition2.GetComponent<UIPrinter>().positionsChild = outboxPosition1.GetComponent<UIPrinter>().positionsChild;
                }

                sendNewMessage = Instantiate(outboxMessagePrefab, outboxPosition1.transform, worldPositionStays: false);
                outboxPosition1.GetComponent<UIPrinter>().positionsChild = sendNewMessage;
                //GameObject newOpenButton = sendNewMessage.GetComponent<OutboxMessage>().openButton;
                //newOpenButton.GetComponent<UIPrinter>().contactPrefab = sendNewMessage;
                
                sendNewMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().contactPrefab = sendNewMessage;
                sendNewMessage.GetComponent<OutboxMessage>().receiverName.text = chosenReceiver.GetComponentInChildren<TextMeshProUGUI>().text;
                sendNewMessage.GetComponent<OutboxMessage>().messageContent.text = messageTextInput.GetComponent<TMP_InputField>().text;
                messageTextInput.GetComponent<TMP_InputField>().DeactivateInputField();
                //newOpenButton.GetComponent<UIPrinter>().messageCanvas = sendMessageButton.GetComponent<UIPrinter>().messageCanvas;
                //sendNewMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().noContactsWarning = sendMessageButton.GetComponent<UIPrinter>().noContactsWarning;
                //Debug.Log("PRINNNNNNNNNNNNNNN");
                sendNewMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().openMessageCanvas = sendMessageButton.GetComponent<UIPrinter>().openMessageCanvas;
                sendNewMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().messageCanvas = sendMessageButton.GetComponent<UIPrinter>().messageCanvas;
                sendNewMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().sendMessageButton = sendMessageButton.GetComponent<UIPrinter>().sendMessageButton;
                sendNewMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().noContactsWarning = sendMessageButton.GetComponent<UIPrinter>().noContactsWarning;
                sendNewMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UITriggerGazeButton>().uiComp = GameObject.Find("UI").GetComponent<UIPrinter>().uiComponent;
                sendNewMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().unreadMessageSign = GameObject.Find("UI").GetComponent<UIPrinter>().unreadMessageSign;
                //Debug.Log("METAAAAAAAAAAAAAAA");
                uiComponent.GetComponent<AudioEvents>().audioEvents.PlayOneShot(uiComponent.GetComponent<AudioEvents>().messageSent);
                chosenReceiver.GetComponentInChildren<TextMeshProUGUI>().text = null;
                if (chosenReceiver.activeSelf) chosenReceiver.SetActive(false);
                if (!newMessageButton.activeSelf) newMessageButton.SetActive(true);
                if (sendMessageButton.activeSelf) sendMessageButton.SetActive(false);
                if (newMessageCanvas.activeSelf) newMessageCanvas.SetActive(false);
                if (!outboxCanvas.activeSelf) outboxCanvas.SetActive(true);
                messageSprites.inboxSpriteOn.SetActive(false);
                messageSprites.outboxSpriteOn.SetActive(true);
                if(!spRec.keywordRecognizer.IsRunning) spRec.RestartKeywordRecognizer();

            }
            else {

                //Debug.Log("ADEIO MINIMA, GRAPSE KATI");
                messageTextInput.GetComponent<TMP_InputField>().ActivateInputField();
                messageTextInput.GetComponent<TMP_InputField>().Select();
                chosenReceiver.GetComponent<UIPrinter>().WriteMessage();
                StartCoroutine(EmptyMessageWarning());

            }




        }

        IEnumerator EmptyMessageWarning()
        {
            if (!emptyMessageWarning.activeSelf) emptyMessageWarning.SetActive(true);
            yield return new WaitForSecondsRealtime(1);

            if (emptyMessageWarning.activeSelf) emptyMessageWarning.SetActive(false);
            messageTextInput.GetComponent<TMP_InputField>().ActivateInputField();
            messageTextInput.GetComponent<TMP_InputField>().Select();
            chosenReceiver.GetComponent<UIPrinter>().WriteMessage();

        }


        public void IncomingMessage(GameObject inboxCanvas, GameObject sender, GameObject context) {

            GameObject newIncomingMessage;
            inboxPosition1 = inboxCanvas.GetComponent<UIPrinter>().inboxPosition1;
            inboxPosition2 = inboxCanvas.GetComponent<UIPrinter>().inboxPosition2;
            inboxPosition3 = inboxCanvas.GetComponent<UIPrinter>().inboxPosition3;
            inboxMessagePrefab = inboxCanvas.GetComponent<UIPrinter>().inboxMessagePrefab;
            if (inboxPosition1.GetComponent<UIPrinter>().positionsChild != null)
            {
                if (inboxPosition2.GetComponent<UIPrinter>().positionsChild != null)
                {
                    if (inboxPosition3.GetComponent<UIPrinter>().positionsChild != null)
                    {
                        Destroy(inboxPosition3.GetComponent<UIPrinter>().positionsChild);
                    }
                    inboxPosition2.GetComponent<UIPrinter>().positionsChild.transform.SetParent(inboxPosition3.transform, false);
                    inboxPosition3.GetComponent<UIPrinter>().positionsChild = inboxPosition2.GetComponent<UIPrinter>().positionsChild;
                }

                inboxPosition1.GetComponent<UIPrinter>().positionsChild.transform.SetParent(inboxPosition2.transform, false);
                inboxPosition2.GetComponent<UIPrinter>().positionsChild = inboxPosition1.GetComponent<UIPrinter>().positionsChild;
            }

            //uiComponent.GetComponent<AudioEvents>().audioEvents.clip = uiComponent.GetComponent<AudioEvents>().incomingMessageAudio;
            uiComponent.GetComponent<AudioEvents>().audioEvents.PlayOneShot(uiComponent.GetComponent<AudioEvents>().incomingMessageAudio);
            newIncomingMessage = Instantiate(inboxMessagePrefab, inboxPosition1.transform, worldPositionStays: false);
            inboxPosition1.GetComponent<UIPrinter>().positionsChild = newIncomingMessage;
            //newIncomingMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().contactPrefab = newIncomingMessage;
            newIncomingMessage.GetComponent<OutboxMessage>().receiverName.text = sender.GetComponent<TextMeshProUGUI>().text;
            newIncomingMessage.GetComponent<OutboxMessage>().messageContent.text = context.GetComponent<TextMeshProUGUI>().text;
            //messageTextInput.GetComponent<TMP_InputField>().DeactivateInputField();
            newIncomingMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().barLogic = GameObject.Find("UI").GetComponent<UIPrinter>().barLogic;
            newIncomingMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().contactPrefab = newIncomingMessage;
            //newIncomingMessage.GetComponent<MessageRead>().unreadSign.SetActive(true);

            newIncomingMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().sendMessageButton = inboxCanvas.GetComponent<UIPrinter>().sendMessageButton;
            newIncomingMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().noContactsWarning = inboxCanvas.GetComponent<UIPrinter>().noContactsWarning;
            newIncomingMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().openMessageCanvas = inboxCanvas.GetComponent<UIPrinter>().openMessageCanvas;
            newIncomingMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().messageCanvas = inboxCanvas.GetComponent<UIPrinter>().messageCanvas;
            newIncomingMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().unreadMessageSign = newIncomingMessage.GetComponent<OutboxMessage>().unreadInboxMessageSign;
            //newIncomingMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UIPrinter>().unreadInboxMessageSign = GameObject.Find("Unread");
            newIncomingMessage.GetComponent<OutboxMessage>().openButton.GetComponent<UITriggerGazeButton>().uiComp = inboxCanvas.GetComponent<UIPrinter>().uiComponent;
            //chosenReceiver.GetComponentInChildren<TextMeshProUGUI>().text = null;
            //if (chosenReceiver.activeSelf) chosenReceiver.SetActive(false);
            //if (!newMessageButton.activeSelf) newMessageButton.SetActive(true);
            //if (sendMessageButton.activeSelf) sendMessageButton.SetActive(false);
            //if (newMessageCanvas.activeSelf) newMessageCanvas.SetActive(false);
            //if (!outboxCanvas.activeSelf) outboxCanvas.SetActive(true);
        }

        public void PrintButtonClicked(GameObject button)
        {
            Debug.Log(button.name + " has been clicked.");
        }

        public void PrintToggleButtonToggled(GameObject toggleButton, bool isToggleOn)
        {
            var toggleString = isToggleOn ? "ON" : "OFF";
            Debug.Log(toggleButton.name + " has been toggled " + toggleString + ".");
        }

        public void PrintSliderValueHasChanged(GameObject slider, int newValue)
        {
            Debug.Log(slider.name + " has been updated to " + newValue + ".");
        }

        public void PrintWantedMessages()
        {
            Debug.Log("mpika koumpi");
        }

        public void ChangeUI(GameObject button)
        {
            //Debug.Log("mpika UIIIIII " + button.name);
            if (originalCanvas.activeSelf && !buttonCanvas.activeSelf)
            {
                originalCanvas.SetActive(false);
                buttonCanvas.SetActive(true);
                AnimationReset(buttonCanvas);
                if (buttonCanvas == messageCanvas) {
                    if (newMessageCanvas.activeSelf) newMessageButton.GetComponent<UIPrinter>().OpenNewMessageCanvas();
                }
            }
            //btnTrigger.timeElapsed = 0f;
        }

        public void ChangeToCarUI() {
            if (originalCanvas.activeSelf && !buttonCanvas.activeSelf)
            {
                originalCanvas.SetActive(false);
                buttonCanvas.SetActive(true);
                AnimationReset(buttonCanvas);

                ShowEnginePresets();
            }
        }

        public void ExitUI()
        {
            if (!originalCanvas.activeSelf && buttonCanvas.activeSelf)
            {
                buttonCanvas.SetActive(false);
                originalCanvas.SetActive(true);
                AnimationReset(originalCanvas);

                if (!spRec.keywordRecognizer.IsRunning) spRec.RestartKeywordRecognizer();
                if (detectionAnimation.activeSelf == true) detectionAnimation.SetActive(false);
            }
            //btnTrigger.timeElapsed = 0f;
        }

        public void ExitPhoneUI()
        {
            if (!originalCanvas.activeSelf && buttonCanvas.activeSelf)
            {
                if (contactName.activeSelf == true)
                {

                    InitializeContactAdd();

                }

                buttonCanvas.SetActive(false);
                originalCanvas.SetActive(true);
                AnimationReset(originalCanvas);

                if (!spRec.keywordRecognizer.IsRunning) spRec.RestartKeywordRecognizer();
                if (detectionAnimation.activeSelf == true) detectionAnimation.SetActive(false);
            }
            //btnTrigger.timeElapsed = 0f;
        }

        


    private void AnimationReset(GameObject canvas)
        {

            //var normalColor = null;
            //Debug.Log("mpika reseeeeeeeeeeet");
            var ret = new System.Collections.Generic.List<GameObject>();
            foreach (Button b in canvas.GetComponentsInChildren<Button>())
            {
                //Debug.Log("listaaaa: " + ret.Count);
                //Debug.Log("button layer: " + b.gameObject.layer.ToString());
                if (b.gameObject.layer.Equals(3)) {
                    //Debug.Log("mpika ifffff");
                    ret.Add(b.gameObject);
                }
            }

            //Debug.Log("arithmos koumpion: "+ret.Count);
            Color ogColor = new Color32(255, 255, 255, 200);

            for (int i=0; i<ret.Count; i++) {
                /*var normal = ret[i].GetComponent<Button>().colors;
                normal.normalColor = new Color32(0, 55, 75, 200);
                ret[i].GetComponent<Button>().colors = normal;*/
                ret[i].GetComponent<Button>().GetComponent<Image>().color = ogColor;
            }

        }

        public void PlayClip() {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = this.audioClip;
                audioSource.Play();
                toolBarTrackPlaying.text = trackPlaying.text;
                Debug.Log("paizei: " + toolBarTrackPlaying.text);
            }
            else if (audioSource.isPlaying && audioSource.clip != this.audioClip) {
                audioSource.clip = this.audioClip;
                audioSource.Play();
                toolBarTrackPlaying.text = trackPlaying.text;
                Debug.Log("paizei: " + toolBarTrackPlaying.text);
            }
            if (audioSource.isPlaying) {
                onButton.gameObject.SetActive(false);
                offButton.gameObject.SetActive(true);
            }
            //btnTrigger.timeElapsed = 0f;
        }

        public void VolumeUp()
        {
            Debug.Log("mpika volumeeeeeeeeeeee UPPPPPPPPPPP");
            if (audioSource != null)
            {
                //Debug.Log("Volume prin: " + audioSource.volume);
                if (audioSource.volume < 1 && volumeUp==true) {
                    StartCoroutine(VolumeUpCoroutine(audioSource));
                }

                //Debug.Log("Volume meta: " + audioSource.volume);
            }
            //btnTrigger.timeElapsed = 0f;
        }


        IEnumerator VolumeUpCoroutine(AudioSource radioAudio)
        {

            radioAudio.volume += 0.1f;
            volumeLevel.text = Math.Truncate((radioAudio.volume) * 10).ToString();
            volumeUp = false;
            yield return new WaitForSeconds(1f);
            volumeUp = true;
        }

        public void VolumeDown()
        {
            Debug.Log("mpika volumeeeeeeeeeeee DOOWWWWWNNNNNNNNNNN");
            if (audioSource != null)
            {
                //Debug.Log("Volume prin: " + audioSource.volume);
                if (audioSource.volume > 0 && volumeDown==true) {

                    StartCoroutine(VolumeDownCoroutine(audioSource));
                }

                //Debug.Log("Volume meta: " + audioSource.volume);
            }

            //btnTrigger.timeElapsed = 0f;
        }

        IEnumerator VolumeDownCoroutine(AudioSource radioAudio)
        {
            radioAudio.volume -= 0.1f;
            volumeLevel.text = Math.Truncate((radioAudio.volume) * 10).ToString();
            tempDown = false;
            yield return new WaitForSeconds(1f);
            tempDown = true;
        }

        public void TempUp()
        {
            int temp = Convert.ToInt32(temperature.GetComponent<TextMeshProUGUI>().text);
            if (temp < 28 && tempUp==true)
            {
                StartCoroutine(TempUpCoroutine(temp));
            }

        }


        IEnumerator TempUpCoroutine(Double temp)
        {

            temp += 2;
            // temp = Math.Truncate((temp) * 10);
            temperature.GetComponent<TextMeshProUGUI>().text = temp.ToString();
            Debug.Log("thermokrasia: " + temp.ToString());
            tempUp = false;
            yield return new WaitForSeconds(1f);
            tempUp = true;
        }

        public void TempDown()
        {
            int temp = Convert.ToInt32(temperature.GetComponent<TextMeshProUGUI>().text);
            if (temp > 12 && tempDown==true)
            {
                StartCoroutine(TempDownCoroutine(temp));
            }

        }

        IEnumerator TempDownCoroutine(Double temp)
        {
            temp -= 2;
            //temp = Math.Truncate((temp) * 10);
            temperature.GetComponent<TextMeshProUGUI>().text = temp.ToString();
            Debug.Log("thermokrasia: " + temp.ToString());
            volumeDown = false;
            yield return new WaitForSeconds(1f);
            volumeDown = true;
        }

        /*public void ToggleFunction() {
            if (this.gameObject.GetComponent<Toggle>().isOn) {

                audioSource.clip = audioClip;
                audioSource.Play();
            }
        }*/

        public void StartMusic() {
            /*if (audioSource.clip == null)
            {
                audioSource.clip = GameObject.Find("Button11").GetComponent<AudioClip>();
            }*/
            audioSource.Play();
            if (audioSource.clip == GameObject.Find("Button 11").GetComponent<UIPrinter>().audioClip) {
                toolBarTrackPlaying.text = "FM1";
            }
            else if(audioSource.clip == GameObject.Find("Button 12").GetComponent<UIPrinter>().audioClip) {
                toolBarTrackPlaying.text = "FM2";
            }
            else if (audioSource.clip == GameObject.Find("Button 13").GetComponent<UIPrinter>().audioClip)
            {
                toolBarTrackPlaying.text = "FM3";
            }
            else if (audioSource.clip == GameObject.Find("Button 14").GetComponent<UIPrinter>().audioClip)
            {
                toolBarTrackPlaying.text = "FM4";
            }
            else if (audioSource.clip == GameObject.Find("Button 15").GetComponent<UIPrinter>().audioClip)
            {
                toolBarTrackPlaying.text = "FM5";
            }
            onButton.GetComponent<Button>().GetComponent<Image>().color = afterFocus;
            onButton.gameObject.SetActive(false);
            offButton.gameObject.SetActive(true);

            //btnTrigger.timeElapsed = 0f;
        }

        public void StopMusic() {
            if (audioSource.isPlaying) {
                audioSource.Stop();
            }

            offButton.GetComponent<Button>().GetComponent<Image>().color = afterFocus;
            offButton.gameObject.SetActive(false);
            onButton.gameObject.SetActive(true);

            //btnTrigger.timeElapsed = 0f;
        }

        public void ShowTirePressure() {

            canvasSpritesOn.enginePresetSprite.SetActive(false);
            canvasSpritesOn.tirePressureSprite.SetActive(true);
            canvasSpritesOn.maintenanceSprite.SetActive(false);

            if (detectionAnimation.activeSelf == true) detectionAnimation.SetActive(false);

            if (enginePresetsContent.activeSelf==true) enginePresetsContent.SetActive(false);

            if (maintenanceUI.activeSelf == true) maintenanceUI.SetActive(false);

            if (tirePressureImage.activeSelf==false) tirePressureImage.SetActive(true);

            //btnTrigger.timeElapsed = 0f;

        }

        public void Mute() {
            Debug.Log("MPIKA MUTE");
            if (audioSource.mute)
            {
                audioSource.mute = false;
                unMutedImage.SetActive(true);
                mutedImage.SetActive(false);
            }
            else
            {
                audioSource.mute = true;
                unMutedImage.SetActive(false);
                mutedImage.SetActive(true);
            }
            //audioSource.mute = !audioSource.mute;
        }

        public void ActivatePreset() {

            if (!isPresetActive)
            {
                if (btn23.GetComponent<UIPrinter>().isPresetActive || btn24.GetComponent<UIPrinter>().isPresetActive || btn25.GetComponent<UIPrinter>().isPresetActive) {
                    btn23.GetComponent<UIPrinter>().isPresetActive = false;
                    btn24.GetComponent<UIPrinter>().isPresetActive = false;
                    btn25.GetComponent<UIPrinter>().isPresetActive = false;
                    if (btn23.GetComponent<UIPrinter>().presetText.activeSelf)
                    {
                        btn23.GetComponent<UIPrinter>().presetText.SetActive(false);
                    }
                    else if (btn24.GetComponent<UIPrinter>().presetText.activeSelf) 
                    {
                        btn24.GetComponent<UIPrinter>().presetText.SetActive(false);
                    }
                    else if (btn25.GetComponent<UIPrinter>().presetText.activeSelf)
                    {
                        btn25.GetComponent<UIPrinter>().presetText.SetActive(false);
                    }
                }
                isPresetActive = true;

                //var presetColor = GetComponent<Button>().GetComponent<Image>().color;
                this.presetText.SetActive(true);


                /*var presetColor = GetComponent<Button>().colors;
                presetColor.normalColor = Color.red;
                GetComponent<Button>().colors = presetColor;*/
            }
            activeEnginePreset.text = thisGameObject.GetComponentInChildren<Text>().name;

        }

        public void ShowEnginePresets() {

            canvasSpritesOn.enginePresetSprite.SetActive(true);
            canvasSpritesOn.tirePressureSprite.SetActive(false);
            canvasSpritesOn.maintenanceSprite.SetActive(false);

            if (detectionAnimation.activeSelf == true) detectionAnimation.SetActive(false);

            if (tirePressureImage.activeSelf == true) tirePressureImage.SetActive(false);

            if (maintenanceUI.activeSelf == true) maintenanceUI.SetActive(false);

            if (enginePresetsContent.activeSelf == false) enginePresetsContent.SetActive(true);
        }

        public void MaintenanceUI() {

            canvasSpritesOn.enginePresetSprite.SetActive(false);
            canvasSpritesOn.tirePressureSprite.SetActive(false);
            canvasSpritesOn.maintenanceSprite.SetActive(true);

            if (tirePressureImage.activeSelf == true) tirePressureImage.SetActive(false);

            if (enginePresetsContent.activeSelf == true) enginePresetsContent.SetActive(false);

            if (maintenanceUI.activeSelf == false) maintenanceUI.SetActive(true);

            if (detectionAnimation.activeSelf == true) detectionAnimation.SetActive(false);

            if (detectingText.activeSelf) detectingText.SetActive(false);
            
            problemDetectionIndicator.SetActive(true);
        }


        public void ProblemDetection() {

            /*time = Time.deltaTime;
            maintenanceBoolean = true;*/


            StartCoroutine(ButtonCoroutine());


        }


        IEnumerator ButtonCoroutine() {
            if (detectionAnimation.activeSelf == false) detectionAnimation.SetActive(true);
            if (problemDetectionIndicator == true) problemDetectionIndicator.SetActive(false);
            /*if (maintenanceUI.activeSelf) maintenanceUI.SetActive(false);
            if (!detectingText.activeSelf) detectingText.SetActive(true);*/
            //if (maintenanceUI.activeSelf==true) maintenanceUI.SetActive(false);

            //Debug.Log("prin");

            yield return new WaitForSecondsRealtime(3);

            /*maintenanceBoolean = false;
            time = 0;*/

            /*if (!maintenanceUI.activeSelf) maintenanceUI.SetActive(true);
            if (detectingText.activeSelf) detectingText.SetActive(false);*/
            if (detectionAnimation.activeSelf == true) detectionAnimation.SetActive(false);
            problemDetectionIndicator.SetActive(true);

            //Debug.Log("meta");
            //if (maintenanceUI.activeSelf == false) maintenanceUI.SetActive(true);
        }

        public void OpenContactCanvas() {

            if (recentCallsCanvas.activeSelf == true)
            {
                recentCallsCanvas.SetActive(false);
            }

            if (contactsCanvas.activeSelf == false)
            {

                phoneSprites.contactsSpriteOn.SetActive(true);
                phoneSprites.recentCallsSpriteOn.SetActive(false);
                contactsCanvas.SetActive(true);
                if (!spRec.keywordRecognizer.IsRunning) spRec.RestartKeywordRecognizer();

            }
        
        }

        public void OpenRecentCallsCanvas() {
            //Debug.Log("MPIKA RECENT CALLSSSSSSSSSSS");
            if (recentCallsCanvas.activeSelf == false) {

                if (contactsCanvas.activeSelf == true)
                {
                    if (contactName.activeSelf == true) {

                        InitializeContactAdd();

                    }

                    contactsCanvas.SetActive(false);

                }
                phoneSprites.contactsSpriteOn.SetActive(false);
                phoneSprites.recentCallsSpriteOn.SetActive(true);
                recentCallsCanvas.SetActive(true);
                if (!spRec.keywordRecognizer.IsRunning) spRec.RestartKeywordRecognizer();

            }
        
        }


        public void DeleteContact() {
            //Debug.Log(position1.GetComponent<UIPrinter>().positionsChild);
            if (this.contactPrefab == position1.GetComponent<UIPrinter>().positionsChild && position2.GetComponent<UIPrinter>().positionsChild != null)
            {
                //Debug.Log("MPIKAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                position2.GetComponent<UIPrinter>().positionsChild.transform.SetParent(position1.transform, false);
                position1.GetComponent<UIPrinter>().positionsChild = position2.GetComponent<UIPrinter>().positionsChild;
                position2.GetComponent<UIPrinter>().positionsChild = null;
                if (position3.GetComponent<UIPrinter>().positionsChild != null)
                {
                    position3.GetComponent<UIPrinter>().positionsChild.transform.SetParent(position2.transform, false);
                    position2.GetComponent<UIPrinter>().positionsChild = position3.GetComponent<UIPrinter>().positionsChild;
                    position3.GetComponent<UIPrinter>().positionsChild = null;
                }
                //position1.GetComponent<UIPrinter>().positionsChild.transform.position = position1.transform.position;
            }
            else if (this.contactPrefab == position2.GetComponent<UIPrinter>().positionsChild && position3.GetComponent<UIPrinter>().positionsChild != null) {

                position3.GetComponent<UIPrinter>().positionsChild.transform.SetParent(position2.transform, false);
                position2.GetComponent<UIPrinter>().positionsChild = position3.GetComponent<UIPrinter>().positionsChild;
                position3.GetComponent<UIPrinter>().positionsChild = null;
            }
            if (contactName.activeSelf == true)
            {

                InitializeContactAdd();

            }
            if (spRec.contactList.Contains(this.contactPrefab.GetComponent<ContactsType>().contactName.text)) {
                spRec.contactList.Remove(this.contactPrefab.GetComponent<ContactsType>().contactName.text);
            }
            //Debug.Log(spRec.contactList.Count);
            Destroy(this.contactPrefab);
            this.positionsChild = null;

        }

        public void IncomingPhoneCall(GameObject callName) {
            callingLabelCallCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().callingLabelCallCanvas;
            incomingLabelCallCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().incomingLabelCallCanvas;
            if (callingLabelCallCanvas.activeSelf) callingLabelCallCanvas.SetActive(false);
            if (!incomingLabelCallCanvas.activeSelf) incomingLabelCallCanvas.SetActive(true);
            recentCallPrefab = GameObject.Find("UI").GetComponent<UIPrinter>().recentCallPrefab;
            phoneCallCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().phoneCallCanvas;
            if (phoneCallCanvas.activeSelf == false)
            {
                phoneCallCanvas.SetActive(true);
                phoneCallCanvas.GetComponent<UIPrinter>().phoneCallName.GetComponent<TextMeshProUGUI>().text = callName.GetComponent<TextMeshProUGUI>().text;
                GameObject newCall;
                if (callPosition2.GetComponentInChildren<ContactsType>() != null)
                {
                    if (callPosition3.GetComponentInChildren<ContactsType>() != null)
                    {


                        if (callPosition4.GetComponentInChildren<ContactsType>() != null)
                        {
                            Destroy(callPosition4.GetComponent<UIPrinter>().positionsChild);
                        }
                        callPosition3.GetComponent<UIPrinter>().positionsChild.transform.SetParent(callPosition4.transform, false);
                        callPosition4.GetComponent<UIPrinter>().positionsChild = callPosition3.GetComponent<UIPrinter>().positionsChild;

                    }
                    callPosition2.GetComponent<UIPrinter>().positionsChild.transform.SetParent(callPosition3.transform, false);
                    callPosition3.GetComponent<UIPrinter>().positionsChild = callPosition2.GetComponent<UIPrinter>().positionsChild;
                }

                callPosition1.GetComponent<UIPrinter>().positionsChild.transform.SetParent(callPosition2.transform, false);
                callPosition2.GetComponent<UIPrinter>().positionsChild = callPosition1.GetComponent<UIPrinter>().positionsChild;


                uiComponent.GetComponent<AudioEvents>().audioEvents.clip = uiComponent.GetComponent<AudioEvents>().incomingCallAudio;
                uiComponent.GetComponent<AudioEvents>().audioEvents.Play();

                newCall = Instantiate(recentCallPrefab, callPosition1.transform, worldPositionStays: false);
                //newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().recentCallsCanvas = GameObject.Find("Canvas - Recent Calls");
                //newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().contactsCanvas = GameObject.Find("Canvas - Contacts");
                newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().recentCallsCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().recentCallsCanvas;
                newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().contactsCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().contactsCanvas;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().contactsCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().contactsCanvas;
                newCall.GetComponent<ContactsType>().contactName.text = callName.GetComponent<TextMeshProUGUI>().text;

                newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().callPosition1 = GameObject.Find("UI").GetComponent<UIPrinter>().callPosition1;
                newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().callPosition2 = GameObject.Find("UI").GetComponent<UIPrinter>().callPosition2;
                newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().callPosition3 = GameObject.Find("UI").GetComponent<UIPrinter>().callPosition3;
                newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().callPosition4 = GameObject.Find("UI").GetComponent<UIPrinter>().callPosition4;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().chosenReceiver = GameObject.Find("UI").GetComponent<UIPrinter>().chosenReceiver;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().newMessageButton = GameObject.Find("UI").GetComponent<UIPrinter>().newMessageButton;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().phoneCallCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().phoneCallCanvas;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().contactsCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().contactsCanvas;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().messageCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().messageCanvas;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().inboxCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().inboxCanvas;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().outboxCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().outboxCanvas;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().newMessageCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().newMessageCanvas;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().sendMessageButton = GameObject.Find("UI").GetComponent<UIPrinter>().sendMessageButton;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().recentCallsCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().recentCallsCanvas;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().messageTextInput = GameObject.Find("UI").GetComponent<UIPrinter>().messageTextInput;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().phoneCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().phoneCanvas;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().noContactsWarning = GameObject.Find("UI").GetComponent<UIPrinter>().noContactsWarning;
                newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().spRec = GameObject.Find("UI").GetComponent<UIPrinter>().spRec;
                newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().uiComponent = GameObject.Find("UI");
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().spRec = GameObject.Find("UI").GetComponent<UIPrinter>().spRec;
                newCall.GetComponent<ContactsType>().callBtn.GetComponent<UITriggerGazeButton>().uiComp = GameObject.Find("UI");
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UITriggerGazeButton>().uiComp = GameObject.Find("UI");
                
                callPosition1.GetComponent<UIPrinter>().positionsChild = newCall;
            }
        }


        public void MakePhoneCall()
        {
            callingLabelCallCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().callingLabelCallCanvas;
            incomingLabelCallCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().incomingLabelCallCanvas;
            if (!callingLabelCallCanvas.activeSelf) callingLabelCallCanvas.SetActive(true);
            if (incomingLabelCallCanvas.activeSelf) incomingLabelCallCanvas.SetActive(false);

            if (phoneCallCanvas == null) {
                phoneCallCanvas = contactsCanvas.GetComponent<UIPrinter>().phoneCallCanvas;
            }
            //Debug.Log("Mpika phone callllllllllllllllllll");

            if(!spRec.endCallButton.activeSelf)
            {
                spRec.endCallButton.SetActive(true);
                spRec.callName.text = this.GetComponent<ContactsType>().contactName.text;
                if (!spRec.upperCanvasCallingLabel.activeSelf) spRec.upperCanvasCallingLabel.SetActive(true);
            }

            if (phoneCallCanvas.activeSelf == false) {
                //Debug.Log("CALL: "+ spRec.contactList.Count);
                if (contactsCanvas.activeSelf == true) {

                    if (contactName.activeSelf == true)
                    {

                        InitializeContactAdd();

                    }

                }
                uiComponent.GetComponent<AudioEvents>().audioEvents.clip = uiComponent.GetComponent<AudioEvents>().outgoingCallAudio;
                uiComponent.GetComponent<AudioEvents>().audioEvents.Play();
                phoneCallCanvas.SetActive(true);
                phoneCallCanvas.GetComponent<UIPrinter>().phoneCallName.GetComponent<TextMeshProUGUI>().text = this.GetComponent<ContactsType>().contactName.text;
                GameObject newCall;
                if (callPosition2.GetComponentInChildren<ContactsType>() != null) {
                    if (callPosition3.GetComponentInChildren<ContactsType>() != null) {


                        if (callPosition4.GetComponentInChildren<ContactsType>() != null) {
                            Destroy(callPosition4.GetComponent<UIPrinter>().positionsChild);
                        }
                        callPosition3.GetComponent<UIPrinter>().positionsChild.transform.SetParent(callPosition4.transform, false);
                        callPosition4.GetComponent<UIPrinter>().positionsChild = callPosition3.GetComponent<UIPrinter>().positionsChild;

                    }
                    callPosition2.GetComponent<UIPrinter>().positionsChild.transform.SetParent(callPosition3.transform, false);
                    callPosition3.GetComponent<UIPrinter>().positionsChild = callPosition2.GetComponent<UIPrinter>().positionsChild;
                }

                callPosition1.GetComponent<UIPrinter>().positionsChild.transform.SetParent(callPosition2.transform, false);
                callPosition2.GetComponent<UIPrinter>().positionsChild = callPosition1.GetComponent<UIPrinter>().positionsChild;


                newCall = Instantiate(recentCallPrefab, callPosition1.transform, worldPositionStays: false);
                //newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().recentCallsCanvas = GameObject.Find("Canvas - Recent Calls");
                //newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().contactsCanvas = GameObject.Find("Canvas - Contacts");
                newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().recentCallsCanvas = this.GetComponent<UIPrinter>().recentCallsCanvas;
                newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().contactsCanvas = this.GetComponent<UIPrinter>().contactsCanvas;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().contactsCanvas = this.GetComponent<UIPrinter>().contactsCanvas;
                newCall.GetComponent<ContactsType>().contactName.text = this.GetComponent<ContactsType>().contactName.text;

                newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().callPosition1 = GameObject.Find("UI").GetComponent<UIPrinter>().callPosition1;
                newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().callPosition2 = GameObject.Find("UI").GetComponent<UIPrinter>().callPosition2;
                newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().callPosition3 = GameObject.Find("UI").GetComponent<UIPrinter>().callPosition3;
                newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().callPosition4 = GameObject.Find("UI").GetComponent<UIPrinter>().callPosition4;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().chosenReceiver = GameObject.Find("UI").GetComponent<UIPrinter>().chosenReceiver;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().newMessageButton = GameObject.Find("UI").GetComponent<UIPrinter>().newMessageButton;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().phoneCallCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().phoneCallCanvas;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().contactsCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().contactsCanvas;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().messageCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().messageCanvas;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().inboxCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().inboxCanvas;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().outboxCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().outboxCanvas;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().newMessageCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().newMessageCanvas;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().sendMessageButton = GameObject.Find("UI").GetComponent<UIPrinter>().sendMessageButton;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().recentCallsCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().recentCallsCanvas;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().messageTextInput = GameObject.Find("UI").GetComponent<UIPrinter>().messageTextInput;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().phoneCanvas = GameObject.Find("UI").GetComponent<UIPrinter>().phoneCanvas;
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().noContactsWarning = GameObject.Find("UI").GetComponent<UIPrinter>().noContactsWarning;
                newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().spRec = GameObject.Find("UI").GetComponent<UIPrinter>().spRec;
                newCall.GetComponent<ContactsType>().callBtn.GetComponent<UITriggerGazeButton>().uiComp = GameObject.Find("UI");
                newCall.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().uiComponent = GameObject.Find("UI");
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UITriggerGazeButton>().uiComp = GameObject.Find("UI");
                newCall.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().spRec = GameObject.Find("UI").GetComponent<UIPrinter>().spRec;
                //newCall.GetComponent<ContactsType>().delBtn.GetComponent<UITriggerGazeButton>().uiComp = GameObject.Find("UI");
                
                callPosition1.GetComponent<UIPrinter>().positionsChild = newCall;
            }


        }

        public void EndCall() {


            
            if (contactName.activeSelf == true)
                {

                    InitializeContactAdd();

                }
            
            if (spRec.endCallButton.activeSelf) {

                if (barLogic.incomingCall.activeSelf) {
                    barLogic.incomingCall.SetActive(false);
                }
                if (!spRec.keywordRecognizer.IsRunning) spRec.RestartKeywordRecognizer();

                if (spRec.upperCanvasCallingLabel.activeSelf) spRec.upperCanvasCallingLabel.SetActive(false);

                spRec.endCallButton.SetActive(false);
                spRec.callName.text = " ";
            }
            if (uiComponent.GetComponent<AudioEvents>().audioEvents.isPlaying) uiComponent.GetComponent<AudioEvents>().audioEvents.Stop();
            phoneCallCanvas.SetActive(false);
        }

        public void NewContact() {
            addContactButton.SetActive(false);
            contactName.SetActive(true);
            saveContactButton.SetActive(true);

            if (contactName.GetComponent<TMP_InputField>().text.Length != 0) {
                contactName.GetComponent<TMP_InputField>().text = null;
            }

            if (spRec.keywordRecognizer.IsRunning)
            {
                spRec.keywordRecognizer.Stop();
            }
            PhraseRecognitionSystem.Shutdown();

            contactName.GetComponent<UIPrinter>().ContactNameWriter();
            /*foreach (char c in Input.inputString) {
                *//*if (nameInput.text.Length <= 8)
                {
                    nameInput.text += c;
                }
                else { break; }*//*
                if (contactName.GetComponent<TMP_InputField>().text.Length <= 8)
                {
                    contactName.GetComponent<TMP_InputField>().text += c;
                }
                else { break; }

            }*/
        }

        public void ContactNameWriter() {

            StartCoroutine(spRec.StartDictationRecognizer());

            contactName.GetComponent<TMP_InputField>().characterLimit = 8;
            //contactName.GetComponent<UIPrinter>().speechInitializer = false;
            /*contactName.GetComponent<TMP_InputField>().ActivateInputField();
            contactName.GetComponent<TMP_InputField>().Select();*/

            //contactName.GetComponent<TMP_InputField>().text = spRec.dictatedPhrase;
        }

        public void MessageButton() {

            if (contactsCanvas.activeSelf == true)
            {

                if (contactName.activeSelf == true)
                {

                    InitializeContactAdd();

                }

            }

        }

        public void SaveContact() {

            //if (!spRec.keywordRecognizer.IsRunning) spRec.OnDictationComplete(DictationCompletionCause.Complete);
            //Debug.Log("Mpika add contactttttttttttttttttt");
            GameObject newOne;
            if (contactName.GetComponent<TMP_InputField>().text.Length != 0) {
                if (position1.GetComponentInChildren<ContactsType>() == null)
                {
                    newOne = Instantiate(contactPrefab, position1.transform, worldPositionStays: false);
                    NewContactInit(newOne);
                    newOne.GetComponent<ContactsType>().contactName.text = contactName.GetComponent<TMP_InputField>().text;
                    //newOne.transform.parent = contactsCanvasParent.transform;
                    //newOne.GetComponent<UIPrinter>().contactPrefab = this.contactPrefab;

                    //newOne.GetComponent<UIPrinter>().phoneCallCanvas = this.phoneCallCanvas;
                    position1.GetComponent<UIPrinter>().positionsChild = newOne;
                }
                else if (position2.GetComponentInChildren<ContactsType>() == null)
                {
                    newOne = Instantiate(contactPrefab, position2.transform, worldPositionStays: false);
                    //newOne.transform.parent = contactsCanvasParent.transform;

                    NewContactInit(newOne);
                    //newOne.GetComponent<UIPrinter>().contactPrefab = this.contactPrefab;
                    newOne.GetComponent<ContactsType>().contactName.text = contactName.GetComponent<TMP_InputField>().text;
                    //newOne.GetComponent<UIPrinter>().phoneCallCanvas = this.phoneCallCanvas;
                    position2.GetComponent<UIPrinter>().positionsChild = newOne;
                }
                else if (position3.GetComponentInChildren<ContactsType>() == null)
                {
                    newOne = Instantiate(contactPrefab, position3.transform, worldPositionStays: false);
                    //newOne.transform.parent = contactsCanvasParent.transform;
                    //newOne.GetComponent<UIPrinter>().contactPrefab = this.contactPrefab;
                    NewContactInit(newOne);
                    newOne.GetComponent<ContactsType>().contactName.text = contactName.GetComponent<TMP_InputField>().text;
                    //newOne.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().contactsCanvas = GameObject.Find("Sprite Mask - Contacts").GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().contactsCanvas;
                    //newOne.GetComponent<UIPrinter>().phoneCallCanvas = this.phoneCallCanvas;
                    position3.GetComponent<UIPrinter>().positionsChild = newOne;
                }
                spRec.contactList.Add(contactName.GetComponent<TMP_InputField>().text);
                InitializeContactAdd();
                //if (!spRec.keywordRecognizer.IsRunning) spRec.RestartKeywordRecognizer();
                if (spRec.dictationRecognizer.Status.Equals(SpeechSystemStatus.Running)) spRec.OnDictationComplete(DictationCompletionCause.Complete);
                //Debug.Log(spRec.contactList.Count);
                
            }
            //ContactsType newContact = gameObject.AddComponent<ContactsType>();
            
            //newOne.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().randomNumber = 3;
            //newOne.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().phoneCallCanvas = phoneCallCanvas;
            //newOne.GetComponent<UIPrinter>().phoneCallCanvas = this.phoneCallCanvas;

        }

        private void InitializeContactAdd() {

            //contactName.GetComponent<TMP_InputField>().DeactivateInputField();
            contactName.SetActive(false);
            saveContactButton.SetActive(false);
            addContactButton.SetActive(true);

        }

        private void NewContactInit(GameObject newContact) {
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().position1 = GameObject.Find("Canvas - Contacts").GetComponent<UIPrinter>().position1;
            newContact.GetComponent<ContactsType>().delBtn.GetComponent<UIPrinter>().position1 = GameObject.Find("Canvas - Contacts").GetComponent<UIPrinter>().position1;
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().position2 = GameObject.Find("Canvas - Contacts").GetComponent<UIPrinter>().position2;
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().position3 = GameObject.Find("Canvas - Contacts").GetComponent<UIPrinter>().position3;
            newContact.GetComponent<ContactsType>().delBtn.GetComponent<UIPrinter>().position2 = GameObject.Find("Canvas - Contacts").GetComponent<UIPrinter>().position2;
            newContact.GetComponent<ContactsType>().delBtn.GetComponent<UIPrinter>().position3 = GameObject.Find("Canvas - Contacts").GetComponent<UIPrinter>().position3;
            newContact.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().callPosition1 = GameObject.Find("Canvas - Contacts").GetComponent<UIPrinter>().callPosition1;
            newContact.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().callPosition2 = GameObject.Find("Canvas - Contacts").GetComponent<UIPrinter>().callPosition2;
            newContact.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().callPosition3 = GameObject.Find("Canvas - Contacts").GetComponent<UIPrinter>().callPosition3;
            newContact.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().callPosition4 = GameObject.Find("Canvas - Contacts").GetComponent<UIPrinter>().callPosition4;
            newContact.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().phoneCallCanvas = GameObject.Find("Canvas - Contacts").GetComponent<UIPrinter>().phoneCallCanvas;
            newContact.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().recentCallsCanvas = GameObject.Find("Canvas - Contacts").GetComponent<UIPrinter>().recentCallsCanvas;
            //newOne.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().contactsCanvas = GameObject.Find("Sprite Mask - Contacts").GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().contactsCanvas;
            newContact.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().contactsCanvas = GameObject.Find("Canvas - Contacts");
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().contactsCanvas = GameObject.Find("Canvas - Contacts");
            newContact.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().contactName = GameObject.Find("Button 34").GetComponent<UIPrinter>().contactName;
            newContact.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().saveContactButton = GameObject.Find("Button 34");
            newContact.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().addContactButton = GameObject.Find("Button 34").GetComponent<UIPrinter>().addContactButton;
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().contactName = GameObject.Find("Button 34").GetComponent<UIPrinter>().contactName;
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().saveContactButton = GameObject.Find("Button 34");
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().addContactButton = GameObject.Find("Button 34").GetComponent<UIPrinter>().addContactButton;
            newContact.GetComponent<ContactsType>().delBtn.GetComponent<UIPrinter>().contactName = GameObject.Find("Button 34").GetComponent<UIPrinter>().contactName;
            newContact.GetComponent<ContactsType>().delBtn.GetComponent<UIPrinter>().saveContactButton = GameObject.Find("Button 34");
            newContact.GetComponent<ContactsType>().delBtn.GetComponent<UIPrinter>().addContactButton = GameObject.Find("Button 34").GetComponent<UIPrinter>().addContactButton;
            newContact.GetComponent<ContactsType>().delBtn.GetComponent<UIPrinter>().spRec = GameObject.Find("Button 34").GetComponent<UIPrinter>().spRec;
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().chosenReceiver = GameObject.Find("Button 34").GetComponent<UIPrinter>().chosenReceiver;
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().newMessageButton = GameObject.Find("Button 34").GetComponent<UIPrinter>().newMessageButton;
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().phoneCallCanvas = GameObject.Find("Button 34").GetComponent<UIPrinter>().phoneCallCanvas;
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().contactsCanvas = GameObject.Find("Button 34").GetComponent<UIPrinter>().contactsCanvas;
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().messageCanvas = GameObject.Find("Button 34").GetComponent<UIPrinter>().messageCanvas;
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().inboxCanvas = GameObject.Find("Button 34").GetComponent<UIPrinter>().inboxCanvas;
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().outboxCanvas = GameObject.Find("Button 34").GetComponent<UIPrinter>().outboxCanvas;
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().newMessageCanvas = GameObject.Find("Button 34").GetComponent<UIPrinter>().newMessageCanvas;
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().sendMessageButton = GameObject.Find("Button 34").GetComponent<UIPrinter>().sendMessageButton;
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().recentCallsCanvas = GameObject.Find("Button 34").GetComponent<UIPrinter>().recentCallsCanvas;
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().messageTextInput = GameObject.Find("Button 34").GetComponent<UIPrinter>().messageTextInput;
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().phoneCanvas = GameObject.Find("Button 34").GetComponent<UIPrinter>().phoneCanvas;
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().noContactsWarning = GameObject.Find("Button 34").GetComponent<UIPrinter>().noContactsWarning;
            newContact.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().spRec = GameObject.Find("Canvas - Contacts").GetComponent<UIPrinter>().spRec;
            newContact.GetComponent<ContactsType>().callBtn.GetComponent<UIPrinter>().uiComponent = GameObject.Find("UI");
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UIPrinter>().spRec = GameObject.Find("Canvas - Contacts").GetComponent<UIPrinter>().spRec;
            newContact.GetComponent<ContactsType>().msgBtn.GetComponent<UITriggerGazeButton>().uiComp = GameObject.Find("Button 34").GetComponent<UITriggerGazeButton>().uiComp;
            newContact.GetComponent<ContactsType>().delBtn.GetComponent<UITriggerGazeButton>().uiComp = GameObject.Find("Button 34").GetComponent<UITriggerGazeButton>().uiComp;
            newContact.GetComponent<ContactsType>().callBtn.GetComponent<UITriggerGazeButton>().uiComp = GameObject.Find("Button 34").GetComponent<UITriggerGazeButton>().uiComp;


        }

    }
}
