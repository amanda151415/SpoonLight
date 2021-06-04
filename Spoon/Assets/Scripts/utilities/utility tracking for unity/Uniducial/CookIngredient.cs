using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookIngredient : MonoBehaviour
{
    public GameObject cooked_meat;
    public GameObject overcooked_meat;
    public float scaleFactor;
    public Vector3 positionAdd;

    private float cookTime = 5.0f;

    private void OnCollisionStay(Collision other)
    {
        // QUIZÁ SE PUEDE METER EL CHILD EN UNA VARIABLE? ASI NO HAY QUE HACER ESTOS CALCULITOS DOS VECES
        GameObject child = null;

        GameObject collider = other.gameObject;
        Transform collider_t = other.transform;
        // Buscamos si hay algun child en el player con el que hemos colisionado que corresponda a un ingrediente
        foreach (Transform child_t in collider_t)
        {
            // Contamos solo si es un ingrediente. No nos interesa cocinar elementos cortados, cocinados o quemados 
            if (child_t.tag == "Ingredient")
            {
                cookTime -= Time.deltaTime;
                // Convertimos a segundos
                float sec = cookTime % 60;
            }
            else if (child_t.tag == "OvercookedIngredient" || child_t.tag == "CookedIngredient" || child_t.tag == "CutIngredient")
            {
                Debug.Log("Sonidito");
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        GameObject child = null;

        GameObject collider = other.gameObject;
        Transform collider_t = other.transform;
        // Buscamos si hay algun child en el player con el que hemos colisionado que corresponda a un ingrediente
        foreach (Transform child_t in collider_t)
        {
            if (child_t.tag == "Ingredient")
            {
                child = child_t.gameObject;
            }
        }

        if (child != null)
        {
            Debug.Log(cookTime);

            if (cookTime < -5.0f)
            {
                Vector3 position = new Vector3(75, 7, 85);

                GameObject ingredient = Instantiate(overcooked_meat, child.transform.position, Quaternion.identity);
                // Instanciamos el ingrediente como un hijo del player para que así lo siga
                ingredient.transform.parent = other.transform;
                ingredient.transform.position = other.transform.position + positionAdd;
                ingredient.transform.localScale = new Vector3(1, 1, 1) * scaleFactor;
                Destroy(child);
                Debug.Log("OVERCOOKED");
            }
            else if (cookTime < 0.0f)
            {
                Vector3 position = new Vector3(75, 7, 85);

                GameObject ingredient = Instantiate(cooked_meat, child.transform.position, Quaternion.identity);
                // Instanciamos el ingrediente como un hijo del player para que así lo siga
                ingredient.transform.parent = other.transform;
                ingredient.transform.position = other.transform.position + positionAdd;
                ingredient.transform.localScale = new Vector3(1, 1, 1) * scaleFactor;
                Destroy(child);
                Debug.Log("COOKED");
            }
        }

        cookTime = 500.0f;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
