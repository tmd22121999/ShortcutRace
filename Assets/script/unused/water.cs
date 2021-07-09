using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    // Start is called before the first frame update
 
    void OnTriggerEnter(Collider other)
    {
        if( (other.tag=="Player") || (other.tag=="other")){
            player player=other.GetComponent<player>();
            Debug.Log("Chạm nước");
            player.onWater=true;
           //Destroy(this);
        }else if(other.tag=="brick"){
            Destroy(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if( (other.tag=="Player") || (other.tag=="other")){
            player player=other.GetComponent<player>();
            player.onWater=false;
            //Destroy(this);
        }
    }
}
