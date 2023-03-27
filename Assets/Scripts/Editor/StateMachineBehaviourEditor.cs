using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StateMachineBehaviour), true)]
public class StateMachineBehaviourEditor : Editor
{
    // https://learn.unity.com/tutorial/editor-scripting#5c7f8528edbc2a002053b5f8
    public override void OnInspectorGUI()
    {
        var currentState = (target as StateMachineBehaviour).CurrentState;
        GUILayout.Label($"Current State: {currentState}");
        GUILayout.Space(10);
        DrawDefaultInspector();
    }
}
