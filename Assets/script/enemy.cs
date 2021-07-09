using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : player
{
    public float speed;
    private float oldspeed;
    Rigidbody rb;
    private void Start() {
        rb=this.GetComponent<Rigidbody>();
        mapYPos=this.transform.position.y;
        oldspeed=speed;
    }
    public override void kill(){
        //if(speed==0)

                    
    }
    public void kill2(){
        foreach(var x in fov.visibleTargets)
                    if(x!=null) {
                    // Debug.Log(x.tag);
                        if( (x.CompareTag("Player") || x.CompareTag("other")) && (!x.GetComponent<player>().isHit) && (canKill) && (brickCount>1)&& (x.GetComponent<player>().brickCount>2) ){// && (rand < (x.GetComponent<player>().brickCount-brickCount)*0.005f)){
                            x.gameObject.GetComponent<player>().isHit=true;
                            isKilling=true;
                            canKill=false;
                            RemainTime=cooldown;
                        }

                    }
    }
    public override void dead(){
        Destroy(this.gameObject); 
    }
    public void move(Vector3 dest){
        //direction.y=0;
        Vector3 direction = Vector3.Normalize(dest - transform.position);
        direction.y=0;
        transform.position+=direction * speed * Time.deltaTime;
        //rb.MovePosition(rb.position+direction*speed);//Debug.Log(rb.position);
        float rotateAngle= Vector3.SignedAngle(direction, Vector3.forward, Vector3.down);
        transform.eulerAngles  =new Vector3(0,rotateAngle,0);
    }
    public override IEnumerator  ishit(){
        //if(speed!=0)
        
        for(int i=0;i<brickCount-2;i++){
            //changeBrick(-1);
            Instantiate (gach,transform.position+new Vector3(0.1f,3,0), Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        changeBrick(-brickCount+2);
        //speed=0;
        yield return new WaitForSeconds(timeGetHit);
        //speed=oldspeed;
        isHit=false;
    }
}
