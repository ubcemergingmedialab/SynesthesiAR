// Audio spectrum component
// By Keijiro Takahashi, 2013
// https://github.com/keijiro/unity-audio-spectrum
using UnityEngine;
using System.Collections;

public class AudioSpectrum : MonoBehaviour
{

    #region Band type definition
    public enum BandType {
        FourBand,
        FourBandVisual,
        EightBand,
        TenBand,
        TwentySixBand,
        ThirtyOneBand
    };

    static float[][] middleFrequenciesForBands = {
        new float[]{ 125.0f, 500, 1000, 2000 },
        new float[]{ 250.0f, 400, 600, 800 },
        new float[]{ 63.0f, 125, 500, 1000, 2000, 4000, 6000, 8000 },
        new float[]{ 31.5f, 63, 125, 250, 500, 1000, 2000, 4000, 8000, 16000 },
        new float[]{ 25.0f, 31.5f, 40, 50, 63, 80, 100, 125, 160, 200, 250, 315, 400, 500, 630, 800, 1000, 1250, 1600, 2000, 2500, 3150, 4000, 5000, 6300, 8000 },
        new float[]{ 20.0f, 25, 31.5f, 40, 50, 63, 80, 100, 125, 160, 200, 250, 315, 400, 500, 630, 800, 1000, 1250, 1600, 2000, 2500, 3150, 4000, 5000, 6300, 8000, 10000, 12500, 16000, 20000 },
    };
    static float[] bandwidthForBands = {
        1.414f, // 2^(1/2)
        1.260f, // 2^(1/3)
        1.414f, // 2^(1/2)
        1.414f, // 2^(1/2)
        1.122f, // 2^(1/6)
        1.122f  // 2^(1/6)
    };
    #endregion

    #region Public variables
    public int numberOfSamples = 1024;
    public BandType bandType = BandType.EightBand;
    public float fallSpeed = 0.08f;
    public float sensibility = 8.0f;

    public int C4;
    public int Csharp;
    public int D4;
    public int Eflat;
    public int E4;
    public int F4;
    public int FSharp;
    public int G4;
    public int GSharp;
    public int A4;
    public int BFlat;
    public int B4;
    #endregion

    #region Private variables
    float[] rawSpectrum;
    float[] levels;
    float[] peakLevels;
    float[] meanLevels;
    float[] maxLevels;
    AudioSource source;
    private float amplitudeHighest = 0.01f;
    private float fMax;
    #endregion

    #region Public property
    public float[] Levels {
        get { return levels; }
    }

    public float[] PeakLevels {
        get { return peakLevels; }
    }
    
    public float[] MeanLevels {
        get { return meanLevels; }
    }

    public float Amplitude { get; set; }
    #endregion

    #region Private functions
    void CheckBuffers ()
    {
        if (rawSpectrum == null || rawSpectrum.Length != numberOfSamples) {
            rawSpectrum = new float[numberOfSamples];
            fMax = AudioSettings.outputSampleRate / 2;
        }
        var bandCount = middleFrequenciesForBands [(int)bandType].Length;
        if (levels == null || levels.Length != bandCount) {
            levels = new float[bandCount];
            peakLevels = new float[bandCount];
            meanLevels = new float[bandCount];
            maxLevels = new float[bandCount];
            for(int i  = 0; i < bandCount; i++)
            {
                maxLevels[i] = 0.01f;
            }
        }
    }

    int FrequencyToSpectrumIndex (float f)
    {
        var i = Mathf.FloorToInt (f / AudioSettings.outputSampleRate * 2.0f * rawSpectrum.Length);
        return Mathf.Clamp (i, 0, rawSpectrum.Length - 1);
    }

    void GetAmplitude()
    {
        float currentAmplitude = 0;
        for (int i = 0; i < levels.Length; i++)
        {
            currentAmplitude += levels[i];
        }

        if (currentAmplitude > amplitudeHighest)
        {
            amplitudeHighest = currentAmplitude;
        }
        Amplitude = currentAmplitude / amplitudeHighest;
    }
    #endregion

