using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public GameObject playButton;
	public VideoPlayer video;
	public RawImage videoObject;

	private bool pressed = false;

	private void Awake()
	{
		video.Pause();
	}
	private void Update()
	{
		if (pressed)
			if (!video.isPlaying)
				LoadGame();

	}
	public void Play()
	{
		if(!pressed)
		{
			playButton.SetActive(false);
			videoObject.enabled = true;
			video.Play();
			pressed = true;
		}

		//SceneManager.LoadScene("VideoScene");
	}

	public void LoadGame()
	{
		SceneManager.LoadScene("MainScene");
	}
}
