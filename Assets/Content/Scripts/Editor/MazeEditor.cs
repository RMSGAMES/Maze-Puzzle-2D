#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(MazeGenerator))]
public class MazeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MazeGenerator t = (MazeGenerator)target;
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Create / Refresh")) t.CreateMap();
        if (GUILayout.Button("Clean")) t.Clear();

        GUILayout.EndHorizontal();
    }
}
#endif