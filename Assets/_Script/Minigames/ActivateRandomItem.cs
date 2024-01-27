using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRandomItem : MonoBehaviour
{
    [SerializeField]
    private GameObject[] items;

    void Start()
    {
        items[Random.Range(0, items.Length)].SetActive(true);
    }

}
