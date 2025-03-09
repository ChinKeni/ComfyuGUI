using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace ComfyuGUIEditor.Widgets
{
    public static class ComfyuGUIEditorFileTab
    {
        private const float BtnCloseSize = 22f;
        private const float UnderlineSize = 3f;

        public static bool DoTab(ComfyuGUIGraph entry, UnityAction closeCallback, bool isOpen)
        {
            //预计算
            var mouseTyep = ComfyuGUIEditorFileTabMouseType.None;
            GUILayout.Label("");
            var labelRect = GUILayoutUtility.GetLastRect();
            var content = new GUIContent(entry.graphName);
            var style = new GUIStyle(EditorStyles.label);
            var labelSize = style.CalcSize(content);
            var labelRectPredicted = new Rect(
                labelRect.x,
                labelRect.y,
                labelSize.x+BtnCloseSize,
                labelSize.y
            );
            
            //选中的下划线...
            var underlineColor = isOpen ? new Color(0.4f, 0.8f, 1f, 1f) : Color.clear;
            var underlineRect = new Rect(labelRectPredicted.x, labelRect.y + labelRectPredicted.height+2f,
                labelRectPredicted.width,
                UnderlineSize);
            GUI.color = underlineColor;
            var lineTex = new Texture2D (1, 1);  
            GUI.DrawTexture(underlineRect,lineTex);
            GUI.color = Color.white;
            
            // 处理鼠标事件
            mouseTyep =  HandleMouseEvent(labelRectPredicted);
            var isHovered = mouseTyep == ComfyuGUIEditorFileTabMouseType.IsHovered;
            var isClick = mouseTyep == ComfyuGUIEditorFileTabMouseType.IsClick;
            
            
            if (isClick) ComfyuGUIEditorWindow.SwitchComfyuGUIGraph(entry);
            
            // 文件名显示
            GUI.color = isHovered||isOpen?Color.white:new Color(1f,1f,1f,0.6f);
            GUILayout.Label(content);
            GUI.color = Color.white;
            GUILayout.Space(BtnCloseSize);
            
            // 关闭按钮
            if (!isHovered && !isClick) return false;
            var buttonRect = new Rect(
                labelRect.x + labelSize.x+BtnCloseSize/2,   // 按钮宽度18
                labelRect.y,                                // 与标签顶部对齐
                BtnCloseSize, BtnCloseSize       // 按钮大小
            );
            //按下后回调关闭方法
            if (GUI.Button(buttonRect, "X", EditorStyles.miniButton))closeCallback.Invoke();
            return false;
        }

        private static ComfyuGUIEditorFileTabMouseType HandleMouseEvent(Rect labelRect)
        {
            var type = ComfyuGUIEditorFileTabMouseType.None;
            var e = Event.current;
            labelRect.width += BtnCloseSize;
            if (labelRect.Contains(e.mousePosition))
            {
                type = e.type == EventType.MouseDown
                    ? ComfyuGUIEditorFileTabMouseType.IsClick
                    : ComfyuGUIEditorFileTabMouseType.IsHovered;
            }
            return type;
        }

        private enum ComfyuGUIEditorFileTabMouseType
        {
            None,
            IsHovered,
            IsClick
        }
    }
}