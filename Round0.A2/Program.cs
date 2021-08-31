using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Round0.A2
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("C:\\code\\HackerCup\\Round0.A2\\input.txt");
            StreamWriter sw = new StreamWriter("C:\\code\\HackerCup\\Round0.A2\\output.txt");
            int birthdayCount = Int32.Parse(sr.ReadLine());

            for (var i = 0; i < birthdayCount; i++)
            {
                string str = sr.ReadLine();
                List<Node> graph = new List<Node>();
                List<int> seconds = new List<int>();
                HashSet<char> toLetters = new HashSet<char>();  // ?
                foreach (char ch in str)
                    toLetters.Add(ch);

                int n = Int32.Parse(sr.ReadLine());   // number of replacements
                string replacement;

                for (var j = 0; j < n; j++)   // build graph
                {
                    replacement = sr.ReadLine();
                    toLetters.Add(replacement[1]);
                    Node node1 = GetNode(graph, replacement[0]);
                    if (node1 == null)
                    {
                        node1 = new Node(replacement[0]);
                        graph.Add(node1);
                    }
                    Node node2 = GetNode(graph, replacement[1]);
                    if (node2 == null)
                    {
                        node2 = new Node(replacement[1]);
                        graph.Add(node2);
                    }
                    bool found = false;
                    for (var k = 0; k < node1.Adjacents.Count; k++)
                    {
                        if (node1.Adjacents[k].Data == node2.Data)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        node1.Adjacents.Add(node2);
                }

                // search letter to which all letters can convert
                foreach (char baseLetter in toLetters)
                {
                    int secPerWord = 0;
                    Dictionary<char, int> pathSecList = new Dictionary<char, int>();

                    for (var k = 0; k < str.Length; k++)
                    {
                        if (str[k] == baseLetter)
                            continue;
                        Node startNode = GetNode(graph, str[k]);
                        if (startNode == null)
                        {
                            secPerWord = -1;   // no path from this lettter
                            break;
                        }
                        int res;
                        if (pathSecList.ContainsKey(str[k]))
                        {
                            res = pathSecList[str[k]];
                        }
                        else
                        {
                            res = GetMinPath(graph, startNode, baseLetter); // in sec
                            pathSecList.Add(str[k], res);
                        }
                        if (res < 0)
                        {
                            secPerWord = -1;
                            break;      // no path exists -> need to change base letter
                        }
                        else
                        {
                            secPerWord += res;
                        }
                    }
                    if (secPerWord >= 0)
                    {
                        seconds.Add(secPerWord);
                    }
                }
                var secResult = (seconds.Count > 0) ? seconds.Min() : -1;
                string line = $"Case #{i + 1}: {secResult}";
                sw.WriteLine(line);
            }
            sr.Close();
            sw.Close();
        }

        private static Node GetNode(List<Node> graph, char data)
        {
            for (var i = 0; i < graph.Count; i++)
            {
                if (graph[i].Data == data)
                {
                    return graph[i];
                }
            }
            return null;
        }

        private static int GetMinPath(List<Node> graph, Node start, char endChar)
        {
            if (start.Data == endChar)
                return 0;

            Dictionary<char, bool> visited = new Dictionary<char, bool>();
            foreach (var node in graph)
                visited.Add(node.Data, false);  // mark all nodes as not visited
            Queue<Node> queue = new Queue<Node>();
            Dictionary<char, int> pathLength = new Dictionary<char, int>();
            Dictionary<char, Node> parents = new Dictionary<char, Node>();
            foreach (var node in graph)
            {
                if (node.Data != endChar)
                    pathLength.Add(node.Data, 0);
                parents.Add(node.Data, null);
            }

            visited[start.Data] = true;
            queue.Enqueue(start);

            while (queue.Count != 0)
            {
                var node = queue.Dequeue();   // v
                if (node.Data == endChar)
                {
                    return pathLength[node.Data];
                }
                for (var i = 0; i < node.Adjacents.Count; i++)
                {
                    Node toNode = node.Adjacents[i];
                    if (!visited[toNode.Data])
                    {
                        visited[toNode.Data] = true;
                        queue.Enqueue(toNode);

                        pathLength[toNode.Data] = pathLength[node.Data] + 1;
                        parents[toNode.Data] = node;

                        //d[to] = d[v] + 1;  //pathLength
                        //p[to] = v;  //parents
                    }
                }
            }
            return -1;
        }
    }

    public class Node
    {
        public char Data { get; set; }
        public List<Node> Adjacents { get; set; }

        public Node(char data)
        {
            Data = data;
            Adjacents = new List<Node>();
        }
    }
}
