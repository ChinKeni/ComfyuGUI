using System;
using UnityEditor;
using UnityEngine;
using XNode;

namespace ComfyuGUIEditor.Nodes.Prefab
{
    [CreateNodeMenu("核心/Prefab/获取Prefab资产")]
    [NodeTint("#594d33")]
    [NodeWidth(300)]
    public class GetPrefabAssetNode: ComfyuGUIBaseNode
    {
        public GameObject prefabAsset;
        [Output] public GameObject prefab;

        private void Reset()
        {
            name = "获取Prefab资产";
        }

        public override object GetValue(NodePort port)
        {
            return prefabAsset;
        }

        private void OnValidate()
        {
            if(prefabAsset==null||PrefabUtility.IsPartOfPrefabAsset(prefabAsset))return;
            prefabAsset = null;
        }
    }
}
