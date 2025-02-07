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
            Console.WriteLine("사용할 스킬을 선택하세요.\n");
            Console.WriteLine("1. 파워 어택!! : 2배의 데미지로 공격 합니다.");
            Console.WriteLine("2. 운수 좋은 날~ : 1~3배의 데미지로 공격합니다.");

            Console.WriteLine("\n공격할 몬스터를 선택하세요.");

            int skillChoice = Text.GetInput(null, 1);
            
            if (skillChoice == 1)
            {
                List<Monster> selcetMonsters = Text.GetInputMulti(1, monsters);
                Warriorskill1(player, selcetMonsters);
            }
            else if (skillChoice == 2)
            {
                
                Console.WriteLine("\n공격할 몬스터를 선택하세요.");
            }
        }

        public void Warriorskill1(Player player, List<Monster> monsters)
        {
            foreach (Monster monster in monsters)
            {
                
                int damage = player.atk * 2;
                Console.WriteLine($"파워 어택!! {monster.Name}에게 {damage}의 피해를 입혔습니다.");
                monster.TakeDamage(damage);
            }
        }

        public void Warriorskill2(Player player, List<Monster> monsters)
        {
            foreach (Monster monster in monsters)
            {
                Random random = new Random();
                int randomDamage = random.Next(1, 4);

                int damage = player.atk * randomDamage;
                Console.WriteLine($"운수 좋은 날~ {monster.Name}에게 {damage}의 피해를 입혔습니다.");
                monster.TakeDamage(damage);
            }
        }
    }


    public class AssassinSkill : Skill
    {
        public AssassinSkill() : base("도적 스킬", "기본 공격에 추가 피해를 입힘") { }

        public override void Use(Player player, List<Monster> monsters)
        {
            Console.WriteLine("사용할 스킬을 선택하세요.\n");
            Console.WriteLine("1. 2명 기본공격");
            Console.WriteLine("2. 4명 기본공격*0.5");

            Console.WriteLine("\n공격할 몬스터를 선택하세요.");

            int skillChoice = Text.GetInput(null, 1);

            

            if (skillChoice == 1)
            {
                List<Monster> selcetMonsters = Text.GetInputMulti(2, monsters);

                AssassinSkil1(player, selcetMonsters);
            }

            else if (skillChoice == 2)
            {
                Console.WriteLine("\n공격할 몬스터를 선택하세요.");
            }
        }

        public void AssassinSkil1(Player player, List<Monster> monsters)
        {
            foreach (Monster monster in monsters)
            {
                int damage = player.atk;
                Text.TextingLine($"어쎄신 1 스킬 사용!\n {damage} 피해를 {monster.Name} 에게 입혔습니다! ", ConsoleColor.Red);
                monster.TakeDamage(player.atk * 2);
            }
        }
    }
}


