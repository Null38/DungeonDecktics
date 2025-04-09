using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;


namespace Astar
{
    public static class PathFinder
    {
        public static List<Vector2Int> FindPath(Vector2Int start, Vector2Int target)
        {
            PriorityQueue<Node, int> openSet = new PriorityQueue<Node, int> ();
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Enqueue(new Node(start, null, target), 0);

            while (openSet.Count > 0)
            {
                Node currNode = openSet.Dequeue();
            }

            
            throw new NotImplementedException("A* ¹Ì±¸Çö");
        }
    }

    [Serializable]
    public class Node
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
}