using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutInPlate : MonoBehaviour
{
    public GameObject ingredientPrefab;

    private void OnCollisionEntry(Collision other)
    {
        // No se por que no me detecta la colision
        Debug.Log("collision:)");
        GameObject child = null;

        GameObject collider = other.gameObject;
        Transform collider_t = other.transform;
        if (gameObject.CompareTag("Ingredient_box"))
        {
            foreach (Transform child_t in collider_t)
            {
                if (child_t.tag == "Ingredient" || child_t.tag == "CutIngredient" || child_t.tag == "CookedIngredient" || child_t.tag == "OvercookedIngredient")
                {
                    child = child_t.gameObject;
                }
            }

            // Si existe, la idea es trasladar el ingrediente que lleva el player, y ponerlo como un child del plato 
            if (child != null)
            {
                Vector3 position = new Vector3(75, 7, 85);
                GameObject ingredient = Instantiate(ingredientPrefab, position, ingredientPrefab.transform.rotation);
                // De momento estoy simplemente intentando instanciar un objeto como hijo del plato
                ingredient.transform.parent = gameObject.transform;
                ingredient.transform.position = gameObject.transform.position;
                ingredient.transform.localScale = new Vector3(1, 1, 1);
            }
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
