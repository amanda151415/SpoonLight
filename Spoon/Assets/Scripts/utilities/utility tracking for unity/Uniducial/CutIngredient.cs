using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutIngredient : MonoBehaviour
{
    public GameObject tomato_cutted;
    public float scaleFactor;
    public Vector3 positionAdd;
    public GameObject bar;

    private float cuttime = 5.0f;

    private void OnCollisionEnter(Collision other)
    {

        GameObject collider = other.gameObject;
        Transform collider_t = other.transform;
        // Buscamos si hay algun child en el player con el que hemos colisionado que corresponda a un ingrediente
        foreach (Transform child_t in collider_t)
        {
            string name_item = child_t.ToString();
            int leftParenthesis = name_item.IndexOf("(");
            string result = name_item.Substring(0, leftParenthesis);
            if (result == "Tomato")
            {
                GetComponent<AudioSource>().Play();
                AnimateBar();
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        GameObject child = null;

        GameObject collider = other.gameObject;
        Transform collider_t = other.transform;
        // Buscamos si hay algun child en el player con el que hemos colisionado que corresponda a un ingrediente
        foreach (Transform child_t in collider_t)
        {
            string name_item = child_t.ToString();
            int leftParenthesis = name_item.IndexOf("(");
            string result = name_item.Substring(0, leftParenthesis);
            if (result == "Tomato")
            {
                child = child_t.gameObject;

            }
        }

        // Si existe, lo eliminamos
        if (child != null)
        {


            cuttime -= Time.deltaTime;
            float sec = cuttime % 60;
            Debug.Log(cuttime);

            if (cuttime < 0.0f)
            {

                Vector3 position = new Vector3(75, 7, 85);

                GameObject ingredient = Instantiate(tomato_cutted, child.transform.position, Quaternion.identity);
                // Instanciamos el ingrediente como un hijo del player para que as� lo siga
                ingredient.transform.parent = other.transform;
                ingredient.transform.position = other.transform.position + positionAdd;
                ingredient.transform.localScale = new Vector3(1, 1, 1) * scaleFactor;
                Destroy(child);
                ResetAnimateBar();
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        StopAnimateBar();
        ResetAnimateBar();
        GetComponent<AudioSource>().Stop();
        cuttime = 5.0f;
        Debug.Log("Exit:)");
    }

    public void AnimateBar()
    {
        LeanTween.scaleX(bar, 1, cuttime);
    }

    public void StopAnimateBar()
    {
        LeanTween.cancel(bar);
    }

    public void ResetAnimateBar()
    {
        LeanTween.scaleX(bar, 0, 0);
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
