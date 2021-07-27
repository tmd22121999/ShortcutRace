using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
[System.Serializable]
public class StaticVar : MonoBehaviour
{
    // Start is called before the first frame update
    public static int coin=0; 
    public static int defaultBrick = 0, upgrade2=0;
     public static float upgrade1=0;
    public static int map = 1,rate=1;
    public static string[] namePlayer={"namePlayer", "someone1", "enemy", "kedich", "nvA", "abcsdf",} ;
   
    public static void setDefaultBrick(int brickNum){
        defaultBrick = brickNum;
    }

}
