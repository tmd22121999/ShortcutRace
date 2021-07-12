using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using PathCreation;
using System.Linq;
public class enemyAI : MonoBehaviour
{
    private enum State{
        running,
        isHit,
        killing,
        picking,
        shortcut,
    }
    public fov2 fov;
    private enemy thisbody;
    private State state,lastState;
    private Vector3 dir, destination;
    private int leng,startpoint;
    public Transform goal;
    float remainTime,p;
    float[] prority;
    List<Collider> targetPick = new List<Collider>();
    public PathCreator map;
    public Animator ani;
    private NavMeshAgent nav;
    int i;
    private bool tmp = false;
    private void Awake() {
        state=State.running;
    }
    // Start is called before the first frame update
    void Start()
    {
        thisbody=this.GetComponent<enemy>();
        //ani=this.transform.GetChild (0).gameObject.GetComponent<Animator>();
        goal=GameObject.FindWithTag("goal").transform;
        nav = GetComponent<NavMeshAgent>();
        var method1 = typeof(PathCreator).GetMethods();
        map = GameObject.FindWithTag("map").GetComponent<PathCreator>();
        leng = map.path.localPoints.Length;
        remainTime=5;
        nav.updateRotation = false;
    }

    // Update is called once per frame
    private void LateUpdate() {
        if (nav.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(nav.velocity.normalized);
        }
    }
    void Update()
    {
        if((thisbody.isKilling) && (state!=State.killing) && (state!=State.isHit)){
            lastState = state;
            state = State.killing;
        }

        if((thisbody.isHit) && (state!=State.isHit) && (state!=State.killing)){
            lastState = state;
            state=State.isHit;
        }
        
        //Debug.Log(state);
        
        switch(state){
            default:
            case State.running:
                ani.SetBool("isHit",false);
                ani.SetBool("isKilling",false);
                if(!nav.enabled)
                    nav.enabled=true;
                ani.SetBool("isrunning",true);
                running();
                break;
            case State.isHit:
                ani.SetBool("isrunning",false);
                ani.SetBool("isHit",true);
                isHit();
                break;
            case State.killing:
                killing();
                break;
            case State.picking:
                ani.SetBool("isHit",false);
                ani.SetBool("isKilling",false);
                ani.SetBool("isrunning",true);
                if(!nav.enabled)
                    nav.enabled=true;
                picking();
                break;
            case State.shortcut:
                ani.SetBool("isHit",false);
                ani.SetBool("isKilling",false);
                ani.SetBool("isrunning",true);
                shortcut();
                break;
        }
    }
    public int running(){
        //tìm đường đến đích
        if(nav.isOnNavMesh){
            if(!nav.pathPending && nav.remainingDistance<0.5f)
                GoToNextPoint();
        //destination = nav.path.corners;
        }
        
        //kiểm tra đổi state sang đập hoặc bị đập 
        
        // nếu có thể đi tắt thì chuyển sang shortcut
        if(remainTime<0){
            remainTime=0.3f;
            if(p>0)
                p-=Time.deltaTime*0.1f;
            else
                p=0;
            //Debug.Log(p);
            prority=new float[]{thisbody.brickCount*0.2f,(22f-thisbody.brickCount)*0.04f-p,0};
            float rand = Random.value;
            

            if( (thisbody.brickCount>1) && (rand<prority[0]) ){
                Vector3 ab=goal.transform.position - thisbody.transform.position;
                float posibleLeng = thisbody.brickCount*2.3f;
                Vector3 giaodiem= thisbody.transform.position;
                if(posibleLeng<ab.magnitude){
                    giaodiem += ab * posibleLeng/ab.magnitude;
                }else
                {
                    giaodiem = goal.transform.position;
                }
                giaodiem.y=this.transform.position.y;
                //Debug.DrawLine(thisbody.transform.position,giaodiem);
                giaodiem = map.path.GetClosestPointOnPath(giaodiem);
                //if((Vector3.Distance(thisbody.transform.position,goal.transform.position))<(Vector3.Distance(goal.transform.position,giaodiem)))
                //    return 1;
                if(map.path.GetClosestDistanceAlongPath(giaodiem) < map.path.GetClosestDistanceAlongPath(thisbody.transform.position))
                    return 1;
                float distance = Vector3.Distance(thisbody.transform.position,giaodiem);
                    if( (distance < posibleLeng+3) && (distance > 6)){
                        
                        state = State.shortcut;
                        destination = giaodiem;
                        //dir = (giaodiem - thisbody.transform.position)/Vector3.Distance(giaodiem,thisbody.transform.position);
                        return 1;
                    }
            }

        
          //đi theo gahcj hoặc player nếu trong tầm
            {
                Collider[] targetInside = Physics.OverlapSphere (transform.position, 10);
                if(targetInside.Length>0){
                    foreach(var target in targetInside){//random
                        if(target.gameObject.CompareTag("brick") && (rand<prority[1] || Vector3.Distance(transform.position,target.transform.position)<2 )){
                            nav.destination=target.transform.position;
                            state = State.picking;
                            //Debug.Log("nhat do");
                        }
                        if((target.gameObject.CompareTag("Player")) || (target.gameObject.CompareTag("other")) )
                            if((thisbody.brickCount>1) && (thisbody.canKill) && (target.gameObject.GetComponent<player>().brickCount>2) && (rand < Mathf.Min((target.gameObject.GetComponent<player>().brickCount-thisbody.brickCount)*0.02f, 0.8f)))
                                {
                                    Vector3 direct = Vector3.Normalize(target.transform.position -thisbody.transform.position);
                                    float rotateAngle= Vector3.SignedAngle(direct, Vector3.forward, Vector3.down);
                                    thisbody.transform.eulerAngles  = new Vector3(0,rotateAngle,0);
                                    //Debug.Log(rotateAngle);
                                    nav.destination=target.transform.position;
                                    nav.stoppingDistance=thisbody.fov.viewRadius-1;
                                    state=State.killing;
                                    break;
                                }
                    }
                }
            }
 
        }else
        {
             remainTime-=Time.deltaTime;
        }
        return 1;
    }
    public void isHit(){
        //nav.enabled = false;
        //nav.Stop(true) ;//= thisbody.transform.position;
        nav.enabled=false;
        thisbody.StartCoroutine("ishit");
        if(!thisbody.isHit){
            state=lastState;
        }
    }
    public void killing(){
        //nav.destination = thisbody.transform.position;
        
        if(nav.enabled)
            if(nav.remainingDistance<thisbody.fov.viewRadius){
                nav.enabled=false;
        //thisbody.kill();
                thisbody.kill2();
                ani.SetBool("isrunning",false);
                ani.SetBool("isKilling",true);
                tmp=true;
            }
        if(tmp ){//&& (!thisbody.isKilling) ){
            nav.enabled=true;
            state=lastState;
            tmp = false;
        }
    }
    public void picking(){
         if (!nav.pathPending && !nav.hasPath) 
            if(i==2){
                i=0;
                p=0.4f;
                state = State.running;
            }
            if(i==0){
                Collider[] targets = Physics.OverlapSphere (transform.position, 4);
                targetPick= targets.ToList();
                i=1;
            }
            if(i==1)
            //Debug.Log(targetPick.Length);
            if(targetPick.Count>0){
                foreach(var target in targetPick.ToList()){//random
                    if(target.gameObject.CompareTag("brick")){
                        nav.destination=target.transform.position;
                        targetPick.Remove(target);
                        //Debug.Log("nhat do");
                    }else
                    {
                        targetPick.Remove(target);
                    }
                }
            }else
            {
                i=2;
            }
        
    }
    public void GoToNextPoint(){
        Vector3 dest=goal.position;
        dest.y=thisbody.transform.position.y+2;
        nav.destination=dest;
        nav.stoppingDistance=0;
    }
    public void shortcut(){

        nav.enabled=false;
        thisbody.move(destination);
        //Debug.DrawLine(transform.position, destination);
        //Debug.Log(Vector3.Distance(destination,thisbody.transform.position));
        if(Vector3.Distance(destination,thisbody.transform.position)<2f){
        //if(map.path.GetClosestDistanceAlongPath(destination) < map.path.GetClosestDistanceAlongPath(thisbody.transform.position)){
            nav.enabled=true;
            state = State.running;
        }

    }

}
