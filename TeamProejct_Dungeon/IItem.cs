using System;
using System.Collections.Generic;
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

    public class Armour : IItem
    {
      
    }

    public class Weapon : IItem
    {
      
    }
    
    public class Consumable: IItem
    {
       
    }
}
