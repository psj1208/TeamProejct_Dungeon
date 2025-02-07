using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{
    public abstract class Skill
    {
        public string Name { get; }
        public string description { get; }

        public Skill(string _name, string _description)
        {
            Name = _name;
            description = _description;
        }

        public abstract void Use(Player player, List<Monster> monsters);
    }


    public class WarriorSkill : Skill
    {
        public WarriorSkill() : base("전사 스킬", "전사 스킬 설명 ") { }

        public override void Use(Player player, List<Monster> monsters)
        {
            Console.WriteLine("사용할 스킬을 선택하세요.");
            Console.WriteLine("1. 워리어 스킬");
            Console.WriteLine("2. 워리어 스킬");

            int skillChoice = Text.GetInput(null, 1);

            Console.WriteLine("\n공격할 몬스터를 선택하세요.");
            List<Monster> selcetMonsters = Text.GetInputMulti(2, monsters);

            if (skillChoice == 1)
            {
                Warriorskill1(player, selcetMonsters);
            }
        }

        public void Warriorskill1(Player player, List<Monster> monsters)
        {
            foreach (Monster monster in monsters)
            {
                int damage = player.atk * 2;
                Console.WriteLine($"전사 스킬1 사용! {monster.Name}에게 {damage}의 피해를 입혔습니다.");
                monster.TakeDamage(damage);
            }
        }
    }


}

    //public class AssassinSkill : Skill
    //{
    //    public AssassinSkill() : base("도적 스킬", "기본 공격에 추가 피해를 입힘") { }

    //    public void UseAssassinSkil1(Player player, List<Monster> monsters)
    //    {
    //        foreach (Monster monster in monsters)
    //        {
    //            int damage = player.atk;
    //            Text.TextingLine($"전사 1 스킬 사용! {damage} 피해를 {monster.Name} 에게 입혔습니다! ", ConsoleColor.Red);
    //            monster.TakeDamage(player.atk * 2);
    //            Console.WriteLine($"{monster.Name}에게 {damage}의 피해를 입혔습니다.");
    //        }
    //    }
    //}

