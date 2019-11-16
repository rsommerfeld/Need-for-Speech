using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPropertyViewer : MonoBehaviour {

    public GameObject audioController;
    public bool volume = true;
    private AudioInput ai;

    // Use this for initialization
    void Start ()
    {
        this.ai = audioController.GetComponent(typeof(AudioInput)) as AudioInput;
        Debug.Log(ai);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (volume)
        {
            Debug.Log("Volume: " + ai.volume.ToString());
            GetComponent<Text>().text = "Volume: " + ai.volume.ToString();
        }
        else
        {
            Debug.Log("Pitch: " + ai.frequency.ToString());
            GetComponent<Text>().text = "Pitch: " + ai.frequency.ToString();
        }
    }

}
