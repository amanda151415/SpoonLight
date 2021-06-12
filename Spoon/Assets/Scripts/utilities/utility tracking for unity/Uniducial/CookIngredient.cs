using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookIngredient : MonoBehaviour
{
    public float scaleFactor;
    public Vector3 positionAdd;
    public GameObject bar;

    private float cookTime = 5.0f;
    private float overcookTime = 10.0f;
    private bool isCooking = false;
    private GameObject cooking_ingredient;
    private bool isOvercooked = false;

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
                GetComponent<AudioSource>().Play();
                AnimateBar();
                cooking_ingredient = GameObject.Instantiate(child_t.gameObject);
                isCooking = true;
                child = child_t.gameObject;
                Destroy(child);
                //cookTime -= Time.deltaTime;
                //// Convertimos a segundos
                //float sec = cookTime % 60;
            }
        }
        if (isCooking && child == null)
        {
            // Si ya estamos cocinando y el player no lleva ningun ingrediente la idea es recogerlo con la tag ya cambiada.

            if (overcookTime < 0.0f)
            {
                // Cambiamos el color para que parezca quemado
                // El material en el que tenemos que cambiar el color es un child del ingrediente, por eso tenemos que hacer este GetChild(0)
                var ingredient_renderer = cooking_ingredient.transform.GetChild(0).GetComponent<Renderer>();
                ingredient_renderer.material.color = Color.black;
                // Cambiamos el tag
                cooking_ingredient.tag = "OvercookedIngredient";
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
                ingredient_renderer.material.color = new Color((100f / 255f), (53 / 255f), (26 / 255f));
                cooking_ingredient.tag = "CookedIngredient";
                cooking_ingredient.transform.parent = other.transform;
                cooking_ingredient.transform.position = other.transform.position + positionAdd;
                cooking_ingredient.transform.localScale = new Vector3(1, 1, 1) * scaleFactor;

                // Reiniciamos contador
                cookTime = 5.0f;
                isCooking = false;
                ResetAnimateBar();
            }
        }
    }


    public void AnimateBar()
    {
        LeanTween.scaleY(bar, 1, cookTime);
    }

    public void AnimateBar_over()
    {
        LeanTween.scaleY(bar, 1, overcookTime);
    }

    public void ResetAnimateBar()
    {
        LeanTween.scaleY(bar, 0, 0);
    }

    public void StopAnimateBar()
    {
        LeanTween.cancel(bar);
    }

    void Start()
    {

    }

    void Update()
    {
        // Si isCooking esta activado, restamos tiempo
        if (isCooking)
        {
            cookTime -= Time.deltaTime;
            overcookTime -= Time.deltaTime;
            //Debug.Log(cookTime);
            // Solo queremos entrar aquí cuando hayamos estemos a punto de pasar la frontera entre cocinado y quemado para resetear la barra
            if (cookTime < 0.0 & !isOvercooked)
            {
                isOvercooked = true;
                StopAnimateBar();
                ResetAnimateBar();
                AnimateBar_over();
            }
            else if (overcookTime < 0.0f)
            {
                bar.transform.GetComponent<Image>().color = Color.blue;
            }
        }
    }
}
