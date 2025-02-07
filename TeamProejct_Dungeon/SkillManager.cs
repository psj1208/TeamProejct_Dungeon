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
    }

    public static class SkillDb
    {
        public static readonly Skill WarriorSkill1 = new Skill(
            "전사스킬1",
            "공격력 2배만큼 선택 몬스터 공격",
            SkillEffectDb.Skill1
            );

        public static readonly Skill AssassinSkill1 = new Skill(
           "도적스킬1",
           "각 몬스터에게 기본공격만큼 추가 딜",
           (player, target) =>
           {
           });
    }

    public static class SkillEffectDb
    {
        public static void Skill1(Player player, params ICharacter[] monsters)
        {
            //여기서 몬스터 그룹 받고. 선택하는 함수?
            //
            //
            foreach(ICharacter monster in monsters)
            {
                int damage = player.atk * 2;
                Text.TextingLine($"전사 1 스킬 사용! {damage} 피해를 {monster.Name} 에게 입혔습니다! ", ConsoleColor.Red);
                monster.TakeDamage(player.atk * 2);
            }
        }
    }
}
