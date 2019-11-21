using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSpectrum process = default;

    [SerializeField, Tooltip("Max value of particles, if all at once is checked. If amplitude buffer = 1, this will be emitted. Otherwise, multiplier for emission speed")]
    private int maxParticlesOrEmissionSpeedMultiplier = default;

    [SerializeField]
    private bool allAtOnce = false;
    [SerializeField]
    private bool massiveBurst = false;

    private ParticleSystem particles;
    private ParticleSystem.MainModule main;
    private ParticleSystem.EmissionModule em;
    private ParticleSystem.EmitParams emitParams;
    ParticleSystem.Particle[] particlesBuffer;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        main = particles.main;
        em = particles.emission;
        emitParams = new ParticleSystem.EmitParams();
        main.loop = true;
    }

    private void LateUpdate()
    {     
        if (allAtOnce)
        {
            if (process.Amplitude > 0.2f)
            {
                if (massiveBurst)
                {
                    int numParticles = (int)(maxParticlesOrEmissionSpeedMultiplier * process.Amplitude);
                    particles.Emit(emitParams, numParticles);
                }
                else
                {
                    particles.Emit(emitParams, 8);

                    //Todo: write particles to buffer here, and have a coroutine where they change based on the band arrays
                }
            }
        }
        else
        {
            em.rateOverTime = process.Amplitude * maxParticlesOrEmissionSpeedMultiplier;
        }
    }

    private void SetColor(Color color)
    {
        main.startColor = color;
    }
}
