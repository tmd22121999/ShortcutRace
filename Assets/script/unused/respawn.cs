using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    public GameObject brick;
    bool rp=false;
    //Vector3 pos;
    //float cooldown=2, RemainTime=0;
    
    private void FixedUpdate() {
        if(transform.childCount <10){
            StartCoroutine(onRespawn());
        }
    }
    IEnumerator onRespawn(){
         yield return new WaitForSeconds(5);
         rp=true;
         Destroy(transform.gameObject);
    }
    private void OnDestroy() {
        if(rp)
        Instantiate (brick,transform.position, transform.rotation,transform.parent).SetActive(true);
        
    }
}
