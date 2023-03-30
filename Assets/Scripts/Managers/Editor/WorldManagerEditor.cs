using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WorldManager))]
public class WorldManagerEditor : Editor
{
    // https://learn.unity.com/tutorial/editor-scripting#5c7f8528edbc2a002053b5f8
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(20);
        if (GUILayout.Button("Regenerate Resources"))
        {
            if (!Application.isPlaying)
                Debug.LogError("Application must be playing to regenerate resources.");
            else
                (target as WorldManager).RegenerateResources();
        }
    }
}
