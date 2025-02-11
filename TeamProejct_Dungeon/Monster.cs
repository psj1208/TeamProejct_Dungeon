using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{    
    // 몬스터 타입 열거
    public enum MonsterType
    {
        Low,   // 하급 몬스터
        Mid,   // 중급 몬스터
        High   // 상급 몬스터
    }
    public class Monster : ICharacter
    {
        public string Name { get; set; }
        public int level { get; set; }
        public int hp { get; set; }
        public int maxHp { get; set; }
        public int atk { get; set; }
        public int exp { get; set; }
        public int gold { get; set; }
        public bool isDead { get; set; }
        public MonsterType type { get; set; }

        // 초기 생성자 (몬스터 기본 정보)
        public Monster(string name, int level, int maxHp, int atk, int exp, int gold, MonsterType type)
        {
            Name = name;
            this.level = level;
            this.maxHp = maxHp;
            hp = this.maxHp;
            this.atk = atk;
            this.exp = exp;
            this.gold = gold;
            isDead = false;
            this.type = type;
        }

        public void MonsterStatus()
        {
            Console.WriteLine($"Lv {level.ToString("00")} {Name} HP {hp}");
        }

        // 몬스터 피해 메서드
        public void TakeDamage(int damage)
        {
            hp -= damage;

            if (Dead())
            {
                isDead = true;
                Console.WriteLine($"{Name}을(를) 처치했습니다!");
            }
            else
                Console.WriteLine($"{Name}을(를) 맞췄습니다. [데미지 : {damage}]");
        }

        // 몬스터 죽음 메서드
        public bool Dead()
        {
            return hp <= 0;
        }

        // 몬스터 공격 메서드 
        public void Attack(ICharacter cha)
        {
            Random random = new Random();

            //몬스터 데미지
            int attackDamage = atk;

            //데미지 오차범위 (-20% ~ +20%)
            double damageRatio = attackDamage * 0.1d;
            int damageChance = random.Next(-2, 3);
            int extraDamage = (int)Math.Round(damageChance * damageRatio);

            attackDamage += extraDamage;

            Console.WriteLine($"Lv.{level} {Name}의 공격!");

            cha.TakeDamage(attackDamage);
            Console.WriteLine();
        }
        
        // 클래스는 주소값을 전달하기에 복사하여 새로운 객체를 추가하려면 따로 짜야된다.(ex.상점에서 구입하여 인벤에 들어가는 경우)
        public Monster GetCopy()
        {
            return new Monster(Name, this.level, this.maxHp, this.atk, this.exp, this.gold, this.type);
        }

        // 보상 지급 메서드
        public void GrantReward(Player player, Monster monster)
        {
            if (isDead)
            {
                player.AddGold(gold);
                player.AddExp(monster.exp);
            }
        }

        // 몬스터를 가져오는 메서드
        public static Monster GetMonster(int index)
        {
            if (index >= 0 && index < MonsterDB.GetMonsters().Count)
                return MonsterDB.GetMonsters()[index];
            return null; // 잘못된 인덱스일 경우 null 반환
        }

        // 랜덤한 몬스터를 가져오는 메서드
        public static Monster GetRandomMonster()
        {
            Random random = new Random();
            int index = random.Next(MonsterDB.GetMonsters().Count);
            return MonsterDB.GetMonsters()[index].GetCopy();
        }
    }
    
    public static class MonsterDB
    {
        // 몬스터 리스트
        private static List<Monster> monsters = new List<Monster>()
        {
             // 하급 몬스터(name, level, maxHp, atk, exp, gold, MonsterType)
             new Monster("슬라임", 1, 10, 2, 8, 100, MonsterType.Low),
             new Monster("미니언", 2, 15, 3, 10, 200, MonsterType.Low),
             new Monster("공허충", 3, 10, 4, 15, 300, MonsterType.Low),
             new Monster("대포미니언", 5, 25, 6, 20, 500, MonsterType.Low),
             new Monster("미믹", 7, 20, 8, 22, 700, MonsterType.Low),
             
             // 중급 몬스터(name, level, maxHp, atk, exp, gold, MonsterType)
             new Monster("고스트", 8, 40, 9, 40, 800, MonsterType.Mid),
             new Monster("스켈레톤", 9, 45, 11, 45, 900, MonsterType.Mid),
             new Monster("고블린", 10, 50, 12, 50, 1000, MonsterType.Mid),
             new Monster("늑대", 12, 60, 14, 70, 1200, MonsterType.Mid),
             new Monster("가고일", 15, 80, 18, 100, 1800, MonsterType.Mid),
             
             // 상급 몬스터(name, level, maxHp, atk, exp, gold, MonsterType)
             new Monster("리치", 17, 100, 22, 200, 2500, MonsterType.High),
             new Monster("바실리스크", 18, 120, 25, 250, 3000, MonsterType.High),
             new Monster("키메라", 20, 150, 30, 300, 4500, MonsterType.High),
             new Monster("드래곤", 25, 180, 40, 400, 7000, MonsterType.High),
             new Monster("마왕", 30, 200, 50, 500, 10000, MonsterType.High)
        };

        // 모든 몬스터를 반환
        public static List<Monster> GetMonsters()
        {
            return monsters;
        }
        
        // 특정 등급 몬스터만 반환
        public static List<Monster> GetMonsterByType(MonsterType type)
        {
            return monsters.Where(monster => monster.type == type).ToList();
        }
        
        // 특정 등급의 몬스터만 반환(스테이지)
        public static List<Monster> GetMonstersByLevel(int minLevel, int maxLevel)
        {
            List<Monster> stageMonsters = new List<Monster>();

            foreach (Monster monster in monsters)
            {
                if (monster.level >= minLevel && monster.level <= maxLevel)
                {
                    stageMonsters.Add(monster.GetCopy());
                }
            }
            return stageMonsters;
        }

    }
}
