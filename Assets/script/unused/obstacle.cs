using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    public int species;
    public float maxRotation = 20,Speed;
    private float y=0.0f;
    public GameObject door1,door2;
   

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (species)
        {
            case 1:
                y += Time.deltaTime * Speed;
                transform.localRotation = Quaternion.Euler(90, y, 0);
                break;
            case 2:
                if(y > maxRotation)
                    Speed=-Speed;
                if(y < -maxRotation)
                    Speed=-Speed;
                y += Time.deltaTime * Speed;
                
                transform.localRotation = Quaternion.Euler(y, 0, 0);
                break;
            case 3:
                if(y > maxRotation-door2.transform.localScale.x)
                    Speed=-Speed;
                if(y < -maxRotation+door2.transform.localScale.x)
                    Speed=-Speed;
                y += Time.deltaTime * Speed;
                
                door1.transform.localPosition  =new Vector3(y+door1.transform.localScale.x,door1.transform.localPosition.y,door1.transform.localPosition.z);
                door2.transform.localPosition  =new Vector3(-y-door2.transform.localScale.x,door2.transform.localPosition.y,door2.transform.localPosition.z);
                break;
        }     
    }
}
