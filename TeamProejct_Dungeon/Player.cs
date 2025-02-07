using System;
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
        public int maxExp { get; set; } = 100;
        public int gold { get; set; } = 1000;
        public int maxHp { get; set; } = 100;
        public int atk { get; set; }
        public int dfs { get; set; }
        public bool isDead = false;

        //변경 가능 정보
        public int hp { get; set; }
        public int equipAtk { get; set; }
        public int equipDfs { get; set; }

        public Inventory inven;
        public Player() { }

        public Skill skill { get;}
            
        //플레이어 생성자  (초기값)
        public Player(string _name, Job _job)
        {
            Name = _name; 
            job = _job;
            hp = maxHp;
            GameManager.player = this;
            inven = new Inventory();

            //직업별 기본스텟 변경
            if (_job == Job.Warrior)
            {
                atk = 5;
                dfs = 3;
                skill = new WarriorSkill();

            }
            else if (_job == Job.Assassin)
            {
                atk = 7;
                dfs = 1;
                //skills.Add(new AssassinSkill());
            }
        }
  
        //플레이어가 공격 
        public void Attack(ICharacter monster)
        {
            //크리티컬 20% 확률
            Random random = new Random();

            int cirticalChance = random.Next(0, 5);
            bool isCrital = (cirticalChance == 0);

            int damage = atk + equipAtk;
            
            // 크리티컬 시 20% 데미지 증가
            if (isCrital)
            {
                damage *= (int)(damage * 1.5);
                Console.WriteLine("크리티컬 !");
            }

            monster.TakeDamage(damage);
        }

        //플레이어가 데미지를 입을시
        public void TakeDamage(int damge)
        {
            if (damge > dfs)
            {
                hp -= damge - dfs;
                Text.TextingLine($"{this.Name} 이 {damge - dfs} 피해를 받았습니다.", ConsoleColor.Red);
            }

            else if (damge <= dfs)
            {
                Text.TextingLine("Miss~!", ConsoleColor.Blue);
            }

            //플레이어 사망
            if (hp <= 0)
            {
                isDead = true;
            }
        }

        //플레이어 상태보기
        public void StatusDisplay()
        {
            Console.Clear();
            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            //플레이어 스텟창
            Console.WriteLine($"Lv. {level} ( Exp. {exp} / {maxExp} )");
            Console.WriteLine($"{Name} ( {job} )");

            string str = equipAtk == 0 ? $"공격력 : {atk}" : $"공격력 : {atk + equipAtk} (+{equipAtk})";
            Console.WriteLine(str);

            str = equipAtk == 0 ? $"방어력 : {dfs}" : $"방어력 : {dfs + equipDfs} (+{equipDfs})";
            Console.WriteLine(str);

            Console.WriteLine($"체력 : {hp} / {maxHp}");
            Console.WriteLine($"Gold : {gold} G");

            Console.WriteLine("\n0. 나가기\n");
            Text.GetInput(null, 0);
        }
        
        //경헙치 획득 메서드
        public void AddExp(int _exp)
        {
            exp += _exp;
            if (exp >= maxExp)
            {
                LevelUp();
            }
        }

        public void AddGold(int _gold)
        {
            gold += _gold;
        }

        //레벨 업 메서드
        public void LevelUp()
        {
            exp = exp - maxExp; // exp 초기화 (초과 시 추가값 받아오도록) 
            maxExp += (level - 1) * 20; // 레벨당 (maxExp 20 증가)
            maxHp += level * 20; // 레벨당 (maxHp 20 증가)
            hp = maxHp; // hp 100%

            //직업별 스텟 증가치 변경
            if (job == Job.Warrior)
            {
                atk += 5;
                dfs += 10;
            }
            else if (job == Job.Assassin)
            {
                atk += 10;
                dfs += 5;
            }
        }
    }
}
