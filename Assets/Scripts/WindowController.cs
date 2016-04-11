using UnityEngine;
using System.Collections;

public class WindowController : MonoBehaviour {

    public GameObject brokenWindow;
    public float minimumVelocityForBreaking;

    void OnCollisionEnter(Collision collision) {
        Debug.Log("new collision");
        if (collision.relativeVelocity.magnitude > minimumVelocityForBreaking) {
            Destroy(gameObject);
            Instantiate(brokenWindow, transform.position, transform.rotation * Quaternion.Euler(-90f, 0, 0));
        }
    }
}
