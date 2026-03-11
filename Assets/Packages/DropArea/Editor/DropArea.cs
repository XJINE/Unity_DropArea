using System;
using UnityEngine;

namespace UnityEditor
{
    public static class DropArea
    {
        public static void Draw(float width, float height, string message, Action<UnityEngine.Object> action)
        {
            var dropArea = GUILayoutUtility.GetRect(width, height, GUILayout.ExpandWidth(float.IsInfinity(width)));

            GUI.Box(dropArea, message);

            Draw(GUILayoutUtility.GetLastRect(), action);
        }

        public static void Draw(Rect dropArea, Action<UnityEngine.Object> action)
        {
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