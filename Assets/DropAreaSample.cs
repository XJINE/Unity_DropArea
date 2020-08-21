using UnityEngine;
using UnityEditor;
using System;

public class DropAreaSample : MonoBehaviour
{
    public int    intField;
    public string stringField;
}

[CustomEditor(typeof(DropAreaSample))]
public class DropAreaSampleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Space();

        DrawDropAreaGUI(float.PositiveInfinity, 100, "Drop Here",
                       (droppedObject) =>
                       {
                           Debug.Log(droppedObject.name);
                       });
    }

    public static void DrawDropAreaGUI(float width, float height, string message,
                                       Action<UnityEngine.Object> action)
    {
        Rect dropArea = GUILayoutUtility.GetRect(width, height,
                        GUILayout.ExpandWidth(float.IsInfinity(width)));

        GUI.Box(dropArea, message);

        if (Event.current.type != EventType.DragUpdated
         && Event.current.type != EventType.DragPerform
         || !dropArea.Contains(Event.current.mousePosition))
        {
            return;
        }

        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

        if (Event.current.type == EventType.DragPerform)
        {
            DragAndDrop.AcceptDrag();

            foreach (UnityEngine.Object droppedObject in DragAndDrop.objectReferences)
            {
                action(droppedObject);
            }
        }
    }
}