using System.Collections.Generic;
using System.Linq;
using ComfyuGUIEditor.Internal;
using UnityEditor;
using UnityEngine;

namespace ComfyuGUIEditor.Widgets
{
    public class ComfyuGUIEditorFileBar
    {
        public static void Draw(List<ComfyuGUIGraph> data)
        {
            if(data==null)return;
            GUILayout.BeginHorizontal(EditorStyles.toolbar, GUILayout.ExpandWidth(false),
                GUILayout.Height(ComfyuGUIVars.FileBarSize));

            foreach (var entry  in data.ToList())
            {
                var isOpen = ComfyuGUIEditorWindow.currentComfyuGUIGraph == entry;
                if (ComfyuGUIEditorFileTab.DoTab(entry, () => data.Remove(entry),isOpen))
                {
                    ComfyuGUIEditorWindow.SwitchComfyuGUIGraph(entry);
                };
            }

            if (GUILayout.Button("+"))
            {
                ComfyuGUIEditorWindow.OpenNewComfyuGUIGraph();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}