using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data 
{
    // Start is called before the first frame update
    public  int coin; 
    public  int defaultBrick = 0;
    public  int map = 1;
    public  string namePlayer;
    public Data(int c,int db,int m, string n){
        coin = c;
        defaultBrick = db;
        map = m;
        namePlayer=n;
    }
    
}
