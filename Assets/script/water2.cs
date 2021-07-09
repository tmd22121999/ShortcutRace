using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water2 : MonoBehaviour
{
    public player thisp;
    private int layerMask;
    // Start is called before the first frame update
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
                if(hit.transform.gameObject.CompareTag("ground"))
                    thisp.GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionY;
            }
             // Debug.Log("hit : " + hit.collider.name);
            //Debug.Log(hit.transform.gameObject.tag);
            //Debug.DrawLine(transform.position+new Vector3(0,1,0),hit.transform.position);
            //Debug.DrawRay(transform.position+new Vector3(0,1,0),direction-new Vector3(0,1,0));
        }else{
            thisp.onWater=true;
        }
    }
}
