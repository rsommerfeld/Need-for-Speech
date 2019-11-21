using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audioskript : MonoBehaviour {
    public GameObject Grafik;
    private float[] data = new float[128];
    private AudioSource aud;
    private bool Run = false;
    private float oldFreq = 0;
    private float oldVolume = 0;
    public float Smoothness = 0.01f;


    //Script MicrophoneInput
    void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.clip = Microphone.Start(Microphone.devices[0], true, 1, 22050);     //aud enthält jeweils 1 Sekunde von Audioaufnahme
        aud.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        aud.Play();

        //aud.Stop();

    }
    // Update is called once per frame
    void Update()
    {
        //aud.clip = Microphone.Start(Microphone.devices[0], false, 1, 44100);
        float[] data = new float[128];
        float fundamentalFrequency = 0.0f;
        aud.GetSpectrumData(data, 0, FFTWindow.BlackmanHarris);

        float s = 0.0f;
        int i = 0;
        float sum = 0;
        for (int j = 0; j < 128; j++)   //bestimmt den höchsten Wert im Array
        {
            if (s < data[j])
            {
                s = data[j];
                i = j;
            }

        }
        fundamentalFrequency = i * 22100 / 128;         //fundamentalFrequency enthält die aktuelle Frequenz.
        float Volume = 15 * LevelMax();
        float zcord = (fundamentalFrequency-500) / 500;
        if (zcord > 1) zcord = 1;

        if(oldFreq < zcord) oldFreq = oldFreq + Smoothness;
        else oldFreq = oldFreq - Smoothness;

        if (oldVolume < zcord) oldVolume = oldVolume + Smoothness;
        else oldVolume = oldVolume - Smoothness;

        Grafik.transform.SetPositionAndRotation(new Vector3(0,oldFreq * 10, (2 * Volume - 10)), Grafik.transform.rotation);
        Debug.Log(fundamentalFrequency + " ; "+Volume); 
    }


    //Bestimmt die Lautstärke des Intervalls (als return)
    float LevelMax()
    {
        AudioClip _clipRecord = new AudioClip();
        _clipRecord = aud.clip;
        float LevelMax = 0;
        float[] waveData = new float[128];
        int micPosition = Microphone.GetPosition(null) - (128 + 1);
        if (micPosition < 0) return 0;
        _clipRecord.GetData(waveData, micPosition);
        for (int i =0;i <128; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if(LevelMax< wavePeak)
            {
                LevelMax = wavePeak;
            }
        }
        return Mathf.Sqrt(Mathf.Sqrt(LevelMax));

    }
}



