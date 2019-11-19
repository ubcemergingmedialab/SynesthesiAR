using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleAudio : MonoBehaviour
{
    [SerializeField]
    private AudioProcessor process = default;

    [SerializeField, Tooltip("Max value of particles, if all at once is checked. If amplitude buffer = 1, this will be emitted. Otherwise, multiplier for emission speed")]
    private int maxParticlesOrEmissionSpeedMultiplier = default;

    [SerializeField]
    private bool allAtOnce = false;

    private ParticleSystem particles;
    private ParticleSystem.MainModule main;
    private ParticleSystem.EmissionModule em;
    private ParticleSystem.EmitParams emitParams;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        main = particles.main;
        em = particles.emission;
        emitParams = new ParticleSystem.EmitParams();
        main.loop = true;
    }

    private void Update()
    {     
        if (allAtOnce)
        {
            if (process.amplitudeBuffer > 0.2f)
            {
                int numParticles = (int)(maxParticlesOrEmissionSpeedMultiplier * process.amplitudeBuffer);
                particles.Emit(emitParams, numParticles);
            }
        }
        else
        {
            em.rateOverTime = process.amplitudeBuffer * maxParticlesOrEmissionSpeedMultiplier;
        }
    }

    private void SetColor(Color color)
    {
        main.startColor = color;
    }
}
