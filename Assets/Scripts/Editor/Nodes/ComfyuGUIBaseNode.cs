using System;
using UnityEngine;
using XNode;

namespace ComfyuGUIEditor.Nodes
{
    [CreateNodeMenu("")]
    public class ComfyuGUIBaseNode : Node
    {
        public Action OnStateChange;

        protected override void Init()
        {
            base.Init();
            OnStateChange ??= OnInputChanged;
        }

        protected virtual void OnInputChanged(){}

        public override void OnCreateConnection(NodePort from, NodePort to) {
            OnInputChanged();
        }

        public override void OnRemoveConnection(NodePort port)
        {
            base.OnRemoveConnection(port);
            if(port.IsInput)
                OnInputChanged();
            else if(port.IsOutput)
                SendAllOutPutConnecction();
        }

        /// <summary>
        /// 数据变更后发送变更信号到Output所连接的对象
        /// </summary>
        protected void OnValidate()
        {
            SendAllOutPutConnecction();
        }


        public void SendAllOutPutConnecction()
        {
            foreach (var output in Outputs)
            {
                SendSignal(output);
            }
        }

        private void Reset()
        {
            name = "未命名节点";
        }

        private void SendSignal(NodePort output) {
            if(output == null)return;
            // Loop through port connections
            int connectionCount = output.ConnectionCount;
            for (int i = 0; i < connectionCount; i++) {
                NodePort connectedPort = output.GetConnection(i);

                // Get connected ports logic node
                var connectedNode = connectedPort.node as ComfyuGUIBaseNode;

                // Trigger it
                if (connectedNode != null) connectedNode.OnInputChanged();
            }

            OnStateChange?.Invoke();
        }
    }
}