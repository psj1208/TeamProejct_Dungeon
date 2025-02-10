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
            Console.WriteLine("1. 파워 어택!! - MP 10\n   2배의 데미지로 공격 합니다.");
            Console.WriteLine("2. 운수 좋은 날~ - MP 15\n   1~3배의 데미지로 공격합니다.");

            Console.WriteLine("\n스킬을 선택하세요.");

            int skillChoice = Text.GetInput(null,1, 2);
            
            if (skillChoice == 1)
            {
                Console.WriteLine("공격할 몬스터를 선택하세요.\n");
                List<Monster> selcetMonsters = Text.GetInputMulti(1, monsters);
                Warriorskill1(player, selcetMonsters);
            }
            else if (skillChoice == 2)
            {
                Console.WriteLine("공격할 몬스터를 선택하세요.\n");
                List<Monster> selcetMonsters = Text.GetInputMulti(1, monsters);
                Warriorskill2(player, selcetMonsters);
            }
        }

        public void Warriorskill1(Player player, List<Monster> monsters)
        {
            foreach (Monster monster in monsters)
            {
                int damage = player.atk * 2;
                Console.WriteLine($"파워 어택!!\n\n{monster.Name}에게 {damage}의 피해를 입혔습니다.");
                monster.TakeDamage(damage);
                Console.ReadLine();
                player.mp -= 10;
            }
        }

        public void Warriorskill2(Player player, List<Monster> monsters)
        {
            foreach (Monster monster in monsters)
            {
                Random random = new Random();
                int randomDamage = random.Next(1, 4);

                int damage = player.atk * randomDamage;
                Console.WriteLine($"운수 좋은 날~\n\n{monster.Name}에게 {player.atk} X {randomDamage}의 피해를 입혔습니다.");
                monster.TakeDamage(damage);
                Console.ReadLine();
                player.mp -= 15;
            }
        }
    }


    public class AssassinSkill : Skill
    {
        public AssassinSkill() : base("도적 스킬", "기본 공격에 추가 피해를 입힘") { }

        public override void Use(Player player, List<Monster> monsters)
        {
            Console.WriteLine("사용할 스킬을 선택하세요.\n");
            Console.WriteLine("1. 슈슉// - MP 10\n   2명 기본공격");
            Console.WriteLine("2. 슈룩슈룩//// - MP 15\n   4명 기본공격 * 0.5");

            Console.WriteLine("\n스킬을 선택하세요.");

            int skillChoice = Text.GetInput(null, 1, 2);

            if (skillChoice == 1)
            {
                Console.WriteLine("공격할 몬스터를 선택하세요.\n");
                List<Monster> selcetMonsters = Text.GetInputMulti(2, monsters);
                AssassinSkil1(player, selcetMonsters);
            }

            else if (skillChoice == 2)
            {
                Console.WriteLine("공격할 몬스터를 선택하세요.\n");
                List<Monster> selcetMonsters = Text.GetInputMulti(4, monsters);
                AssassinSkil2(player, selcetMonsters);
            }
        }

        public void AssassinSkil1(Player player, List<Monster> monsters)
        {
            Console.WriteLine($"슈슉// 스킬 사용!\n");
            foreach (Monster monster in monsters)
            {
                int damage = player.atk;
                monster.TakeDamage(player.atk);
                player.mp -= 10;
            }
            Console.ReadLine();
        }

        public void AssassinSkil2(Player player, List<Monster> monsters)
        {
            Console.WriteLine($"슈룩슈룩//// 스킬 사용!\n");
            foreach (Monster monster in monsters)
            {
                int damage = player.atk;
                monster.TakeDamage((int)(player.atk * 0.5f));
                player.mp -= 15;
            }
            Console.ReadLine();
        }
    }
}


