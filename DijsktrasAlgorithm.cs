using System;
using System.Text;
namespace idk
{
    class Node
    {
        char Name { get; set; }
        bool HasBeenVisited { get; set; }
        int CurrentQuickestPathDistance { get; set; }
        public static int CompareDistances(Node nodea, Node nodeb)
        {
            return nodea.CurrentQuickestPathDistance.CompareTo(nodeb.CurrentQuickestPathDistance);
        }
        StringBuilder CurrentQuickestPath = new StringBuilder();
        Dictionary<Node, int> NeighboringNodes = new Dictionary<Node, int>();
        public Node(char na)
        {
            Name = na;
            CurrentQuickestPathDistance = int.MaxValue;
            HasBeenVisited = false;
        }
        public Node(char na, int num)
        {
            Name = na;
            CurrentQuickestPath.Append(na);
            CurrentQuickestPathDistance = num;
            HasBeenVisited = true;
        }

        public static void Main(string[] args)
        {
            List<Node> VisitableNodes = new List<Node>();
            Node A = new Node('A', 0);
            Node B = new Node('B');
            Node C = new Node('C');
            Node D = new Node('D');
            Node E = new Node('E');
            Node F = new Node('F');
            A.NeighboringNodes.Add(B, 3);
            A.NeighboringNodes.Add(C, 1);
            B.NeighboringNodes.Add(A, 3);
            B.NeighboringNodes.Add(C, 7);
            B.NeighboringNodes.Add(D, 5);
            B.NeighboringNodes.Add(E, 1);
            C.NeighboringNodes.Add(A, 1);
            C.NeighboringNodes.Add(B, 7);
            C.NeighboringNodes.Add(F, 2);
            C.NeighboringNodes.Add(D, 2);
            D.NeighboringNodes.Add(C, 2);
            D.NeighboringNodes.Add(B, 5);
            D.NeighboringNodes.Add(E, 7);
            E.NeighboringNodes.Add(B, 1);
            E.NeighboringNodes.Add(D, 7);
            F.NeighboringNodes.Add(C, 2);
            Node currentNode = A;
            do
            {
                foreach (Node neighboringnode in currentNode.NeighboringNodes.Keys)
                {
                    if (VisitableNodes.Contains(neighboringnode) == false && neighboringnode.HasBeenVisited == false)
                    {
                        VisitableNodes.Add(neighboringnode);
                    }
                    // if distance of current node + distance to neighboring node is less than the neighboring nodes current distance
                    if (currentNode.CurrentQuickestPathDistance + currentNode.NeighboringNodes[neighboringnode] < neighboringnode.CurrentQuickestPathDistance)
                    {
                        //Nodes new distance = currentnodes distances + distance from current nodes to node
                        neighboringnode.CurrentQuickestPathDistance = currentNode.CurrentQuickestPathDistance + currentNode.NeighboringNodes[neighboringnode];
                        neighboringnode.CurrentQuickestPath = new StringBuilder(currentNode.CurrentQuickestPath.ToString() + neighboringnode.Name);
                    }
                }
                currentNode.HasBeenVisited = true;
                VisitableNodes.Remove(currentNode);
                if (VisitableNodes.Count >= 1)
                {
                    VisitableNodes.Sort(CompareDistances);
                    currentNode = VisitableNodes[0];
                }
            }
            while (VisitableNodes.Count > 0);
            Console.WriteLine(E.CurrentQuickestPath.ToString());
        }
    }
}