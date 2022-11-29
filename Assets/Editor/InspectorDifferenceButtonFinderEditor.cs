using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InspectorDifferenceButtonWriter))]
public class InspectorDifferenceButtonFinderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        InspectorDifferenceButtonWriter myScript = (InspectorDifferenceButtonWriter)target;
        if (GUILayout.Button("Log Button Info"))
        {
            myScript.LogAllButtonsInfo();
        }
    }
}