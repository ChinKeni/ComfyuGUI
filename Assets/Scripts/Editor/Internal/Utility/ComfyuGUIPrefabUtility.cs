using System.Collections.Generic;
using UnityEngine;

namespace ComfyuGUIEditor.Internal.Utility
{
    public class ComfyuGUIPrefabUtility
    {
        public static List<GameObject> GetAllChildren(GameObject parent)
        {
            if (parent == null) return new List<GameObject>();
        
            var result = new List<GameObject>();
            foreach (Transform child in parent.transform)
            {
                result.Add(child.gameObject);
                result.AddRange(GetAllChildren(child.gameObject));
            }
            return result;
        }
    }
}