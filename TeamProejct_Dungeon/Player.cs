﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{
    //직업 열거 ( 몬스터 포함)
    public enum Job
    {
        Warrior,
        Assassin,
        Monster
    }
    
    public class Player : ICharacter
    {
        //기본 정보 및 초기값 설정
        public string Name { get; set; }
        public Job job { get; set; }
        public int level { get; set; } = 1;
        public int exp { get; set; } = 0;
        public int gold { get; set; } = 1000;
        public int maxHp { get; } = 100;
        public int atk { get; }
        public int dfs { get; }
        public bool isDead = false;

        //변경 가능 정보
        public int hp { get; set; }
        public int equipAtk { get; set; }
        public int equipDfs { get; set; }


        //플레이어 생성자  (초기값)
        public Player(string _name, Job _job)
        {
            Name = _name; 
            job = _job;
            hp = maxHp;

            //직업별 기본스텟 변경
            if (_job == Job.Warrior)
            {
                atk = 5;
                dfs = 10;
            }
            else if (_job == Job.Assassin)
            {
                atk = 10;
                dfs = 5;
            }
        }

        //플레이어 / 몬스터 같이 구현
        //플레이어가 공격 (매개변수 몬스터로 변경)
        public void Attack(int damge)
        {
            //몬스터 체력 감소 
              //몬스터 체력이 0이하면 몬스터 isDead
              
            Console.WriteLine($"Lv.//몬스터레벨 //몬스터이름 을(를) 맞췄습니다. [데미지 : {atk + equipAtk}]");

           
        }

        //데미지를 입을시
        public void TakeDamage(int damge)
        {
            
        }

        public void StatusDisplay()
        {
            Console.Clear();
            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            //플레이어 스텟창
            Console.WriteLine($"Lv. {level}");
            Console.WriteLine($"{Name} ( {job} )");

            string str = equipAtk == 0 ? $"공격력 : {atk}" : $"공격력 : {atk + equipAtk} (+{equipAtk})";
            Console.WriteLine(str);

            str = equipAtk == 0 ? $"방어력 : {dfs}" : $"방어력 : {dfs + equipDfs} (+{equipDfs})";
            Console.WriteLine(str);

            Console.WriteLine($"체력 : {hp} / {maxHp}");
            Console.WriteLine($"Gold : {gold} G");

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Text.GetInput();
        }
        
        //아이템 장착별 스텟변경 메서드 (인벤토리 작업 완료후)
        public void EquipItem()
        {

        }



    }
}
