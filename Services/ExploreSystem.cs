using System;
using System.Collections.Generic;
using RPGGame.Models;

namespace RPGGame.Services
{
    public static class ExploreSystem
    {
        private static List<Enemy> monsters = new List<Enemy>
        {
            new Enemy("野生哥布林", 60, 10),
            new Enemy("骷髏戰士", 80, 10),
            new Enemy("哥布林首領", 100, 15),
            new Enemy("哥布林巫師", 70, 12)
        };

        public static void Explore(Player player, string location)
        {
            Console.WriteLine($"\n🔍 你正在探索：{location}");

            var rand = new Random();
            int roll = rand.Next(100); // 0～99

            List<Enemy> monsters = new List<Enemy>();
            int encounterRate = 0, itemRate = 0;

            // 設定地區對應資料
            if (location == "forest")
            {
                monsters.Add(new Enemy("哥布林", 60, 10));
                monsters.Add(new Enemy("史萊姆", 50, 8));
                encounterRate = 50;
                itemRate = 80;
            }
            else if (location == "desert")
            {
                monsters.Add(new Enemy("沙蟲", 70, 12));
                monsters.Add(new Enemy("沙漠巨蠍", 90, 15));
                encounterRate = 60;
                itemRate = 85;
            }
            else if (location == "mountain")
            {
                monsters.Add(new Enemy("山巨人", 100, 20));
                monsters.Add(new Enemy("鷹身女妖", 80, 18));
                encounterRate = 70;
                itemRate = 90;
            }
            else if (location == "river")
            {
                monsters.Add(new Enemy("水元素", 60, 15));
                monsters.Add(new Enemy("魚人戰士", 70, 12));
                encounterRate = 55;
                itemRate = 85;
            }

            if (roll < encounterRate)
            {
                var enemy = monsters[rand.Next(monsters.Count)];
                Console.WriteLine($"⚠️ 你遇到了 {enemy.Name}！");
                StartBattle(player, enemy);
            }
            else if (roll < itemRate)
            {
                var item = new Item("神秘礦石", "可以在村莊換錢", Item.ItemType.Material, 50, 1);
                player.Backpack.AddItem(item);
                Console.WriteLine("獲得了神秘礦石！");
            }
            else
            {
                Console.WriteLine("😶 你走了一圈，什麼都沒發現。");
            }
        }

        public static void chooseLocation(Player player)
        {
            Console.WriteLine("\n請選擇你要去的地點");
            Console.WriteLine("1. 森林");
            Console.WriteLine("2. 沙漠");
            Console.WriteLine("3. 山脈");
            Console.WriteLine("4. 河流");
            Console.WriteLine("5. 離開遊戲");
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Explore(player, "forest");
                    break;
                case "2":
                    Explore(player, "desert");
                    break;
                case "3":
                    Explore(player, "mountain");
                    break;
                case "4":
                    Explore(player, "river");
                    break;
                case "5":
                    Console.WriteLine("你選擇了離開遊戲");
                    break; 
            }
        }

        public static void StartBattle(Player player, Enemy enemy)
        {
            Console.WriteLine($"\n⚔️ 戰鬥開始！你遇到了 {enemy.Name}！");
            
            while (!player.IsDefeated() && !enemy.IsDefeated())
            {
                Console.WriteLine("\n=== 戰鬥回合開始 ===");
                Console.WriteLine($"你的狀態：HP {player.Health}/{player.MaxHealth}, MP {player.MagicPower}/{player.MaxMagicPower}");
                Console.WriteLine($"敵人狀態：HP {enemy.Health}/{enemy.MaxHealth}");
                
                // 玩家回合
                PlayerTurn(player, enemy);
                if (enemy.IsDefeated()) break;
                
                // 敵人回合
                EnemyTurn(player, enemy);
                if (player.IsDefeated()) break;
            }
            
            if (player.IsDefeated())
            {
                Console.WriteLine("你被打敗了！");
            }
            else
            {
                Console.WriteLine($"你擊敗了 {enemy.Name}！");
                // 掉落物品
                if (new Random().Next(100) < 30) // 30% 機率掉落物品
                {
                    var item = new Item("神秘礦石", "可以在村莊換錢", Item.ItemType.Valuable, 50, 1);
                    player.Backpack.AddItem(item);
                    Console.WriteLine("獲得了神秘礦石！");
                }
            }
        }

        private static void PlayerTurn(Player player, Enemy enemy)
        {
            Console.WriteLine("\n請選擇行動：");
            Console.WriteLine("1. 普通攻擊");
            Console.WriteLine("2. 使用技能");
            Console.WriteLine("3. 使用道具");
            
            string? input = Console.ReadLine();
            
            switch (input)
            {
                case "1":
                    enemy.TakeDamage(player.Attack);
                    break;
                case "2":
                    UseSkill(player, enemy);
                    break;
                case "3":
                    if (player.Backpack.HasItems())
                    {
                        player.Backpack.ShowItems();
                        Console.WriteLine("請選擇要使用的道具編號（輸入 0 返回）：");
                        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0)
                        {
                            player.Backpack.UseItem(choice - 1, player);
                        }
                    }
                    else
                    {
                        Console.WriteLine("背包是空的！");
                    }
                    break;
                default:
                    Console.WriteLine("無效的選擇，跳過回合");
                    break;
            }
        }

        private static void UseSkill(Player player, Enemy enemy)
        {
            Console.WriteLine("\n可用的技能：");
            for (int i = 0; i < player.Skills.Count; i++)
            {
                var skill = player.Skills[i];
                Console.Write($"{i + 1}. {skill.Name}");
                if (!skill.IsReady())
                {
                    Console.Write($" (冷卻中，剩餘 {skill.CurrentCooldown} 回合)");
                }
                Console.WriteLine();
            }
            
            Console.WriteLine("請選擇技能（輸入數字）或輸入 0 返回：");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= player.Skills.Count)
            {
                var skill = player.Skills[choice - 1];
                
                if (!skill.IsReady())
                {
                    Console.WriteLine("技能還在冷卻中！");
                    return;
                }
                
                if (player.MagicPower < skill.MP)
                {
                    Console.WriteLine("MP 不足！");
                    return;
                }
                
                player.UseSkill(choice - 1, enemy);
                skill.StartCooldown();
            }
        }

        private static void EnemyTurn(Player player, Enemy enemy)
        {
            Console.WriteLine($"\n{enemy.Name} 的回合！");
            player.TakeDamage(enemy.Attack);
        }
    }
}
