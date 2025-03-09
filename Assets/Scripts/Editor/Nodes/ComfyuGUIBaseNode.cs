using System;
using UnityEngine;
using XNode;

namespace ComfyuGUIEditor.Nodes
{
    [CreateNodeMenu("")]
    public class ComfyuGUIBaseNode : Node
    {
        protected virtual void OnInputChanged(){}

        public override void OnCreateConnection(NodePort from, NodePort to) {
            OnInputChanged();
        }
        private void Reset()
        {
            name = "未命名节点";
        }
    }
}