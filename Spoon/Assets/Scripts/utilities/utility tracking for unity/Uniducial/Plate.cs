using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Plate : MonoBehaviour
{
    public float scaleFactor;
    public Vector3 positionAdd;
    public Combinations combinations;

    public int tomata_count_1 = 0;
    public int meat_count_1 = 0;

    public int tomata_count_2 = 0;
    public int meat_count_2 = 0;

    public bool passed = false;

    private void OnCollisionEnter(Collision other)
    {
        

        GameObject collider = other.gameObject;
        Transform collider_t = other.transform;

        if (collider.name == "Plate1")
        {
            foreach (Transform child_t in collider_t)
            {
                string name_child_1 = child_t.ToString();
                int parentesis_1 = name_child_1.IndexOf("(");
                string output_1 = name_child_1.Substring(0, parentesis_1);

                Debug.Log(output_1);
                if (output_1 == "Cutted")
                {
                    tomata_count_1 += 1;
                }

                else if (output_1 == "Meat")
                {
                    meat_count_1 += 1;

                }
            }

            if (tomata_count_1 == combinations.n_t & meat_count_1 == combinations.n_m || tomata_count_1 == combinations.n_t_2 & meat_count_1 == combinations.n_m_2)
            {
                GameObject ingredient = Instantiate(collider, collider.transform.position, collider.transform.rotation);
                ingredient.transform.parent = collider.transform.parent;
                ingredient.transform.position = collider.transform.position;
                ingredient.transform.localScale = collider.transform.localScale;
                foreach (Transform ing_ch in ingredient.transform)
                {
                    Destroy(ing_ch.gameObject);
                }
                collider_t.GetComponent<BoxCollider>().enabled = false;
                collider_t.transform.parent = gameObject.transform;
                collider_t.transform.position = gameObject.transform.position + positionAdd;
                collider_t.transform.localScale = new Vector3(1, 1, 1) * scaleFactor;
            }           
        }

        else if (collider.name == "Plate2")
        {
            foreach (Transform child_t in collider_t)
            {
                string name_child = child_t.ToString();
                int parentesis = name_child.IndexOf("(");
                string output = name_child.Substring(0, parentesis);


                if (output == "Cutted")
                {
                    tomata_count_2 += 1;
                }

                else if (output == "Meat")
                {
                    meat_count_2 += 1;
                }

            }

            if (tomata_count_2 == combinations.n_t & meat_count_2 == combinations.n_m || tomata_count_2 == combinations.n_t_2 & meat_count_2 == combinations.n_m_2)
            {
                GameObject ingredient = Instantiate(collider, collider.transform.position, collider.transform.rotation);
                ingredient.transform.parent = collider.transform.parent;
                ingredient.transform.position = collider.transform.position;
                ingredient.transform.localScale = collider.transform.localScale;
                foreach (Transform ing_ch in ingredient.transform)
                {
                    Destroy(ing_ch.gameObject);
                }
                collider_t.GetComponent<BoxCollider>().enabled = false;
                collider_t.transform.parent = gameObject.transform;
                collider_t.transform.position = gameObject.transform.position + positionAdd;
                collider_t.transform.localScale = new Vector3(1, 1, 1) * scaleFactor;
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
        Debug.Log(gameObject.name + ": " + passed);
    }
}
