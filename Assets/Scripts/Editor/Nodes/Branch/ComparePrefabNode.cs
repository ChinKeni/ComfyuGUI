using ComfyuGUIEditor.Internal.Utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using XNode;

namespace ComfyuGUIEditor.Nodes.Branch
{
    [CreateNodeMenu("核心/Branch/比较Prefab")]
    [NodeTint(ComfyuNodeGlobalVars.ColorBranch)]
    [NodeWidth(300)]
    public class ComparePrefabNode: ComfyuGUIDoNode
    {
        public GameObject comparePrefab;
        [Input(connectionType = ConnectionType.Override)] public GameObject inData;
        [Output] public GameObject result;
        
        private GameObject currentData;
        private GameObject Result =>  ComfyuGUIPrefabUtility.ComparePrefab(currentData,comparePrefab) ? currentData : null;
        
        protected override void OnInputChanged()
        {
            currentData = GetPort("inData")?.GetInputValue<GameObject>();
            Do();
        }
        
        public override object GetValue(NodePort port)
        {
            return Result;
        }
        
        private void Reset()
        {
            name = "比较Prefab";
        }
        
        private new void OnValidate()
        {
            base.OnValidate();
            if(comparePrefab==null||PrefabUtility.IsPartOfPrefabAsset(comparePrefab))return;
            comparePrefab = null;
        }
    }
}