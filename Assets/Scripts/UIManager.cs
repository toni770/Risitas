using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

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
	
	[SerializeField] private TMP_InputField nameInput;
	[SerializeField] private TMP_InputField surnameInput;
	[SerializeField] private TMP_InputField mailInput;

	[SerializeField] private Button sendButton;

	[SerializeField] private GameObject logo;

	private EmailSender emailSender;


	bool form = true;

	private void Awake()
	{
		emailSender = GetComponent<EmailSender>();
		musica = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
		musicAnim = musica.gameObject.GetComponent<Animator>();

		form =  PlayerPrefs.GetInt("form", 0) == 0;

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
					logo.SetActive(false);
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
		//emailSender.SendMail("Magical Smile - Nuevo usuario", CalcSubject());
		PlayerPrefs.SetInt("form", 1);
		formPanel.SetActive(false);
		logo.SetActive(true);
    }

	private string CalcSubject()
    {
		return "Nombre: " + nameInput.text + "\n" +
				"Apellido: " + surnameInput.text + "\n" +
				"E-mail: " + mailInput.text + "\n";
    }

	public void CheckForm()
    {
		sendButton.interactable = nameInput.text != "" && surnameInput.text != "" && mailInput.text != "";
    }

}
