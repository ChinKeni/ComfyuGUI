using UnityEditor;
using UnityEngine;

namespace ComfyuGUIEditor.Widgets
{
    public class ComfyuGUIEditorQuickMenu
    {
        public static void Draw(Vector2 pos,Vector2 size)
        {
            
            var tooltipRect = new Rect(
                pos.x,
                pos.y,
                size.x,
                size.y
            );
    
            GUILayout.BeginArea(tooltipRect,EditorStyles.helpBox);
            GUILayout.Label("正在创建新节点...", EditorStyles.boldLabel);
            GUILayout.EndArea();
        }
    }
}