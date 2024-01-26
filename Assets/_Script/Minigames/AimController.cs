using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;

    [Header("Read-only")]

    [SerializeField]
    float value = 0f;
    public float Value => value;

    [SerializeField]
    float normalizedValue = 0f;
    public float NormalizedValue => normalizedValue;

    float angle = 0;

    void Update()
    {
        value = Mathf.Sin((angle * Mathf.PI) / 180);
        normalizedValue = Mathf.Lerp(0, 1, (value + 1) / 2f);

        angle += speed * Time.deltaTime;
    }
}
