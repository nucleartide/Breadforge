using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    // https://learn.unity.com/tutorial/editor-scripting#5c7f8528edbc2a002053b5f8
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var gridManager = target as GridManager;
        if (GUILayout.Button("Regenerate Terrain"))
        {
            if (!Application.isPlaying)
                Debug.LogError("Application must be playing to regenerate terrain.");
            else
                Debug.Log("blah");
                // gridManager.RegenerateTerrain();
        }
    }
}
