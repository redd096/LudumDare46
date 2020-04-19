using UnityEngine;
using Pathfinding;
using System.Linq;

namespace LudumDare46
{

    public static class Utils
    {

        // Start is called before the first frame update
        public static Vector3 GetRandomWalkableNode()
        {
            Vector3 toRet = Vector3.zero;
            GraphNode randomNode;

            // For grid graphs
            var grid = AstarPath.active.data.gridGraph;
            if (grid == null) { return toRet; }

            var nodes = grid.nodes.Where(x => x.Walkable == true).ToList();
            randomNode = nodes[Random.Range(0, nodes.Count)];

            toRet = (Vector3)randomNode.position;

            return toRet;
        }

    }

}