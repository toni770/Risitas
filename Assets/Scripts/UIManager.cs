using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
	public GameObject playButton;
	public VideoPlayer video;
	public RawImage videoObject;
	public float videoDuration = 21;
	public FadeController fade;
	AudioSource musica;
	Animator musicAnim;

	private bool pressed = false;
	private float videoCount;

	private void Awake()
	{
		musica = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
		musicAnim = musica.gameObject.GetComponent<Animator>();

		
		musicAnim.SetTrigger("End");
		musica.Stop();
		video.Pause();
		
	}
	private void Update()
	{
		if (pressed)
			if (Time.time >= videoCount)
				fade.LoadNextLevel();

	}
	public void Play()
	{
		if(!pressed)
		{
			playButton.SetActive(false);
			videoObject.enabled = true;
			video.Play();
			pressed = true;
			videoCount = Time.time + videoDuration;
			musica.Play();
		}

		//SceneManager.LoadScene("VideoScene");
	}

}
