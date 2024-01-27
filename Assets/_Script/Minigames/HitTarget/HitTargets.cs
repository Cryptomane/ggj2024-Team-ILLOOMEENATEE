using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTargets : MonoBehaviour
{
    [SerializeField]
    private Transform targetTemplate;

    [Space]

    [SerializeField]
    private Transform minPosition;
    [SerializeField]
    private Transform maxPosition;

    [SerializeField]
    private Transform[] targets;

    public void InitTargets(int count)
    {
        for(int i = 0; i < count; i++)
        {
            targets[i] = Instantiate(targetTemplate.gameObject, targetTemplate.transform).transform;
        }

        ResetTargets();
    }

    public void ResetTargets()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].position = new Vector3(Random.Range(minPosition.position.x, maxPosition.position.x), 0, 0);
        }
    }
}
