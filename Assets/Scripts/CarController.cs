using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {
    private Rigidbody rb;

    public float speedMultiplier = 10;
    public float steeringSensitivity = 1;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();	
	}
	
	// Update is called once per frame
	void Update () {
        float hAxis = Input.GetAxis("Horizontal");
        float speed = Input.GetAxis("Vertical");

        transform.position += transform.forward * Time.deltaTime * speed * speedMultiplier;
        transform.Rotate(new Vector3(0, hAxis * steeringSensitivity, 0));
	}
}
