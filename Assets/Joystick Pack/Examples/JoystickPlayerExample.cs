using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public FloatingJoystick FloatingJoystick;
    public Rigidbody rb;
    public player thisplayer;
    public Animator ani;
    public Vector3 direction;
    
    private void Update() {
        
    }    public void FixedUpdate()
    {
        FloatingJoystick = GameObject.FindGameObjectWithTag("joystick").GetComponent<FloatingJoystick>();
        if(ani.GetBool("isHit")!=thisplayer.isHit)
            ani.SetBool("isHit",thisplayer.isHit);
        if(ani.GetBool("isKilling")!=thisplayer.isKilling)
            ani.SetBool("isKilling",thisplayer.isKilling);
        if(ani.GetInteger("end") != 0)
            ani.SetInteger("end",0);
        if(speed > 0.1f)
            //direction = Vector3.forward * FloatingJoystick.Vertical + ;
            direction = Vector3.right * FloatingJoystick.Horizontal;
      
        //if(direction.magnitude>0.1f){
            ani.SetBool("isrunning",true);
            //direction = Vector3.Normalize(direction);
            //rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            thisplayer.transform.position+=thisplayer.transform.forward * speed * Time.fixedDeltaTime;
            Quaternion rotationY = Quaternion.Euler(0f,direction.x*speed,0f);
            Quaternion newRotation = rotationY*thisplayer.transform.rotation;
            thisplayer.transform.rotation = Quaternion.Lerp(thisplayer.transform.rotation,newRotation,Time.fixedDeltaTime*5);
            //thisplayer.transform.Rotate(0.0f,direction.x*2 , 0.0f, Space.World);
            //Debug.Log(direction);
            //float rotateAngle= Vector3.SignedAngle(direction, Vector3.forward, Vector3.down);
            //transform.eulerAngles  =new Vector3(0,rotateAngle,0);
        //}else
        //{
        //    ani.SetBool("isrunning",false);
        //}
        
    }
}