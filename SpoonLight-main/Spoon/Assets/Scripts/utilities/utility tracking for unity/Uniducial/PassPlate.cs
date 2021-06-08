using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassPlate : MonoBehaviour
{
    public Vector3 addPosition;
    private GameObject child_collider = null;
    private GameObject child_this = null;

    void OnCollisionEnter(Collision other)
    {
        GameObject collider = other.gameObject;
        // Comprobamos que hemos colisionado con un player. Entonces intercambiamos los platos que lleven ambos (si los llevan)
        if (collider.tag == "Player1" || collider.tag == "Player2")
        {
            Transform collider_t = other.transform;
            // Comprobamos primero los childs del player que ha colisionado con este
            foreach (Transform child_t in collider_t)
            {
                if (child_t.tag == "Ingredient" || child_t.tag == "CookedIngredient" || child_t.tag == "OvercookedIngredient" || child_t.tag == "CutIngredient")
                {
                    child_collider = child_t.gameObject;
                }
            }
            // Ahora comprobamos los childs de este player
            foreach (Transform child_t in gameObject.transform)
            {
                if (child_t.tag == "Ingredient" || child_t.tag == "CookedIngredient" || child_t.tag == "OvercookedIngredient" || child_t.tag == "CutIngredient")
                {
                    child_this = child_t.gameObject;
                }
            }
            // Intercambiamos los childs
            if (child_collider != null)
            {
                child_collider.transform.parent = gameObject.transform;
                child_collider.transform.position = gameObject.transform.position + addPosition;
                child_collider.transform.localScale = new Vector3(1, 1, 1);
            }
            if (child_this != null)
            {
                child_this.transform.parent = collider.transform;
                child_this.transform.position = collider.transform.position - addPosition;
                child_this.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
