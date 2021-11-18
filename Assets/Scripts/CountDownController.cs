using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{

    public int countDownTime;
    Text countDownDisplay;
    Animator anim;

    AudioSource aud;
    public AudioClip goSound;

    private void Awake()
    {
        countDownDisplay = GetComponent<Text>();
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        StartCoroutine(CountDownStart());
    }
    IEnumerator CountDownStart()
    {
        while(countDownTime > 0)
        {
            anim.SetTrigger("change");
            countDownDisplay.text = countDownTime.ToString();
            aud.Play();
            yield return new WaitForSeconds(1f);

            countDownTime--;
        }
        aud.clip = goSound;
        aud.Play();
        anim.SetTrigger("change");
        countDownDisplay.text = "GO!";


        GameManager.Instance.StartPlay();
        yield return new WaitForSeconds(1f);

        countDownDisplay.gameObject.SetActive(false);
    }
}
