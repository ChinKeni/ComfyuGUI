using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using XNode;

namespace ComfyuGUIEditor.Nodes
{
    [CreateNodeMenu("核心/GameObject/获取所选Gameobject")]
    [NodeTint(ComfyuNodeGlobalVars.ColorEntry)]
    [NodeWidth(300)]
    public class GetGameObjectSelectionNode : ComfyuGUIDoNode
    {
        [Output] public GameObject activeTarget;

        protected override void Do()
        {
            UpdateData();
            base.Do();
        }
        
        public override object GetValue(NodePort port)
        {
            return activeTarget;
        }

        private void Reset()
        {
            name = "获取所选Gameobject";
        }

        private void UpdateData()
        {
            activeTarget = Selection.activeGameObject;
        }
    }
}