using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {
    private Rigidbody rb;
    
    public float steeringSensitivity = .9f;
    public float maxSteeringAngle = 30;
    [Range(0, 1)]
    public float steeringSpringTension = .4f;

    [Range(0, 1)]
    public float friction = .3f;
    public float acceleration = 1;
    public float maxSpeed = 30;


    private float steeringAngle = 0;
    private float speed = 0;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();	
	}
	
    public float getSpeed()
    {
        return speed;
    }

	// Update is called once per frame
	void Update () {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        steeringAngle += (1 - (Mathf.Abs(steeringAngle) / maxSteeringAngle)) * hAxis * steeringSensitivity;
        steeringAngle *= 1 - steeringSpringTension;

        speed += (1-(Mathf.Abs(speed) / maxSpeed)) * acceleration * vAxis;
        speed *= 1-friction;

        transform.position += transform.forward * Time.deltaTime * speed;
        transform.Rotate(new Vector3(0, steeringAngle , 0));
	}
}
