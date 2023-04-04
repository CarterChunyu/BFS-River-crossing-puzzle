using System;
using System.Collections.Generic;

namespace BFS_FarmerPassRiver
{
    class Program
    {
        static void Main(string[] args)
        {
            string Start = "0,0,0,0";
            string Destination = "1,1,1,1";
            new Farmer().Get_Path(Start, Destination);
        }
    }
    class Farmer
    {
        // 農夫,狼,羊,菜 0 表示出發位置,1表示對岸 
        enum Role
        {
            農夫,狼,羊,菜
        }
        // C# 字串是享元的, 而陣列是記憶體位置, 用字串比較較為方便
        private bool IsWin(string[] position)
        {
            // 農夫在對岸 狼跟羊在一起
            if (position[0] != position[1] && position[1] == position[2])
                return false;
            // 農夫在對岸 羊跟菜在一起
            if (position[0] != position[2] && position[2] == position[3])
                return false;
            return true;
        }
        private Dictionary<string,List<string>> Create_Position_Dictionary(string Start)
        {
            Dictionary<string, List<string>> Map = new Dictionary<string, List<string>>();
            Create_Position_Dictionary(Map, Start);
            return Map;
        }
        private void Create_Position_Dictionary(Dictionary<string, List<string>> Map, string position)
        {
            // 終止條件 全部到達
            if (position == "1,1,1,1")
            {
                if (!Map.ContainsKey(position))
                    Map.Add(position, new List<string>());
                return;
            }
            else
            {
                List<string> result = new List<string>();
                // 列出各種情況                
                for (int i = 0; i < 4; i++)
                {
                    string[] arr = position.Split(",");
                    // 農夫自己走
                    if (i == 0)
                    {
                        arr[0] = arr[0] == "0" ? "1" : "0";
                    }
                    else if(arr[i]==arr[0])
                    {
                        arr[i] = arr[i] == "0" ? "1" : "0";
                        arr[0] = arr[i];
                    }
                    else
                    {
                        continue;
                    }

                    // 判斷是可以的走法保存起來  
                    if (IsWin(arr))
                        result.Add(string.Join(",", arr));
                }

                Map.Add(position, result);

                foreach (var item in result)
                {
                    if (!Map.ContainsKey(item))
                        Create_Position_Dictionary(Map, item);
                }
            }
        }
        private Dictionary<string,string> BFS(string Start,Dictionary<string,List<string>> Map)
        {
            Dictionary<string, string> parent = new Dictionary<string, string>() { { Start, null } };  
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(Start);
            List<string> Seens = new List<string>() { Start };
            while (queue.Count > 0)
            {
                string position = queue.Dequeue();
                foreach (var item in Map[position])
                {
                    if (!Seens.Contains(item))
                    {
                        queue.Enqueue(item);
                        parent.Add(item, position);
                        Seens.Add(item);
                    }
                }
            }
            return parent;
        }
        public void Get_Path(string Start,string Destination)
        {
            Dictionary<string, List<string>> Map = Create_Position_Dictionary(Start);
            Dictionary<string, string> parent = BFS(Start, Map);
            Stack<string> path = new Stack<string>();
            while (Destination != null)
            {
                path.Push(Destination);
                Destination = parent[Destination];
            }
            Print(path);
        }
        private void Print(Stack<string> path)
        {
            while (path.Count > 0)
            {
                string[] posititon = path.Pop().Split(",");
                List<string> left = new List<string>();
                List<string> right = new List<string>();
                for (int i = 0; i < 4; i++)
                {
                    if (posititon[i] == "0")
                        left.Add(((Role)i).ToString());
                    else
                        right.Add(((Role)i).ToString());
                }

                Console.WriteLine($"左岸: {string.Join(" , ",left)}");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("----------- 河流 ------------");
                Console.WriteLine("-----------------------------");
                Console.WriteLine($"右岸: {string.Join(" , ", right)}");
                Console.WriteLine();
                Console.WriteLine();
            }



        }
    }
}
