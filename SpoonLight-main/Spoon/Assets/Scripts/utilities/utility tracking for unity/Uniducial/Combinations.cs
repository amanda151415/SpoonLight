using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Combinations : MonoBehaviour
{

    public Image t1;
    public Image t2;
    public Image m1;
    public Image t1_m1;
    private int n_t = 0;
    private int n_m = 0;
    private int seed = 0;

    private int tomata_count_1 = 0;
    private int meat_count_1 = 0;
    private int point = 0;
    private bool recipe_done = false;
    private float time = 60.0f;

    public Text scoreText;
    public Text timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitBeforeRemove());

    }

    void Requesets()
    {       
        n_t = Random.Range(0, 3);
        n_m = Random.Range(0, 2);        
  
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject collider = other.gameObject;
        Transform collider_t = other.transform;
        GameObject object_to_destroy = null;

        Debug.Log(collider_t.name);
        if (collider.tag == "Player1" || collider.tag == "Player2")
        {
            foreach (Transform child_t in collider_t)
            {

                if (child_t.tag == "Plate")
                {
                    object_to_destroy = child_t.gameObject;

                    foreach (Transform child_b in child_t)
                    {
                        string name_child_1 = child_b.ToString();
                        int parentesis_1 = name_child_1.IndexOf("(");
                        string output_1 = name_child_1.Substring(0, parentesis_1);

                        if (output_1 == "Cutted")
                        {
                            
                            tomata_count_1 += 1;
                            Debug.Log("Tomata: " + tomata_count_1);
                        }

                        else if (output_1 == "Meat")
                        {
                            meat_count_1 += 1;
                            Debug.Log("Meat: " + meat_count_1);
                        }
                        
                    }
                    Score(object_to_destroy);
                    tomata_count_1 = 0;
                    meat_count_1 = 0;
                }
                

            }
        }
        
    }


        IEnumerator WaitBeforeRemove()
    {
        bool start = true;
        while (start == true)
        {
            Requesets();
            Random.seed = System.DateTime.Now.Millisecond;
            if ((n_t == 1 && n_m == 0))
            {
                Debug.Log("Tomata: " + n_t + "Meat: " + n_m);
                t1.enabled = true;
                yield return new WaitUntil(() => (recipe_done == true | time < 0.0f));
                t1.enabled = false;
                recipe_done = false;
                time = 60.0f;

            }
            else if ((n_t == 1 && n_m == 1))
            {
                Debug.Log("Tomata: " + n_t + "Meat: " + n_m);
                t1_m1.enabled = true;
                yield return new WaitUntil(() => (recipe_done == true | time < 0.0f));
                t1_m1.enabled = false;
                recipe_done = false;
                time = 60.0f;
            }
            else if ((n_t == 0 && n_m == 1))
            {
                Debug.Log("Tomata: " + n_t + "Meat: " + n_m);
                m1.enabled = true;
                yield return new WaitUntil(() => (recipe_done == true | time < 0.0f));
                m1.enabled = false;
                recipe_done = false;
                time = 60.0f;
            }
            else if ((n_t == 2 && n_m == 0))
            {
                Debug.Log("Tomata: " + n_t + "Meat: " + n_m);
                t2.enabled = true;
                yield return new WaitUntil(() => (recipe_done == true | time < 0.0f));
                t2.enabled = false;
                recipe_done = false;
                time = 60.0f;

            }



        }
        

    }

    void Score(GameObject child_t)
    {
        Debug.Log(child_t.tag);
        Debug.Log("Entro");
        if (n_t == tomata_count_1 && n_m == meat_count_1)
        {
            point += 1;
            Debug.Log(child_t.tag);
            Destroy(child_t);
            recipe_done = true;
            scoreText.text = "Score: " + point.ToString();

        }

        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        string seconds = (time % 60).ToString("0");

        timeLeft.text = "Next recipe in: " + seconds; 
    }
}
