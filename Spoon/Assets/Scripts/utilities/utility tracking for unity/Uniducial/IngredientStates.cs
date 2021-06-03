using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class IngredientStates : MonoBehaviour
{

    public class Global
    {
        public static int ingr_count = 0;
    }

    //public List<GameObject> ingredientsList = new List<GameObject>();
    public float scaleFactor;
    public Vector3 positionAdd;
    public GameObject ingredientPrefab;


    private void OnCollisionEnter(Collision other)
    {


        if (gameObject.CompareTag("Ingredient_box"))
        {

            Spawn(other);

        }
        if (gameObject.CompareTag("Garbage"))
        {
            RemoveIngredient(other);

        }


    }

    void Spawn(Collision other)
    {
        if (Global.ingr_count == 0)
        {
            Vector3 position = new Vector3(75, 7, 85);
            GameObject ingredient = Instantiate(ingredientPrefab, position, ingredientPrefab.transform.rotation);
            // Instanciamos el ingrediente como un hijo del player para que así lo siga
            ingredient.transform.parent = other.transform;
            ingredient.transform.position = other.transform.position + positionAdd;
            ingredient.transform.localScale = new Vector3(1, 1, 1) * scaleFactor;
            //ingredientsList.Add(ingredient);
            Global.ingr_count = Global.ingr_count + 1;
            Debug.Log(Global.ingr_count);

        }

    }

    void RemoveIngredient(Collision other)
    {
        Debug.Log(Global.ingr_count);
        if (Global.ingr_count >= 0)
        {
            Debug.Log(Global.ingr_count);
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
                Debug.Log("Entro");
                Destroy(child);
                Global.ingr_count = Global.ingr_count - 1;
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
