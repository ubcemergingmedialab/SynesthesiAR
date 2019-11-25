using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaCube : MonoBehaviour
{
    public AudioSpectrum process = default;

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
        float value = process.PeakLevels[band];
        transform.localScale = new Vector3(transform.localScale.x, (value * scaleMultiplier) + startScale, transform.localScale.z);
        float val = Mathf.Clamp(value, 0.0f, 0.2f);
        Color color = new Color(val, val, val);
        mat.SetColor("_EmissionColor", color);
    }
}
