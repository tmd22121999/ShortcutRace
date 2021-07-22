using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
     public GameObject[] mapPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(StaticVar.map);
        Instantiate(mapPrefab[(StaticVar.map-1)%4]);
    }

}
