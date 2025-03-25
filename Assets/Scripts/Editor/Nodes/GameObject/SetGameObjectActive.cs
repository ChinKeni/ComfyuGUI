using UnityEngine;
using XNode;

namespace ComfyuGUIEditor.Nodes
{
    [CreateNodeMenu("核心/GameObject/设置GameObject显隐")]
    [NodeTint("#335955")]
    [NodeWidth(200)]
    public class SetGameObjectActive: ComfyuGUIDoNode
    {
        [Input] public GameObject gameObject;
        public bool active;
        private GameObject _gameObject;
        public GameObject currentGameObject => _gameObject;
        
        protected override void OnInputChanged()
        {
            var go = GetPort("gameObject").GetInputValue<GameObject>();
            if (go == null || _gameObject == go)return;
            _gameObject = go;
            active = go.activeSelf;
        }
        
        private void Reset()
        {
            name = "设置GameObject显隐";
        }

        public override void Do()
        {
            _gameObject.SetActive(active);
        }
    }
}