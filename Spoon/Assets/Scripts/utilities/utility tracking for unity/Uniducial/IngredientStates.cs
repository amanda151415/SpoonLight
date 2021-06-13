using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientStates : MonoBehaviour
{
    //public List<GameObject> ingredientsList = new List<GameObject>();
    public float scaleFactor;
    public Vector3 positionAdd;
    public GameObject ingredientPrefab;

    private void OnCollisionEnter(Collision other)
    {
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

            // En el caso de que el player no lleve ning�n otro ingrediente del tipo que sea, instanciamos uno nuevo
            if (child == null)
            {
                GetComponent<AudioSource>().Play();
                Spawn(other);
            }
        }

        if (gameObject.CompareTag("Garbage"))
        {
            GetComponent<AudioSource>().Play();
            RemoveIngredient(other);
        }
    }

    void Spawn(Collision other)
    {
        Vector3 position = new Vector3(75, 7, 85);
        GameObject ingredient = Instantiate(ingredientPrefab, position, ingredientPrefab.transform.rotation);
        // Instanciamos el ingrediente como un hijo del player para que as� lo siga
        ingredient.transform.parent = other.transform;
        ingredient.transform.position = other.transform.position + positionAdd;
        ingredient.transform.localScale = new Vector3(1, 1, 1) * scaleFactor;
        //ingredientsList.Add(ingredient);
    }

    void RemoveIngredient(Collision other)
    {
        {
            GameObject child = null;

            GameObject collider = other.gameObject;
            Transform collider_t = other.transform;
            // Buscamos si hay algun child en el player con el que hemos colisionado que corresponda a un ingrediente
            foreach (Transform child_t in collider_t)
            {
                // NO SE SI AL ELIMINAR EL PLATO SE ELIMINARAN TAMBIEN SUS HIJOS. ASUMO QUE SI, PERO TENDRIA QUE HACER PRUEBAS. SINO ESTO NO FUNCIONAR� YA QUE ELIMINAR� SOLO EL �LTIMO CHILD QUE SE COMPRUEBE
                if (child_t.tag == "Ingredient" || child_t.tag == "CutIngredient" || child_t.tag == "CookedIngredient" || child_t.tag == "OvercookedIngredient" || child_t.tag == "Plate" ){
                    child = child_t.gameObject;
                }
            }

            // Si existe, lo eliminamos
            if (child != null)
            {
                
                Destroy(child);
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
