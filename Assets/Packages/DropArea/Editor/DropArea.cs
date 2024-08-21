using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityEditor
{
    public static class DropArea
    {
        public static void Draw(float width, float height, string message, Action<UnityEngine.Object> action)
        {
            var dropArea = GUILayoutUtility.GetRect(width, height, GUILayout.ExpandWidth(float.IsInfinity(width)));

            GUI.Box(dropArea, message);

            if ((Event.current.type != EventType.DragUpdated
              && Event.current.type != EventType.DragPerform)
              || !dropArea.Contains(Event.current.mousePosition))
            {
                return;
            }

            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

            if (Event.current.type == EventType.DragPerform)
            {
                DragAndDrop.AcceptDrag();

                foreach (var droppedObject in DragAndDrop.objectReferences)
                {
                    action(droppedObject);
                }
            }
        }
    }
}