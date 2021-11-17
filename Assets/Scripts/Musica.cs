using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musica : MonoBehaviour
{

    GameObject[] obj;
    void Awake()
    {
        obj = GameObject.FindGameObjectsWithTag("music");

        if (obj.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
