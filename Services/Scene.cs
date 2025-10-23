using System;
using System.Threading;
using RPGGame.Models;

namespace RPGGame.Services
{
    public static class Scene
    {
        public static void ShowTitle()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("🌙《希爾薇的魔法冒險》🌙");
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void WaitAndClear(string message)
        {
            Console.WriteLine(message);
            Thread.Sleep(1500);
            Console.Clear();
        }
    }
}
