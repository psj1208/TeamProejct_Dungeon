using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
        public int maxMp { get; set; } = 100;
        public int atk { get; set; }
        public int dfs { get; set; }
        public bool isDead = false;

        //변경 가능 정보
        public int hp { get; set; }
        public int mp { get; set; }
        public int equipAtk { get; set; }
        public int equipDfs { get; set; }

        public IItem curWeapon = null;
        public IItem curArmour = null;

        public Player() { }

        public Skill skill { get; }
        public Inventory inven;
        //플레이어 생성자  (초기값)
        public Player(string _name, Job _job)
        {
            Name = _name;
            job = _job;
            hp = maxHp;
            mp = maxMp;
            inven = new Inventory();
            atk += equipAtk;
            dfs += equipDfs;
            
            //직업별 기본스텟 변경
            if (_job == Job.Warrior)
            {
                atk = 5;
                dfs = 3;
                skill = new WarriorSkill();
            }
            else if (_job == Job.Assassin)
            {
                atk = 100;
                dfs = 100;
                skill = new AssassinSkill();
            }
        }

        //플레이어가 공격 
        public void Attack(ICharacter monster)
        {
            Random random = new Random();

            //플레이어 데미지
            int damage = atk;

            //데미지 오차범위 (-20% ~ +20%)
            double damageRatio = damage * 0.1d;
            int damageChance = random.Next(-2, 3);
            int extraDamage = (int)Math.Round(damageChance * damageRatio);

            damage += extraDamage;

            //크리티컬 20% 확률
            int cirticalChance = random.Next(0, 5);
            bool isCrital = (cirticalChance == 0);


            // 크리티컬 시 20% 데미지 증가
            if (isCrital)
            {
                damage = (int)(damage * 1.5);
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
            Text.TextingLine("[ 상태 보기 ]\n", ConsoleColor.Yellow, false);
            Text.TextingLine("캐릭터의 정보가 표시됩니다.\n", ConsoleColor.Green, false);
            Text.TextingLine("---------------------------------\n", ConsoleColor.Green, false);

            //플레이어 스텟창
            Text.TextingLine($"Lv. {level} ( Exp. {exp} / {maxExp} )", ConsoleColor.Green, false);
            Text.TextingLine($"{Name} ( {job} )", ConsoleColor.Green, false);

            string str = equipAtk == 0 ? $"{atk}" : $"{atk} (+{equipAtk})";

            Text.Texting("공격력 : ", ConsoleColor.Magenta, false);
            Text.TextingLine(str, ConsoleColor.Green, false);

            str = equipDfs == 0 ? $"{dfs}" : $"{dfs} (+{equipDfs})";
            Text.Texting("방어력 : ", ConsoleColor.Magenta, false);
            Text.TextingLine(str, ConsoleColor.Green, false);
            Text.Texting("체  력 : ", ConsoleColor.Red, false);
            Text.TextingLine($"{hp} / {maxHp}", ConsoleColor.Green, false);
            Text.Texting("마  나 : ", ConsoleColor.Blue, false);
            Text.TextingLine($"{mp} / {maxMp}", ConsoleColor.Green, false);
            Text.Texting("G o l d : ", ConsoleColor.Yellow, false);
            Text.Texting($"{gold}", ConsoleColor.Green, false);
            Text.TextingLine(" G", ConsoleColor.Yellow, false);


            Text.TextingLine("\n0. 나가기\n", ConsoleColor.Green, false);
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
        public void LevelUp(Quest2 quest = null)
        {
            exp = 0; // exp 초기화 (초과 시 추가값 받아오도록) 
            maxExp += 20; // 레벨당 (maxExp 20 증가)
            maxHp += level * 20; // 레벨당 (maxHp 20 증가)
            hp = maxHp; // hp 100%
            mp = 100;
            level++;
            
            //직업별 스텟 증가치 변경
            if (job == Job.Warrior)
            {
                atk += 2;
                dfs += 4;
            }
            else if (job == Job.Assassin)
            {
                atk += 4;
                dfs += 2;
            }

            if (quest.isAccept && level ==3) 
            {
                quest.isClear = true;
            }
        }
    }
}
