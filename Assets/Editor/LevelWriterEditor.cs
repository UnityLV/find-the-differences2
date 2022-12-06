using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelWriter))]
public class LevelWriterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LevelWriter myTarget = (LevelWriter)target;

        
        if (GUILayout.Button("Write"))
        {
            myTarget.WriteLvel();
        }
    }
}
