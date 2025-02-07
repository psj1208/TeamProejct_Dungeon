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
        public Action<Player, ICharacter> effect { get; }

        public Skill(string _name, string _description, Action<Player, ICharacter> _effect)
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
            (player, target) =>
            {
                int damage = player.atk + player.equipAtk;
                target.TakeDamage(damage);
            });

        public static readonly Skill AssassinSkill1 = new Skill(
           "도적스킬1",
           "각 몬스터에게 기본공격만큼 추가 딜",
           (player, target) =>
           {
               //GameManager -> Stage 로 바꾸기
               foreach (Monster monster in GameManager.monsters)
               {
                   int damage = player.atk + player.atk + player.equipAtk;
                   monster.TakeDamage(damage);
               }
           });

    }

}
