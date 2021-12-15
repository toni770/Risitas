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
	public VideoPlayer splash;
	public Image fadeImage;

	private bool pressed = false;
	private float videoCount;

	public Animator splashAnim;

	float splashCount;
	public float splashDuration;
	public GameObject formPanel;

	bool form = true;

	private void Awake()
	{
		musica = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
		musicAnim = musica.gameObject.GetComponent<Animator>();

		form = PlayerPrefs.GetInt("form", 0) == 0;

		if(PlayerPrefs.GetInt("jugado",0) == 1)
        {
			splash.gameObject.SetActive(false);
			fadeImage.color = Color.white;
        }
		else
        {
			splashCount = Time.time + splashDuration;
		}
		musicAnim.SetTrigger("End");
		musica.Stop();
		video.Pause();

		PlayerPrefs.SetInt("jugado", 0);


	}
	private void Update()
	{
		if (pressed)
        {
			fadeImage.color = Color.white;
			if (Time.time >= videoCount)
				fade.LoadNextLevel();
		}
		else
        {
			if (Time.time >= splashCount)
            {
				splashAnim.SetTrigger("fade");
                if (form)
                {
					formPanel.SetActive(true);
					form = false;
                }
			}

		}
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

	public void Send()
    {
		PlayerPrefs.SetInt("form", 1);
		formPanel.SetActive(false);
    }

}
