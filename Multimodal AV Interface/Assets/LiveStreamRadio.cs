using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(AudioSource))]
public class LiveStreamRadio : MonoBehaviour
{
    //public string streamUrl = "https://ec3.yesstreaming.net:3585/stream";
    //public string streamUrl = "https://stream-redirect.bauermedia.fi/classic/classic_128.mp3";
    public string streamUrl = "https://loveradio.live24.gr/loveradio-1000?listenerid=7333e733b343781d9ba088fd079192dd&awparams=companionAds%3Atrue";
    private AudioSource audioSource;
    private bool isPlaying = false;
    private const float bufferDuration = 5f; // Duration to buffer
    //private float interval = 3f;

    //private bool played = false;
    //private bool play = false;
    private AudioClip audioClip;
    //private WWW www;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StreamAudio());
    }

    IEnumerator StreamAudio()
    {
        while (true)
        {
            if (!isPlaying)
            {
                Debug.Log("R1");
                using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(streamUrl, AudioType.MPEG))
                {
                    www.SendWebRequest();
                    Debug.Log("Request started.");
                    //float requestTime = 0f;

                    while (!www.isDone)
                    {
                        Debug.Log("R2");
                        //requestTime += Time.deltaTime;
                        yield return null;
                    }

                    if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                    {
                        Debug.LogError("Error: " + www.error);
                        yield return new WaitForSeconds(5f); // Retry after a delay
                    }
                    else
                    {
                        audioClip = DownloadHandlerAudioClip.GetContent(www);
                        Debug.Log("R3");
                        while (audioClip.loadState != AudioDataLoadState.Loaded)
                        {
                            yield return null;
                        }
                        //AudioClip myClip = DownloadHandlerAudioClip.GetContent(www);
                        audioSource.clip = audioClip;
                        audioSource.Play();
                        isPlaying = true;
                    }
                }
            }
            if (!audioSource.isPlaying && isPlaying)
            {
                isPlaying = false;
            }

            yield return null;
        }
    }

    /*IEnumerator Stream()
    {
        //Debug.Log(timer + " INT : " + interval);
        float timer = 0;
        timer += 100 * Time.deltaTime;

        if (timer >= interval)
        {             //if(timer%interval == 0){
            if (www != null)
            {
                www.Dispose();
                www = null;
                played = false;
                timer = 0;
            }
        }

        if (www == null)
        {
            //PLOG("www is empty. Going to initialize www.");
            //www = new WWW(url);
            www = new WWW("http://dromos898.live24.gr/dromos898");
            //PLOG("Downloading...");
            //wait for the download to build up a buffer
            while (www.progress < 0.001f)
                yield return null;
            //PLOG("We got www. Lets proceed.");
        }
        clip = www.GetAudioClip(false, true, AudioType.MPEG);
        yield return clip;

        if (clip != null)
        {
            //PLOG("Clip is not null. Trying to play clip");
            if (clip.loadState == AudioDataLoadState.Loaded && !played)
            {
                //PLOG("Clip loaded. Going to play?");
                if (!audioSource.isPlaying)
                {
                    //PLOG("We are not playing. So lets move on...");
                    audioSource.clip = clip;
                    audioSource.Play();
                    played = true;
                    //clip = new AudioClip;

                }
            }
            play = true;
        }
    }*/

    void Update()
    {
        if (!audioSource.isPlaying && isPlaying)
        {
            Debug.Log("R4");
            isPlaying = false; // Handle stream interruptions
        }
        /*if (play)
        {
            StartCoroutine(Stream());
        }*/
    }
}
