using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

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

	private bool sendingMail = false;
	
	[SerializeField] private TMP_InputField[] field;

	[SerializeField] private Button sendButton;

	[SerializeField] private GameObject logo;

	[SerializeField] private GameObject backGroundImage;

	[SerializeField] private GameObject loadingIcon;

	[SerializeField] private float mailTime = 5;

	[SerializeField] private Toggle toggle;

	[SerializeField] private GameObject termsForm;
	private float mailCount = 0;

	private EmailSender emailSender;
	private SendToGoogle sendToGoogle;

	public TextMeshProUGUI errorText;

	int count = 0;

	bool sceneLoad = false;


	bool form = true;

	private void Awake()
	{
		emailSender = GetComponent<EmailSender>();
		sendToGoogle = GetComponent<SendToGoogle>();

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
		if (pressed && !sceneLoad)
        {
			if (Time.time >= videoCount && !sceneLoad)
			{
				fadeImage.color = Color.white;
				sceneLoad = true;
				fade.LoadNextLevel();
				
			}
				
		}
		else
        {
			if (Time.time >= splashCount && form)
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

		if(sendingMail)
		{
			if(Time.time >= mailCount)
			{
				EndSendMail();
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

	}

	public void Send()
    {
		sendingMail = true;
		mailCount = Time.time + mailTime;
		sendButton.gameObject.SetActive(false);
		loadingIcon.SetActive(true);
		StartCoroutine(SendMail());
		
    }

	IEnumerator SendMail()
    {
		yield return new WaitForSeconds(0.2f);

		sendToGoogle.Send(new UserData(field[0].text, field[1].text, field[2].text));
		emailSender.SendMail("Magical Smile - Nuevo usuario", CalcSubject());
		EndSendMail();
	}

	public void EndSendMail()
	{
		PlayerPrefs.SetInt("form", 1);
		formPanel.SetActive(false);
		logo.SetActive(true);
		sendingMail = false;
	}

	private string CalcSubject()
    {
		return "Nombre: " + field[0].text + "\n" +
				"Apellido: " + field[1].text + "\n" +
				"E-mail: " + field[2].text + "\n";
    }

	public void CheckForm()
    {
		sendButton.interactable = CanSend();
    }

	private bool CanSend()
	{
		bool result = true;
		for(int i=0;i<field.Length;i++)
		{
			if(field[i].text == "") result = false;
		}

		if(!toggle.isOn) result = false;

		return result;
	}

	public void OpenTerms(bool open)
	{
		termsForm.SetActive(open);
	}

}
