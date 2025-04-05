using System.Collections.Generic;
using System.Linq;
using ComfyuGUIEditor.Internal.Utility;
using ComfyuGUIEditor.Nodes;
using UnityEditor;
using UnityEngine;
using XNode;
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
                if(nextIndex<gos.Count && gos[nextIndex]!=null && gos[nextIndex].GameObject!=null && go.gameObject!=null)
                    isEnabled = gos[nextIndex].GameObject.transform.parent ==  go.transform;
                var isSwitch = HierarchyItem(targetNode,go,depthDate.Depth,isEnabled,i,depthDate.GameObjectName);
                if(!isSwitch)continue;
                var rang = Vector2Int.zero;
                //如果产生操作变化，需要取反判断内容
                var isOpen = true;
                if (!isEnabled)
                {
                    rang = ComfyuGUIGameObjectUtility.Open(data,depthDate);
                }
                else
                {
                    rang = ComfyuGUIGameObjectUtility.Close(data,depthDate);
                    isOpen = false;
                }
                UpdatePortData(targetNode,data,rang,isOpen);
            }
            EditorGUILayout.EndVertical();
        }

        public static void InitData(GetPrefabGameObjectListNode targetNode, List<GameObjectDepthDate> data)
        {
            targetNode.outputTargets = new List<GameObject>();
            targetNode.ClearCustomPorts();
            var rang = new Vector2Int(0,data.Count);
            UpdatePortData(targetNode,data,rang);
        }

        private static void UpdatePortData(GetPrefabGameObjectListNode targetNode, List<GameObjectDepthDate> data,
            Vector2Int rang,bool isOpen = true)
        {
            //关闭节点更新数据
            if (!isOpen)
            {
                targetNode.RemoveRangCustomPorts(rang);
                targetNode.ClearOutputTargets(rang);
                return;
            }

            const string fieldName = "outputTargets ";
            var end = rang.x + rang.y;
            for (var i = rang.x; i < end; i++)
            {
                targetNode.outputTargets.Insert(i,data[i].GameObject);
            }
            targetNode.InsertEmptyCustomOutput(rang,typeof(GameObject), fieldName:fieldName,connectionType: Node.ConnectionType.Override);
        }


        private static bool HierarchyItem(GetPrefabGameObjectListNode targetNode, GameObject go, int depth,
            bool enabled, int i,string name="")
        {
            
            var tmpSwitch = false;
            EditorGUILayout.BeginHorizontal();
            if(depth!=0)
                EditorGUILayout.LabelField("", GUILayout.Width(6f*depth));
            if (go == null)
            {
                
                EditorGUILayout.LabelField("", GUILayout.Width(10f));
                EditorGUILayout.LabelField(name+" (Missing)",
                    new GUIStyle(EditorStyles.label){normal = {textColor = Color.yellow}});
            }
            else
            {
                if(go.transform.childCount>0)
                    tmpSwitch = EditorGUILayout.Foldout(enabled,go.name);
                else
                {
                    EditorGUILayout.LabelField("", GUILayout.Width(10f));
                    EditorGUILayout.LabelField(go.name);
                }
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