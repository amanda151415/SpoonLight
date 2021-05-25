using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsSpawner : MonoBehaviour
{
    public List<GameObject> ingredientsList = new List<GameObject>();
    public float scaleFactor;
    public Vector3 positionAdd;

    public GameObject ingredientPrefab;

    private void OnCollisionEnter(Collision other)
    {
        Vector3 position = new Vector3(75, 7, 85);
        GameObject ingredient = Instantiate(ingredientPrefab, position, ingredientPrefab.transform.rotation);
        // Instanciamos el ingrediente como un hijo del player para que asi lo siga
        ingredient.transform.parent = other.transform;
        ingredient.transform.position = other.transform.position + positionAdd;
        ingredient.transform.localScale = new Vector3(1, 1, 1)*scaleFactor;
        ingredientsList.Add(ingredient);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
