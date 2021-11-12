using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;


	public GameObject items;
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

	int itemFalled = 0;

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
	}

	private void Update()
	{
		if(Time.time>= introCount)
		{
			playing = true;
		}
	}

	public void ItemFell()
	{
		itemFalled++;
		if(itemFalled >= itemCount)
		{
			Instantiate(items, tree);
			itemFalled = 0;
		}
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
