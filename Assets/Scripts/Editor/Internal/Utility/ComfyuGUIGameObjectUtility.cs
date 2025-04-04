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

        
        /// <summary>
        /// 打开 子对象深度节点数据
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="depthDate">需要进行操作的节点</param>
        /// <returns>返回一个范围，x为index，y为length</returns>
        public static Vector2Int Open(List<GameObjectDepthDate> data, GameObjectDepthDate depthDate)
        {
            var result = Vector2Int.zero;
            if(data == null||data.Count==0)return result;
            if(!data.Contains(depthDate))return result;
            var i = data.IndexOf(depthDate) + 1;
            var collection = GetChildren(depthDate);
            result = new Vector2Int(i, collection.Count);
            data.InsertRange(i,collection);
            return result;
        }

        /// <summary>
        /// 关闭/移除 子对象深度节点数据
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="depthDate">需要进行操作的节点</param>
        /// <returns>返回的是一个范围，x为index，y为length</returns>
        public static Vector2Int Close(List<GameObjectDepthDate> data, GameObjectDepthDate depthDate)
        {
            var result = Vector2Int.zero;
            if(data == null||data.Count==0)return result;
            if(!data.Contains(depthDate))return result;
            var gos = data.ToList();
            var i = data.IndexOf(depthDate)+1;
            result.x = i;
            for (; i < gos.Count; i++)
            {
                var d = gos[i];
                if (d.Depth == depthDate.Depth) return result;
                data.Remove(d);
                result.y += 1;
            }
            
            return result;
        }
    }
}