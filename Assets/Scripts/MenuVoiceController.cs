using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuVoiceController : MonoBehaviour {

    public GameObject audioController;
    private AudioInput ai;

    public Transform btn1, btn2, btn3;

    // Use this for initialization
    void Start ()
    {
        this.ai = audioController.GetComponent(typeof(AudioInput)) as AudioInput;
        Debug.Log(ai);
    }
	
	// Update is called once per frame
	void Update ()
    {
        int c = gameObject.GetComponentsInChildren<Transform>().Length-2;
        //Debug.Log("c "+c);
        float dist = 3f / c;
        //Debug.Log("dist " + dist);
        int val = (int)( (ai.volume+1)/dist );
        switch (val)
        {
            case 0:
                btn1.GetComponentInChildren<CarButtonListener>().Press();
                break;
            case 1:
                btn2.GetComponentInChildren<CarButtonListener>().Press();
                break;
            case 2:
                btn3.GetComponentInChildren<CarButtonListener>().Press();
                break;
        }
        //gameObject.GetComponentsInChildren<Transform>()[val].GetComponentInChildren<CarButtonListener>().Press();
    }
}
