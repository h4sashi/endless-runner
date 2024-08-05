using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpawner : MonoBehaviour
{
    public GameObject[] abilitiesPrefab;


    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, abilitiesPrefab.Length);
        Instantiate(abilitiesPrefab[index], transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
