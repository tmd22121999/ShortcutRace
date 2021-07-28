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
    public Text rankText,scoreText;
    public RectTransform pointer;
    public Animator pointAni;
    public GameObject reviveobj, retry;
    private bool isCooldown = false, canRevive=true;
    [Header ("unlock bonus")]
    public Text unlockText;
    public Image unlockImg,bonusSkinImg;
    public GameObject thanks,xemads;
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
     [Header ("upgrade, shop, coin")]
     public Text lv1;
     public Text lv2, cost1, cost2, coinRewardText;
     private int coinReward;
     public GameObject unlock;
     public shop shopCtl;
     public int text=0;
     public GameObject[] skinB;

    private void Start() {
        Time.timeScale = 0;
        if(SaveData.load() == null)
            chooseMap(StaticVar.map);
        else {
            Data savedata = SaveData.load();
            chooseMap(savedata.map);
            SaveData.save();
        }
        coin.text = StaticVar.coin.ToString();
        lv1.text="Lvl "+(StaticVar.upgrade1+1).ToString();
        lv2.text="Lvl "+(StaticVar.upgrade2+1).ToString();
        cost1.text=((StaticVar.upgrade1+1)*1000).ToString();
        cost2.text=((StaticVar.upgrade2+1)*1000).ToString();
    }
    public void GameOver() {
        pauseButton.enabled = false;
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
            if(pointAni.GetCurrentAnimatorStateInfo(0).IsName("pointwheel"))
            if(pointAni.GetCurrentAnimatorStateInfo(0).normalizedTime > loop + rand/9f){
                pointAni.speed = 0;
                int rate ;
                if(rand % 4 == 1)
                    rate =2;
                else if(rand % 4 == 3)
                    rate = 4;
                else    
                    rate = 3;
                StaticVar.coin +=coinReward * (rate-1);
                coinReward = coinReward * rate;
                coinRewardText.text = "   Get\n"+coinReward.ToString();
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
        pauseButton.enabled = false;
        progress.SetActive(true);
        Time.timeScale = 0;
        if(cur!=null)
            cur.SetActive(false); 
        
        if(rank == 1){
            pre = end;
            cur = unlock;
            cur.SetActive(true);

            if(StaticVar.bonusSkin<6){
                StaticVar.bonus++;
            bonusSkinImg.sprite = shopCtl.skinSprite[StaticVar.bonusSkin];
            unlockText.text=(StaticVar.bonus/4f*100).ToString()+"%";
            unlockImg.fillAmount = StaticVar.bonus/4f;
            if(StaticVar.bonus ==4){
                bonusSkin();
            }
            }
            


            morePrize.enabled = true;
            coinReward =  (50+StaticVar.map*10)*StaticVar.rate;
            StaticVar.coin += coinReward;
            coinRewardText.text ="  Get\n"+ coinReward.ToString();
            coin.text = StaticVar.coin.ToString();
            StaticVar.map ++;
            audioSource.clip = audioClips[0];
            audioSource.Play();
            win.SetActive(true);
            lose.SetActive(false);
        }else{
            cur = end;
            cur.SetActive(true); 
            rankText.text = rank.ToString();
             audioSource.clip = audioClips[1];
            audioSource.Play();
            lose.SetActive(true);
            win.SetActive(false);
        }
    }
    public void skipLevel(){
        progress.SetActive(true);
        Time.timeScale = 0;
        if(cur!=null)
            cur.SetActive(false); 
        cur.SetActive(true); 
            StaticVar.map ++;
            audioSource.clip = audioClips[0];
            audioSource.Play();
            returnMap();
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
        //cur.SetActive(false);
        //cur = menu;
        //cur.SetActive(true);
        //pre=cur;
    }
    public void startGame(){
         text =3;
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
        chooseMap(StaticVar.map);
    }
    public void pauseGame(){
        cur = pause;
        cur.SetActive(true);
        Time.timeScale = 0;
    }
   
    public void returnMap(){
        morePrizeImg.SetActive(true);
        morePrize.enabled = true;
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
        StaticVar.defaultBrick = StaticVar.upgrade2;
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
        Debug.Log(rand);
        morePrizeImg.SetActive(false);
        morePrize.enabled = false;
        pointAni.speed = 1;
        pointAni.Play("pointwheel",0,0f);
        isCooldown = true;
    }
    public void changeName(Text namePlayer){
        StaticVar.namePlayer[0] = namePlayer.text;
        chooseMap(StaticVar.map);
    }
    public void Upgrade2(){
        if(StaticVar.coin >= (StaticVar.upgrade2+1)*1000){
            StaticVar.coin -=(StaticVar.upgrade2+1)*1000;
            StaticVar.upgrade2++;
            coin.text = StaticVar.coin.ToString();
            lv2.text="Lvl "+(StaticVar.upgrade2+1).ToString();
            cost2.text=((StaticVar.upgrade2+1)*1000).ToString();
            StaticVar.defaultBrick = StaticVar.upgrade2;
            chooseMap(StaticVar.map);
        }
    }
    
    public void Upgrade1(){
        if(StaticVar.coin >= (StaticVar.upgrade1+1)*1000){
            StaticVar.coin -=(int)((StaticVar.upgrade1+1)*1000);
            StaticVar.upgrade1++;
            coin.text = StaticVar.coin.ToString();
            lv1.text="Lvl "+(StaticVar.upgrade1+1).ToString();
            cost1.text=((StaticVar.upgrade1+1)*1000).ToString();
            chooseMap(StaticVar.map);
        }
    }
    public void changeSkin(int skinNum){
        if(StaticVar.skinUnlocked[skinNum]>1){
            StaticVar.skinBrick = skinNum;
            shopCtl.updateShop();
             SaveData.save();  

        }else{
            if(StaticVar.skinUnlocked[skinNum]==1){
                if(StaticVar.coin >= StaticVar.skinCost[skinNum]){
                    
                    StaticVar.coin -= StaticVar.skinCost[skinNum];
                    StaticVar.skinUnlocked[skinNum]+=2;
                    StaticVar.skinBrick = skinNum;
                    shopCtl.updateShop();
                    SaveData.save();  
                }
            }else{
                // play ads
                StaticVar.skinBrick = skinNum;
                shopCtl.updateShop();
                 SaveData.save();  
            }
        }
         SaveData.load();  
    }
    public void bonusSkin(){
        StaticVar.bonus =0;
        StaticVar.skinUnlocked[StaticVar.bonusSkin]+=2;
        SaveData.save();
        SaveData.load();
    }
    public void resetSave(){
        SaveData.resetSave();
        updateLoad();
    }
    public void updateLoad(){
        if(SaveData.load() == null)
            chooseMap(StaticVar.map);
        else {
            Data savedata = SaveData.load();
            chooseMap(savedata.map);
            SaveData.save();
        }
        coin.text = StaticVar.coin.ToString();
        lv1.text="Lvl "+(StaticVar.upgrade1+1).ToString();
        lv2.text="Lvl "+(StaticVar.upgrade2+1).ToString();
        cost1.text=((StaticVar.upgrade1+1)*1000).ToString();
        cost2.text=((StaticVar.upgrade2+1)*1000).ToString();
    }
}
