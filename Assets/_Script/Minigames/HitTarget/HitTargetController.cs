using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTargetController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float speedDifficultyMultiplier = 1.25f;

    [SerializeField]
    private float hitRadius = 1;

    [Space]

    [SerializeField]
    private Transform leftPosition;
    [SerializeField]
    private Transform rightPosition;

    [Space]

    [SerializeField]
    private Transform crosshair;

    private List<Transform> targets = new List<Transform>();
    private List<int> targetsSigns = new List<int>();
    
    [Header("Read-only")]

    [SerializeField]
    private float currentSpeed;

    public bool IsInitialized { get; private set; }
    public bool HasTargets => targets.Count > 0;

    private void Update()
    {
        if (IsInitialized)
        {
            for (int i = targets.Count - 1; i >= 0; i--)
            {
                targets[i].position += Vector3.right * targetsSigns[i] * currentSpeed * Time.deltaTime;

                bool removeTarget;

                if (targetsSigns[i] > 0)
                {
                    removeTarget = targets[i].position.x > rightPosition.position.x;
                }
                else
                {
                    removeTarget = targets[i].position.x < leftPosition.position.x;
                }

                if (removeTarget)
                {
                    targets.RemoveAt(i);
                    targetsSigns.RemoveAt(i);
                }
            }
        }
    }

    public void Initialize(float difficulty)
    {
        if (difficulty <= 1)
        {
            currentSpeed = speed;
        }
        else
        {
            currentSpeed = (difficulty - 1) * speedDifficultyMultiplier * speed;
        }

        IsInitialized = true;
    }

    public void AddTarget(Transform target, int sign)
    {
        target.position = new Vector3(sign > 0 ? leftPosition.position.x : rightPosition.position.x, crosshair.position.y, 0);

        target.localRotation = Quaternion.Euler(0, sign > 0 ? 0 : 180, 0);

        targets.Add(target);
        targetsSigns.Add(sign);
    }

    public bool CheckHit()
    {
        if (!IsInitialized)
        {
            return false;
        }

        int hits = 0;

        for (int i = 0; i < targets.Count; i++)
        {
            if(Vector3.Distance(crosshair.position, targets[i].position) < hitRadius)
            {
                hits++;
            }
        }

        return hits > 0;
    }
}
