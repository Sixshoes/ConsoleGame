/*
 * BattleSystem.cs
 * 戰鬥系統的核心實現
 * 
 * 功能說明：
 * 1. 管理戰鬥流程：
 *    - 回合制戰鬥
 *    - 玩家和敵人的行動順序
 *    - 戰鬥結果判定
 * 2. 處理戰鬥相關的計算：
 *    - 傷害計算
 *    - 技能效果
 *    - 狀態效果
 * 3. 提供戰鬥介面：
 *    - 顯示戰鬥資訊
 *    - 玩家指令輸入
 *    - 戰鬥結果顯示
 * 
 * 主要方法：
 * - StartBattle()：開始戰鬥
 * - ProcessTurn()：處理回合
 * - HandlePlayerAction()：處理玩家行動
 * - HandleEnemyAction()：處理敵人行動
 * - CheckBattleEnd()：檢查戰鬥結束條件
 * 
 * 注意事項：
 * 1. 需要確保玩家和敵人的狀態正確更新
 * 2. 戰鬥結束時要清理所有暫時性效果
 * 3. 戰鬥系統需要與其他系統（如物品系統）協同工作
 * 4. 要處理特殊情況（如逃跑、技能失敗等）
 */

using System;
using System.Collections.Generic;
using RPGGame.Models;

namespace RPGGame.Services
{
    public static class BattleSystem
    {
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
                
                // 回合結束，減少技能冷卻和處理增益效果
                ReduceSkillCooldowns(player);
                player.EndTurn();
            }
            
            if (player.IsDefeated())
            {
                Console.WriteLine("你被打敗了！");
            }
            else
            {
                Console.WriteLine($"你擊敗了 {enemy.Name}！");
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
                    // 道具系統待實現
                    Console.WriteLine("道具系統尚未實作");
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

        private static void ReduceSkillCooldowns(Player player)
        {
            foreach (var skill in player.Skills)
            {
                skill.ReduceCooldown();
            }
        }
    }
}
