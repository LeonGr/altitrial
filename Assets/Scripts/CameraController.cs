using UnityEngine;
using System.Collections;
public class CameraController : MonoBehaviour {


  
    public Transform target;
    public Transform targetCameraPosition;
    public Transform targetCameraLookPosition;
 
    private Vector3 relativeCameraPos;
    
    public float smooth = 1.5f;
  
  
    void FixedUpdate () {
        
        transform.position = Vector3.Lerp(transform.position, targetCameraPosition.position, smooth * Time.deltaTime);
        
        SmoothLookAt();
    }
    
    
    void SmoothLookAt ()
    {
        // Create a vector from the camera towards the player.
        Vector3 relPlayerPosition = targetCameraLookPosition.position - transform.position;
        
        // Create a rotation based on the relative position of the player being the forward vector.
        Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition);
        
        // Lerp the camera's rotation between it's current rotation and the rotation that looks at the player.
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
    }



}