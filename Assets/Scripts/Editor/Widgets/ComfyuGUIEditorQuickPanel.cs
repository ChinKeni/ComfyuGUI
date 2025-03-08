using ComfyuGUIEditor.Internal;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace ComfyuGUIEditor.Widgets
{
    public class ComfyuGUIEditorQuickPanel
    {
        public static void Draw()
        {
            GUILayout.BeginHorizontal();
            // 工具名称
            // GUILayout.Label(ComfyuGUIVars.Name, EditorStyles.boldLabel);
            GUILayout.BeginVertical(EditorStyles.helpBox,GUILayout.Width(ComfyuGUIVars.QuickPanelBtnSize));
            //临时...要换做法
            GUILayout.Button("面板1",GUILayout.Height(ComfyuGUIVars.QuickPanelBtnSize));
            GUILayout.Button("面板2",GUILayout.Height(ComfyuGUIVars.QuickPanelBtnSize));
            GUILayout.Button("面板3",GUILayout.Height(ComfyuGUIVars.QuickPanelBtnSize));
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            //菜单
            GUILayout.EndHorizontal();
        }
    }
}