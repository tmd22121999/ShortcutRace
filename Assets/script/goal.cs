using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goal : MonoBehaviour
{
    public int rank, rankNow;
    public GameObject bonus;
    public GameObject[] another;
       public GameObject player;
       public Text rankText;
 

    
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }
    void FixedUpdate()
    {
        rankNow = rank+1;
        for ( int r = 0; r < 5 ; r++) {
        if(another.Length > 0)
        //Debug.Log(another.Length);
            if(another[r] != null)
            if( Vector3.Distance(transform.position, player.transform.position) > Vector3.Distance(transform.position, another[r].transform.position)) {
                rankNow++;
                
            
            }
           // Debug.Log(another[r]);
        }
        rankText.text = rankNow.ToString();
        //Debug.Log(rankNow);
    }
       void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.CompareTag("Player")) && !(other.gameObject.GetComponent<player>().passGoal)){
            rankText.enabled = false;
            other.gameObject.GetComponent<player>().passGoal=true;
            rank++;
            another = GameObject.FindGameObjectsWithTag("other");
            foreach(var x in another){
                Destroy(x);
                }
            if(rank==1)
                bonus.SetActive(true);  
            else
                other.gameObject.GetComponent<player>().dead();
            
            
        }else if(other.gameObject.CompareTag("other") ){
            other.gameObject.GetComponent<player>().passGoal=true;
            rank++;
            Destroy(other.gameObject);
        }
    }

}
