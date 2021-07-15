using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class score : MonoBehaviour
{
    // Start is called before the first frame update
     public TextMeshProUGUI ScoreText;
     float timer;
     player P;
     public float scorePoint,finalPoint;

    void Start()
    {
        P=this.gameObject.GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        //score :
        timer-=Time.deltaTime;
        if((timer < 0) && (!P.passGoal)){
            scorePoint+=100;
             timer=3;
             finalPoint=scorePoint;
        }
        //ScoreText.text= finalPoint.ToString();

    }
 
}
