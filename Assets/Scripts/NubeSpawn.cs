using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NubeSpawn : MonoBehaviour
{
	public Transform leftSpawn, rightSpawn;

	public float minY= -4, maxY = 4;
	public float spawnTime = 5;

	private float spawnCount;

	public GameObject nube;
	GameObject obj;

	bool left = false;

	private void Awake()
	{
		spawnCount = 0;
	}

	private void Update()
	{
		if (Time.time >= spawnCount)
		{
			Spawn();
			spawnCount = Time.time + spawnTime;
		}
	}

	void Spawn()
	{
		if(left)
		{
			obj = Instantiate(nube, new Vector3(leftSpawn.position.x, Random.Range(minY, maxY), 0), Quaternion.identity);
			obj.GetComponent<Nube>().dir = Vector3.right;
		}
		else
		{
			obj = Instantiate(nube, new Vector3(rightSpawn.position.x, Random.Range(minY, maxY), 0), Quaternion.identity);
		}

		left = !left;
	}
}
