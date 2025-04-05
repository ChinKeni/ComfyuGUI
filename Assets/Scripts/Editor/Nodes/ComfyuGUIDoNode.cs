using XNode;

namespace ComfyuGUIEditor.Nodes
{
    public abstract class ComfyuGUIDoNode: ComfyuGUIBaseNode
    {
        protected virtual void Do()
        {
            foreach (var output in Outputs)
            {
                Do(output);
            }
        }

        private void Do(NodePort output)
        {
            if(output == null)return;
            // Loop through port connections
            var connectionCount = output.ConnectionCount;
            for (var i = 0; i < connectionCount; i++) {
                var connectedPort = output.GetConnection(i);

                // Get connected ports logic node
                var connectedNode = connectedPort.node as ComfyuGUIDoNode;

                // Trigger it
                if (connectedNode != null) connectedNode.Do();
            }

        }
    }
}