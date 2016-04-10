using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerGenerator : MonoBehaviour {

    public GameObject towerSegment;
    public GameObject ramp;
    
    
    public List<FloorInfo> floorInfos;
    
    
    public void GenerateTower() {
        
        DestroyAllChildren();
        
        // Iterate through the floorInfo's
        for (int i = 0; i < floorInfos.Count; i++){
            AddFloor(i, floorInfos[i]);
        }
    }
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void AddFloor(int floorNumber, FloorInfo floor) {  
                
        float floorHeight = 20f; // hardcoded cause why not
        float newHeight = transform.position.y + (float) floorNumber * floorHeight;
        
        Debug.Log("Floorheight " + newHeight);
        
        Vector3 newPosition = new Vector3(transform.position.x, 
                                          newHeight,
                                          transform.position.z);
        
        GameObject newFloor = (GameObject) Instantiate(towerSegment, newPosition, transform.rotation);
        newFloor.transform.parent = transform;
        
        
        
        if (floor.northSideIsRamp) {
            Vector3 rampPosition = newFloor.transform.position + new Vector3(0f, 0f, 23f);
            Quaternion rampRotation = transform.rotation * Quaternion.Euler(0f, 90f, 0);
            
            GameObject newRamp = (GameObject) Instantiate(ramp, rampPosition, rampRotation);
            newRamp.transform.parent = newFloor.transform;
        }
    }
    
    // Use this before benerating
    void DestroyAllChildren () {
        
        // Iterating through the array backwards, because the childCount changes halfway through
        for (int i = transform.childCount - 1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        
    }
}

[System.Serializable]
public class FloorInfo {
    public bool northSideIsRamp;
    public bool eastSideIsRamp;
    public bool southSideIsRamp;
    public bool westSideIsRamp;
}
