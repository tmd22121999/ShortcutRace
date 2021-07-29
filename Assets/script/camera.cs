using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

    public GameObject player;        //Public variable to store a reference to the player game object

    private Vector3 offset;     //Private variable to store the offset distance between the player and camera
    public float smoothing = 5f;       
    private player pl;
    private void Awake() {
        pl =  GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        transform.LookAt(player.transform.position);
    }
    // LateUpdate is called after Update each frame
    void LateUpdate () 
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        int ratio = 45 + Mathf.Min(pl.brickCount,40)/2;
        Vector3 targetCamPos = player.transform.position + new Vector3(-ratio*Vector3.Normalize(player.transform.forward).x,ratio-25,-ratio*Vector3.Normalize(player.transform.forward).z);
        //transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
        
        if(pl.gameController.text>0){
            //transform.LookAt(player.transform.position+new Vector3(0,12,0));
            var targetRotation = Quaternion.LookRotation(player.transform.position + new Vector3(0,10,0) - transform.position);
       
            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothing * Time.unscaledDeltaTime);

            transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.unscaledDeltaTime);
        }else{
            if(pl.isHit){
                transform.position = targetCamPos*Time.timeScale;
        }
            else{
                transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
        }
    }
    
}