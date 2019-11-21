using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioInput : MonoBehaviour
{
    //public GameObject Grafik;
    private float[] data = new float[128];
    private AudioSource aud;
    private bool Run = false;
    private float oldFreq = 0;
    private float oldVolume = 0;
    public float Smoothness = 0.01f;

    public float volume = 0, frequency = 0;

    private Queue<float> freqBuffer = new Queue<float>();
    private Queue<float> volBuffer = new Queue<float>();
    private Queue<float> timeBuffer = new Queue<float>();

    public const int minfreq = 100, maxfreq = 1100;
    public int maxMeasuredFreq = 5120;
    int maxI;
    //public float minfreq = -0.6f, avgfreq = -0.3f, maxfreq = 0f;
    //public float minfreq = -0.6f, avgfreq = 42f, maxfreq = -0.5f;


    //Script MicrophoneInput
    void Start(){

        aud = GetComponent<AudioSource>();
        aud.clip = Microphone.Start(Microphone.devices[0], true, 1, maxMeasuredFreq);
        aud.loop = true;

        while (!(Microphone.GetPosition(null) > 0)) { }
        aud.Play();

        //initialize maxI (which is used for measuring the frequency)
        maxI = 1 + ((maxMeasuredFreq * 512) / AudioSettings.outputSampleRate);
    }

    // Update is called once per frame
    void Update(){
        
        float frequency = getFrequency();
        float volume = getVolume();

        float normalizedVolume = getNormalizedVolume(volume);
        float normalizedFrequency = getNormalizedFrequency(frequency);



        //Debug.Log("Freq: " + normalizedFrequency + " (" + frequency + ") Vol: " + normalizedVolume + " (" + volume + ")");

        //TRANSFER
        float curTime = Time.time;
        volBuffer.Enqueue(normalizedVolume);
        freqBuffer.Enqueue(normalizedFrequency);
        timeBuffer.Enqueue(curTime);

        float volsum = 0;
        foreach (float v in volBuffer)
        {
            volsum += v;
        }
        float freqsum = 0;
        foreach (float v in freqBuffer)
        {
            freqsum += v;
        }

        this.volume = volsum / volBuffer.Count;
        this.frequency = (freqsum / freqBuffer.Count);
        

        if (this.volume < 0)
        {
            this.frequency = 0;
        }
        while (timeBuffer.Count > 0 && (curTime - timeBuffer.Peek()) > 0.1)
        {
            timeBuffer.Dequeue();
            volBuffer.Dequeue();
            freqBuffer.Dequeue();
        }
    }


    //produces values between -100 and 100
    float getNormalizedVolume(float volume){
        if(volume < -90) volume = -90; //to rule out infinity

        float percentage = (volume + 90) * 200;
        percentage = percentage / 90;
        percentage = percentage - 100; //percentage should be between -10 and 100 now

        return percentage / 100;
    }


    //returns values between -90 and 0, while 0 is the maximum
    float getVolume(){

        AudioClip _clipRecord = new AudioClip();
        _clipRecord = aud.clip;

        float LevelMax = 0;
        float original = 0;

        float[] waveData = new float[128];
        int micPosition = Microphone.GetPosition(null) - (128 + 1);
        if (micPosition < 0) return 0;

        _clipRecord.GetData(waveData, micPosition);

        for (int i = 0; i < 128; i++) {

            float wavePeak = waveData[i] * waveData[i];
            if (LevelMax < wavePeak){

                LevelMax = wavePeak;
                original = waveData[i];

            }
        }

            float db = 20 * Mathf.Log10(Mathf.Abs(original));
            return db;
    }


    //produces values between -100 and 100
    float getNormalizedFrequency(float frequency){

        if(frequency > maxfreq) frequency = maxfreq;
        if(frequency < minfreq) frequency = minfreq;

        frequency = frequency - minfreq;
        float difference = maxfreq - minfreq;

        float percentage = 200 * frequency;
        percentage = percentage / difference;
        percentage = percentage - 100;

        return percentage / 100;
    }

    //returns the single loudest frequency of the measured spectrum
    float getFrequency(){

        float[] data = new float[512];
        float fundamentalFrequency = 0.0f;

        //Debug.Log(data);
        aud.GetSpectrumData(data, 0, FFTWindow.BlackmanHarris);

        float s = 0.0f;
        int i = 0;
        float sum = 0;
        for (int j = 0; j < maxI; j++)
        {
            if (s < data[j])
            {
                s = data[j];
                i = j;
            }
        }
        fundamentalFrequency = i * AudioSettings.outputSampleRate / 1024;
        return fundamentalFrequency;
    }
}