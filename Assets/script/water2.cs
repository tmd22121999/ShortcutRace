using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water2 : MonoBehaviour
{
    public player thisp;
    private int layerMask;
    private void Start() {
        layerMask = 1 << 9;
    }
        void Update()
    {
        Vector3 direction = new Vector3(0,-1,0);
        //Debug.DrawRay(transform.position+new Vector3(0,3,0),direction);
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position+new Vector3(0,3,0),direction, out hit, 3.1f,~layerMask)){
        //if(Physics.BoxCast(transform.position+new Vector3(0,3,0), transform.lossyScale, direction, out hit, transform.rotation, 4,~layerMask)){
            if(hit.transform.gameObject.CompareTag("water")){
                thisp.onWater=true;
            }else{
                thisp.onWater=false;
                if(hit.transform.gameObject.CompareTag("ground")){
                    //thisp.GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionY;
                    if(thisp.isJump){
                        thisp.GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionY;
                    }
                }
                thisp.lastPosOnGround =this.transform.position;    
            }
        }else{
            thisp.onWater=true;
        }
        if (Physics.Raycast(transform.position+new Vector3(0,3,0),direction, out hit, 10,1<<8)){
            thisp.changeMove(0);
        }
    }
}
