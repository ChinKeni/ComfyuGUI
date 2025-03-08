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
                if (ComfyuGUIEditorFileTab.DoTab(entry, () => data.Remove(entry)))
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