using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CurveDomain))]
public class CurveDomainEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CurveDomain curveDomain = (CurveDomain)target;


        EditorGUILayout.Space();
       
        EditorGUILayout.HelpBox("-0.013 is max to curve the terrain high, -0.009 is mid high. 0.012 is max to curve it low, 0.008 is the mid low", MessageType.Info);

        EditorGUILayout.Space();

        // Call the default Inspector GUI for the remaining variables
        DrawDefaultInspector();
    
    }
}
