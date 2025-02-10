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

        //데미지 적용 메서드
        protected void DealDamage(Player player, List<Monster> monsters, int damage)
        {
            foreach (Monster monster in monsters)
            {
                monster.TakeDamage(damage);
            }
            Console.ReadLine();
        }

        protected List<Monster> SelectTarget(string skillDes, int max, List<Monster> monsters)
        {
            Console.WriteLine(skillDes);
            Console.WriteLine("\n공격할 몬스터를 선택하세요.");
            return Text.GetInputMulti(max, monsters);
        }
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
            List<Monster> selectedMonsters = SelectTarget("스킬을 사용할 몬스터를 선택하세요.", 1, monsters);

            if (skillChoice == 1)
            {
                int damage = player.atk * 2;
                DealDamage(player, selectedMonsters, damage);
                player.mp -= 10;
            }
            else if (skillChoice == 2)
            {
                Random random = new Random();
                int multiplier = random.Next(1, 4);
                int damage = player.atk * multiplier;
                DealDamage(player, selectedMonsters, damage);
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

            int maxCount = (skillChoice == 1) ? 2 : 4;
            int targetCount = Math.Min(maxCount, monsters.Count);

            List<Monster> selectedMonsters = SelectTarget("스킬을 사용할 몬스터를 선택하세요.", targetCount, monsters);

            if (skillChoice == 1)
            {
                int damage = player.atk;
                player.mp -= 10;
                DealDamage(player, selectedMonsters, damage);
            }
            else if (skillChoice == 2)
            {
                int damage = (int)(player.atk * 0.5f);
                player.mp -= 15; // 
                DealDamage(player, selectedMonsters, damage);
            }
        }
    }
}


