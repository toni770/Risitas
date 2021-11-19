using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nube : MonoBehaviour
{
	[SerializeField]
	private float speed = 5;

	[SerializeField]
	private float lifeTime = 3;

	public bool isLifeTime = false;
	float lifeCOunt;

	public Vector2 dir = Vector2.left;

    private void Awake()
    {
        if(isLifeTime)
        {
			lifeCOunt = Time.time + lifeTime;
        }
    }
    private void Update()
	{
		transform.Translate(dir * speed * Time.deltaTime);

		if(Time.time >= lifeCOunt && isLifeTime)
        {
			NubeSpawn.Instance.DestroyObj(gameObject);
			Destroy(gameObject);
		}
	}

	void OnBecameInvisible()
	{
		if(!isLifeTime)
			Destroy(gameObject);
	}


}
