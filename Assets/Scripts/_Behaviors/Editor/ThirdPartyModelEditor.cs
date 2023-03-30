using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ThirdPartyModel))]
public class ThirdPartyModelEditor : Editor
{
    // https://learn.unity.com/tutorial/editor-scripting#5c7f8528edbc2a002053b5f8
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(10);
        if (GUILayout.Button("Instantiate Model"))
            (target as ThirdPartyModel).InstantiateModel();
    }
}
