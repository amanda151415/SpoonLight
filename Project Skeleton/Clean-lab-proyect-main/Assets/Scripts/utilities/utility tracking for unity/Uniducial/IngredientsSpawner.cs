using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsSpawner : MonoBehaviour
{
    public List<GameObject> tomatoesList = new List<GameObject>();
    public List<GameObject> meatList = new List<GameObject>();

    public GameObject tomatoePrefab;
    public GameObject meatPrefab;

    public void SpawnTomato()
    {
        // La he puesto ahora a 0, pero tendría que aparecer la posición del personaje un poco movida para que parezca que lo está cogiendo
        Vector3 position = new Vector3(0,0,0);
        // Instanciamos un nuevo tomate
        GameObject tomato = Instantiate(tomatoePrefab, position, tomatoePrefab.transform.rotation);
        tomatoesList.Add(tomato);
        Debug.Log("Tomato spawned");
        //tomato.GetComponent<Tomato>().SetSpawner(this);
    }

    public void SpawnMeat()
    {
        // La he puesto ahora a 0, pero tendría que aparecer la posición del personaje un poco movida para que parezca que lo está cogiendo
        Vector3 position = new Vector3(0, 0, 0);
        GameObject meat = Instantiate(meatPrefab, position, meatPrefab.transform.rotation);
        meatList.Add(meat);
        //meat.GetComponent<Meat>().SetSpawner(this);
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
