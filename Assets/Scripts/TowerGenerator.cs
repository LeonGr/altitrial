using UnityEngine;
using System.Collections;

public class TowerGenerator : MonoBehaviour {

    public GameObject towerSegment;
    
	// Use this for initialization
	void Start () {
        AddSegment(0);
        AddSegment(1);
        AddSegment(2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void AddSegment(int floorNumber) {  
        
        // Objects get imported with a 90 degrees angle for some reason... this is a simple workaround
        Quaternion newRotation = transform.rotation;
        newRotation *= Quaternion.Euler(90, 0, 0);
        
        float floorHeight = towerSegment.transform.lossyScale.y;
        float newHeight = transform.position.y + 0.5f * + floorHeight + (float) floorNumber * floorHeight;
        
        
        Vector3 newPosition = new Vector3(transform.position.x, 
                                          newHeight,
                                          transform.position.z);
        
        Instantiate(towerSegment, newPosition, newRotation);
    }
}
