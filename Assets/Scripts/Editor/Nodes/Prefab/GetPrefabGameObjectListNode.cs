using System;
using System.Collections.Generic;
using ComfyuGUIEditor.Internal.Utility;
using ComfyuGUIEditor.Widgets.Nodes;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace ComfyuGUIEditor.Nodes
{
    [CreateNodeMenu("核心/Prefab/获取Prefab对象列表")]
    [NodeTint("#335955")]
    [NodeWidth(500)]
    public class GetPrefabGameObjectListNode: ComfyuGUIBaseNode
    {
        [Input] public GameObject prefab;
        [HideInInspector]public List<GameObjectDepthDate> selectedTargets;
        [HideInInspector]public List<GameObject> outputTargets;
        private GameObject _currentPrefab;
        public GameObject currentPrefab => _currentPrefab;

        protected override void OnInputChanged()
        {
            var newPrefab = GetPort("prefab").GetInputValue<GameObject>();
            if (newPrefab == null)
            {
                selectedTargets?.Clear();
                selectedTargets = null;
                _currentPrefab = null;
                ClearCustomPorts();
                return;
            }
            if(currentPrefab==newPrefab)return;
            _currentPrefab = newPrefab;
            // selectedTargets = ComfyuGUIPrefabUtility.GetAllChildren(newPrefab); //修改成展开方法
            selectedTargets= new List<GameObjectDepthDate> { new(newPrefab,0) };
        }


        public override object GetValue(NodePort port)
        {
            if (port.fieldName.StartsWith("outputTargets ")) {
                int index = int.Parse(port.fieldName.Split(' ')[1]);
                return outputTargets[index];
            }
            return null;
        }
        
        private void Reset()
        {
            name = "获取Prefab对象列表";
        }

        public void ClearOutputTargets(Vector2Int rang)
        {
            var gos = new List<GameObject>(outputTargets);
            var end = rang.x + rang.y;
            for (var i = rang.x; i < end; i++)
            {
                outputTargets.Remove(gos[i]);
            }
        }
    }
    # region NodeData
    public class GameObjectDepthDate
    {
        public GameObject GameObject;
        public int Depth;
        public string GameObjectName;
        
        public GameObjectDepthDate(GameObject go, int depth)
        {
            GameObject = go;
            Depth = depth;
            GameObjectName = go.name;
        }

    }
    #endregion NodeData


    # region NodeEditor

    [CustomNodeEditor(typeof(GetPrefabGameObjectListNode))]
    public class GetPrefabGameObjectListNodeEditor : NodeEditor
    {
        private GetPrefabGameObjectListNode targetNode;
        private GameObject lastPrefab;
        public override void OnBodyGUI()
        {
            base.OnBodyGUI();
            if(targetNode == null)targetNode = target as GetPrefabGameObjectListNode;
            if (targetNode == null) return;
            if (lastPrefab != targetNode.currentPrefab)
            {
                lastPrefab = targetNode.currentPrefab;
                if(lastPrefab == null)return;
                GameObjectTreeView.InitData(targetNode,targetNode.selectedTargets);
            }
            
            GameObjectTreeView.Draw(targetNode,targetNode.selectedTargets);
        }
    }
    
    # endregion NodeEditor
}