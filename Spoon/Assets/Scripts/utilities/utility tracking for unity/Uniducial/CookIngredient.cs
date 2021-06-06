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
    private bool isCooking = false;
    private GameObject cooking_ingredient;

    private void OnCollisionEnter(Collision other)
    {
        GameObject child = null;

        GameObject collider = other.gameObject;
        Transform collider_t = other.transform;
        // Buscamos si hay algun child en el player con el que hemos colisionado que corresponda a un ingrediente
        foreach (Transform child_t in collider_t)
        {
            // Cocinamos solo si es un ingrediente. No nos interesa cocinar elementos cortados, cocinados o quemados 
            if (child_t.tag == "Ingredient" && !isCooking)
            {
                // Instanciamos el child en la variable cooking_ingredient, para as� poder recoger el ingrediente al volver
                SoundManager.Instance.PlayFryPan();//

                cooking_ingredient = GameObject.Instantiate(child_t.gameObject);
                isCooking = true;
                Debug.Log("Cooking");
                child = child_t.gameObject;
                Destroy(child);
                //cookTime -= Time.deltaTime;
                //// Convertimos a segundos
                //float sec = cookTime % 60;
            }
        }
        if (isCooking && child == null) {
            // Si ya estamos cocinando y el player no lleva ningun ingrediente la idea es recogerlo con la tag ya cambiada.

            if (cookTime < -5.0f)
            {
                // Cambiamos el color para que parezca quemado
                // El material en el que tenemos que cambiar el color es un child del ingrediente, por eso tenemos que hacer este GetChild(0)
                var ingredient_renderer = cooking_ingredient.transform.GetChild(0).GetComponent<Renderer>();
                ingredient_renderer.material.color = Color.black;
                // Cambiamos el tag
                cooking_ingredient.tag = "OvercookedIngredient";
                Debug.Log(cooking_ingredient.tag);
                cooking_ingredient.transform.parent = other.transform;
                cooking_ingredient.transform.position = other.transform.position + positionAdd;
                cooking_ingredient.transform.localScale = new Vector3(1, 1, 1) * scaleFactor;

                // Reiniciamos contador
                cookTime = 5.0f;
                isCooking = false;
            }
            else if (cookTime < 0.0f)
            {
                //cooking_ingredient.renderer.material.color = new Color(1, 1, 1);
                var ingredient_renderer = cooking_ingredient.transform.GetChild(0).GetComponent<Renderer>();
                ingredient_renderer.material.color = new Color(92,15,15);
                cooking_ingredient.tag = "CookedIngredient";
                Debug.Log(cooking_ingredient.tag);
                cooking_ingredient.transform.parent = other.transform;
                cooking_ingredient.transform.position = other.transform.position + positionAdd;
                cooking_ingredient.transform.localScale = new Vector3(1, 1, 1) * scaleFactor;

                // Reiniciamos contador
                cookTime = 5.0f;
                isCooking = false;
            }
        }
        else {
            Debug.Log("sonidito");
        }
    }
    //private void OnCollisionStay(Collision other)
    //{
    //    // QUIZ� SE PUEDE METER EL CHILD EN UNA VARIABLE? ASI NO HAY QUE HACER ESTOS CALCULITOS DOS VECES
    //    GameObject child = null;

    //    GameObject collider = other.gameObject;
    //    Transform collider_t = other.transform;
    //    // Buscamos si hay algun child en el player con el que hemos colisionado que corresponda a un ingrediente
    //    foreach (Transform child_t in collider_t)
    //    {
    //        // Contamos solo si es un ingrediente. No nos interesa cocinar elementos cortados, cocinados o quemados 
    //        if (child_t.tag == "Ingredient")
    //        {
    //            cookTime -= Time.deltaTime;
    //            // Convertimos a segundos
    //            float sec = cookTime % 60;
    //        }
    //        else if (child_t.tag == "OvercookedIngredient" || child_t.tag == "CookedIngredient" || child_t.tag == "CutIngredient")
    //        {
    //            Debug.Log("Sonidito");
    //        }
    //    }
    //}

    //private void OnCollisionExit(Collision other)
    //{
    //    GameObject child = null;

    //    GameObject collider = other.gameObject;
    //    Transform collider_t = other.transform;
    //    // Buscamos si hay algun child en el player con el que hemos colisionado que corresponda a un ingrediente
    //    foreach (Transform child_t in collider_t)
    //    {
    //        if (child_t.tag == "Ingredient")
    //        {
    //            child = child_t.gameObject;
    //        }
    //    }

    //    if (child != null)
    //    {
    //        Debug.Log(cookTime);

    //        if (cookTime < -5.0f)
    //        {
    //            Vector3 position = new Vector3(75, 7, 85);

    //            GameObject ingredient = Instantiate(overcooked_meat, child.transform.position, Quaternion.identity);
    //            // Instanciamos el ingrediente como un hijo del player para que as� lo siga
    //            ingredient.transform.parent = other.transform;
    //            ingredient.transform.position = other.transform.position + positionAdd;
    //            ingredient.transform.localScale = new Vector3(1, 1, 1) * scaleFactor;
    //            Destroy(child);
    //            Debug.Log("OVERCOOKED");
    //        }
    //        else if (cookTime < 0.0f)
    //        {
    //            Vector3 position = new Vector3(75, 7, 85);

    //            GameObject ingredient = Instantiate(cooked_meat, child.transform.position, Quaternion.identity);
    //            // Instanciamos el ingrediente como un hijo del player para que as� lo siga
    //            ingredient.transform.parent = other.transform;
    //            ingredient.transform.position = other.transform.position + positionAdd;
    //            ingredient.transform.localScale = new Vector3(1, 1, 1) * scaleFactor;
    //            Destroy(child);
    //            Debug.Log("COOKED");
    //        }
    //    }

    //    cookTime = 500.0f;
    //}
    
    void Start()
    {
        
    }

    void Update()
    {
        // Si isCooking esta activado, restamos tiempo
        if (isCooking)
        {
            cookTime -= Time.deltaTime;
            Debug.Log(cookTime);
        }
    }
}
