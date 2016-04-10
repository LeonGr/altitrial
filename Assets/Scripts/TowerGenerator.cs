using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerGenerator : MonoBehaviour {

    public GameObject towerSegment;
    
    public List<FloorInfo> floorInfos;
    
    
	// Use this for initialization
	void Start () {
        
        // Iterate through the floorInfo's
        for (int i = 0; i < floorInfos.Count; i++){
            AddFloor(i, floorInfos[i]);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void AddFloor(int floorNumber, FloorInfo floor) {  
        
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

[System.Serializable]
public class FloorInfo {
    public bool northSideIsRamp;
    public bool eastSideIsRamp;
    public bool southSideIsRamp;
    public bool westSideIsRamp;
}
