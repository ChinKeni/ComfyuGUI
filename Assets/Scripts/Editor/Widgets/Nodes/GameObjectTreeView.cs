using System.Collections.Generic;
using System.Linq;
using ComfyuGUIEditor.Internal.Utility;
using ComfyuGUIEditor.Nodes.Prefab;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
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
                if(nextIndex<gos.Count)
                    isEnabled = gos[nextIndex].GameObject.transform.parent ==  go.transform;
                var isSwitch = HierarchyItem(targetNode,go,depthDate.Depth,isEnabled,i);
                if(!isSwitch)continue;
                //如果产生操作变化，需要取反判断内容
                if (!isEnabled)
                    ComfyuGUIGameObjectUtility.Open(data,depthDate);
                else
                    ComfyuGUIGameObjectUtility.Close(data,depthDate);
                // GetAllGameObject(targetNode,data);
            }
            EditorGUILayout.EndVertical();
        }

        // private static void GetAllGameObject(GetPrefabGameObjectListNode targetNode, List<GameObjectDepthDate> data)
        // {
        //     targetNode.outputTargets =  (from cell in data where cell.GameObject != null select cell.GameObject).ToList();
        // }


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
            NodeEditorGUILayout.PortField(pos,new NodePort("selectedTargets "+i,typeof(GameObject),NodePort.IO.Output,Node.ConnectionType.Multiple,Node.TypeConstraint.InheritedInverse,targetNode));
            EditorGUILayout.EndHorizontal();
            return tmpSwitch != enabled;
        }
    }
}