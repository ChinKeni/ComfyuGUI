using System.Collections.Generic;
using System.Linq;
using ComfyuGUIEditor.Internal.Utility;
using ComfyuGUIEditor.Nodes;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace ComfyuGUIEditor.Widgets.Nodes
{
    public static class GameObjectTreeView
    {
        //绘画
        public static void Draw(GetPrefabGameObjectListNode targetNode, List<GameObjectDepthDate> data)
        {
            if(data==null||data.Count==0)return;
            var gos = data.ToList();
            EditorGUILayout.BeginVertical();
            for (var i = 0; i < gos.Count; i++)
            {
                var depthDate = gos[i];
                var go = depthDate.GameObject;
                var nextIndex = i+1;
                var isEnabled = false;
                if(nextIndex<gos.Count)
                    isEnabled = gos[nextIndex].GameObject.transform.parent ==  go.transform;
                var isSwitch = HierarchyItem(targetNode,go,depthDate.Depth,isEnabled,i);
                if(!isSwitch)continue;
                //如果产生操作变化，需要取反判断内容
                if (!isEnabled)
                    ComfyuGUIGameObjectUtility.Open(data,depthDate);
                else
                    ComfyuGUIGameObjectUtility.Close(data,depthDate);
                UpdatePortData(targetNode,data);
            }
            EditorGUILayout.EndVertical();
        }

        public static void InitData(GetPrefabGameObjectListNode targetNode, List<GameObjectDepthDate> data)
        {
            targetNode.outputTargets = new List<GameObject>();
            UpdatePortData(targetNode,data);
        }

        private static void UpdatePortData(GetPrefabGameObjectListNode targetNode, List<GameObjectDepthDate> data)
        {
            targetNode.ClearCustomPorts();

            targetNode.outputTargets.Clear();
            foreach (var cell in data)
            {
                targetNode.outputTargets.Add(cell.GameObject);
            }

            for (var i = 0; i < targetNode.outputTargets.Count; i++)
            {
                targetNode.AddCustomOutput(typeof(GameObject), fieldName:"outputTargets " + i);
            }
        }


        private static bool HierarchyItem(GetPrefabGameObjectListNode targetNode, GameObject go, int depth,
            bool enabled, int i)
        {
            if(go==null)return false;
            var tmpSwitch = false;
            EditorGUILayout.BeginHorizontal();
            if(depth!=0)
                EditorGUILayout.LabelField("", GUILayout.Width(6f*depth));
            if(go.transform.childCount>0)
                tmpSwitch = EditorGUILayout.Foldout(enabled,go.name);
            else
            {
                EditorGUILayout.LabelField("", GUILayout.Width(10f));
                EditorGUILayout.LabelField(go.name);
            }

            var rect = GUILayoutUtility.GetLastRect();
            var pos = new Vector2(480, rect.y);
            var port = targetNode.GetPort("outputTargets " + i);
            NodeEditorGUILayout.PortField(pos,port);
            EditorGUILayout.EndHorizontal();
            return tmpSwitch != enabled;
        }
    }
}