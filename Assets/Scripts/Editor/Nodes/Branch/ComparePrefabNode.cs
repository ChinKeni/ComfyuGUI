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
        
        //根据结果进行传递，并且离线模式True是获取comparePrefab的值，运行模式的时候，替换成源数据
        [Output] public GameObject resultTrue;
        [Output] public GameObject resultFalse;
        
        private GameObject currentData;
        private GameObject Result =>  ComfyuGUIPrefabUtility.ComparePrefab(currentData,comparePrefab) ? currentData : null;
        
        protected override void OnInputChanged()
        {
            currentData = GetPort("inData")?.GetInputValue<GameObject>();
            Do();
        }
        
        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "resultTrue")
            {
                //默认返回比较对象，实际上在运行模式传递过去的是选中对象（因为要提取数据）
                return comparePrefab;
            }else if (port.fieldName == "resultFalse")
            {
                return currentData;
            }
            return null;
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