using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Combinations : MonoBehaviour
{

    public Image t1;
    private int n_t = 0;
    private int n_m = 0;

    // Start is called before the first frame update
    void Start()
    {
        Requesets();
        StartCoroutine(WaitBeforeRemove());
        Requesets();
        Requesets();
    }

    void Requesets()
    {
        while (n_t == 0 && n_m == 0 || n_t == 2 && n_m == 1)
        {
            n_t = Random.Range(0, 2);
            n_m = Random.Range(0, 1);            
        }
        Debug.Log(n_t);
        Debug.Log(n_m);

    }

    IEnumerator WaitBeforeRemove()
    {
        yield return new WaitForSeconds(5);
        t1.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        Requesets();
    }
}
