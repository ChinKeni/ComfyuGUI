using System;
using System.Collections.Generic;
using ComfyuGUIEditor.Internal.Utility;
using ComfyuGUIEditor.Widgets.Nodes;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace ComfyuGUIEditor.Nodes.Prefab
{
    [CreateNodeMenu("核心/Prefab/获取Prefab对象列表")]
    [NodeTint("#335955")]
    [NodeWidth(500)]
    public class GetPrefabGameObjectListNode: ComfyuGUIBaseNode
    {
        [Input] public GameObject prefab;
        [HideInInspector] public List<GameObject> selectedTargets;
        private GameObject _currentPrefab;
        public GameObject currentPrefab => _currentPrefab;

        protected override void OnInputChanged()
        {
            var newPrefab = GetPort("prefab").GetInputValue<GameObject>();
            if (newPrefab == null)
            {
                selectedTargets = null;
                return;
            }
            if(currentPrefab==newPrefab)return;
            _currentPrefab = newPrefab;
            selectedTargets = ComfyuGUIPrefabUtility.GetAllChildren(newPrefab);
        }


        public override object GetValue(NodePort port) {
            
            if (port.fieldName.StartsWith("selectedTargets ")) {
                int index = int.Parse(port.fieldName.Split(' ')[1]);
                return selectedTargets[index];
            }
            return null;
        }
        
        private void Reset()
        {
            name = "获取Prefab对象列表";
        }
    }
    
    
    # region NodeEditor

    [CustomNodeEditor(typeof(GetPrefabGameObjectListNode))]
    public class GetPrefabGameObjectListNodeEditor : NodeEditor
    {
        private GetPrefabGameObjectListNode targetNode;
        
        public override void OnBodyGUI()
        {
            base.OnBodyGUI();
            if(targetNode == null)targetNode = target as GetPrefabGameObjectListNode;
            if(targetNode != null && targetNode.currentPrefab==null)return;
            GUILayout.BeginVertical();
            //改成TreeView解析做法
            NodeEditorGUILayout.InstancePortList("selectedTargets", typeof(int), serializedObject, NodePort.IO.Output);
            //制作一个TreeView
            GUILayout.EndVertical();
        }
    }
    
    # endregion NodeEditor
}