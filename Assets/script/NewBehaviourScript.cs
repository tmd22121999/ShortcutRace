using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
     public GameObject[] mapPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(StaticVar.map);
        Debug.Log(mapPrefab);
        Instantiate(mapPrefab[StaticVar.map-1]);
    }

}
