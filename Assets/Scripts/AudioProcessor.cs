using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class AudioProcessor : MonoBehaviour
{
    AudioSource source;
    public float[] samples = new float[512];
    public float[] freqBands = new float[8];
    public float[] bandBuffer = new float[8];
    float[] bufferDecrease = new float[8];

    float[] _freqBandHighest = new float[8];
    public float[] audioBand = new float[8];
    public float[] audioBandBuffer = new float[8];

    public float amplitude, amplitudeBuffer;

    private float amplitudeHighest = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
    }

    void GetAmplitude()
    {
        float currentAmplitude = 0;
        float currentAmplitudeBuffer = 0;
        for(int i =0; i < 8; i++)
        {
            currentAmplitude += audioBand[i];
            currentAmplitudeBuffer += audioBandBuffer[i];
        }

        if(currentAmplitude > amplitudeHighest)
        {
            amplitudeHighest = currentAmplitude;
        }
        amplitude = currentAmplitude / amplitudeHighest;
        amplitudeBuffer = currentAmplitudeBuffer / amplitudeHighest;
    }

    void CreateAudioBands()
    {
        for(int i = 0; i < 8; i++)
        {
            if(freqBands[i] > _freqBandHighest[i])
            {
                _freqBandHighest[i] = freqBands[i];
            }
            float valBand = freqBands[i] / _freqBandHighest[i];
            if (!float.IsNaN(valBand))
            {
                audioBand[i] = valBand;
            }
            else
            {
                audioBand[i] = 0;
            }
            float valBandBuffer = bandBuffer[i] / _freqBandHighest[i];
            if (!float.IsNaN(valBand))
            {
                audioBandBuffer[i] = valBandBuffer;
            }
            else
            {
                audioBandBuffer[i] = 0;
            }
           
            
        }
    }

    void GetSpectrumAudioSource()
    {
        source.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    void BandBuffer()
    {
        for(int i = 0; i < 8; i++)
        {
            if(freqBands[i] > bandBuffer[i])
            {
                bandBuffer[i] = freqBands[i];
                bufferDecrease[i] = 0.005f;
            }
            if (freqBands[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }
        }
    }

    void MakeFrequencyBands()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i + 1);
            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;
            freqBands[i] = average * 10;
        }
    }
}
