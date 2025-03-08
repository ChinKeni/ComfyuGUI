using System.Collections.Generic;
using ComfyuGUIEditor.Internal;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace ComfyuGUIEditor
{
    [NodeGraphEditor.CustomNodeGraphEditor(typeof(ComfyuGUIGraph))]
    public class ComfyuGUIEditorWindow : NodeEditorWindow
    {
        public static Dictionary<string,ComfyuGUIGraph> openList;
        
        [MenuItem("ComfyuGUI/Editor Window")]
        static void OpenWindow()
        {
            var w = GetWindow(typeof(ComfyuGUIEditorWindow), false, ComfyuGUIVars.Name, true) as ComfyuGUIEditorWindow;
            if (w == null) return;
            w.wantsMouseMove = true;
            var newGraph = ScriptableObject.CreateInstance<ComfyuGUIGraph>();
            w.graph = newGraph;
            openList = new Dictionary<string, ComfyuGUIGraph>
            {
                ["未命名 1"] = newGraph
            };
        }
        
    }
}