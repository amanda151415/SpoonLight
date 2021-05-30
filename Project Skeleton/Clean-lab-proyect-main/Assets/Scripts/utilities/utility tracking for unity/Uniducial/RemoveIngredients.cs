using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveIngredients : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
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

        // Si existe, lo eliminamos
        if (child != null)
        {
            Destroy(child);
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
