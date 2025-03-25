using System.Collections.Generic;
using System.Linq;
using ComfyuGUIEditor.Nodes;
using UnityEngine;

namespace ComfyuGUIEditor.Internal.Utility
{
    public class ComfyuGUIGameObjectUtility
    {
        public static List<GameObjectDepthDate> GetChildren(GameObjectDepthDate depthDate)
        {
            if (depthDate == null) return null;
            var result = new List<GameObjectDepthDate>();
            var go = depthDate.GameObject;
            var nextDepth = depthDate.Depth+1;
            foreach (Transform tr in go.transform)
            {
                result.Add(new GameObjectDepthDate(tr.gameObject,nextDepth));
            }
            return result;
        }

        public static void Open(List<GameObjectDepthDate> data, GameObjectDepthDate depthDate)
        {
            if(data == null||data.Count==0)return;
            if(!data.Contains(depthDate))return;
            var i = data.IndexOf(depthDate);
            data.InsertRange(i + 1, GetChildren(depthDate));
        }

        public static void Close(List<GameObjectDepthDate> data, GameObjectDepthDate depthDate)
        {
            if(data == null||data.Count==0)return;
            if(!data.Contains(depthDate))return;
            var gos = data.ToList();
            var i = data.IndexOf(depthDate)+1;
            for (; i < gos.Count; i++)
            {
                var d = gos[i];
                if (d.Depth == depthDate.Depth) return;
                data.Remove(d);
            }
        }
    }
}