﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using TMPro;

public class genMap : MonoBehaviour
{
    [Header ("brick")]
    public Transform[] spawnPos;
    
    public GameObject brick, startText, startBrick;
    public Transform road;
    [Header ("Enemy")]
    public PathCreator map;
    int leng;
    public GameObject character;
    public Transform characterTransform;
   [Header ("Bonus")]
    public GameObject bonusObj;
    public Texture[] bonusTexture;
    public MeshRenderer bonusMesh;
    public Transform bonusTransform,goal;
    private Vector3 lastPos;
     [TextArea]
    [Tooltip("a và b là max khoảng cách giữa 2 bonus")]
     public string Notes = "a và b là max khoảng cách giữa 2 bonus";
    public float a;
    public float b;
    [Header ("water")]
        public MeshRenderer waterMeshRenderer;
        public Color[] waterColor = {new Color(0f, 0f, 0f, 255f), new Color(150f, 150f, 255f, 255f), new Color(110f, 110f, 110f, 255f) };
    void Awake()
    {
        //map = GameObject.FindWithTag("ground").GetComponent<PathCreator>();
        leng = map.path.localPoints.Length;
        generateBrick(10);
        lastPos = goal.position;
         genBonus(a,b);
         genEnemy();
         Destroy(this);
    }
    /*private void Update() {
        if(Input.GetMouseButton(0)){
            Time.timeScale = 1;
            startText.SetActive(false);
            
        }
    }*/
    public void generateBrick(int k){
        // Vector3 instantPos,dir;
        GameObject br;
        for(int i=0 ; i < spawnPos.Length ; i++){
            
            int rand = Random.Range(-1,2); 
            br = Instantiate (brick,spawnPos[i]);
            br.transform.localPosition += new Vector3(rand*4,0,0);
        }
    }
    
    public void genBonus(float a,float b){
        float x,z;
        GameObject bnOjTmp;
        for(int i=2;i<11;i++){
            float rand = Random.Range(-a, a);
            x = goal.position.x + rand;
            rand = Random.Range(lastPos.z+20, goal.position.z+b*(i-1));
            z = rand;
            lastPos = new Vector3(x,bonusTransform.position.y,z);
            bnOjTmp = Instantiate (bonusObj , lastPos , Quaternion.Euler(new Vector3(0, 180, 0)) , bonusTransform);
            bnOjTmp.GetComponent<bonus>().rate=i;
            bnOjTmp.GetComponent<bonus>().buc.material.mainTexture = bonusTexture[i-2];
            bnOjTmp.GetComponent<bonus>().bonusSfx = bonusTransform.gameObject.GetComponent<AudioSource>();
        }
    }

    public void genEnemy(){
        float [][] random = {new float[]{0,0,0},new float[]{0.8f,0.3f,0},new float[]{0.2f,0.9f,0.4f},new float[]{0.6f,0.5f,0.5f},new float[]{0.5f,0.2f,1}};
        GameObject[] chrOjTmp = new GameObject[5];
        Vector3 instantPos;
        for(int i=0 ; i<5 ; i++){
            instantPos = map.path.GetPoint(0)+new Vector3(0,0,30) + new Vector3(4*(i%2*2-1)-1,0,(i*2+0.2f)*6+25);
            chrOjTmp[i] = Instantiate (character , instantPos , Quaternion.Euler(new Vector3(0, 0, 0)) , characterTransform);
            chrOjTmp[i].GetComponent<enemyAI>().prority=random[i];
            string[] names = new string[] { "Peter", "Ron", "Satchmo","sachou","tencho","sakura","johnathan","magot", "someone1", "pro gamer", "talon", "nguyen van C", "abcsdf"};
            Instantiate (startBrick,instantPos+new Vector3(0,0,12),Quaternion.Euler(new Vector3(0, 0, 0)) );
            chrOjTmp[i].GetComponent<player>().nameText.text = names[Random.Range(0, names.Length)]; ;
          
        }
          goal.gameObject.GetComponent<goal>().another= chrOjTmp;
    }
}
