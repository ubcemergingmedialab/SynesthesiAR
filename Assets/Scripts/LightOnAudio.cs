﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Light))]
public class LightOnAudio : MonoBehaviour
{

    public AudioProcessor process;
    private Light l;

    public int band;
    public float minIntensity;
    public float maxIntensity;

    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        l.intensity = (process.audioBand[band] * (maxIntensity - minIntensity)) + minIntensity;
    }
}
