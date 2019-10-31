using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SetAudioProcessor : EditorWindow
{
    [MenuItem("CONTEXT/ScaleOnAmplitude/SetChildren")]
    static void SetChildren(MenuCommand command)
    {
        ScaleOnAmplitude body = (ScaleOnAmplitude)command.context;
        Transform t = body.transform;

        ParaCube[] children = t.GetComponentsInChildren<ParaCube>();

        foreach(ParaCube p in children)
        {
            p.process = body.process;
        }
    }
}
