using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutInPlate : MonoBehaviour
{
    public Vector3 addPosition;
    public float scaleFactor;
    public GameObject ingredientPrefab;

    private void OnCollisionEnter(Collision other)
    {
        GameObject child = null;

        GameObject collider = other.gameObject;
        Transform collider_t = other.transform;
        foreach (Transform child_t in collider_t)
        {
            if (child_t.tag == "Ingredient" || child_t.tag == "CutIngredient" || child_t.tag == "CookedIngredient")
            {
                child = child_t.gameObject;
            }
            // No nos interesa meter en el plato ingredientes quemados
            else if (child_t.tag == "OvercookedIngredient")
            {
                Debug.Log("Sonidito");
            }
        }

        // Si existe, la idea es trasladar el ingrediente que lleva el player, y ponerlo como un child del plato 
        if (child != null)
        {
            child.transform.parent = gameObject.transform;
            child.transform.position = gameObject.transform.position + addPosition;
            child.transform.localScale = new Vector3(1, 1, 1) * scaleFactor;
        }
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
