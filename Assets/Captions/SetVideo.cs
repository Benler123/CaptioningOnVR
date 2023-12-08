using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Networking;
using System.IO;


public class SetVideo : MonoBehaviour
{
    // Start is called before the first frame update
    public Parameters Params;
    public VideoPlayer videoPlayer;
    void Start()
    {
        string videoName = string.Format("Videos/main.{0}.mp4", Params.video);
        if (Application.platform == RuntimePlatform.Android) 
        {
            string url = Path.Combine(Application.streamingAssetsPath, videoName);
            StartCoroutine(GetRequest(url));
        } else {
            videoPlayer = GetComponent<VideoPlayer>();
            videoPlayer.url = Path.Combine(Application.dataPath + "/StreamingAssets/", videoName);
            videoPlayer.Prepare();

            // Play the video.
            videoPlayer.Play();
        }
    }
    IEnumerator GetRequest(string url)
    {
     
        using UnityWebRequest webRequest = UnityWebRequest.Get(url);
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();
            videoPlayer.url = www.url;
            videoPlayer.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
