﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bonus : MonoBehaviour
{
    public score scr;
    public MeshRenderer buc;
    public Material mau;
    public int rate;
    public AudioSource bonusSfx;
     private void Start() {
        scr = GameObject.FindGameObjectWithTag("Player").GetComponent<score>();
    }
       void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            scr.finalPoint=scr.finalPoint<rate*scr.scorePoint?rate*scr.scorePoint:scr.finalPoint;
            StaticVar.rate = rate;
            buc.material = mau;
            if(rate==10){
                other.gameObject.GetComponent<player>().dead();
            }
            Destroy(this);
        }
    }


}
