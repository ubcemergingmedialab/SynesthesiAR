using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// For the "AmpSphere" prefab - sets all paracube objects in the children to use the same Audio Processor as the parent sphere
/// </summary>
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
            Undo.RecordObject(p, "Paracube");
            p.process = body.process;
            
            PrefabUtility.RecordPrefabInstancePropertyModifications(p.process);
        }
        PrefabUtility.RecordPrefabInstancePropertyModifications(t);

    }
}
