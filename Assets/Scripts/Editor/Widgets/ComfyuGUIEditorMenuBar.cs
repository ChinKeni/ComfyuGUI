using ComfyuGUIEditor.Internal;
using UnityEditor;
using UnityEngine;

namespace ComfyuGUIEditor.Widgets
{
    public class ComfyuGUIEditorMenuBar
    {
        public static void Draw()
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar,GUILayout.ExpandWidth(false),GUILayout.Height(ComfyuGUIVars.MenuBarSize));
            // 工具名称
            // GUILayout.Label(ComfyuGUIVars.Name, EditorStyles.boldLabel);
            if (GUILayout.Button(" 菜单 ", EditorStyles.toolbarButton))
            {
                var menu = new GenericMenu();
                menu.AddItem(new GUIContent("新建"), false, () => ComfyuGUIEditorWindow.OpenNewComfyuGUIGraph());
                menu.AddItem(new GUIContent("打开"), false, () => Debug.Log(""));
                menu.AddItem(new GUIContent("保存"), false, () => Debug.Log(""));
                menu.ShowAsContext();
            };
            if (GUILayout.Button(" 编辑 ", EditorStyles.toolbarButton))
            {
                var menu = new GenericMenu();
                menu.AddItem(new GUIContent("节点/创建"), false, () => Debug.Log(""));
                menu.AddItem(new GUIContent("节点/删除"), false, () => Debug.Log(""));
                menu.ShowAsContext();
            };
            if (GUILayout.Button(" 运行 ", EditorStyles.toolbarButton))
            {
                var menu = new GenericMenu();
                menu.AddItem(new GUIContent("运行当前工作流"), false, () => Debug.Log(""));
                menu.ShowAsContext();
            };
            if (GUILayout.Button(" 帮助 ", EditorStyles.toolbarButton))
            {
                var menu = new GenericMenu();
                menu.AddItem(new GUIContent("关于"), false, () => Debug.Log(""));
                menu.ShowAsContext();
            };
            GUILayout.FlexibleSpace();
            //菜单
            GUILayout.EndHorizontal();
        }
    }
}