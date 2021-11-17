using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{

    public int countDownTime;
    Text countDownDisplay;
    Animator anim;

    private void Awake()
    {
        countDownDisplay = GetComponent<Text>();
        anim = GetComponent<Animator>();
        StartCoroutine(CountDownStart());
    }
    IEnumerator CountDownStart()
    {
        while(countDownTime > 0)
        {
            anim.SetTrigger("change");
            countDownDisplay.text = countDownTime.ToString();

            yield return new WaitForSeconds(1f);

            countDownTime--;
        }
        anim.SetTrigger("change");
        countDownDisplay.text = "GO!";


        GameManager.Instance.StartPlay();
        yield return new WaitForSeconds(1f);

        countDownDisplay.gameObject.SetActive(false);
    }
}
