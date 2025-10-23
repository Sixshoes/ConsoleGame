using System;
using RPGGame.Models;
using RPGGame.Services;

namespace RPGGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("歡迎來到 RPG 遊戲！");
            Console.WriteLine("請輸入你的角色名稱：");
            string? playerName = Console.ReadLine();
            
            Player player = new Player(playerName ?? "無名勇者", 100, "冒險者", 120, 30, 10);
            Console.WriteLine($"你好，{player.Name}！");
            
            while (true)
            {
                Console.WriteLine("\n請選擇你要進行的操作：");
                Console.WriteLine("1. 探索");
                Console.WriteLine("2. 查看狀態");
                Console.WriteLine("3. 離開遊戲");
                
                string? input = Console.ReadLine();
                
                switch (input)
                {
                    case "1":
                        ExploreSystem.chooseLocation(player);
                        break;
                    case "2":
                        player.DisplayInfo();
                        break;
                    case "3":
                        Console.WriteLine("感謝遊玩，再見！");
                        return;
                    default:
                        Console.WriteLine("無效的選擇，請重新輸入。");
                        break;
                }
            }
        }
    }
}