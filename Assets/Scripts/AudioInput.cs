using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

using Pitch;

[RequireComponent(typeof(AudioSource))]
public class AudioInput : MonoBehaviour
{
    public GameObject Grafik;
    private float[] data = new float[128];
    private AudioSource aud;
    private bool Run = false;
    private float oldFreq = 0;
    private float oldVolume = 0;
    public float Smoothness = 0.01f;

    private Thread t;
    private bool isRunning = false;

    private float pitch;

    //Script MicrophoneInput
    void Start()
    {
        Debug.Log("Start");
        aud = GetComponent<AudioSource>();
        aud.clip = Microphone.Start(Microphone.devices[0], true, 1, 22050);
        aud.loop = true;
        Debug.Log("Vor");
        while (!(Microphone.GetPosition(null) > 0)) { }
        Debug.Log("Nach");
        aud.Play();

        //aud.Stop();

    }
    // Update is called once per frame
    void Update()
    {
        //aud.clip = Microphone.Start(Microphone.devices[0], false, 1, 44100);
        float[] data = new float[128];
        float fundamentalFrequency = 0.0f;

        if (!isRunning)
        {
            if (t != null)
            {
                t.Join();
                Debug.Log(pitch);
            }
            t = new Thread(new ThreadStart(GetPitchInThread));
            t.Start();
        }

        // Debug.Log(pitchTracker.CurrentPitchRecord.Pitch);

        //Grafik.transform.SetPositionAndRotation(new Vector3(0, oldFreq * 10, 0), Grafik.transform.rotation);
    }

    void GetPitchInThread()
    {
        isRunning = true;
        float[] buffer = new float[256];
        aud.GetOutputData(buffer, 0);

        PitchTracker pitchTracker = new PitchTracker();
        pitchTracker.SampleRate = 44100;
        pitchTracker.ProcessBuffer(buffer);
        pitch = pitchTracker.CurrentPitchRecord.Pitch;
        Thread.Sleep(10);
        isRunning = false;
    }
}