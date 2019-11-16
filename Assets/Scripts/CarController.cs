using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {
    private Rigidbody rb;

    public AudioInput audioInput;
    public GameObject startPoint;
    
    public float steeringSensitivity = .9f;
    public float maxSteeringAngle = 30;
    [Range(0, 1)]
    public float steeringSpringTension = .4f;
    private Vector3 originalPos;
    private Vector3 originalRot;


    [Range(0, 1)]
    public float friction = .3f;
    public float acceleration = 1;
    public float maxSpeed = 30;

    private float steeringAngle = 0;
    private float speed = 0;

    public bool useMic = true;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        originalPos = transform.position;
        originalRot = transform.eulerAngles;
    }
	
    public float getSpeed()
    {
        return speed;
    }

	// Update is called once per frame
	void Update () {
        float hAxis = useMic ? audioInput.frequency : Input.GetAxis("Horizontal");
        float vAxis = useMic ? audioInput.volume : Input.GetAxis("Vertical");

        steeringAngle += (1 - (Mathf.Abs(steeringAngle) / maxSteeringAngle)) * hAxis * steeringSensitivity;
        steeringAngle *= 1 - steeringSpringTension;

        speed += (1-(Mathf.Abs(speed) / maxSpeed)) * acceleration * vAxis;
        speed *= 1-friction;

        transform.position += transform.forward * Time.deltaTime * speed;
        transform.Rotate(new Vector3(0, steeringAngle , 0));
	}

    public void Reset()
    {
        if (!startPoint)
        {
            transform.position = originalPos;
            transform.eulerAngles = originalRot;
        }
        else
        {
            transform.position = startPoint.transform.position;
            transform.rotation = startPoint.transform.rotation;
        }
    }
}
    