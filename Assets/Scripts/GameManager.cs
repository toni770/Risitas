using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;


	public GameObject itemsNoche;
	public GameObject itemsDia;

	public Transform tree;

	[HideInInspector]
	public bool playing = false;

	[SerializeField]
	private Camera cam;

	[SerializeField]
	private float introTime = 2;

	[SerializeField]
	public int itemCount = 6;

	[SerializeField]
	PataGAto pataDerecha;
	[SerializeField]
	PataGAto pataIzquierda;

	[SerializeField]
	private Animator nocheAnim;
	[SerializeField]
	private Animator arbolDia_anim;

	public CountDownController countDown;

	public Animator animLogo;

	public bool noche = false;
	int itemFalled = 0;

	Animator musicAnim;

	GameObject obj;

	bool start = false;

	private float introCount = 0;
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
				Debug.LogError("GameManager is NULL");

			return _instance;
		}
	}

	private void Awake()
	{
		_instance = this;
		introCount = Time.time + introTime;

		if((obj = GameObject.FindGameObjectWithTag("music")) != null)
        {
			musicAnim = obj.GetComponent<Animator>();
        }

		if (musicAnim != null) musicAnim.SetTrigger("Play");

	}

    private void Update()
    {
		if (Time.time >= introCount && !start)
		{
			countDown.StartGame();
			start = true;
		}
    }

    public void StartPlay()
    {
		playing = true;
    }

	public void ItemFell()
	{
		itemFalled++;
		if(itemFalled >= itemCount)
		{
			noche = !noche;
			NubeSpawn.Instance.ChangeTime(noche);

			StartCoroutine(cambiarEstado());

			itemFalled = 0;
		}
	}

	IEnumerator cambiarEstado()
    {
		nocheAnim.SetBool("noche", noche);
		animLogo.SetBool("night", noche);
		arbolDia_anim.SetBool("noche", noche);
		yield return new WaitForSeconds(1);

		if (noche)
			Instantiate(itemsNoche, tree);
		else
			Instantiate(itemsDia, tree);
	}
	public void Tocar(Vector3 pos)
	{
		if (IsRight(pos))
			pataDerecha.Touch(pos);
		else
			pataIzquierda.Touch(pos);
	}

	public bool IsRight(Vector3 pos)
	{
		return cam.WorldToScreenPoint(pos).x > Screen.width / 2;
	}
}
