using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Networking;
using System.IO;


public class VideoController : MonoBehaviour
{
    public Parameters Params;
    private VideoPlayer videoPlayer;

    void Start() 
    {    
        videoPlayer = GetComponent<VideoPlayer>();
        string videoName = string.Format("Videos/main.{0}.mp4", Params.video);
        string url = Path.Combine(Application.streamingAssetsPath, videoName);

        videoPlayer.url = url;
        StartCoroutine(PrepareAndPlay());
    }

    IEnumerator PrepareAndPlay()
    {
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared) 
        {
            yield return null;
        }

        videoPlayer.Play();
    }
}