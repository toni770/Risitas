using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class FadeController : MonoBehaviour
{

    Animator transition;
    public float transitionTime;
    
	public TextMeshProUGUI errorText;

    private void Awake()
    {
        transition = GetComponent<Animator>();
    }
    public void LoadNextLevel()
    {
        try{
            StartCoroutine(loadLevel(1));
        }
        catch(Exception e)
        {
            errorText.text = e.ToString();
        }
        
    }

    IEnumerator loadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadSceneAsync(levelIndex);
    }

    public void LoadSpecificScene(int scene)
    {
        StartCoroutine(loadLevel(scene));
        PlayerPrefs.SetInt("jugado", 1);
    }
}
