using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    
    public GameObject map, menu, cur, pre,bonus;
    public int State; 
    [Header ("End game UI")]
     public GameObject end;
    public TextMeshProUGUI gameOverText,rankText,scoreText;
    
    public void GameOver(int rank, float score) {
        //gameOverText.gameObject.SetActive(true); 
        cur = end;
        cur.SetActive(true); 
        if(rank>0){
            rankText.text += rank;
            scoreText.text += score;
        }else{
            rankText.text = "";
            scoreText.text = "";
            gameOverText.gameObject.SetActive(true);
        }
        Time.timeScale = 0;
    }
    public void enterMap(){
        cur.SetActive(false);
        pre=menu;
        cur=map;
        cur.SetActive(true);  
    }
    public void startGame(int i){
        string map="Map"+i;
        SceneManager.LoadScene(map);
        cur.SetActive(false);
        //pre=cur;
    }
    public void back(){
        if(pre!=null){
            cur.SetActive(false);
            pre.SetActive(true);
            cur=pre;
            pre=null;

        }
    }
    public void activeBonus(){
        bonus.SetActive(true);  
    }
    public void returnMap(){
         SceneManager.LoadScene("UItest");
    }
}
