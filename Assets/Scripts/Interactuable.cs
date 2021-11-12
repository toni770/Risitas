using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuable : MonoBehaviour
{
	[SerializeField]
	private int toquesMaximos = 5;
	[SerializeField]
	private ParticleSystem ps;
	[SerializeField]
	private float velocity = 1;

	private int toquesActuales = 0;
	private AudioSource aud;

	[SerializeField]
	private Animator animItem;
	[SerializeField]
	private Animator animSprite;

	bool caido = false;

	void Awake()
    {
		aud = GetComponent<AudioSource>();
    }

	private void Update()
	{
		if(caido)
		{
			transform.Translate(Vector2.down * Time.deltaTime * velocity, Space.World);
		}
	}

	private void OnMouseDown()
	{
		if(GameManager.Instance.playing)
		{
			if (!caido)
			{
				toquesActuales++;

				GameManager.Instance.Tocar(transform.position);

				if (toquesActuales >= toquesMaximos)
				{
					caido = true;
					animItem.SetBool("final", true);
					animSprite.SetBool("final", true);
				}
				aud.Play();
				animItem.SetTrigger("touched");
				animSprite.SetTrigger("touched");

				if (ps != null && !caido)
				{
					var main = ps.main;
					main.startSize = (float)(0.1 + (0.1*toquesActuales));

					ps.Play();
				}
					

			}
		}
		
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("end"))
		{
			GameManager.Instance.ItemFell();
			Destroy(gameObject);
		}
	}

}
