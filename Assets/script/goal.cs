using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal : MonoBehaviour
{
    public int rank;
    public GameController gameController;
    public GameObject[] another;
 

    
    private void Start() {
         another = GameObject.FindGameObjectsWithTag("other");
        
    }
       void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.CompareTag("Player")) && !(other.gameObject.GetComponent<player>().passGoal)){
            
            other.gameObject.GetComponent<player>().passGoal=true;
            rank++;
            another = GameObject.FindGameObjectsWithTag("other");
            foreach(var x in another){
                Destroy(x);
                }
            if(rank==1)
                gameController.activeBonus();
            else
                other.gameObject.GetComponent<player>().dead();
            
            
        }else if(other.gameObject.CompareTag("other") ){
            other.gameObject.GetComponent<player>().passGoal=true;
            rank++;
            Destroy(other.gameObject);
        }
    }

}
