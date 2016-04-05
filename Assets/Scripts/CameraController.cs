using UnityEngine;
using System.Collections;
public class CameraController : MonoBehaviour {


  
    public GameObject target;
 
    private Vector3 offet;
    
    public float damp = 0.5f;

   // Use this for initialization
   void Start () {

        offet = transform.position;
   }
   

    void LateUpdate()
    {
        Vector3 currentPos = target.transform.position;
       
        float currentHeight = this.transform.position.y;
 
        //wantedHeight should be the height of the car's position plus some distance
        float wantedHeight = target.transform.position.y + 10;
    
        //lerp from currentHeight to wantedHeight
        currentHeight = Mathf.Lerp(currentHeight,wantedHeight, damp * Time.deltaTime);
        
        //add a distance between the car's position and the camera's desired position, in the x coordinate because
        //of how the x/z axes on the cars are reversed
        Vector3 wantedPos = new Vector3(currentPos.x - 10, currentHeight, currentPos.z);
                
        //finally, set the camera's position to its desired position
        this.transform.position = wantedPos;
        
        this.transform.LookAt(target.transform);
      
    }


}