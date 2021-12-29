using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using System.Collections;

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

	[SerializeField] private GameObject backGroundImage;

	[SerializeField] private GameObject loadingIcon;

	private EmailSender emailSender;
	private SendToGoogle sendToGoogle;


	bool form = true;

	private void Awake()
	{
		emailSender = GetComponent<EmailSender>();
		sendToGoogle = GetComponent<SendToGoogle>();

		musica = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
		musicAnim = musica.gameObject.GetComponent<Animator>();

		form = true;// PlayerPrefs.GetInt("form", 0) == 0;

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
			backGroundImage.SetActive(false);
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
		sendButton.gameObject.SetActive(false);
		loadingIcon.SetActive(true);
		StartCoroutine(SendMail());
		
    }

	IEnumerator SendMail()
    {
		yield return new WaitForSeconds(0.2f);

		emailSender.SendMail("Magical Smile - Nuevo usuario", CalcSubject());
		sendToGoogle.Send(new UserData(nameInput.text, surnameInput.text, mailInput.text));

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
