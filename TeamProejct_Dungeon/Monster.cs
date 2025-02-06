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
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Exp { get; set; }
        public bool IsDead { get; set; }
        
        // 초기 생성자
        public Monster(string name, int level, int hp, int atk, int exp, bool isDead)
        {
            Name = name;
            Level = level;
            Hp = hp;
            Atk = atk;
            Exp = exp;
            IsDead = isDead;
        }

        public void MonsterStatus()
        {
            Console.WriteLine($"Lv {Level.ToString("00")} {Name} HP {Hp}");
        }
        
        // Lv.2 미니언 HP 15
        // Lv.5 대포미니언 HP 25
        // LV.3 공허충 HP 10
    }
}
