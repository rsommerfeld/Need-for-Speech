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
            //GetComponent<Text>().text = "Volume: " + ((int)(ai.volume*100)/100f).ToString();
            int i = (int)((ai.volume + 1)*5);
            string s = "Volume: ";
            for (int q = 0; q < i; q++) s += "#";
            GetComponent<Text>().text = s;
        }
        else
        {
            Debug.Log("Pitch: " + ai.frequency.ToString());
            //GetComponent<Text>().text = "Pitch: " + ((int)(ai.frequency * 100) / 100f);
            int i = (int)((ai.frequency + 1) * 5);
            string s = "Pitch: ";
            for (int q = 0; q < i; q++) s += "#";
            GetComponent<Text>().text = s;
        }
    }

}
