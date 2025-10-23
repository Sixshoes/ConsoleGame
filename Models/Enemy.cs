/*
 * Enemy.cs
 * 敵人類別的實現
 * 
 * 功能說明：
 * 1. 敵人基本屬性：
 *    - 名稱
 *    - 生命值
 *    - 攻擊力
 *    - 防禦力
 *    - 等級
 * 
 * 2. 敵人行為：
 *    - 基本攻擊
 *    - 特殊技能
 *    - 掉落物品
 * 
 * 3. 狀態管理：
 *    - 生命值變化
 *    - 狀態效果
 *    - 戰鬥狀態
 * 
 * 主要方法：
 * - Attack()：執行攻擊
 * - TakeDamage()：受到傷害
 * - Display()：顯示敵人資訊
 * - DropItems()：產生掉落物品
 * 
 * 注意事項：
 * 1. 敵人的屬性應該根據等級進行平衡
 * 2. 掉落物品需要考慮遊戲平衡性
 * 3. 特殊技能的使用條件和冷卻時間
 * 4. 不同類型敵人可能需要特殊的行為模式
 */

using System;
using RPGGame.Models;
using RPGGame.Services;

namespace RPGGame.Models
{
    public class Enemy
    {
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Attack { get; private set; }
        public int MaxHealth { get; private set; }

        public Enemy(string name, int health, int attack)
        {
            Name = name;
            Health = health;
            MaxHealth = health;
            Attack = attack; // 預設攻擊力
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Console.WriteLine($"💥 {Name} 受到 {damage} 傷害，剩餘 HP：{Health}");
        }

        public bool IsDefeated()
        {
            return Health <= 0;
        }
    }
}
