using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

    public GameObject player;        //Public variable to store a reference to the player game object
    private Vector3 offset;     //Private variable to store the offset distance between the player and camera
    public float smoothing = 5f;       
    // Use this for initialization
    void Awake () 
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = new Vector3(0,26,-17);
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        Vector3 targetCamPos = player.transform.position + new Vector3(-10*Vector3.Normalize(player.transform.forward).x,10,-10*Vector3.Normalize(player.transform.forward).z);
        transform.position = targetCamPos;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate () 
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        Vector3 targetCamPos = player.transform.position + new Vector3(-30*Vector3.Normalize(player.transform.forward).x,20,-30*Vector3.Normalize(player.transform.forward).z);
        transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
    
}