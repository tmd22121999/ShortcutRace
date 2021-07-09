using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brick : MonoBehaviour
{
    //public player thisbody;
    


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("other")){
            player thisbody=other.GetComponent<player>();
            if((!thisbody.isKilling) && (!thisbody.isHit)){
                thisbody.changeBrick(1);
                Destroy(this.gameObject);
            }
        }
        else if(other.gameObject.CompareTag("water")){
            Destroy(gameObject);
        }
    }
    
}
