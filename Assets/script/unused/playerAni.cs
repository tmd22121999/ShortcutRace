using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAni : MonoBehaviour
{
    // Start is called before the first frame update
    public player thisp; 
    public void aniCallback(){
        thisp.aniCallback(this.transform.position);
    }
    public void afterhit(){
        thisp.afterhit(this.transform.position);
    }
}
