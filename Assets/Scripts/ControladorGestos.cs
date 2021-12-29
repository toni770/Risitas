using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorGestos : MonoBehaviour
{
    [SerializeField] private float tiempoEntreGestos = 5;

    private List<Interactuable> items;

    private float countGestos;

    int rand;

    private void Awake()
    {
        items = new List<Interactuable>();

        for (int i = 0; i < transform.childCount; i++)
        {
            items.Add(transform.GetChild(i).GetComponent<Interactuable>());
        }

        countGestos = Time.time + tiempoEntreGestos;
    }

    private void Update()
    {
        if(Time.time >= countGestos)
        {
            if (items.Count > 0)
            {
                HacerGesto();
            }
        }
    }

    private void HacerGesto()
    {
        rand = Random.Range(0, items.Count);

        items[rand].HacerGesto();

        countGestos = Time.time + tiempoEntreGestos;
    }

    public void DeleteItem(Interactuable item)
    {
        items.Remove(item);
    }

}
