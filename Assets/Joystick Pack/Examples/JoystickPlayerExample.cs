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
    public void FixedUpdate()
    {
        if(ani.GetBool("isHit")!=thisplayer.isHit)
            ani.SetBool("isHit",thisplayer.isHit);
        if(ani.GetBool("isKilling")!=thisplayer.isKilling)
            ani.SetBool("isKilling",thisplayer.isKilling);
        if(ani.GetInteger("end") != 0)
            ani.SetInteger("end",0);
        
        direction = Vector3.forward * FloatingJoystick.Vertical + Vector3.right * FloatingJoystick.Horizontal;
        if(direction.magnitude>0.1f){
            ani.SetBool("isrunning",true);
            direction = Vector3.Normalize(direction);
            //rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            thisplayer.transform.position+=direction * speed * Time.fixedDeltaTime;
            //Debug.Log(direction);
            float rotateAngle= Vector3.SignedAngle(direction, Vector3.forward, Vector3.down);
            transform.eulerAngles  =new Vector3(0,rotateAngle,0);
        }else
        {
            ani.SetBool("isrunning",false);
        }
        
    }
}