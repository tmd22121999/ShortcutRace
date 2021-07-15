using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVar : MonoBehaviour
{
    // Start is called before the first frame update
    public static int defaultBrick = 0;
    public static int map = 1;
    
    private void Start() {
        defaultBrick = 0;
         map = 1;
    }
    public static void setDefaultBrick(int brickNum){
        defaultBrick = brickNum;
    }

}
