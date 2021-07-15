using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [Header ("Menu UI")]
    public GameObject map;
    public GameObject menu, cur, pre, setting;
    public int State; 
    [Header ("End game UI")]
     public GameObject end;

     public GameObject gameOver,win,lose, morePrizeImg;
     public Image cooldown;
    public TextMeshProUGUI rankText,scoreText;
    public RectTransform pointer;
    public Animator pointAni;
    private bool isCooldown = false, canRevive=true;
    [Header ("Setting")]
    public Image soundImg;
    public Image vibrateImg;
    public Sprite soundOn, soundOff, vibrateOn, vibrateOff;
    public Animator soundToggle, vibrateToggle; 
    [Header ("game")]
     public GameObject[] mapPrefab;
    int rand,loop;
    public void GameOver() {
        cur=gameOver;
        cur.SetActive(true);
        Time.timeScale = 0;
        if(canRevive)
            StartCoroutine(revive(5));
        else
            endGame(7);
    }
    private void Update() {
        if(isCooldown){
            cooldown.fillAmount -= (1.0f / 5.0f )*Time.unscaledDeltaTime;
            if(win.activeSelf)
            if(pointAni.GetCurrentAnimatorStateInfo(0).normalizedTime > loop + rand/9f-0.02f){
                pointAni.speed = 0;
                int rate ;
                if(rand % 4 == 1)
                    rate =2;
                else if(rand % 4 == 3)
                    rate = 5;
                else    
                    rate = 3;
                Debug.Log(rate);
            }
        }
    }
     private IEnumerator  revive(float waitTime){
        isCooldown = true;
        canRevive = false;
        yield return new WaitForSecondsRealtime(waitTime);
        endGame(7);
        isCooldown = false;
    }
    public void resume(){
        StopAllCoroutines();
        cur.SetActive(false);
        isCooldown = false;
        Time.timeScale = 1;
    }
    public void endGame(int rank){
        Time.timeScale = 0;
        if(cur!=null)
            cur.SetActive(false); 
        cur = end;
        cur.SetActive(true); 
        if(rank == 1){
            win.SetActive(true);
        }else{
            lose.SetActive(true);
        }
    }
    
    public void enterMap(){
        cur.SetActive(false);
        pre=menu;
        cur=map;
        cur.SetActive(true);  
    }
    public void chooseMap(int i){
        StaticVar.map=i;
        //pre=cur;
    }
    public void startGame(int i){
        StaticVar.map = i ;
        cur.SetActive(false);
        //SceneManager.LoadSceneAsync("game");
        Instantiate(mapPrefab[StaticVar.map-1]);
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
   
    public void returnMap(){
         SceneManager.LoadScene("UI");
    }
    public void onSetting(){
        pre = menu;
        cur=setting;
        cur.SetActive(true);  
        
    }

    public void onToogleSound(Toggle sound){
        if(sound.isOn){
            soundImg.sprite =soundOn;
            soundToggle.Play("toggleOn");
        }else{
            soundImg.sprite =soundOff;
            soundToggle.Play("toggleOff");
        }
    }
    public void onToogleVibrate(Toggle vibrate){
        if(vibrate.isOn){
            vibrateImg.sprite =vibrateOn;
            vibrateToggle.Play("toggleOn");
        }else{
            vibrateImg.sprite =vibrateOff;
            vibrateToggle.Play("toggleOff");
        }
    }
    public void changeDefaultBrick(int brickNum){
        StaticVar.setDefaultBrick(brickNum);
    }
    public void getMorePrize(){
        
        rand = Random.Range(1,10);
        loop = Random.Range(2,5);
        morePrizeImg.SetActive(false);
        pointAni.Play("pointwheel",0,0f);
        isCooldown = true;
    }
    
}
