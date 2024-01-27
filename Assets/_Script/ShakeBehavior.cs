using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehavior : MonoBehaviour
{
    // Transform of the GameObject you want to shake
    public Transform originalTransform;

    // Desired duration of the shake effect
    public float shakeDuration = 0f;

    // A measure of magnitude for the shake
    public float shakeMagnitude = 0.7f;

    // Shake damping speed 
    public float dampingSpeed = 1.0f;

    // Start time 
    public float startTime;
    public float timeout = 2.0f;

    // The initial position of the GameObject
    private Vector3 initialPosition;

    public ShakeBehavior(float shakeDuration)
    {
        this.shakeDuration = shakeDuration;
    }

    void Awake()
    {
        if (originalTransform == null)
        {
            originalTransform = this.GetComponent<Transform>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = originalTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            originalTransform.localPosition = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            originalTransform.localPosition = initialPosition;
        }
    }

    public void TriggerShake()
    {
        startTime=Time.time;
        shakeDuration = Mathf.Lerp( .5f, timeout,(Time.time - startTime)/60f);
    }
}