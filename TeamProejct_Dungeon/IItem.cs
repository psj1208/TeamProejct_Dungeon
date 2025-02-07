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
        public override string Description() { return null; }

        public override void Use() { GameManager.player.equipDfs += defend; }// 장착시 장비 방어력 증가
        public override void UnUse() { GameManager.player.equipDfs -= defend; }// 해제시 장비 방어력 감소

        public Armour(string name, double buyPrice, int defend= 0)
        {
            this.name = name;
            this.buyPrice = buyPrice;
            this.sellPrice = Math.Round(buyPrice * 0.85);
            this.defend = defend;
            type = ItemType.Armour;

        }
    }

    public class Weapon : IItem // 무기
    {
        public override string name { get; set; }
        public override double buyPrice { get; set; }
        public override double sellPrice { get; set; }

        public int attack;
        public override ItemType type { get; set; }

        public override string Description() { return null; }

        public override void Use(){ GameManager.player.equipAtk += attack; } // 장착시 공격력 상승
        public override void UnUse() { GameManager.player.equipAtk -= attack; } // 장착시 공격력 감소

        public Weapon(string name, double buyPrice, int attack = 0)
        {
            this.name = name;
            this.buyPrice = buyPrice;
            this.sellPrice = Math.Round(buyPrice * 0.85);
            this.attack = attack;
            type = ItemType.Weapon;
        }
    }
    
    public class Consumable: IItem // 소비
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

            GameManager.player.hp += pHeatlh; // 체력 상승
            GameManager.player.atk += pStrength; // 공격력 상승

            if (GameManager.player.hp > GameManager.player.maxHp) // 체력 회복 후, 최대 체력보다 높으면
            {
                GameManager.player.hp = GameManager.player.maxHp; // 최대 체력으로 보정

            }
            amt--; // 포션 수량 감소
        }
     
        public Consumable(string name, double buyPrice, int health, int strength)
        {
            this.name = name;
            this.buyPrice = buyPrice;
            sellPrice = Math.Round(buyPrice * 0.85);
            pHeatlh = health; // 체력 포션 상승값
            pStrength = strength; // 힘 포션 상승값
           

        }
      
    }


   
}
