using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{

    public int countDownTime = 0;
    Text countDownDisplay;
    Animator anim;

    AudioSource aud;
    public AudioClip goSound;

    private void Awake()
    {
        countDownDisplay = GetComponent<Text>();
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();


    }

    public void StartGame()
    {
        StartCoroutine(CountDownStart());
    }
    IEnumerator CountDownStart()
    {
        while(countDownTime <=3)
        {
            if(!aud.isPlaying)aud.Play();
            anim.SetTrigger("change");
            countDownDisplay.text = countDownTime.ToString();
            yield return new WaitForSeconds(0.8f);

            countDownTime++;
        }
        anim.SetTrigger("change");
        countDownDisplay.text = "GO!";


        GameManager.Instance.StartPlay();
        yield return new WaitForSeconds(1f);

        countDownDisplay.gameObject.SetActive(false);
    }
}
