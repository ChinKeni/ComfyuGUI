using ComfyuGUIEditor.Internal;
using ComfyuGUIEditor.Widgets;
using UnityEngine;
using XNodeEditor;

namespace ComfyuGUIEditor
{
    [NodeGraphEditor.CustomNodeGraphEditor(typeof(ComfyuGUIGraph),"ComfyuGUIGraph.Settings")]
    public class ComfyuGUIEditor : NodeGraphEditor
    {
        private bool isQuickMenuOpen = false;
        public override NodeEditorPreferences.Settings GetDefaultPreferences() {
            return new NodeEditorPreferences.Settings() {
                gridBgColor = new Color(0.1333f,0.1333f,0.1333f),
                gridLineColor = new Color(0.098f,0.098f,0.098f),
            };
        }

        public override void OnGUI()
        {
            base.OnGUI();
            
            QuickNode();
            GUILayout.BeginVertical();
            ComfyuGUIEditorMenuBar.Draw();
            ComfyuGUIEditorFileBar.Draw(ComfyuGUIEditorWindow.openList);
            ComfyuGUIEditorQuickPanel.Draw();
            GUILayout.EndVertical();
            //每帧都刷新
            NodeEditorWindow.current.Repaint();
        }

        /// <summary>
        /// 快捷搜索指令
        /// </summary>
        private void QuickNode()
        {
            var mouse = Event.current;
            // 这里的QuickPanelBtnSize还得考虑弹出窗口的问题
            var leftOffset = ComfyuGUIVars.QuickPanelBtnSize;
            var topOffset = ComfyuGUIVars.MenuBarSize;
            var winSize = NodeEditorWindow.current.position.size;
            var winSizeRel = new Vector2(winSize.x - leftOffset,
                winSize.y - topOffset);
            if (isQuickMenuOpen)
            {
                var menuSize = new Vector2(winSizeRel.x * 0.8f, winSizeRel.y * 0.5f);
                var pos = new Vector2((winSize.x - menuSize.x + leftOffset) / 2f,
                    (winSize.y - menuSize.y + topOffset) / 2f);
                //关闭检测
                if (mouse.isMouse && mouse.type == EventType.MouseDown)
                {
                    if (mouse.mousePosition.x < pos.x || mouse.mousePosition.y < pos.y ||
                        mouse.mousePosition.x > pos.x + menuSize.x || mouse.mousePosition.y > pos.y + menuSize.y)
                    {
                        isQuickMenuOpen = false;
                        return;
                    }
                }
                //正式绘制
                ComfyuGUIEditorQuickMenu.Draw(pos,menuSize);
                return;
            }
            //判断打开
            if (mouse.isMouse && mouse.type == EventType.MouseDown && mouse.clickCount == 2)
            {
                var pos = winSize - winSizeRel;
                if (mouse.mousePosition.x > pos.x && mouse.mousePosition.y > pos.y &&
                    mouse.mousePosition.x < winSize.x && mouse.mousePosition.y < winSize.y)
                {
                    isQuickMenuOpen = true;
                }
            }

            
        }
    }
}