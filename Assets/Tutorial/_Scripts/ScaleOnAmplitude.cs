using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAmplitude : MonoBehaviour
{
    public float startScale, scaleMultiplier;
    public bool useBuffer;
    public float red, green, blue;
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
        float value = useBuffer ? AudioProcessor.amplitudeBuffer : AudioProcessor.amplitude;
        transform.localScale = new Vector3((value * scaleMultiplier) + startScale, (value * scaleMultiplier) + startScale, (value * scaleMultiplier) + startScale);
        Color color = new Color(red*value, green*value, blue*value);
        mat.SetColor("_EmissionColor", color);
    }
}
