using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapDisplay))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MapDisplay MapDisplay = (MapDisplay)target;

        if (GUILayout.Button("Generate texture"))
        {
            MapDisplay.CreateTexture();
        }
    }

}
