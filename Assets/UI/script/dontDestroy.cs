using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    //public static SceneLoaderGameObject instance = null;
    public static dontDestroy instance = null;
    private void Awake() {
                if (instance == null) {
        instance = this;
        DontDestroyOnLoad(gameObject);
        }
        else {
        Destroy(gameObject);
        }
    }
}
