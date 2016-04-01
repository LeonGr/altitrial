using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BikeController : MonoBehaviour {
    
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;


    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider) {
        if (collider.transform.childCount == 0) {
            return;
        }
        
        Transform visualWheel = collider.transform.GetChild(0);
    
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        rotation *= Quaternion.Euler(0, 90, 0);
        
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
      

	void FixedUpdate () {
	    float motorInput = maxMotorTorque * Input.GetAxis("Vertical");
        float steeringInput = maxSteeringAngle * Input.GetAxis("Horizontal");
        
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.rightWheel.steerAngle = steeringInput;
                axleInfo.leftWheel.steerAngle = steeringInput;
            }
            
            if (axleInfo.motor) {
                axleInfo.rightWheel.motorTorque = motorInput;
                axleInfo.leftWheel.motorTorque = motorInput;
            }
        
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
	}
}
[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}