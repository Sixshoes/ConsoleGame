/*
 * Item.cs
 * 道具系統的核心類別
 * 
 * 功能說明：
 * 1. 定義遊戲中所有道具的基本屬性和行為
 * 2. 提供道具類型枚舉，包括：
 *    - 治療道具 (Heal)：回復生命值
 *    - 魔力道具 (Mana)：回復魔力值
 *    - 攻擊增益 (AttackBoost)：提升攻擊力
 *    - 防禦增益 (DefenseBoost)：提升防禦力
 *    - 貴重物品 (Valuable)：可以賣錢或交換的物品
 *    - 材料 (Material)：合成或任務用的材料
 *    - 任務道具 (Quest)：特殊任務相關道具
 * 
 * 主要方法：
 * - Display()：顯示道具資訊，包括名稱、描述和效果
 * - Use(Player)：使用道具，根據道具類型產生不同效果
 * 
 * 注意事項：
 * 1. 道具效果值(Value)的意義根據道具類型不同而不同
 * 2. 某些道具類型（如材料）不能直接使用
 * 3. 治療和魔力回復會考慮最大值限制
 */

using System;
using System.Collections.Generic;
using RPGGame.Models;

namespace RPGGame.Models
{
    public class Item
    {
        public enum ItemType
        {
            Heal,
            Mana,
            AttackBoost,
            DefenseBoost,
            Valuable,      // 貴重物品
            Material,      // 材料
            Quest         // 任務道具
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ItemType Type { get; private set; }
        public int Value { get; private set; }
        public int Quantity { get; private set; }

        public Item(string name, string description, ItemType type, int value, int quantity)
        {
            Name = name;
            Description = description;
            Type = type;
            Value = value;
            Quantity = quantity;
        }

        public void Display()
        {
            string effectText = Type switch
            {
                ItemType.Heal => $"回復 {Value} HP",
                ItemType.Mana => $"回復 {Value} MP",
                ItemType.AttackBoost => $"提升 {Value} 攻擊力",
                ItemType.DefenseBoost => $"提升 {Value} 防禦力",
                ItemType.Valuable => $"價值 {Value} 金幣",
                ItemType.Material => "材料",
                ItemType.Quest => "任務道具",
                _ => ""
            };
            Console.WriteLine($"🎁 道具：{Name} - {Description} ({effectText})");
        }

        public void Use(Player player)
        {
            switch (Type)
            {
                case ItemType.Heal:
                    int healAmount = Math.Min(Value, player.MaxHealth - player.Health);
                    player.Health += healAmount;
                    Console.WriteLine($"💚 {player.Name} 恢復了 {healAmount} 點生命值！");
                    break;

                case ItemType.Mana:
                    int manaAmount = Math.Min(Value, player.MaxMagicPower - player.MagicPower);
                    player.MagicPower += manaAmount;
                    Console.WriteLine($"💙 {player.Name} 恢復了 {manaAmount} 點魔力值！");
                    break;

                case ItemType.AttackBoost:
                    player.Attack += Value;
                    Console.WriteLine($"💪 {player.Name} 的攻擊力提升了 {Value} 點！");
                    break;

                case ItemType.DefenseBoost:
                    player.Defense += Value;
                    Console.WriteLine($"🛡️ {player.Name} 的防禦力提升了 {Value} 點！");
                    break;

                default:
                    Console.WriteLine("這個道具不能直接使用！");
                    break;
            }
        }
    }
}
