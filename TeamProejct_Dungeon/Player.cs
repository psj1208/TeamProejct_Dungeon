using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{
    
    public class Player : ICharacter
    {
        //기본 정보 및 초기값 설정
        public string Name { get; set; }
        public string job { get; set; }
        public int level { get; set; } = 1;
        public int exp { get; set; } = 0;
        public int gold { get; set; } = 1000;
        public int maxHp { get; } = 100;
        public int atk { get; } = 10;
        public int dfs { get; } = 5;

        //변경 가능 정보
        public int hp { get; set; }
        public int equipAtk { get; set; }
        public int equipDfs { get; set; }


        //플레이어 생성자  (초기값)
        public Player(string _name, string _job)
        {
            Name = _name; 
            job = _job;
            hp = maxHp;
        }

        //공격시
        public void Attack()
        {
           
        }

        //데미지를 입을시
        public void TakeDamage()
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
            Console.WriteLine($"공격력 : {atk}");
            Console.WriteLine($"방어력 : {dfs}");
            Console.WriteLine($"체력 : {hp} / {maxHp}");
            Console.WriteLine($"Gold : {gold} G");

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
        }
    }
}
