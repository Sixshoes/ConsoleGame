using System;
using System.Collections.Generic;
using RPGGame.Models;

namespace RPGGame.Models
{
    public class SkillFactory
    {
        public static List<Skill> CreateSkillList()
        {
            return new List<Skill>
            {
                new Skill("火球術", 50, "對敵人造成火焰傷害，基礎遠距攻擊魔法", SkillType.Attack, 30, 2),
                new Skill("冰封術", 45, "對敵人造成冰屬性傷害，並有機率延緩敵人回合", SkillType.Attack, 35, 3),
                new Skill("雷擊術", 60, "召喚閃電攻擊，對防禦高的敵人特別有效", SkillType.Attack, 40, 3),
                new Skill("治癒術", 50, "恢復自身 50 點生命", SkillType.Heal, 25, 2),
                new Skill("淨化光", 0, "移除一項負面狀態（後續可擴充）", SkillType.Heal, 20, 3),
                new Skill("魔力注入", 30, "恢復自身 30 魔力", SkillType.Buff, 20, 4),
                new Skill("魔法護盾", 0, "本回合減少 50% 傷害", SkillType.Buff, 15, 2),
                new Skill("烈焰爆發", 80, "強力火系魔法，對全體敵人造成範圍傷害（若有）", SkillType.Attack, 50, 4),
                new Skill("靜心冥想", 0, "本回合不能攻擊，下回合開始魔力恢復翻倍（可疊加）", SkillType.Buff, 10, 5),
                new Skill("魔能衝擊", 120, "消耗大量 MP，對敵人造成超高純魔力傷害，會暫時降低自身 MP 上限", SkillType.Attack, 60, 5)
            };
        }
    }
}