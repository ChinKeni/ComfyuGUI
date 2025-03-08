using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ComfyuGUIEditor.Internal;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace ComfyuGUIEditor
{
    [NodeGraphEditor.CustomNodeGraphEditor(typeof(ComfyuGUIGraph))]
    public class ComfyuGUIEditorWindow : NodeEditorWindow
    {
        public static List<ComfyuGUIGraph> openList;
        public static ComfyuGUIGraph currentComfyuGUIGraph;
        
        [MenuItem("ComfyuGUI/Editor Window")]
        static void OpenWindow()
        {
            var w = GetWindow(typeof(ComfyuGUIEditorWindow), false, ComfyuGUIVars.Name, true) as ComfyuGUIEditorWindow;
            if (w == null) return;
            w.wantsMouseMove = true;
            var newGraph = ScriptableObject.CreateInstance<ComfyuGUIGraph>();
            w.graph = newGraph;
            openList = new List<ComfyuGUIGraph>();
            OpenNewComfyuGUIGraph();
        }

        /// <summary>
        /// 创建并打开新文件
        /// </summary>
        public static void OpenNewComfyuGUIGraph()
        {
            var newGraph = CreateNewComfyuGUIGraph(GetNewName());
             openList.Add(newGraph); 
             currentComfyuGUIGraph = newGraph;
        }

        /// <summary>
        /// 切换文件
        /// </summary>
        public static void SwitchComfyuGUIGraph(ComfyuGUIGraph graph)
        {
            if(!openList.Contains(graph))openList.Add(graph);
            currentComfyuGUIGraph = graph;
        }

        static ComfyuGUIGraph CreateNewComfyuGUIGraph(string newName)
        {
            var newGraph = ScriptableObject.CreateInstance<ComfyuGUIGraph>();
            newGraph.graphName = newName;
            return newGraph;
        }

        public static string GetNewName()
        {
            int maxNumber = 0;
            const string pattern = @"^未命名-(\d+)$"; 
    
            // 遍历所有图表对象，提取名称进行匹配
            foreach (var graph in openList)
            {
                var match = Regex.Match(graph.graphName, pattern);
                if (!match.Success) continue;
        
                string numberStr = match.Groups[1].Value;
                if (int.TryParse(numberStr, out int number))
                {
                    maxNumber = Math.Max(maxNumber, number);
                }
            }
    
            return $"未命名-{maxNumber + 1}";
        }
    }
}