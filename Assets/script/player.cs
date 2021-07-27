using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class player : MonoBehaviour
{
    public int brickCount,brickDefault,maxScale = 70;
    [Header ("State")]
    public bool onWater=false;
    public bool isHit=false,canKill=true, isKilling=false, passGoal=false,isJump = false;
    
    
    [Header ("Reference")]
    public fov2 fov;
    public GameObject brick,dat,gach;
    public TextMeshPro nameText;
    [Header ("Other")]
    public float cooldown;
    public Vector3 stickPos;
    public GameController gameController;
    [Header ("move")]
    public JoystickPlayerExample pmove; 
    public float maxspeed, timeGetHit;
    int layerMask = 1 << 9;
    public Animator jump;
    [Header ("SFX")]
    public AudioClip[] allSfx;
    public AudioSource Sfx, pickupSfx;
    
     [HideInInspector]
     public float oldspeed,RemainTime,mapYPos;
     public Vector3 lastBridgePos, lastPosOnGround;
     bool stillAlive = false;
     RaycastHit hit;
     
    void Start()
    {
        oldspeed=pmove.speed;
        mapYPos=this.transform.position.y ;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        // số gạch mặc định
        brickDefault = StaticVar.defaultBrick;
        nameText.text= StaticVar.namePlayer[0];
        brick.transform.localScale = new Vector3(0,0,0);
        fov.viewRadius=brickCount * 0.71f + 5;
        changeBrick(brickDefault - brickCount);
        this.GetComponent<Rigidbody>().sleepThreshold = 0.0f;//rigidbody hoạt động khi ko sử dụng
    }

    // Update is called once per frame

    void FixedUpdate()
    {
          if(isJump){
            Sfx.clip = allSfx[1];
        }else if(isHit ){
            Sfx.clip = allSfx[2];
        }else{
            Sfx.clip = allSfx[0];
        }
        if(!Sfx.isPlaying)
            Sfx.Play();
        if(Time.timeScale == 0)
            Sfx.Stop();

        if(isHit){
            //StartCoroutine("ishit");
        }else{
            
            if(onWater==true){
                if(Physics.BoxCast(transform.position+new Vector3(0,3,0), dat.transform.lossyScale/1.5f, new Vector3(0,-1,0), out RaycastHit hit2, transform.rotation, 6,~layerMask)){
                    if(hit2.transform.gameObject.CompareTag("water")){
                        if(brickCount>0){
                            //đặt gạch để đi trên nước
                            changeBrick(-1);
                            placeBridge();
                            //transform.position+=new Vector3(0,1f,0);
                            onWater=false;
                        }
                        else if(brickCount<1){
                    //nhay them 1 doan ngan, neu cham duong thi song ko thì thua
                            if(!jump.enabled){
                                jump.enabled = true;
                                jump.SetBool("isjump", true);
                                jump.Play("jump",0,0f);
                                isJump = true;
                            // changeMove(false);
                            }
                        
                           
                        }
                    }
                }
                
            }
                //destroy
            if(RemainTime<0){
                RemainTime=0;
                canKill=true;
            }else if(canKill==false)
                RemainTime-=Time.deltaTime;
            if(RemainTime < cooldown - timeGetHit)
                isKilling=false;
            if((canKill==true) && (brickCount>1)){}
                //kill();
        }
    }

        public void aniCallback(Vector3 pos){
            //jump.SetBool("isjump", false);
            changeMove(true);
            jump.enabled = false;
            isJump = false;
            
            //if (Physics.Raycast(pos+new Vector3(0,1,0),direction, out hit, 10)){
             if(Physics.BoxCast(transform.position+new Vector3(0,3,0), dat.transform.lossyScale/1.5f, new Vector3(0,-1,0), out hit, transform.rotation, 6,~layerMask))   {
                if(hit.transform.gameObject.CompareTag("water") ){
                    Debug.Log("endgame");
                        stillAlive = false;
                }else {
                    stillAlive = true;
                } 
            }else{
                Debug.Log("endgame");
                    stillAlive = false;
            }
            if(stillAlive){
                onWater=false;
                Vector3 posr = hit.point;
                posr.y = mapYPos;
                transform.position = posr;
            }else{
                transform.position = lastPosOnGround;
                dead();
                changeBrick(5);
            }
        }
    //tang giam gacho.j,.
    public void changeBrick(int i){
        //oldspeed = oldspeed+ i/5f;
        //oldspeed = Mathf.Max(oldspeed,15f);
        //oldspeed = Mathf.Min(oldspeed,maxspeed);
        brickCount+=i;
        if(brickCount<=0) {
            brickCount=0;
            brick.transform.localScale = new Vector3(0,0,0);
             fov.viewRadius=brickCount * 0.71f + 5;
             transform.localScale= new Vector3(1,1,1);
            //Debug.Log("after"+brick.transform.position);
            return;
        }else{
            brick.transform.localScale = new Vector3(brickCount, 1, 1f);
            fov.viewRadius=brickCount * 0.71f + 5;
            if( (brickCount <maxScale))
                transform.localScale += new Vector3(1,1,1)*i/40;
        }
    }
    //dat cau
    public void placeBridge(){
        changeMove(2);
        lastBridgePos=transform.position;
        lastBridgePos.y=mapYPos;// - 0.055f;
        float rotateAngle= Vector3.SignedAngle(transform.forward, Vector3.forward, Vector3.down);
        Instantiate (dat,lastBridgePos, Quaternion.Euler(new Vector3(0, rotateAngle, 0)));
        this.transform.position=lastBridgePos + new Vector3(0,0.055f,0); ;
    
    }
    
    public virtual void dead(){
        Sfx.Stop();
        if(passGoal){
            pmove.ani.SetInteger("end",1);
            int rank = GameObject.FindWithTag("goal").GetComponent<goal>().rank;
            gameController.endGame(rank);
        }else{
            pmove.ani.SetInteger("end",-1);
            gameController.GameOver();
        }
    }
    public void ishit(){
        
        /*for(int i=0;i<brickCount-2;i++){
            Instantiate (gach,transform.position+new Vector3(-0.2f,3,0), Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        changeBrick(-brickCount);
        */
    }
    public virtual void changeMove(bool dk){
        if(!dk) pmove.speed = 0;
        else pmove.speed = oldspeed;
    }
    public virtual float changeMove(float deltaSpeed){
        if(deltaSpeed > 0){
            pmove.speed += deltaSpeed;
            pmove.speed = Mathf.Min(maxspeed, pmove.speed);
        }else
            pmove.speed = oldspeed;
        return pmove.speed;
    }
    
    // bat tat vat ly chieu x z khi va cham vat can
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("obstacle")){
             this.gameObject.GetComponent<Rigidbody>().constraints &= ~(RigidbodyConstraints.FreezePositionX|RigidbodyConstraints.FreezePositionZ);
        }else if(other.gameObject.CompareTag("other") || other.gameObject.CompareTag("Player")){
            if(other.gameObject.GetComponent<player>().brickCount > brickCount){   
                if(!isHit && other.gameObject.GetComponent<player>().canKill && !isJump)
                if(!jump.enabled){
                           // float rotateAngle= Vector3.SignedAngle(-other.transform.forward, Vector3.forward, Vector3.down);
                            //transform.eulerAngles  =new Vector3(0,rotateAngle,0);
                            jump.enabled = true;
                            jump.SetBool("ishit", true);
                            jump.Play("hit",0,0f);
                            changeMove(false);
                            isHit = true;
                            other.gameObject.GetComponent<player>().canKill =false;
                            other.gameObject.GetComponent<player>().isKilling =true;
                            RemainTime = 3;
                        }
                     
                  
            }
              }
    }
    public void afterhit(Vector3 pos){
        this.transform.position = pos;
        
            jump.SetBool("ishit", false);
            changeMove(true);
            jump.enabled = false;
         if(Physics.BoxCast(transform.position+new Vector3(0,3,0), dat.transform.lossyScale/1.5f, new Vector3(0,-1,0), out hit, transform.rotation, 6,~layerMask))   {
                if(hit.transform.gameObject.CompareTag("water") ){
                    Debug.Log("endgame");
                        stillAlive = false;
                }else {
                    stillAlive = true;
                } 
            }else{
                Debug.Log("endgame");
                    stillAlive = false;
            }
            isHit=false;
            if(!stillAlive){
                transform.position = lastPosOnGround;
                changeBrick(5);
                dead();
                
            }
    }
    private void OnCollisionExit(Collision other) {
        if(other.gameObject.CompareTag("obstacle") ){//} || other.gameObject.CompareTag("other") || other.gameObject.CompareTag("Player")){
             this.gameObject.GetComponent<Rigidbody>().constraints |= (RigidbodyConstraints.FreezePositionX|RigidbodyConstraints.FreezePositionZ);
        }
    }
}
