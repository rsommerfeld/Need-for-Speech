using System.Collections;
using System.Collections.Generic;
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

    public float minfreq = -0.35f, avgfreq = 0.0f, maxfreq = 0.35f;
    //public float minfreq = -0.6f, avgfreq = -0.3f, maxfreq = 0f;
    //public float minfreq = -0.6f, avgfreq = 42f, maxfreq = -0.5f;


    //Script MicrophoneInput
    void Start()
    {
        //Debug.Log("Start");
        aud = GetComponent<AudioSource>();
        aud.clip = Microphone.Start(Microphone.devices[0], true, 1, 22050);
        aud.loop = true;
        //Debug.Log("Vor");
        while (!(Microphone.GetPosition(null) > 0)) { }
        //Debug.Log("Nach");
        aud.Play();

        //aud.Stop();

    }
    // Update is called once per frame
    void Update()
    {
        //aud.clip = Microphone.Start(Microphone.devices[0], false, 1, 44100);
        float[] data = new float[128];
        float fundamentalFrequency = 0.0f;

        //Debug.Log(data);
        aud.GetSpectrumData(data, 0, FFTWindow.BlackmanHarris);

        float s = 0.0f;
        int i = 0;
        float sum = 0;
        for (int j = 0; j < 128; j++)
        {
            if (s < data[j])
            {
                s = data[j];
                i = j;
            }

        }
        fundamentalFrequency = i * 22100 / 128;
        float Volume = 15 * LevelMax();
        float zcord = (fundamentalFrequency - 500) / 500;
        if (zcord > 1) zcord = 1;

        if (oldFreq < zcord) oldFreq = oldFreq + Smoothness;
        else oldFreq = oldFreq - Smoothness;

        if (oldVolume < zcord) oldVolume = oldVolume + Smoothness;
        else oldVolume = oldVolume - Smoothness;

        var freq = oldFreq;
        var vol = (2 * Volume - 10) / 10;
        //this.frequency = oldFreq;
        //this.volume = (2 * Volume - 10) / 10;

        float curTime = Time.time;
        volBuffer.Enqueue(vol);
        freqBuffer.Enqueue(freq);
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
        /*Debug.Log("cnt: " + volBuffer.Count);
        Debug.Log("timeheaddelta: " + (curTime-timeBuffer.Peek()));*/
        this.volume = volsum / volBuffer.Count;
        this.frequency = (freqsum / freqBuffer.Count);
        if (this.frequency > this.avgfreq)
        {
            this.frequency = 1 - Mathf.Max(0, this.maxfreq - this.frequency) / (this.maxfreq - this.avgfreq);
        }
        else
        {
            this.frequency = Mathf.Max(this.minfreq, -this.avgfreq + this.frequency) / (this.avgfreq - this.minfreq);
        }
        /*Debug.Log("origfreq "+this.frequency);
        if(this.frequency < this.minfreq)
        {
            this.frequency = -1;
        } else if (this.frequency > this.maxfreq)
        {
            this.frequency = 1;
        } else
        {
            this.frequency = 0;
        }*/
        if (this.volume < 0)
        {
            //this.volume = 
            this.frequency = 0;
        }
        while (timeBuffer.Count > 0 && (curTime - timeBuffer.Peek()) > 0.1)
        {
            timeBuffer.Dequeue();
            volBuffer.Dequeue();
            freqBuffer.Dequeue();
        }

    }

    float LevelMax()
    {
        AudioClip _clipRecord = new AudioClip();
        _clipRecord = aud.clip;
        float LevelMax = 0;
        float[] waveData = new float[128];
        int micPosition = Microphone.GetPosition(null) - (128 + 1);
        if (micPosition < 0) return 0;
        _clipRecord.GetData(waveData, micPosition);
        for (int i = 0; i < 128; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (LevelMax < wavePeak)
            {
                LevelMax = wavePeak;
            }
        }
        return Mathf.Sqrt(Mathf.Sqrt(LevelMax));

    }
}