using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data 
{
    public  int coin; 
    public  int defaultBrick ;
    public  int map ;
    public  string namePlayer;
    public Data(int c,int db,int m, string n){
        coin = c;
        defaultBrick = db;
        map = m;
        namePlayer=n;
    }
    
}
