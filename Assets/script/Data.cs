using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data 
{
    public  int coin; 
    public  int defaultBrick ;
    public  int map ,upgrade2;
    public  string namePlayer;
    public Data(int c,int db,int m,int u2, string n){
        coin = c;
        defaultBrick = db;
        map = m;
        upgrade2 = u2;
        namePlayer=n;
    }
    
}
