using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PataGAto : MonoBehaviour
{

	[SerializeField]
	private float speed = 5;

	[HideInInspector]
	public Vector3 pos;

	private bool touched = false;
	private Vector3 originalPos;

	private void Awake()
	{
		originalPos = transform.position;
	}

	private void Update()
	{
		if(pos != Vector3.zero)
		{
			if(!touched)
			{
				transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
				if (transform.position == pos)
					touched = true;
				print("A tocar");
			}
			else
			{
				transform.position = Vector3.MoveTowards(transform.position, originalPos, Time.deltaTime * speed);
				//transform.position = Vector3.Lerp(transform.position, originalPos, speed);
				print("Me vuelvo");
			}
				
		}
	}

	public void Touch(Vector3 position)
	{
		pos = position;
		touched = false;
	}


	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("item"))
		{
			touched = true;
			print("Hey");
		}
	}
}
