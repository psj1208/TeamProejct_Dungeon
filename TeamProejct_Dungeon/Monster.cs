using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{
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

        // 초기 생성자 (몬스터 기본 정보)
        public Monster(string name, int level, int maxHp, int atk, int exp, int gold)
        {
            Name = name;
            this.level = level;
            hp = maxHp;
            this.atk = atk;
            this.exp = exp;
            this.gold = gold;
            isDead = false;
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
            int attackDamage = atk;

            Console.WriteLine($"Lv.{level} {Name}의 공격!");
            //(Console.WriteLine($"{cha.Name}을(를) 맞췄습니다. [데미지 : {attackDamage}]");

            cha.TakeDamage(attackDamage);
            Console.WriteLine();
        }

        // 모르겠어서 성준님 블로그 복붙했습니다..
        // 클래스는 주소값을 전달하기에 복사하여 새로운 객체를 추가하려면 따로 짜야된다.(ex.상점에서 구입하여 인벤에 들어가는 경우)
        public Monster GetCopy()
        {
            return new Monster(Name, this.level, this.maxHp, this.atk, this.exp, this.gold);
        }

        // 보상 지급 메서드
        public void GrantReward(Player player)
        {
            if (isDead)
            {
                player.gold += gold;
                Console.WriteLine($"{gold} 골드를 획득했습니다!");
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
            return MonsterDB.GetMonsters()[index];
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
}
