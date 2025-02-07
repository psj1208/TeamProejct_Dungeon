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

        public virtual double buyPrice { get; set; }
        public virtual double sellPrice { get; set; }
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

        public override void Use() { } // 장착
        public override void UnUse() { }

        public Armour(string name, double buyPrice, int defend)
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

        public override void Use() { }//장착. 혹은 소비.
        public override void UnUse() { } 

        public Weapon(string name, double buyPrice, int attack)
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

        public static int amt = 0; // 소모품 수량 체크
        public override string Description() { return null; }

        public override void Use() { }

        public Consumable(string name, double buyPrice)
        {
            this.name = name;
            this.buyPrice = buyPrice;
            this.sellPrice = Math.Round(buyPrice * 0.85);
            type = ItemType.Consumable;
            amt++;
        }
    }
}
