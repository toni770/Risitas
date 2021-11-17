using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuable : MonoBehaviour
{

	[Header("PRUEBAS")]
	[SerializeField]
	private int tipo = 0;

	[SerializeField]
	private int toquesMaximos = 5;
	[SerializeField]
	private ParticleSystem ps;
	[SerializeField]
	private float velocity = 1;

	[SerializeField]
	private Color[] coloresBrillo;

	[SerializeField]
	private float vibrateTime = 0.2f;
	[SerializeField]
	private float fellTime = 0.5f;

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
					StartCoroutine(Fell());
					animItem.SetBool("final", true);
					animSprite.SetBool("final", true);
				}
				aud.Play();
				animItem.SetTrigger("touched");
				animSprite.SetTrigger("touched");

				CrearParticulas();

				StartCoroutine(Vibrar());
			}
		}
	}

	private void CrearParticulas()
    {
		if (ps != null && !caido)
		{
			var main = ps.main;
			/* if (tipo == 0)
				main.startSize = (float)(0.1 + (0.1 * toquesActuales));*/
			//main.startColor = coloresBrillo[toquesActuales - 1];

			ps.Play();
		}
	}

	IEnumerator Vibrar()
    {
		yield return new WaitForSeconds(vibrateTime);
		Handheld.Vibrate();
	}

	IEnumerator Fell()
    {
		yield return new WaitForSeconds(fellTime);
		caido = true;

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
