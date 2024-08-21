using UnityEngine;
using UnityEditor;

public class Sample : MonoBehaviour
{
    public int    intField;
    public string stringField;
}

[CustomEditor(typeof(Sample))]
public class DropAreaSampleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        EditorGUILayout.Space();

        DropArea.Draw(float.PositiveInfinity, 100, "Drop Here", (droppedObject) =>
        {
            Debug.Log(droppedObject.name);
        });

        serializedObject.ApplyModifiedProperties();
    }
}