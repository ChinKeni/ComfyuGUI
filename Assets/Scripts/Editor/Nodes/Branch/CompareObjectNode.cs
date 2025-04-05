using UnityEngine;
using XNode;

namespace ComfyuGUIEditor.Nodes.Branch
{
    [CreateNodeMenu("核心/Branch/比较对象")]
    [NodeTint(ComfyuNodeGlobalVars.ColorBranch)]
    [NodeWidth(300)]
    public class CompareObjectNode : ComfyuGUIDoNode
    {
        public Object compareObject;
        
        [Input(connectionType = ConnectionType.Override)] public Object inData;
        [Output] public Object result;
        private Object currentData;
        private Object Result => compareObject == currentData ? compareObject : null;
        
        protected override void OnInputChanged()
        {
            currentData = GetPort("inData")?.GetInputValue<Object>();
            Do();
        }
        
        public override object GetValue(NodePort port)
        {
            return Result;
        }
        
        private void Reset()
        {
            name = "比较对象";
        }
    }
}