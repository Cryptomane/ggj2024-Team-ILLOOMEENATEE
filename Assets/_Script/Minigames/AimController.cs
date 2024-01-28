using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;

    [SerializeField]
    private float rightValueDelta = 0.2f;

    [Header("Read-only")]

    [SerializeField]
    private float currentSpeed;

    [SerializeField]
    float currentValue = 0f;

    public float Value => currentValue;

    [SerializeField]
    float currentNormalizedValue = 0f;
    public float CurrentNormalizedValue => currentNormalizedValue;

    [SerializeField]
    private float currentRightValueDelta;

    [SerializeField]
    private Vector2 rightValueRange;

    float angle = 0;

    public float TargetValue { get; private set; }

    public bool IsInitialized { get; private set; }

    void Update()
    {
        if (IsInitialized)
        {
            currentValue = Mathf.Sin((angle * Mathf.PI) / 180);
            currentNormalizedValue = Mathf.Lerp(0, 1, (currentValue + 1) / 2f);

            angle += currentSpeed * Time.deltaTime;
        }
    }

    public void Initialize(float difficulty)
    {
        TargetValue = Random.Range(0.5f, 0.8f);

        if (difficulty <= 1)
        {
            currentSpeed = speed;
            currentRightValueDelta = rightValueDelta;
        }
        else
        {
            currentSpeed = difficulty * speed;
            currentRightValueDelta = rightValueDelta / difficulty;
        }        

        float halvedDelta = currentRightValueDelta / 2;
        
        rightValueRange = new Vector2(TargetValue - halvedDelta, TargetValue + halvedDelta);

        if (rightValueRange.x < 0)
        {
            rightValueRange.y -= rightValueRange.x;
            rightValueRange.x = 0;
        }
        else if (rightValueRange.y > 1)
        {
            rightValueRange.x -= (rightValueRange.y - 1);
            rightValueRange.y = 1;
        }

        IsInitialized = true;
    }

    public bool CheckHit()
    {
        if (!IsInitialized)
        {
            return false;
        }

        return CurrentNormalizedValue >= rightValueRange.x && CurrentNormalizedValue <= rightValueRange.y;
    }

    public bool IsHigher()
    {
        if (!IsInitialized)
        {
            return false;
        }

        return CurrentNormalizedValue > TargetValue;
    }
}
