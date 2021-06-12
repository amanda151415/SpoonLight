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

    public Image t1_2;
    public Image t2_2;
    public Image m1_2;
    public Image t1_m1_2;

    public int n_t = 0;
    public int n_m = 0;
    public int n_t_2 = 0;
    public int n_m_2 = 0;
    private int seed = 0;

    private int point = 0;
    private bool recipe_done = false;
    private float time = 60.0f;

    public Text scoreText;
    public Text timeLeft;

    public Plate plate;
    public Plate plate2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitBeforeRemove());

    }

    void Requesets()
    {       
        n_t = Random.Range(0, 3);
        n_m = Random.Range(0, 2);
        n_t_2 = Random.Range(0, 3);
        n_m_2 = Random.Range(0, 2);

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
                // El player tiene que tener un plato y haberselo pasado almenos una vez con el otro player para mirar si coincide con la receta
                Plate plateScript = collider.GetComponent<Plate>();
                if (child_t.tag == "Plate" & plateScript.passed == true)
                {

                    object_to_destroy = child_t.gameObject;

                    Score(object_to_destroy, plateScript);
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
                SecondRecipe();
                yield return new WaitUntil(() => (recipe_done == true | time < 0.0f));
                t1.enabled = false;
                recipe_done = false;
                time = 60.0f;

            }
            else if ((n_t == 1 && n_m == 1))
            {
                Debug.Log("Tomata: " + n_t + "Meat: " + n_m);
                t1_m1.enabled = true;
                SecondRecipe();
                yield return new WaitUntil(() => (recipe_done == true | time < 0.0f));
                t1_m1.enabled = false;
                recipe_done = false;
                time = 60.0f;
            }
            else if ((n_t == 0 && n_m == 1))
            {
                Debug.Log("Tomata: " + n_t + "Meat: " + n_m);
                m1.enabled = true;
                SecondRecipe();
                yield return new WaitUntil(() => (recipe_done == true | time < 0.0f));
                m1.enabled = false;
                recipe_done = false;
                time = 60.0f;
            }
            else if ((n_t == 2 && n_m == 0))
            {
                Debug.Log("Tomata: " + n_t + "Meat: " + n_m);
                t2.enabled = true;
                SecondRecipe();
                yield return new WaitUntil(() => (recipe_done == true | time < 0.0f));
                t2.enabled = false;
                recipe_done = false;
                time = 60.0f;

            }
            t2_2.enabled = false;
            t1_2.enabled = false;
            t1_m1_2.enabled = false;
            m1_2.enabled = false;
        }
        

    }

    public void SecondRecipe()
    {
        bool check = true;
        while (check == true){
            Requesets();
            Random.seed = System.DateTime.Now.Millisecond;
            //Debug.Log("concha tu maree");
            if ((n_t_2 == 1 && n_m_2 == 0))
            {
                Debug.Log("Tomata: " + n_t_2 + "Meat: " + n_m_2);
                t1_2.enabled = true;
                check = false;              

            }
            else if ((n_t_2 == 1 && n_m_2 == 1))
            {
                Debug.Log("Tomata: " + n_t_2 + "Meat: " + n_m_2);
                t1_m1_2.enabled = true;
                check = false;                
            }
            else if ((n_t_2 == 0 && n_m_2 == 1))
            {
                Debug.Log("Tomata: " + n_t_2 + "Meat: " + n_m_2);
                m1_2.enabled = true;
                check = false;                
            }
            else if ((n_t_2 == 2 && n_m_2 == 0))
            {
                Debug.Log("Tomata: " + n_t_2 + "Meat: " + n_m_2);
                t2_2.enabled = true;
                check = false;
            }

        }
        
    }

    void Score(GameObject child_t, Plate plateScript)
    {
        //Debug.Log("Entro: " + plate.tomata_count_1 + "XD" + plate.meat_count_1);
        if (n_t == plate.tomata_count_1 && n_m == plate.meat_count_1 || n_t_2 == plate.tomata_count_2 && n_m_2 == plate.meat_count_2)
        {
            point += 1;
            GetComponent<AudioSource>().Play();
            //Debug.Log(child_t.tag);
            Destroy(child_t);
            recipe_done = true;
            scoreText.text = "Score: " + point.ToString();
            plate.tomata_count_1 = 0;
            plate.meat_count_1 = 0;
            plate.tomata_count_2 = 0;
            plate.meat_count_2 = 0;

            // Reseteamos la variable que marca si el plato ha sido pasado entre players almenos una vez
            plateScript.passed = false;
        }

        else if (n_t == plate2.tomata_count_1 && n_m == plate2.meat_count_1 || n_t_2 == plate2.tomata_count_2 && n_m_2 == plate2.meat_count_2)
        {
            point += 1;
            GetComponent<AudioSource>().Play();
            //Debug.Log(child_t.tag);
            Destroy(child_t);
            recipe_done = true;
            scoreText.text = "Score: " + point.ToString();
            plate2.tomata_count_1 = 0;
            plate2.meat_count_1 = 0;
            plate2.tomata_count_2 = 0;
            plate2.meat_count_2 = 0;

            // Reseteamos la variable que marca si el plato ha sido pasado entre players almenos una vez
            plateScript.passed = false;
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
