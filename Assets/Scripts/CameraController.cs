using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject objectToFollow;
    public float offset;
    public float height;
    public float interpolationSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        float interpolation = interpolationSpeed * Time.deltaTime;

        Vector3 position = this.transform.position;

        Vector3 angle = objectToFollow.transform.rotation.ToEulerAngles();

        float newY = objectToFollow.transform.position.y + height;
        //position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x + offsetX, interpolation);
        float newX = -1 * Mathf.Sin(angle.y) * offset + objectToFollow.transform.position.x;
        float newZ = -1 * Mathf.Cos(angle.y) * offset + objectToFollow.transform.position.z;

        position = new Vector3(newX, newY, newZ);
        
        Quaternion targetRotation = objectToFollow.transform.rotation;
        targetRotation.y = Mathf.LerpAngle(this.transform.rotation.y, targetRotation.y, interpolation);



        this.transform.rotation = targetRotation;
        this.transform.position = position;
    }
}
