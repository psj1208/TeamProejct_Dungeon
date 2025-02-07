using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{
    public class Monster
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int Atk { get; set; }
        public int Exp { get; set; }
        public int Gold { get; set; }
        public bool IsDead { get; set; }
        
        // 초기 생성자 (몬스터 기본 정보)
        public Monster(string name, int level, int maxHp, int atk, int exp, int gold)
        {
            Name = name;
            Level = level;
            HP = maxHp;
            Atk = atk;
            Exp = exp;
            Gold = gold;
            IsDead = false;
        }

        public void MonsterStatus()
        {
            Console.WriteLine($"Lv {Level.ToString("00")} {Name} HP {HP}");
        }
        
        // 몬스터 피해 메서드
        public void TakeDamage(int damage)
        {
            HP -= damage;
            
            if (Dead())
            {
                IsDead = true;
                Console.WriteLine($"{Name}을(를) 처치했습니다!");
            }
            else
                Console.WriteLine($"{Name}을(를) 맞췄습니다. [데미지 : {damage}]");
        }
        
        // 몬스터 죽음 메서드
        public bool Dead()
        {
            return HP <= 0;
        }
        
        // 몬스터 공격 메서드 
        public void AttackEnemy(Monster cha)
        {
            int attackDamage = Atk;
            
            Console.WriteLine($"Lv.{Level} {Name}의 공격!");
            Console.WriteLine($"{cha.Name}을(를) 맞췄습니다. [데미지 : {attackDamage}]");
            
            cha.TakeDamage(attackDamage);
            Console.WriteLine();
        }
        
        // 모르겠어서 성준님 블로그 복붙했습니다..
        // 클래스는 주소값을 전달하기에 복사하여 새로운 객체를 추가하려면 따로 짜야된다.(ex.상점에서 구입하여 인벤에 들어가는 경우)
        public Monster GetCopy()
        {
            return new Monster(this.Name, this.Level, this.MaxHP, this.Atk, this.Exp, this.Gold);
        }
        
        // 보상 지급 메서드
        public void GrantReward(Player player)
        {
            if (IsDead)
            {
                player.Gold += Gold;
                Console.WriteLine($"{Gold} 골드를 획득했습니다!");
            }
        }
        
        public static class MonsterDB
        {
            // 몬스터 리스트
            // Lv.2 미니언 HP 15
            // Lv.5 대포미니언 HP 25
            // LV.3 공허충 HP 10
            private static List<Monster> monsters = new List<Monster>()
            {
                new Monster("미니언", 2, 15, 3, 10, 200),
                new Monster("대포미니언", 5, 25, 6, 20, 500),
                new Monster("공허충", 3, 10, 4, 15, 300)
            };

            public static List<Monster> GetMonsters()
            {
                return monsters;
            }
        }
        
        // 몬스터를 가져오는 메서드
        public static Monster GetMonster(int index)
        {
            if (index >= 0 && index < monsters.Count)
                return monsters[index];
            return null;                // 잘못된 인덱스일 경우 null 반환
        }
        
        // 랜덤한 몬스터를 가져오는 메서드
        public static Monster GetRandomMonster()
        {
            Random random = new Random();
            int index = random.Next(monsters.Count);
            return monsters[index];
        }
    }
}