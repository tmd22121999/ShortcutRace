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
    public GameObject menu, cur, pre, setting, shop,progress;
    public GameObject[] progressMap;
    public Text coin;
    public int State; 
    [Header ("End game UI")]
     public GameObject end;

     public GameObject gameOver,win,lose, morePrizeImg;
     public Button morePrize;
     public Image cooldown;
    public TextMeshProUGUI rankText,scoreText;
    public RectTransform pointer;
    public Animator pointAni;
    public GameObject reviveobj, retry;
    private bool isCooldown = false, canRevive=true;
    [Header ("Setting")]
    public Image soundImg;
    public Image vibrateImg;
    public Sprite soundOn, soundOff, vibrateOn, vibrateOff;
    public Animator soundToggle, vibrateToggle; 
    [Header ("game")]
     public GameObject[] mapPrefab;
         int rand,loop;
    public Text startText;
    public Animator startAni;
    public Button pauseButton;
    public GameObject startImg,pause,loadingPic;
    public AudioSource audioSource;
     public AudioClip[] audioClips;

    private void Start() {
        Data savedata = SaveData.load();
        chooseMap(StaticVar.map);
        coin.text = StaticVar.coin.ToString();
    }
    public void GameOver() {
        cur=gameOver;
        cur.SetActive(true);
        Time.timeScale = 0;
        if(canRevive)
            StartCoroutine(revive(5));
        else{
            reviveobj.SetActive(false);
            retry.SetActive(true);
            //endGame(7);
        }
    }
    private void Update() {
        if(isCooldown){
            cooldown.fillAmount -= (1.0f / 5.0f )*Time.unscaledDeltaTime;
            if(win.activeSelf)
            if(pointAni.GetCurrentAnimatorStateInfo(0).normalizedTime > loop + rand/9f){
                pointAni.speed = 0;
                int rate ;
                if(rand % 4 == 1)
                    rate =2;
                else if(rand % 4 == 3)
                    rate = 4;
                else    
                    rate = 3;
                StaticVar.coin +=150 * (rate-1);
                
                coin.text = StaticVar.coin.ToString();
                isCooldown = false;
                Debug.Log(rate);
            }
        }
    }
     private IEnumerator  revive(float waitTime){
        isCooldown = true;
        canRevive = false;
        yield return new WaitForSecondsRealtime(waitTime);
        reviveobj.SetActive(false);
        retry.SetActive(true);
        isCooldown = false;
    }
    public void resume(){
        StopAllCoroutines();
        cur.SetActive(false);
        isCooldown = false;
        Time.timeScale = 1;
    }
    public void endGame(int rank){
        
        progress.SetActive(true);
        Time.timeScale = 0;
        if(cur!=null)
            cur.SetActive(false); 
        cur = end;
        cur.SetActive(true); 
        if(rank == 1){
            morePrize.enabled = true;
            StaticVar.map ++;
            audioSource.clip = audioClips[0];
            audioSource.Play();
            StaticVar.coin += 150;
            coin.text = StaticVar.coin.ToString();
            SaveData.save();
            win.SetActive(true);
            lose.SetActive(false);
        }else{
             audioSource.clip = audioClips[1];
            audioSource.Play();
            lose.SetActive(true);
            win.SetActive(false);
        }
    }
    
    public void enterMap(){
        cur.SetActive(false);
        pre=menu;
        cur=map;
        cur.SetActive(true);
    }
    
    public void enterShop(){
        
        progress.SetActive(false);
        cur.SetActive(false);
        pre=menu;
        cur=shop;
        cur.SetActive(true);
    }
    public void chooseMap(int i){

        StaticVar.map=i;
        progress.SetActive(true);
        for(int j=0;j<4;j++){
            if( j < (StaticVar.map+3) % 4)
                progressMap[j].SetActive(false);
            else 
                progressMap[j].SetActive(true);
        }
        SaveData.save();  
        {
            StartCoroutine(LoadAsynchronously(1));
        }
    
        IEnumerator LoadAsynchronously(int sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
    
            loadingPic.SetActive(true);
    
            while (operation.isDone == false)
            {
                
                yield return null;
            }
            loadingPic.SetActive(false);
        }
        cur.SetActive(false);
        cur = menu;
        cur.SetActive(true);
        //pre=cur;
    }
    public void startGame(){
        int text =3;
        cur.SetActive(false);
        
        progress.SetActive(false);
        StartCoroutine(startCooldown());
        IEnumerator startCooldown( )
        {
            startImg.SetActive(true);
            startText.text = text.ToString();
            startAni.speed = 1;
            while(text>0){
                yield return new WaitForSecondsRealtime(1);
                text--;
                startText.text = text.ToString();
            }
            //startText.SetActive(true);
            //yield return new WaitForSecondsRealtime(3);
            Time.timeScale = 1;
            pauseButton.enabled = true;
            startImg.SetActive(false);
        }

        //SceneManager.LoadSceneAsync("game");
        //pre=cur;
    }
    public void back(){
        if(pre!=null){
            progress.SetActive(true);
            cur.SetActive(false);
            pre.SetActive(true);
            cur=pre;
            pre=null;

        }
    }
    public void pauseGame(){
        cur = pause;
        cur.SetActive(true);
        Time.timeScale = 0;
    }
   
    public void returnMap(){
        reviveobj.SetActive(true);
            retry.SetActive(false);
        StopAllCoroutines();
        canRevive = true ;
        pauseButton.enabled = false;
        cooldown.fillAmount = 1;
        isCooldown = false;
         cur.SetActive(false);
         cur = menu ; 
         cur.SetActive(true);
        StaticVar.setDefaultBrick(0);
         chooseMap(StaticVar.map);
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
        StaticVar.defaultBrick = StaticVar.upgrade2 + brickNum ;
        chooseMap(StaticVar.map);
    }
    public void getMorePrize(){
        
        rand = Random.Range(1,10);
        loop = Random.Range(2,5);
        morePrizeImg.SetActive(false);
        morePrize.enabled = false;
        pointAni.Play("pointwheel",0,0f);
        isCooldown = true;
    }
    public void changeName(Text namePlayer){
        StaticVar.namePlayer[0] = namePlayer.text;
        chooseMap(StaticVar.map);
    }
    public void Upgrade2(){
        if(StaticVar.coin >= 1500){
            StaticVar.coin -=1500;
            StaticVar.upgrade2++;
            chooseMap(StaticVar.map);

        }
    }
}
