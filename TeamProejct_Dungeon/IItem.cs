using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{
    public enum ItemType
    {
        Armour,
        Weapon,
        Consumable
    }
    public class IItem
    {
        public virtual string name { get; set; }

        public virtual double buyPrice { get; set; } // 구매 가격
        public virtual double sellPrice { get; set; } // 판매 가격 85퍼센트의 가격 판매 
        public virtual ItemType type { get; set; }
        public virtual string Description() { return null; }


        // 부모 virtual deepcopy 메소드 선언
        // new Armour(this.name, ~
        // new Weapon
        // new Consumalbe(
        // name = this.name
        // ?? 

        public virtual IItem DeepCopy() // Deep Copy 함수 
        {
            return null;
        }
        public virtual void Use() { }//장착. 혹은 소비.
        public virtual void UnUse() { }//장착 해제(포션은 제외)

    }

    //상속해가지고 3가지 클래스 만드는 거로. 생성자에서 type 변수는 열거형 맞춰서.

    public class Armour : IItem // 방어구
    {
        public override string name { get; set; }
        public override double buyPrice { get; set; }
        public override double sellPrice { get; set; }
        public override ItemType type { get; set; }

        public int defend; // 방어력
        public override string Description() { return $"방어력이 {defend}만큼 상승하였습니다."; }

        public override void Use() { GameManager.player.equipDfs += defend; }// 장착시 장비 방어력 증가
        public override void UnUse() { GameManager.player.equipDfs -= defend; }// 해제시 장비 방어력 감소

        public Armour(string name, double buyPrice, int defend = 0)
        {
            this.name = name;
            this.buyPrice = buyPrice;
            this.sellPrice = Math.Round(buyPrice * 0.85);
            this.defend = defend;
            type = ItemType.Armour;

        }

        public override IItem DeepCopy()
        {
            return new Armour(this.name, this.buyPrice, this.defend);
        }

    }

    public class Weapon : IItem // 무기
    {
        public override string name { get; set; }
        public override double buyPrice { get; set; }
        public override double sellPrice { get; set; }

        public int attack;
        public override ItemType type { get; set; }

        public override string Description() { return $"공격력이 {attack}만큼 상승하였습니다"; }

        public override void Use() { GameManager.player.equipAtk += attack; } // 장착시 공격력 상승
        public override void UnUse() { GameManager.player.equipAtk -= attack; } // 장착시 공격력 감소

        public Weapon(string name, double buyPrice, int attack = 0)
        {
            this.name = name;
            this.buyPrice = buyPrice;
            this.sellPrice = Math.Round(buyPrice * 0.85);
            this.attack = attack;
            type = ItemType.Weapon;
        }

        public override IItem DeepCopy()
        {
            return new Weapon(this.name, this.buyPrice, this.attack);
        }
    }

    public class Consumable : IItem // 소비
    {
        public override string name { get; set; }
        public override double buyPrice { get; set; }
        public override double sellPrice { get; set; }

        public override ItemType type { get; set; }

        public static int amt = 0; // 포션 수량

        public int pHeatlh; // 체력포션의 체력 올려주는 값

        public int pStrength; // 힘포션의 공격력양
        public override string Description() { return null; }

        public override void Use()
        {
            if (amt <= 0) // 포션이 없을 때 
            {
                Console.WriteLine("포션이 없습니다. 포션을 구매해주세요."); // 포션 구매 요청
                return;
            }

            if (pHeatlh > 0)
            {
                GameManager.player.hp += pHeatlh; // 체력 상승
                Text.TextingLine($"{this.pHeatlh} 를 회복했습니다.", ConsoleColor.Green);
            }
            if (pStrength > 0)
            {
                GameManager.player.atk += pStrength; // 공격력 상승
                Text.TextingLine($"{this.pStrength} 만큼 공격력이 상승했습니다.", ConsoleColor.Green);
            }

            if (GameManager.player.hp > GameManager.player.maxHp) // 체력 회복 후, 최대 체력보다 높으면
            {
                GameManager.player.hp = GameManager.player.maxHp; // 최대 체력으로 보정

            }
            amt--; // 포션 수량 감소
        }

        public Consumable(string name, double buyPrice, int health, int strength, int amount = 1)
        {
            this.name = name;
            this.buyPrice = buyPrice;
            sellPrice = Math.Round(buyPrice * 0.85);
            pHeatlh = health; // 체력 포션 상승값
            pStrength = strength; // 힘 포션 상승값

            type = ItemType.Consumable;
            amt += amount;


        }

        public override IItem DeepCopy()
        {
            return new Consumable(this.name, this.buyPrice, this.pHeatlh, this.pStrength);
        }



    }

    // 데이터베이스 
    public static class ItemDatabase
    {

        static public List<Armour> armourList = new List<Armour>
        {
            new Armour("천 갑옷",100,3),
            new Armour("불사의 갑옷",200,5),
            new Armour("스파르타의 갑옷",300,10)
        };

        static public List<Weapon> weaponList = new List<Weapon>
        {
            new Weapon("나무 칼",50,3),
            new Weapon("여포의 창",100, 7),
            new Weapon("스파르타의 칼", 200 ,10)
        };

        static public List<Consumable> consumableList = new List<Consumable>
        {
            new Consumable("힐링 포션",50,30,0),
            new Consumable("힘 포션",100,0,7),
            new Consumable("만병통치약",200,10,10)
        };
    }

}
