using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOnAmplitude : MonoBehaviour
{
    public AudioProcessor process;

    public float startScale, scaleMultiplier;
    public bool useBuffer;
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
        float value = useBuffer ? process.amplitudeBuffer : process.amplitude;
        transform.localScale = new Vector3((value * scaleMultiplier) + startScale, (value * scaleMultiplier) + startScale, (value * scaleMultiplier) + startScale);
        if (UseColor)
        {
            Color c = new Color(red, green, blue);
            switch (color)
            {
                case ColorOptions.red:
                    c.r = value;
                    break;
                case ColorOptions.green:
                    c.g = value;
                    break;
                case ColorOptions.blue:
                    c.b = value;
                    break;
                case ColorOptions.all:
                    c = new Color(red*value, green*value, blue*value);
                    break;
            }
            mat.color = c;
        }
    }



}
