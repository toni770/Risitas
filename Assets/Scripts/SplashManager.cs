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

    public Animator splashAnim;

    public float transistionTime = 2;


    float videoCount;

    private void Awake()
    {
        videoCount = Time.time + videoDuration;
        //video.Play();
    }
    private void Update()
    {
        if(Time.time >= videoCount)
        {
            StartCoroutine(loadMenu());
        }
            
    }

    IEnumerator loadMenu()
    {
        splashAnim.SetTrigger("fade");

        yield return new WaitForSeconds(transistionTime);

        SceneManager.LoadScene("MainMenu");
    }
}
