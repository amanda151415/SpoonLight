using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutIngredient : MonoBehaviour
{
    public GameObject tomato_cutted;
    public float scaleFactor;
    public Vector3 positionAdd;

    private float cuttime = 5.0f;

    private void OnCollisionStay(Collision other)
    {
        GameObject child = null;

        GameObject collider = other.gameObject;
        Transform collider_t = other.transform;
        // Buscamos si hay algun child en el player con el que hemos colisionado que corresponda a un ingrediente
        foreach (Transform child_t in collider_t)
        {
            if (child_t.tag == "Ingredient" || child_t.tag == "CutIngredient" || child_t.tag == "CookedIngredient" || child_t.tag == "OvercookedIngredient")
            {
                child = child_t.gameObject;
            }
        }

        // Si existe, lo eliminamos
        if (child != null)
        {
            SoundManager.Instance.PlayKnifeCut();//

            cuttime -= Time.deltaTime;
            float sec = cuttime % 60;

            if (cuttime < 0.0f)
            {
                Vector3 position = new Vector3(75, 7, 85);

                GameObject ingredient = Instantiate(tomato_cutted, child.transform.position, Quaternion.identity);
                // Instanciamos el ingrediente como un hijo del player para que as� lo siga
                ingredient.transform.parent = other.transform;
                ingredient.transform.position = other.transform.position + positionAdd;
                ingredient.transform.localScale = new Vector3(1, 1, 1) * scaleFactor;
                Destroy(child);
            }        
        }
    }

    private void OnCollisionExit(Collision other)
    {
        cuttime = 5.0f;
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
