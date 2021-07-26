using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StaticVar : MonoBehaviour
{
    // Start is called before the first frame update
    public static int coin; 
    public static int defaultBrick = 0, upgrade2;
    public static int map = 1;
    public static string[] namePlayer={"namePlayer", "someone1", "enemy", "kedich", "nvA", "abcsdf",} ;
   
    public static void setDefaultBrick(int brickNum){
        defaultBrick = brickNum;
    }

}
