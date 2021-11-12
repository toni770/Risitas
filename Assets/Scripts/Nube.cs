using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nube : MonoBehaviour
{
	[SerializeField]
	private float speed = 5;

	public Vector2 dir = Vector2.left;

	private void Update()
	{
		transform.Translate(dir * speed * Time.deltaTime);
	}

	void OnBecameInvisible()
	{
		Destroy(gameObject);
	}


}
