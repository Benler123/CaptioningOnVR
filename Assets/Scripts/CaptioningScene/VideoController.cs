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

        videoPlayer.loopPointReached += CheckOver;

         StartCoroutine(PauseOnLoad());
    }

    IEnumerator PauseOnLoad()
    {
        Time.timeScale = 0;  // This pauses the scene
        yield return new WaitForSecondsRealtime(2);  // This waits for 2 seconds
        Time.timeScale = 1;  // This resumes the scene
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

    void CheckOver(VideoPlayer vp)
    {
        GameManager.Instance.SetState(GameManager.GameStates.PreScene);
        Debug.Log("Video is over");
    }
}