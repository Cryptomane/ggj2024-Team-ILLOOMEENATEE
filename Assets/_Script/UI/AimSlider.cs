using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimSlider : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [Space]

    [SerializeField]
    private Image targetImage;
    [SerializeField] 
    private RectTransform targetImageMinPos;
    [SerializeField] 
    private RectTransform targetImageMaxPos;

    [Space]

    [SerializeField]
    private AimController controller;

    private void Start()
    {
        targetImage.rectTransform.position = Vector3.Lerp(targetImageMinPos.position, targetImageMaxPos.position, controller.TargetValue);

    }

    void Update()
    {
        slider.value = controller.CurrentNormalizedValue;
    }
}
