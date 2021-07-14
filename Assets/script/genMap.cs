using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using TMPro;

public class genMap : MonoBehaviour
{
    [Header ("brick")]
    public Transform[] spawnPos;
    
    public GameObject brick, startText;
    public Transform road;
    [Header ("Enemy")]
    public PathCreator map;
    int leng;
    public GameObject character;
    public Transform characterTransform;
   [Header ("Bonus")]
    public GameObject bonusObj;
    public Transform bonusTransform,goal;
    private Vector3 lastPos;
     [TextArea]
    [Tooltip("a và b là max khoảng cách giữa 2 bonus")]
     public string Notes = "a và b là max khoảng cách giữa 2 bonus";
    public float a;
    public float b;
    void Awake()
    {
        //map = GameObject.FindWithTag("ground").GetComponent<PathCreator>();
        leng = map.path.localPoints.Length;
        generateBrick(10);
        Time.timeScale = 0;
        lastPos = bonusTransform.position;
         genBonus(a,b);
         genEnemy();
    }
    private void Update() {
        if(Input.GetMouseButton(0)){
            Time.timeScale = 1;
            startText.SetActive(false);
            Destroy(this);
        }
    }
    // Update is called once per frame
    public void generateBrick(int k){
        Vector3 instantPos,dir;
        GameObject br;
        for(int i=0 ; i < spawnPos.Length ; i++){
            /*
            dir = map.path.GetDirection(i);
            //Debug.Log(map.path.GetRotationAtDistance(i));
            dir = new Vector3(dir.z/dir.z,dir.y,-dir.x/dir.x);
            Vector3.Normalize(dir);
            instantPos = map.path.GetPoint(i)-10*new Vector3(dir.x,-0.5f,dir.z)+(rand+0.5f)*dir*6;
            dir.y=map.path.GetRotationAtDistance(i).eulerAngles.y;
            Instantiate (brick,instantPos, Quaternion.Euler(dir),road);
            Debug.Log(dir);*/
            int rand = Random.Range(-1,2); 
            br = Instantiate (brick,spawnPos[i]);
            br.transform.localPosition += new Vector3(rand*7,0,0);
        }
    }
    
    public void genBonus(float a,float b){
        float x,z;
        GameObject bnOjTmp;
        for(int i=2;i<11;i++){
            float rand = Random.Range(-a, a);
            x = goal.position.x -a/2+ rand;
            rand = Random.Range(lastPos.z+7, goal.position.z+b*(i-1));
            z = rand;
            lastPos = new Vector3(x,bonusTransform.position.y,z);
            bnOjTmp = Instantiate (bonusObj , lastPos , Quaternion.Euler(new Vector3(0, 0, 0)) , bonusTransform);
            bnOjTmp.GetComponent<bonus>().rate=i;
        }
    }

    public void genEnemy(){
        float [][] random = {new float[]{0,0,0},new float[]{1,1,0},new float[]{1,0,0.4f},new float[]{1,0.5f,0.5f},new float[]{0.5f,0.2f,1}};
        GameObject chrOjTmp;
        Vector3 instantPos;
        for(int i=0 ; i<5 ; i++){
            instantPos = map.path.GetPoint(0) + new Vector3(-4*(i%2*2-1)*(-1),0,(i*2+0.2f)*2+4);
            chrOjTmp = Instantiate (character , instantPos , Quaternion.Euler(new Vector3(0, 0, 0)) , characterTransform);
            chrOjTmp.GetComponent<enemyAI>().prority=random[i];
        }
    }
}
