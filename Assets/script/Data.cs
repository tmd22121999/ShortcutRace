using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;

[System.Serializable]
public class Data 
{
    public  int coin; 
    public  int defaultBrick ;
    public  int map, upgrade2;
    public float upgrade1;
    public  string namePlayer;
    public DateTime lastSaveTime;
    public Data(int c,int db,int m,float u1,int u2, string n,DateTime lst){
        coin = c;
        defaultBrick = db;
        map = m;
        upgrade1 = u1;
        upgrade2 = u2;
        namePlayer=n;
        lastSaveTime = lst;
    }
    
}
