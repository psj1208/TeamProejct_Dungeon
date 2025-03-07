﻿using System;
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

        public abstract bool Use(Player player, List<Monster> monsters);

        //데미지 적용 메서드
        protected void DealDamage(Player player, List<Monster> monsters, int damage)
        {
            foreach (Monster monster in monsters)
            {
                monster.TakeDamage(damage);
            }
            Console.ReadLine();
        }

        //몬스터 리스트 출력 후 몬스터 선택 메서드 / max : 선택할 몬스터 수)
        protected List<Monster> SelectTarget(string skillDes, int max, List<Monster> monsters, bool Cancel)
        {
            Console.WriteLine(skillDes);
            Console.WriteLine("\n공격할 몬스터를 선택하세요.");

           
            List<Monster> selectedMonsters = Text.GetInputMulti(max, monsters, Cancel);

            if (selectedMonsters == null || selectedMonsters.Count == 0)
            {
                return null;
            }

            return selectedMonsters;
        }
    }

    //워리어스킬
    public class WarriorSkill : Skill
    {
        public WarriorSkill() : base("전사 스킬", "전사 스킬 설명 ") { }

        public override bool Use(Player player, List<Monster> monsters)
        {
            Console.WriteLine("사용할 스킬을 선택하세요.\n");
            Console.WriteLine("1. 파워 어택!! - MP 10\n   2배의 데미지로 공격 합니다.");
            Console.WriteLine("2. 운수 좋은 날~ - MP 15\n   1~3배의 데미지로 공격합니다.");

            Console.WriteLine("\n스킬을 선택하세요.");

            int? skillChoice = Text.GetInput(null, 1, 2);

            if (skillChoice == null)
            {
                return false;
            }

            // 마나 체크 부족하면 스킬 사용 불가
            if ((skillChoice == 1 && player.mp < 10) || (skillChoice == 2 && player.mp < 15))
            {
                Console.WriteLine("사용할 마나가 부족합니다.");
                Console.ReadLine();
                return false;
            }

            // 몬스터 선택 (ESC 가능)
            List<Monster> selectedMonsters = SelectTarget("스킬을 사용할 몬스터를 선택하세요.", 1, monsters, true);

            // 몬스터 선택 취소 시 스킬 사용 취소
            if (selectedMonsters == null || selectedMonsters.Count == 0)
            {
                return false;
            }

            // 전사 스킬 사용
            if (skillChoice == 1)
            {
                int damage = player.atk * 2;
                player.mp -= 10;
                DealDamage(player, selectedMonsters, damage);
            }
            else if (skillChoice == 2)
            {
                Random random = new Random();
                int multiplier = random.Next(1, 4);
                int damage = player.atk * multiplier;
                player.mp -= 15;
                DealDamage(player, selectedMonsters, damage);
            }

            return true;
        }
    }

    //도적스킬
    public class AssassinSkill : Skill
    {
        public AssassinSkill() : base("도적 스킬", "기본 공격에 추가 피해를 입힘") { }

        public override bool Use(Player player, List<Monster> monsters)
        {
            Console.WriteLine("사용할 스킬을 선택하세요.\n");
            Console.WriteLine("1. 슈슉// - MP 10\n   2명 기본공격");
            Console.WriteLine("2. 슈룩슈룩//// - MP 15\n   4명 기본공격 * 0.5");

            Console.WriteLine("\n스킬을 선택하세요.");

            int? skillChoice = Text.GetInput(null, 1, 2);

            if (skillChoice == null)
            {
                return false;
            }

            //  1번 스킬은 2명 선택 , 2번 스킬은 4명 선택
            int maxCount = (skillChoice == 1) ? 2 : 4;
            int targetCount = Math.Min(maxCount, monsters.Count);

            // MP가 부족하면 스킬 사용 불가
            if ((skillChoice == 1 && player.mp < 10) || (skillChoice == 2 && player.mp < 15))
            {
                Console.WriteLine("사용할 마나가 부족합니다.");
                Console.ReadLine();
                return false;
            }

            // 몬스터 선택 (ESC 가능)
            List<Monster> selectedMonsters = SelectTarget("스킬을 사용할 몬스터를 선택하세요.", targetCount, monsters, true);

            // 몬스터 선택 취소 시 스킬 사용 취소
            if (selectedMonsters == null || selectedMonsters.Count == 0)
            {
                return false;
            }

            // 도적 스킬 사용
            if (skillChoice == 1)
            {
                int damage = player.atk;
                player.mp -= 10;
                DealDamage(player, selectedMonsters, damage);
            }
            else if (skillChoice == 2)
            {
                int damage = (int)(player.atk * 0.5f);
                player.mp -= 15; 
                DealDamage(player, selectedMonsters, damage);
            }
            return true;
        }
    }
}


