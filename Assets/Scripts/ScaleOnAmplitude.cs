using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAmplitude : MonoBehaviour
{
    public AudioSpectrum process;

    public float startScale, scaleMultiplier;
    [Range(0, 1)]
    public float red, green, blue;
    Material mat;

    public bool UseColor = false;
    public enum ColorOptions {red, green, blue, all };
    public ColorOptions color;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().materials[0];
        //mat.EnableKeyword("EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(value);
        transform.localScale = new Vector3((process.Amplitude * scaleMultiplier) + startScale, (process.Amplitude * scaleMultiplier) + startScale, (process.Amplitude * scaleMultiplier) + startScale);
        if (UseColor)
        {
            Color c = new Color(red, green, blue);
            switch (color)
            {
                case ColorOptions.red:
                    c.r = process.Amplitude;
                    break;
                case ColorOptions.green:
                    c.g = process.Amplitude;
                    break;
                case ColorOptions.blue:
                    c.b = process.Amplitude;
                    break;
                case ColorOptions.all:
                    c = new Color(red* process.Amplitude, green* process.Amplitude, blue* process.Amplitude);
                    break;
            }
            mat.color = c;
        }
    }



}
