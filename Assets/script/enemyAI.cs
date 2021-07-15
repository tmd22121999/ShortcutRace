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
    //float remainTime,p;
    public float[] prority;
    List<Collider> targetPick = new List<Collider>();
    public PathCreator map;
    public Animator ani;
    private NavMeshAgent nav;
    int i;
    //private bool tmp = false;
    private void Awake() {
        state=State.running;
    }
    // Start is called before the first frame update
    void Start()
    {
        thisbody=this.GetComponent<enemy>();
        goal=GameObject.FindWithTag("goal").transform;
        nav = GetComponent<NavMeshAgent>();
        var method1 = typeof(PathCreator).GetMethods();
        map = GameObject.FindWithTag("map").GetComponent<PathCreator>();
        leng = map.path.localPoints.Length;
        destination =  map.path.GetPointAtDistance(20);
    }

    
    void FixedUpdate()
    {
        
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
                ani.SetBool("isrunning",true);
                running();
                break;
            case State.isHit:
                ani.SetBool("isrunning",false);
                ani.SetBool("isHit",true);
                isHit();
                break;
            
        }
        
        //kiểm tra đổi state sang đập hoặc bị đập 
        
        // nếu có thể đi tắt thì chuyển sang shortcut
        if((Vector3.Distance(thisbody.transform.position,destination) < 2) || map.path.GetClosestDistanceAlongPath(destination) < (map.path.GetClosestDistanceAlongPath(thisbody.transform.position))){
            float rand = Random.value;
            

            if( (thisbody.brickCount>1) && (rand<prority[0])  ){
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
                
                if(map.path.GetClosestDistanceAlongPath(giaodiem) < map.path.GetClosestDistanceAlongPath(thisbody.transform.position))
                    return;
                float distance = Vector3.Distance(thisbody.transform.position,giaodiem);
                    if( (distance < posibleLeng+3) && (distance > 6)){
                        destination = giaodiem;
                        return;
                    }
            }

        
          //đi theo gahcj hoặc player nếu trong tầm
            {
                
                 RaycastHit target;
                if(Physics.BoxCast(transform.position, new Vector3(12,12,12), transform.forward, out target, Quaternion.Euler(0, 0, 0), 20, 1 << 10)){
                        if( (target.transform.tag == "brick")&& (rand<prority[1] || Vector3.Distance(transform.position,target.point)<2 )){
                            destination=target.point;
                            return;
                        }
                  }
                //}
            }
            if(Vector3.Distance(transform.position,goal.transform.position) >15){
                float dis = map.path.GetClosestDistanceAlongPath(thisbody.transform.position);
                destination =  map.path.GetPointAtDistance(dis + 20);

            }
        }

    }
    public void running(){
        
        Debug.DrawLine(thisbody.transform.position, destination);
        thisbody.transform.position+=thisbody.transform.forward * thisbody.speed * Time.fixedDeltaTime;
        GoToNextPoint();
        
        
    }
    public void isHit(){
        //nav.enabled=false;
        if(!thisbody.isHit){
            state=lastState;
        }
    }
    
    public void GoToNextPoint(){
        //Vector3 dest=goal.position;
        //dest.y=thisbody.mapYPos;
//        Debug.Log(destination);
        Vector3 direction = destination - thisbody.transform.position;
        direction = Vector3.Normalize(direction);
        float angle = Vector3.SignedAngle(direction, thisbody.transform.forward, Vector3.up);
        if( angle > 0.5f){
            thisbody.transform.Rotate(0.0f,-2 , 0.0f, Space.World);
        }else if( angle < -0.5f){
            thisbody.transform.Rotate(0.0f,2 , 0.0f, Space.World);
        }
    }
}
