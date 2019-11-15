using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class Audioskript : MonoBehaviour {

    private float[] data = new float[128];
    //Script MicrophoneInput
    void Start()
    {
        Debug.Log("Start");
        AudioSource aud = GetComponent<AudioSource>();
        aud.clip = Microphone.Start(Microphone.devices[0], true, 1, 44100);
        aud.loop = true;
        Debug.Log("Vor");
        while (!(Microphone.GetPosition(null) > 0)) { }
        Debug.Log("Nach");
        aud.Play();   
        aud.clip.GetData(data, 0);
        foreach(float f in data)
        Debug.Log(f);
    }
    void Update()
    {
        foreach (float f in data)
         Debug.Log(f);
    }



}
