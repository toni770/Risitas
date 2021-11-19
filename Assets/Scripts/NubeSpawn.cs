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
	public GameObject estrella;
	GameObject obj;

	List<GameObject> itemsSpawned;

	GameObject itemChosed;

	bool left = false;

	private static NubeSpawn _instance;
	public static NubeSpawn Instance
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
		spawnCount = 0;
		itemChosed = nube;
		itemsSpawned = new List<GameObject>();
	}

	private void Update()
	{
		if (Time.time >= spawnCount)
		{
			Spawn();
			spawnCount = Time.time + spawnTime;
		}
	}

	public void ChangeTime(bool noche)
    {
		if (noche) itemChosed = estrella;
		else itemChosed = nube;

		for(int i=0;i<itemsSpawned.Count;i++)
        {
			if(itemsSpawned[i]!=null)
				itemsSpawned[i].GetComponent<Animator>().SetTrigger("fade");
        }
	}

	void Spawn()
	{
		if(left)
		{
			obj = Instantiate(itemChosed, new Vector3(leftSpawn.position.x, Random.Range(minY, maxY), 0), Quaternion.identity);
			obj.GetComponent<Nube>().dir = Vector3.right;
			
		}
		else
		{
			obj = Instantiate(itemChosed, new Vector3(rightSpawn.position.x, Random.Range(minY, maxY), 0), Quaternion.identity);
		}
		itemsSpawned.Add(obj);

		left = !left;
	}

	public void DestroyObj(GameObject obj)
    {
		itemsSpawned.Remove(obj);
    }
}
