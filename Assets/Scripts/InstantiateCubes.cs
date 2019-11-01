using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCubes : MonoBehaviour
{
    public AudioProcessor process;

    public GameObject cubePrefab;
    static int length = 512;
    private GameObject[] sampleCubes = new GameObject[length];
    public float maxScale;

    // Start is called before the first frame update
    void Start()
    {
        float angles = 360f / length;
        for (int i = 0; i < 512; i++)
        {
            GameObject instance = Instantiate(cubePrefab);
            instance.transform.position = this.transform.position;
            instance.transform.parent = this.transform;
            instance.name = "Sample Cube " + i;
            this.transform.eulerAngles = new Vector3(0, -angles * i, 0);
            instance.transform.position = Vector3.forward * 1;
            sampleCubes[i] = instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 512; i++)
        {
            if(sampleCubes != null)
            {
                sampleCubes[i].transform.localScale = new Vector3(.1f, (process.samples[i] * maxScale) + .1f, .1f);
            }
        }
    }
}
