using System.Collections.Generic;
using RPGGame.Models;

namespace RPGGame.Models
{
    public static class ItemFactory
    {
        public static List<Item> CreateBasicItems()
        {
            return new List<Item>
            {
                new Item("小型治療藥水", "回復 30 點 HP", Item.ItemType.Heal, 30, 1),
                new Item("中型治療藥水", "回復 60 點 HP", Item.ItemType.Heal, 60, 1),
                new Item("小型魔力藥水", "回復 30 點 MP", Item.ItemType.Mana, 30, 1),
                new Item("中型魔力藥水", "回復 60 點 MP", Item.ItemType.Mana, 60, 1),
                new Item("戰士之心", "臨時提升攻擊力 20（尚未實作效果）", Item.ItemType.AttackBoost, 20, 1),
                new Item("守護紋章", "臨時提升防禦力 20（尚未實作效果）", Item.ItemType.DefenseBoost, 20, 1)
            };
        }
    }
}
