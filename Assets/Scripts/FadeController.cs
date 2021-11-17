using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{

    Animator transition;
    public float transitionTime;

    private void Awake()
    {
        transition = GetComponent<Animator>();
    }
    public void LoadNextLevel()
    {
        StartCoroutine(loadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator loadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    public void LoadSpecificScene(int scene)
    {
        StartCoroutine(loadLevel(scene));
    }
}
