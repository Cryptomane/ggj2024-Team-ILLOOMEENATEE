using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HamburgerController : MonoBehaviour
{
    private void OnEnable()
    {
        InputManager.OnAHit += OnAHit;
        InputManager.OnBHit += OnBHit;
        InputManager.OnCHit += OnCHit;
    }

    private void OnDisable()
    {
        InputManager.OnAHit -= OnAHit;
        InputManager.OnBHit -= OnBHit;
        InputManager.OnCHit -= OnCHit;
    }
    // Start is called before the first frame update

    [SerializeField] private GameObject[] ingredientsList;
    [SerializeField] private GameObject[] choosedIngredientsList;
    [SerializeField] private int maxIngredients = 9;
    [SerializeField] private int minIngredients = 3;
    [SerializeField] private int numOfIngredients = 3;

    [SerializeField] private float difficulty = 0f;
    [SerializeField] private float currDifficulty = 1f;
    [SerializeField] private GameObject imageHamburger;
    void Start()
    {
        numOfIngredients = (int)(3 + (difficulty * 1.5f));
        if(numOfIngredients > 9)
            numOfIngredients = 9;
        Fill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAHit()
    {
        
    }

    private void OnBHit()
    {

    }

    private void OnCHit()
    {

    }

    public void Fill()
    {
        choosedIngredientsList = new GameObject[numOfIngredients];
        for (int i = 0; i < numOfIngredients; i++)
        {
            int index = Random.Range(0, ingredientsList.Length);
            choosedIngredientsList[i] = ingredientsList[index] as GameObject;
            Vector3 positionInstance = imageHamburger.transform.position;
            var instantiateIngr = Instantiate(ingredientsList[index], positionInstance, Quaternion.identity);
            instantiateIngr.transform.parent = imageHamburger.transform;
            instantiateIngr.transform.position += new Vector3 (0f, 0.5f*i, 0f);
        }
    }
}
