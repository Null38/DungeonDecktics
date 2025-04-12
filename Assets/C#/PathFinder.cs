using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;


namespace Astar
{

    public static class PathFinder
    {
        public delegate int CostHandler(Vector2Int position);
        public delegate bool PassableHandler(Vector2Int position);

        [Serializable]
        private class Node
        {
            const int StraightCost = 10;
            const int DiagonalCost = 14;

            public Vector2Int Position;
            public Node Parent;
            public int G;
            public int H;
            public int F => G + H;

            public Node(Vector2Int pos, Node parent, Vector2Int target)
            {
                Position = pos;
                Parent = parent;
                H = Mathf.Abs(target.x - pos.x) + Mathf.Abs(target.y - pos.y);
                if (parent != null)
                {
                    int dx = Mathf.Abs(parent.Position.x - pos.x);
                    int dy = Mathf.Abs(parent.Position.y - pos.y);
                    G = parent.G + (dx == 1 && dy == 1 ? DiagonalCost : StraightCost);
                }
                else
                    G = 0;
            }

            public override bool Equals(object obj)
            {
                return obj is Node node && Position == node.Position;
            }

            public override int GetHashCode()
            {
                return Position.GetHashCode();
            }
        }


        public static List<Vector2Int> FindPath(Vector2Int start, Vector2Int target)
        {
            return FindPath(start, target, GetCost, IsPassable);
        }

        public static List<Vector2Int> FindPath(Vector2Int start, Vector2Int target, CostHandler GetCost, PassableHandler IsPassable)
        {
            PriorityQueue<Node, int> openSet = new PriorityQueue<Node, int> ();
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Enqueue(new Node(start, null, target), 0);

            while (openSet.Count > 0)
            {
                Node currNode = openSet.Dequeue();

                if (closedSet.Contains(currNode))
                    continue;

                closedSet.Add(currNode);



            }

            
            throw new NotImplementedException("A* 미구현");
        }

        public static int GetCost(Vector2Int position) {return 1;}
        public static bool IsPassable(Vector2Int position)
        {
            throw new NotImplementedException("이동 검사 미구현");
        }
    }
}