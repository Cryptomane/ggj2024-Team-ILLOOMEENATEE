using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTargetMinigame : MonoBehaviour
{
    [SerializeField]
    private Transform targetTemplate;

    [SerializeField]
    private int targetsCount = 10;

    [SerializeField]
    private float minSpawnTime = 0.2f;
    [SerializeField]
    private float maxSpawnTime = 1.5f;

    [Space]

    [SerializeField]
    private HitTargetController controllerA;
    [SerializeField]
    private HitTargetController controllerB;
    [SerializeField]
    private HitTargetController controllerC;

    [Space]

    [SerializeField]
    private FloatValue difficulty;

    private Transform[] targets;
    private int targetIndex;

    private Minigame minigame;

    private void OnEnable()
    {
        InputManager.OnAHit += OnAHit;
        InputManager.OnBHit += OnBHit;
        InputManager.OnCHit += OnCHit;

        MinigameManager.OnGameStartRequested += StartMinigame;
    }

    private void OnDisable()
    {
        InputManager.OnAHit -= OnAHit;

        MinigameManager.OnGameStartRequested -= StartMinigame;

        StopAllCoroutines();
    }

    private void OnAHit()
    {
        CheckHit(controllerA);
    }

    private void OnBHit()
    {
        CheckHit(controllerB);
    }

    private void OnCHit()
    {
        CheckHit(controllerC);
    }

    private void CheckHit(HitTargetController controller)
    {
        if (controller.CheckHit())
        {
            minigame.AddToScore(1);
        }
    }

    private void StartMinigame(Minigame minigame)
    {
        this.minigame = minigame;

        InitTargets();

        InitControllers();

        StartCoroutine(SpawnTargets());
    }

    private void InitTargets()
    {
        targets = new Transform[targetsCount];

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = Instantiate(targetTemplate.gameObject, targetTemplate.transform).transform;
        }

        targetIndex = 0;
    }

    private void InitControllers()
    {
        controllerA.Initialize(difficulty.Value);
        controllerB.Initialize(difficulty.Value);
        controllerC.Initialize(difficulty.Value);
    }

    private IEnumerator SpawnTargets()
    {
        while (targetIndex < targets.Length)
        {
            Transform target = targets[targetIndex];

            SpawnTarget(target);

            targetIndex++;

            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }

        //while (controllerA.HasTargets || controllerB.HasTargets || controllerC.HasTargets)
        //{
        //    yield return null;
        //}

        //Debug.Log("Minigame ended");
    }

    private void SpawnTarget(Transform target)
    {
        int random = Random.Range(0, 6);

        switch(random)
        {
            case 0:
                controllerA.AddTarget(target, 1);
                break;
            case 1:
                controllerA.AddTarget(target, -1);
                break;
            case 2:
                controllerB.AddTarget(target, 1);
                break;
            case 3:
                controllerB.AddTarget(target, -1);
                break;
            case 4:
                controllerC.AddTarget(target, 1);
                break;
            case 5:
                controllerC.AddTarget(target, -1);
                break;
        }
    }

}
