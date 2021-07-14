using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVar : MonoBehaviour
{
    // Start is called before the first frame update
    public static int defaultBrick;
    private void Start() {
        defaultBrick = 2;
    }
    public static void setDefaultBrick(int brickNum){
        defaultBrick = brickNum;
    }

}
