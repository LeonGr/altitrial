using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerGenerator : MonoBehaviour {

    public GameObject towerSegment;
    
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
        
        // Objects get imported with a 90 degrees angle for some reason... this is a simple workaround
        Quaternion newRotation = transform.rotation;
        newRotation *= Quaternion.Euler(90, 0, 0);
        
        float floorHeight = towerSegment.transform.lossyScale.y;
        float newHeight = transform.position.y + 0.5f * + floorHeight + (float) floorNumber * floorHeight;
        
        
        Vector3 newPosition = new Vector3(transform.position.x, 
                                          newHeight,
                                          transform.position.z);
        
        GameObject newFloor = (GameObject) Instantiate(towerSegment, newPosition, newRotation);
        newFloor.transform.parent = transform;
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