    #region Public functions
    /*public float BandVol(float fLow, float fHigh)
    {
        fLow = Mathf.Clamp(fLow, 20, fMax);
        fHigh = Mathf.Clamp(fHigh, fLow, fMax);

        int imin = FrequencyToSpectrumIndex(fLow);
        int imax = FrequencyToSpectrumIndex(fHigh);

        var bandAcc = 0.0f;
        for (var fi = imin; fi <= imax; fi++)
        {
            bandAcc += rawSpectrum[fi];
        }
        return bandAcc / (imax - imin +1);
    }*/

        public void NodeSpectrumIndex()
    {
        C4 = FrequencyToSpectrumIndex(261.6256f);
        Csharp = FrequencyToSpectrumIndex(277.1826f);
        D4 = FrequencyToSpectrumIndex(293.6648f);
        Eflat = FrequencyToSpectrumIndex(311.1270f);
        E4 = FrequencyToSpectrumIndex(329.6276f);
        F4 = FrequencyToSpectrumIndex(349.2282f);
        FSharp = FrequencyToSpectrumIndex(369.9944f);
        G4 = FrequencyToSpectrumIndex(391.9954f);
        GSharp = FrequencyToSpectrumIndex(415.3047f);
        A4 = FrequencyToSpectrumIndex(440.0f);
        BFlat = FrequencyToSpectrumIndex(466.1638f);
        B4 = FrequencyToSpectrumIndex(493.8833f);
    }

    public string MaxNode() //determine the max node from its index
    {
        float maxNodeValue = 0;
        string max = "";
        if (C4 > maxNodeValue)
        {
            maxNodeValue = C4;
            max = "C4";
        }
        if (Csharp > maxNodeValue)
        {
            maxNodeValue = Csharp;
            max = "CSharp";
        }
        if (D4 > maxNodeValue)
        {
            maxNodeValue = D4;
            max = "D4";
        }
        if (Eflat > maxNodeValue)
        {
            maxNodeValue = Eflat;
            max = "Eflat";
        }
        if (E4 > maxNodeValue)
        {
            maxNodeValue = E4;
            max = "E4";
        }
        if (F4 > maxNodeValue)
        {
            maxNodeValue = F4;
            max = "F4";
        }
        if (FSharp > maxNodeValue)
        {
            maxNodeValue = FSharp;
            max = "FSharp";
        }
        if (G4 > maxNodeValue)
        {
            maxNodeValue = G4;
            max = "G4";
        }
        if (GSharp > maxNodeValue)
        {
            maxNodeValue = GSharp;
            max = "GSharp";
        }
        if (A4 > maxNodeValue)
        {
            maxNodeValue = A4;
            max = "A4";
        }
        if (BFlat > maxNodeValue)
        {
            maxNodeValue = BFlat;
            max = "BFlat";
        }
        if (B4 > maxNodeValue)
        {
            maxNodeValue = B4;
            max = "B4";
        }
        return max;
    }
    #endregion //PUBLIC_METHODS

    #region Monobehaviour functions
    void Awake ()
    {
        source = GetComponent<AudioSource>();
        CheckBuffers ();

        NodeSpectrumIndex();
    }

    void Update ()
    {
        CheckBuffers ();

        source.GetSpectrumData (rawSpectrum, 0, FFTWindow.BlackmanHarris);

        float[] middlefrequencies = middleFrequenciesForBands [(int)bandType];
        var bandwidth = bandwidthForBands [(int)bandType];

        var falldown = fallSpeed * Time.deltaTime;
        var filter = Mathf.Exp (-sensibility * Time.deltaTime);

        for (var bi = 0; bi < levels.Length; bi++) {
            int imin = FrequencyToSpectrumIndex (middlefrequencies [bi] / bandwidth);
            int imax = FrequencyToSpectrumIndex (middlefrequencies [bi] * bandwidth);

            var bandAcc = 0.0f;
            for (var fi = imin; fi <= imax; fi++) {
                bandAcc += rawSpectrum [fi];
            }

            maxLevels[bi] = Mathf.Max(maxLevels[bi], bandAcc);
            if(maxLevels[bi] == 0)
            {
                Debug.Log("Ya done fucked up");
            }
            levels [bi] = bandAcc / maxLevels[bi];
            peakLevels [bi] = Mathf.Max (peakLevels [bi] - falldown, levels[bi]);
            meanLevels[bi] = bandAcc - (levels[bi] - meanLevels[bi]) * filter;
        }
        GetAmplitude();


    }
    #endregion
}