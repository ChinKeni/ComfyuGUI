using ComfyuGUIEditor.Widgets;
using UnityEngine;
using XNodeEditor;

namespace ComfyuGUIEditor
{
    [NodeGraphEditor.CustomNodeGraphEditor(typeof(ComfyuGUIGraph),"ComfyuGUIGraph.Settings")]
    public class ComfyuGUIEditor : NodeGraphEditor
    {
        public override NodeEditorPreferences.Settings GetDefaultPreferences() {
            return new NodeEditorPreferences.Settings() {
                gridBgColor = new Color(0.1333f,0.1333f,0.1333f),
                gridLineColor = new Color(0.098f,0.098f,0.098f),
            };
        }

        public override void OnGUI()
        {
            base.OnGUI();
            GUILayout.BeginVertical();
            ComfyuGUIEditorMenuBar.Draw();
            ComfyuGUIEditorQuickPanel.Draw();
            GUILayout.EndVertical();
            //每帧都刷新
            NodeEditorWindow.current.Repaint();
        }
    }
}