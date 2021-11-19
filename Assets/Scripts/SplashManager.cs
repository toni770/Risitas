using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SplashManager : MonoBehaviour
{
    public VideoPlayer video;

    public float videoDuration;

    public FadeController fade;


    float videoCount;

    private void Awake()
    {
        videoCount = Time.time + videoDuration;
        //video.Play();
    }
    private void Update()
    {
        if(Time.time >= videoCount)
            fade.LoadNextLevel();
    }
}
