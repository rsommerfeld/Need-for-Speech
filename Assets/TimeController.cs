using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

    private float _timeStart;
    private float _lastTime = 0;

    private bool _running = false;
    public bool Running { get { return _running; } }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float getTime()
    {
        return _running ? Time.time - _timeStart : _lastTime;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "CarPrefab")
        {
            if (_running)
            {
                _lastTime = Time.time - _timeStart;
                other.gameObject.GetComponent<CarController>().Reset();
            } else
            {
                _timeStart = Time.time;
            }
            _running = !_running;
        }
    }
}
