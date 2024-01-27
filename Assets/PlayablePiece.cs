using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayablePiece : MonoBehaviour
{
    [SerializeField] private float interval = 1;
    [SerializeField] private float nextTime = 0;
    [SerializeField] private int factSpeed = 2;
    private bool speedUp=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTime)
        {
            if (!speedUp)
            {
                transform.position = transform.position + new Vector3(0f, -0.25f, 0f);
            }
            else
            {
                transform.position = transform.position + new Vector3(0f, -0.25f * factSpeed, 0f);
            }
            nextTime += interval;
        }
    }
}
