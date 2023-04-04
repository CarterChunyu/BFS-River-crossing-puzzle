using System;
using System.Collections.Generic;

namespace DFS
{
    class Program
    {
        static void Main(string[] args)
        {
            string start =Console.ReadLine();
            string destination = Console.ReadLine();
            new DFS().Print_Path(start, destination);
        }
    }
    // DFS得到的並不一定是最短路徑
    class DFS
    {
        private Dictionary<string, List<string>> Map;
        public DFS()
        {
            this.Map=new Dictionary<string, List<string>>()
            {
                {"A",new List<string>(){"B","C"}},
                {"B",new List<string>(){"A","C","D"}},
                {"C",new List<string>(){"A","B","D","E"}},
                {"D",new List<string>(){"B","C","E","F"}},
                {"E",new List<string>(){"C","D"}},
                {"F",new List<string>(){"D"}}
            };
        }
        private Dictionary<string,string> Calculator(string Start)
        {
            Dictionary<string, string> parent = new Dictionary<string, string>() { { Start, null } };
            List<string> Seens = new List<string>() { Start };
            Stack<string> stack = new Stack<string>();
            stack.Push(Start);
            while (stack.Count > 0)
            {
                string node = stack.Pop();
                foreach (var item in Map[node])
                {
                    if (!Seens.Contains(item))
                    {
                        Seens.Add(item);
                        parent.Add(item, node);
                        stack.Push(item);
                    }
                }
            }
            return parent;
        }
        public void Print_Path(string start,string destination)
        {
            Dictionary<string,string> parent = Calculator(start);
            Stack<string> stack = new Stack<string>();
            while (destination != null)
            {
                stack.Push(destination);
                destination = parent[destination];
            }
            while (stack.Count > 0)
            {
                Console.Write($"{stack.Pop()}=>");
            }
        }
    }
}
