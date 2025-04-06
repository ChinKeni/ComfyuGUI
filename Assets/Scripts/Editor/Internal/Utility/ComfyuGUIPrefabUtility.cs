using System.Collections.Generic;
using UnityEditor;
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


        public static bool ComparePrefab(GameObject currentData, GameObject compareTarget)
        {
            if (currentData == null || compareTarget == null)
                return false;

            // 检查currentData是否为Prefab实例且处于Connected状态
            var instanceStatus = PrefabUtility.GetPrefabInstanceStatus(currentData);
            if (instanceStatus != PrefabInstanceStatus.Connected)
                return false;

            // 获取currentData对应的原始Prefab
            var currentPrefab = PrefabUtility.GetCorrespondingObjectFromSource(currentData);
            if (currentPrefab == null)
                return false;

            // 获取compareObject对应的原始Prefab（处理Prefab资源和非实例情况）
            var comparePrefab = PrefabUtility.GetCorrespondingObjectFromSource(compareTarget);
            if (comparePrefab != null) return currentPrefab == comparePrefab;
            // 检查compareObject是否为Prefab资源
            if (PrefabUtility.IsPartOfPrefabAsset(compareTarget))
                comparePrefab = compareTarget;
            else
                return false; // compareObject既不是实例也不是Prefab资源

            // 比较原始Prefab是否相同
            return currentPrefab == comparePrefab;
        }
    }
}