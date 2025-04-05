using System;
using UnityEditor;
using UnityEngine;
using XNode;

namespace ComfyuGUIEditor.Nodes
{
    [CreateNodeMenu("核心/Prefab/获取Prefab资产")]
    [NodeTint(ComfyuNodeGlobalVars.ColorEntry)]
    [NodeWidth(300)]
    public class GetPrefabAssetNode: ComfyuGUIDoNode
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

        private new void OnValidate()
        {
            base.OnValidate();
            if(prefabAsset==null||PrefabUtility.IsPartOfPrefabAsset(prefabAsset))return;
            prefabAsset = null;
        }
    }
}
