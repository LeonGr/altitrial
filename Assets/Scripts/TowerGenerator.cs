using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerGenerator : MonoBehaviour {

    public GameObject towerSegment;
    public GameObject ramp; 
    public GameObject window;
    public List<FloorInfo> floorInfos;
    
    // Automatically update tower, which can be quite expensive
    public bool shouldAutoUpdate;
    
    
    private Vector3[] windowPositions = new Vector3[4] {new Vector3(0, 7.5f, 24.9f), new Vector3(24.9f, 7.5f, 0), new Vector3(0, 7.5f, -24.9f), new Vector3(-24.9f, 7.5f, 0)};
    private Quaternion[] windowRotations = new Quaternion[4] {Quaternion.Euler(90f, 0, 0), Quaternion.Euler(90f, 90f, 0), Quaternion.Euler(90f, 180f, 0), Quaternion.Euler(90f, 270f, 0)};
    
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
    
    void AddFloor(int floorNumber, FloorInfo floorInfo) {  
                
        float floorHeight = 20f; // hardcoded cause why not
        
        // Calculate the y position of the bloor based on the floor number
        float yPosition = transform.position.y + (float) floorNumber * floorHeight;
        
        // Use the calculated yPosition to determine the position of the new floor          
        Vector3 newPosition = new Vector3(transform.position.x, yPosition,transform.position.z);
        
        // Create the floor
        GameObject newFloor = (GameObject) Instantiate(towerSegment, newPosition, transform.rotation);
        
        // Make the floor a child of the parent, used for cleaning up old towers
        newFloor.transform.parent = transform;
        
        AddRampsToFloor(newFloor, floorInfo);
        AddWindowsToFloor(newFloor, floorInfo);
    }
    
    void AddWindowsToFloor(GameObject floor, FloorInfo floorInfo) {
        if (floorInfo.hasWindows) {
            for (int i = 0; i < 4; i++)
            {
                Vector3 windowPosition = floor.transform.position + windowPositions[i];
                Quaternion windowRotation = floor.transform.rotation * windowRotations[i];
                
                GameObject newWindow = (GameObject) Instantiate(window, windowPosition, windowRotation);
                newWindow.transform.parent = floor.transform;
            }    
        }
    }
    
    void AddRampsToFloor(GameObject floor, FloorInfo floorInfo) {
        
        if (floorInfo.northSideIsRamp) {
        
            Vector3 rampPosition = floor.transform.position + new Vector3(0f, 0f, 23f);
            Quaternion rampRotation = transform.rotation * Quaternion.Euler(0f, 90f, 0);
            
            GameObject newRamp = (GameObject) Instantiate(ramp, rampPosition, rampRotation);
            newRamp.transform.parent = floor.transform;
        } 
        
        if (floorInfo.eastSideIsRamp) {
        
            Vector3 rampPosition = floor.transform.position + new Vector3(23f, 0f, 0f);
            Quaternion rampRotation = transform.rotation * Quaternion.Euler(0, 180, 0);
            
            GameObject newRamp = (GameObject) Instantiate(ramp, rampPosition, rampRotation);
            newRamp.transform.parent = floor.transform;
        } 
        
        if (floorInfo.southSideIsRamp) {
        
            Vector3 rampPosition = floor.transform.position + new Vector3(0f, 0f, -23f);
            Quaternion rampRotation = transform.rotation * Quaternion.Euler(0f, -90f, 0);
            
            GameObject newRamp = (GameObject) Instantiate(ramp, rampPosition, rampRotation);
            newRamp.transform.parent = floor.transform;
        } 
        
        if (floorInfo.westSideIsRamp) {
        
            Vector3 rampPosition = floor.transform.position + new Vector3(-23f, 0f, 0f);
            Quaternion rampRotation = transform.rotation * Quaternion.Euler(0f, 0, 0);
            
            GameObject newRamp = (GameObject) Instantiate(ramp, rampPosition, rampRotation);
            newRamp.transform.parent = floor.transform;
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
    
    public bool hasWindows;
}
