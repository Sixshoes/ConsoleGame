using System;
using System.Collections.Generic;
using RPGGame.Models;

namespace RPGGame.Models
{
    public class Inventory
    {
        private List<Item> items = new List<Item>();

        public void AddItem(Item item)
        {
            items.Add(item);
            Console.WriteLine($"✅ 獲得道具：{item.Name}");
        }

        public void ShowItems()
        {
            Console.Clear();
            Console.WriteLine("\n🎒 你的背包內容：");

            if (items.Count == 0)
            {
                Console.WriteLine("（背包是空的）");
                return;
            }

            for (int i = 0; i < items.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                items[i].Display();
            }
        }

        public void UseItem(int index, Player player)
        {
            if (index < 0 || index >= items.Count)
            {
                Console.WriteLine("無效的道具編號");
                return;
            }

            var item = items[index];
            switch (item.Type)
            {
                case Item.ItemType.Heal:
                    player.Heal(item.Value);
                    break;
                case Item.ItemType.Mana:
                    player.RestoreMana(item.Value);
                    break;
                case Item.ItemType.AttackBoost:
                    player.Attack += item.Value;
                    break;
                case Item.ItemType.DefenseBoost:
                    player.Defense += item.Value;
                    break;
            }   
            items.RemoveAt(index);
            Console.WriteLine($"✅ 使用道具：{item.Name}");
        }

        public bool HasItems() => items.Count > 0;
    }
}
