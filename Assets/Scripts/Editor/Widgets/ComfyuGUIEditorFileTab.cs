using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace ComfyuGUIEditor.Widgets
{
    public class ComfyuGUIEditorFileTab
    {
        private const float BtnCloseSize = 22f;

        public static bool DoTab(ComfyuGUIGraph entry,UnityAction closeCallback)
        {
            //画一个范围用来作鼠标事件的检测
            bool isHovered = false;
            
            // 根据状态设置颜色
            // var backgroundColor = isHovered? Color.white : Color.gray;
            // GUI.backgroundColor = backgroundColor;
            
            
            // 文件名显示
            var content = new GUIContent(entry.graphName);
            GUILayout.Label(content);
            var labelRect = GUILayoutUtility.GetLastRect();
            GUILayout.Space(BtnCloseSize);
            // 处理鼠标事件
            HandleMouseEvent(labelRect, ref isHovered);
            // 关闭按钮
            if (!isHovered) return false;
            Rect buttonRect = new Rect(
                labelRect.x + labelRect.width, // 按钮宽度18
                labelRect.y,                   // 与标签顶部对齐
                BtnCloseSize, BtnCloseSize            // 按钮大小
            );
            // 计算按钮位置
            if (GUI.Button(buttonRect, "X", EditorStyles.miniButton))closeCallback.Invoke();
            return false;
        }

        private static void HandleMouseEvent(Rect labelRect, ref bool isHovered)
        {
            var e = Event.current;
            labelRect.width += BtnCloseSize;
            isHovered = labelRect.Contains(e.mousePosition);
        }
    }
}