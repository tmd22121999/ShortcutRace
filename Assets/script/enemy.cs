using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : player
{
    public float speed;
    Rigidbody rb;
    private void Start() {
        rb=this.GetComponent<Rigidbody>();
        mapYPos=this.transform.position.y;
        oldspeed=speed;
    }
    private void Awake() {
        
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
    
       public override void changeMove(bool dk){
        if(!dk) speed = 0;
        else speed = oldspeed;
    }
    public override float changeMove(float deltaSpeed){
        if(deltaSpeed > 0){
            this.speed += deltaSpeed;
            this.speed = Mathf.Min(maxspeed, this.speed);
        }else
            this.speed = oldspeed;
            return this.speed;
    }
}
