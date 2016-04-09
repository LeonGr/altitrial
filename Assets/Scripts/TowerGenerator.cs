using UnityEngine;
using System.Collections;

public class TowerGenerator : MonoBehaviour {

    public GameObject towerSegment;
    
	// Use this for initialization
	void Start () {
        AddSegment();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void AddSegment() {
        
        // Objects get imported with a 90 degrees angle for some reason... this is a simple workaround
        Quaternion newRotation = transform.rotation;
        newRotation *= Quaternion.Euler(90, 0, 0);
        Instantiate(towerSegment, transform.position, newRotation);
    }
}
