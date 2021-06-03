using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutIngredient : MonoBehaviour
{

    public GameObject tomato_cutted;
    public float scaleFactor;
    public Vector3 positionAdd;

    public float cuttime = 500.0f;




    private void OnCollisionStay(Collision other)
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
           
            cuttime -= Time.deltaTime;
            float sec = cuttime % 60;

            if (sec < 0.0f)
            {

                Vector3 position = new Vector3(75, 7, 85);

                GameObject ingredient = Instantiate(tomato_cutted, child.transform.position, Quaternion.identity);
                // Instanciamos el ingrediente como un hijo del player para que así lo siga
                ingredient.transform.parent = other.transform;
                ingredient.transform.position = other.transform.position + positionAdd;
                ingredient.transform.localScale = new Vector3(1, 1, 1) * scaleFactor;
                Destroy(child);
           
                Debug.Log("Entro");

            }        


        }
    }

    private void OnCollisionExit(Collision other)
    {
        cuttime = 500.0f;
        Debug.Log("Surto");
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
