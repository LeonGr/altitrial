using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BikeController : MonoBehaviour {
    
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;

	void FixedUpdate () {
	    float motorInput = maxMotorTorque * Input.GetAxis("Vertical");
        float steeringInput = maxSteeringAngle * Input.GetAxis("Horizontal");
        
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.wheel.steerAngle = steeringInput;
            }
            
            if (axleInfo.motor) {
                axleInfo.wheel.motorTorque = motorInput;
            }
        }
	}
}
[System.Serializable]
public class AxleInfo {
    public WheelCollider wheel;
    public bool motor;
    public bool steering;
}