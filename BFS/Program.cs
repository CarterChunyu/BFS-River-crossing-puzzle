using System;
using System.Collections.Generic;

namespace BFS
{
    class Program
    {
        static void Main(string[] args)
        {
            string start = Console.ReadLine();
            string destination = Console.ReadLine();
            new BFS().Print(start, destination);
        }
    }
    class BFS
    {
        private Dictionary<string, List<string>> Map;
        public BFS()
        {
            this.Map = new Dictionary<string, List<string>>()
            {
                {"A",new List<string>(){"B","C"}},
                {"B",new List<string>(){"A","C","D"}},
                {"C",new List<string>(){"A","B","D","E"}},
                {"D",new List<string>(){"B","C","E","F"}},
                {"E",new List<string>(){"C","D"}},
                {"F",new List<string>(){"D"}}
            };
        }
        private void Calculator(string Start,out Dictionary<string,int> Deep,out Dictionary<string,string> Parent)
        {
            Deep = new Dictionary<string, int>() { { Start, 0 } };
            Parent = new Dictionary<string, string>() { { Start, null } };
            List<string> Seens = new List<string>() {Start };
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(Start);
            while (queue.Count > 0)
            {
                string node = queue.Dequeue();
                foreach (var item in Map[node])
                {
                    if (!Seens.Contains(item))
                    {
                        Seens.Add(item);
                        queue.Enqueue(item);
                        Deep.Add(item, Deep[node] + 1);
                        Parent.Add(item, node);
                    }
                }
            }
        }
        public void Print(string Start,string Destination)
        {
            Dictionary<string, int> deep;
            Dictionary<string, string> parent;
            Calculator(Start, out deep, out parent);
            // 印出到各點距離
            foreach (var item in deep)
            {
                Console.WriteLine($"位置:{item.Key}-距離:{item.Value}");
            }
            // 印出路徑圖
            Stack<string> stack = new Stack<string>();
            do
            {
                stack.Push(Destination);
                Destination = parent[Destination];
            }
            while (Destination != null);
            while(stack.Count>0)
            {
                Console.Write($"{stack.Pop()}=>");
            }
        }
    }
}
