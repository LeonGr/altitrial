using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BikeController : MonoBehaviour {
    
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
   
   
    public float maxSpeed;
    public float accelerationMultiplier;
    
    public float defaultDrag;
    public float defaultAngularDrag;
    public float turboDrag;
    public float turboAngularDrag;


    public void Start() {
        
        // Fix jittery car movement by changing sub-stepping
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.rightWheel.ConfigureVehicleSubsteps(5, 12, 15);
                axleInfo.leftWheel.ConfigureVehicleSubsteps(5, 12, 15);
            }
            
            if (axleInfo.motor) {
                axleInfo.rightWheel.ConfigureVehicleSubsteps(5, 12, 15);
                axleInfo.leftWheel.ConfigureVehicleSubsteps(5, 12, 15);            
            }
        }
        
        
    }

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider, bool isLeftWheel) {
        if (collider.transform.childCount == 0) {
            return;
        }
        
        Transform visualWheel = collider.transform.GetChild(0);
    
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        
        if (isLeftWheel) {
            rotation *= Quaternion.Euler(0, -90, 0);    
        } else {
            rotation *= Quaternion.Euler(0, 90, 0);
        }
        
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
        
            ApplyLocalPositionToVisuals(axleInfo.leftWheel, true);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel, false);
        }
        
        
        ApplyTurbo();
	}
    
    void ApplyTurbo () {
        Rigidbody rb = GetComponent<Rigidbody>();
        
        if (Input.GetAxis("Boost") > 0) {
            rb.drag = turboDrag;
            rb.angularDrag = turboAngularDrag;
            
            rb.velocity *= 1.01f;
        } else {
            rb.drag = defaultDrag;
            rb.angularDrag = defaultAngularDrag;
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