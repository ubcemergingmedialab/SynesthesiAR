using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaCube : MonoBehaviour
{
    public AudioSpectrum process;

    public int band;
    public float startScale, scaleMultiplier;
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().materials[0];
        mat.EnableKeyword("EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        float value = process.Levels[band];
        transform.localScale = new Vector3(transform.localScale.x, (value * scaleMultiplier) + startScale, transform.localScale.z);
        Color color = new Color(value, value, value);
        mat.SetColor("_EmissionColor", color);
    }
}
