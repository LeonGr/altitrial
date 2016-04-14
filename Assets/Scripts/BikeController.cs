using UnityEngine;
using UnityEngine.SceneManagement;
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

	public float jumpForce;
    float distToGround;


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
    
    public void Update() {
        if (transform.position.y <= 2f) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        
        if (!isLeftWheel) {
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
		flipCar();
	}

	void flipCar() {
		if (Input.GetAxis("Flip") > 0) {
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 up = transform.TransformDirection(Vector3.up);
            Vector3 left = transform.TransformDirection(Vector3.left);
            Vector3 right = transform.TransformDirection(Vector3.right);
            
            // Check if there is an object directly above the car
            if(Physics.Raycast(transform.position, up, 2)){
                rb.AddForce(new Vector3 (0, jumpForce, 0));
                Vector3 flip = transform.TransformDirection(Vector3.forward);
                rb.AddTorque(flip * 10000f);
            }
            
            else if(Physics.Raycast(transform.position, left, 1)){
                rb.AddForce(new Vector3 (0, jumpForce, 0));
                Vector3 flip = transform.TransformDirection(Vector3.forward);
                rb.AddTorque(flip * -2000f);
            }
           
            else if(Physics.Raycast(transform.position, right, 1)){
                rb.AddForce(new Vector3 (0, jumpForce, 0));
                Vector3 flip = transform.TransformDirection(Vector3.forward);
                rb.AddTorque(flip * 2000f);
            }

		}
	
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
