using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{
    public class Skill
    {
        public string Name { get; }
        public string description { get; }
        public Action<Player, ICharacter[]> effect { get; }

        public Skill(string _name, string _description, Action<Player, ICharacter[]> _effect)
        {
            Name = _name;
            description = _description;
            effect = _effect;
        }

        public void Use(Player player, ICharacter[] target)
        {
            Console.WriteLine($"{player.Name} 이 {Name}을(를) 사용했습니다.");
            effect(player, target);
        }
    }

    public static class SkillDb
    {
        public static readonly Skill WarriorSkill1 = new Skill("전사스킬1", "공격력 2배만큼 공격", SkillEffectDb.WarriorSkill1);

    }

    public static class SkillEffectDb
    {
        public static void WarriorSkill1(Player player, ICharacter[] targets)
        {
            foreach(ICharacter target in targets)
            {
                int damage = player.atk * 2;
                Text.TextingLine($"전사 1 스킬 사용! {damage} 피해를 {target.Name} 에게 입혔습니다! ", ConsoleColor.Red);
                target.TakeDamage(player.atk * 2);
            }
        }
    }
}
