using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace ComfyuGUIEditor.Nodes.Branch
{
    [CreateNodeMenu("核心/Branch/枚举切换")]
    [NodeTint(ComfyuNodeGlobalVars.ColorBranch)]
    [NodeWidth(300)]
    public class EnumSwitchNode: ComfyuGUIDoNode
    {
        [Input(connectionType = ConnectionType.Override)] public GameObject inData;
        [Output(dynamicPortList = true)]public List<string> output;
        private GameObject currentData;
        
        protected override void OnInputChanged()
        {
            currentData = GetPort("inData")?.GetInputValue<GameObject>();
            Do();
        }
        
        
        public override object GetValue(NodePort port)
        {
            return currentData;
        }
        
        private void Reset()
        {
            name = "枚举切换";
        }
    }
}