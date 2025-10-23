using System;
using System.Collections.Generic;
using RPGGame.Services;

namespace RPGGame.Models
{
    // 玩家類別：管理玩家的所有屬性、技能和狀態
    public class Player
    {
        // 基本屬性
        public string Name { get; private set; }        // 玩家名稱
        public int Health { get; set; }                 // 當前生命值
        public int MaxHealth { get; private set; }      // 最大生命值
        public string Title { get; private set; }       // 玩家稱號
        public int Attack { get; set; }                 // 當前攻擊力
        public int BaseAttack { get; private set; }     // 基礎攻擊力
        public int Defense { get; set; }                // 當前防禦力
        public int BaseDefense { get; private set; }    // 基礎防禦力
        public int MagicPower { get; set; }            // 當前魔力值
        public int MaxMagicPower { get; private set; }  // 最大魔力值
        public List<Skill> Skills { get; private set; } // 技能列表
        public Inventory Backpack { get; private set; } // 背包系統

        // 增益效果持續時間追蹤
        private int attackBoostTurns = 0;   // 攻擊力提升持續回合
        private int defenseBoostTurns = 0;  // 防禦力提升持續回合

        // 建構子：初始化玩家屬性
        public Player(string name, int health, string title, int magicPower, int attack, int defense)
        {
            Name = name;
            MaxHealth = health;
            Health = MaxHealth;
            Title = title;
            Attack = attack;
            BaseAttack = attack;
            Defense = defense;
            BaseDefense = 10; // 預設基礎防禦力
            MagicPower = magicPower;
            MaxMagicPower = magicPower;
            Skills = new List<Skill>();
            basicSkill();
            Backpack = new Inventory();
        }

        // 初始化基礎技能
        public void basicSkill()
        {
            Skills.Add(SkillFactory.CreateSkillList()[0]);
            Skills.Add(SkillFactory.CreateSkillList()[1]);
        }

        // 顯示玩家資訊
        public void DisplayInfo()
        {
            Console.Clear();
            Console.WriteLine($"🧝 角色名稱：{Name}");
            Console.WriteLine($"❤️ 生命值：{Health}/{MaxHealth}");
            Console.WriteLine($"💥 攻擊力：{Attack} (基礎：{BaseAttack})");
            Console.WriteLine($"🛡️ 防禦力：{Defense} (基礎：{BaseDefense})");
            Console.WriteLine($"🏷️ 稱號：{Title}");
            Console.WriteLine($"✨ 魔力值：{MagicPower}/{MaxMagicPower}");
        }

        // 學習新技能
        public void LearnSkill(Skill skill)
        {
            Skills.Add(skill);
            Console.WriteLine($"🎉 {Name} 學會了 {skill.Name}！");
        }

        // 顯示已學習的技能列表
        public void DisplaySkills()
        {
            Console.WriteLine($"\n📚 {Name} 的技能清單：");
            for (int i = 0; i < Skills.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Skills[i].Name}");
            }
        }

        // 使用技能：根據技能類型產生不同效果
        public void UseSkill(int index, Enemy enemy)
        {
            if (index >= 0 && index < Skills.Count)
            {
                Skill skill = Skills[index];
                switch (skill.Type)
                {
                    case SkillType.Attack:  // 攻擊型技能
                        enemy.TakeDamage(skill.Power);
                        MagicPower -= skill.MP;
                        Console.WriteLine($"🔥 {Name} 使用 {skill.Name} 對 {enemy.Name} 造成 {skill.Power} 傷害！");
                        skill.Cooldown = skill.Cooldown + 1;
                        break;
                    case SkillType.Heal:    // 治療型技能
                        Health = Math.Min(Health + skill.Power, MaxHealth);
                        MagicPower -= skill.MP;
                        Console.WriteLine($"💚 {Name} 使用 {skill.Name} 恢復了 {skill.Power} 點生命值！");
                        skill.Cooldown = skill.Cooldown + 1;
                        break;
                    case SkillType.Buff:    // 增益型技能
                        Attack += skill.Power;
                        attackBoostTurns = 3; // 持續3回合
                        Console.WriteLine($"💪 {Name} 使用 {skill.Name} 提升了 {skill.Power} 點攻擊力！");
                        MagicPower -= skill.MP;
                        skill.Cooldown = skill.Cooldown + 1;
                        break;
                }
            }
        }

        // 受到傷害：計算實際傷害並更新生命值
        public void TakeDamage(int damage)
        {
            int actualDamage = Math.Max(1, damage - Defense);
            Health -= actualDamage;
            Console.WriteLine($"💥 {Name} 受到 {actualDamage} 傷害，剩餘 HP：{Health}");
        }

        // 恢復生命值
        public void Heal(int amount)
        {
            int healAmount = Math.Min(amount, MaxHealth - Health);
            Health += healAmount;
            Console.WriteLine($"💚 {Name} 恢復了 {healAmount} 點生命值，剩餘 HP：{Health}");
        }

        // 恢復魔力值
        public void RestoreMana(int amount)
        {
            int manaAmount = Math.Min(amount, MaxMagicPower - MagicPower);
            MagicPower += manaAmount;
            Console.WriteLine($"💙 {Name} 恢復了 {manaAmount} 點魔力值，剩餘 MP：{MagicPower}");
        }

        // 回合結束：處理所有增益效果的持續時間
        public void EndTurn()
        {
            // 處理攻擊力增益效果
            if (attackBoostTurns > 0)
            {
                attackBoostTurns--;
                if (attackBoostTurns == 0)
                {
                    Attack = BaseAttack;
                    Console.WriteLine($"💪 {Name} 的攻擊力增益效果結束了！");
                }
            }

            // 處理防禦力增益效果
            if (defenseBoostTurns > 0)
            {
                defenseBoostTurns--;
                if (defenseBoostTurns == 0)
                {
                    Defense = BaseDefense;
                    Console.WriteLine($"🛡️ {Name} 的防禦力增益效果結束了！");
                }
            }
        }

        // 檢查玩家是否被擊敗
        public bool IsDefeated()
        {
            return Health <= 0;
        }
    }
}
