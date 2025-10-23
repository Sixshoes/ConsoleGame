using System;
using System.Collections.Generic;

namespace RPGGame.Models
{
    public enum SkillType
    {
        Attack,
        Heal,
        Buff,
        Debuff
    }

    public class Skill
    {
        public string Name { get; set; }
        public int Power { get; set; }
        public string Description { get; set; }
        public SkillType Type { get; set; }
        public int MP { get; set; }
        public int Cooldown { get; set; }
        public int CurrentCooldown { get; private set; }

        public Skill(string name, int power, string description, SkillType type, int mp, int cooldown)
        {
            Name = name;
            Power = power;
            Description = description;
            Type = type;
            MP = mp;
            Cooldown = cooldown;
            CurrentCooldown = 0;
        }

        public bool IsReady()
        {
            return CurrentCooldown == 0;
        }

        public void StartCooldown()
        {
            CurrentCooldown = Cooldown;
        }

        public void ReduceCooldown()
        {
            if (CurrentCooldown > 0)
            {
                CurrentCooldown--;
            }
        }

        public void ResetCooldown()
        {
            CurrentCooldown = 0;
        }

        public void Display()
        {
            Console.WriteLine($"📖 技能：{Name}");
            Console.WriteLine($"💪 威力：{Power}");
            Console.WriteLine($"📝 描述：{Description}");
            Console.WriteLine($"🎯 類型：{Type}");
            Console.WriteLine($"🔮 MP消耗：{MP}");
            Console.WriteLine($"⏱️ 冷卻：{Cooldown}回合");
            if (CurrentCooldown > 0)
            {
                Console.WriteLine($"⏳ 剩餘冷卻：{CurrentCooldown}回合");
            }
        }
    }
}
