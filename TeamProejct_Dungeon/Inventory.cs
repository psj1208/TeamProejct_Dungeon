using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{
    public class Inventory
    {
        List<IItem> items;//인벤이 갖고 있는 아이템.

        public Inventory()
        {
            items = new List<IItem>();
            GameManager.inven = this;
        }

        public void ShowInventory()
        {
            Text.TextingLine("------------------------인벤토리-------------------------", ConsoleColor.Red, false);
            for (int i = 0; i < items.Count; i++)
            {
                Text.TextingLine($"{i + 1} . {items[i].name} : {items[i].Description()}");
            }
            Text.TextingLine("---------------------------------------------------------\n", ConsoleColor.Red, false);
        }

        public void UseItem(int num)
        {
            items[num - 1].Use();
            Text.TextingLine($"{items[num - 1].name} 사용 !", ConsoleColor.Green);
        }

        //찾는 아이템의 인덱스를 찾아 반환합니다. 발견을 못 할 시 -1을 반환.
        public int FindItemIndex(IItem item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name == item.name)
                {
                    return i;
                }
            }
            return -1;
        }

        //찾는 아이템을 반환합니다. 찾지 못하면 null을 반환.
        public IItem FindItem(IItem item)
        {
            int index = FindItemIndex(item);
            if(index != -1)
            {
                IItem copy = items[index];
                items.RemoveAt(index);
                return copy;
            }
            return null;
        }

        //아이템 추가(IItem 클래스 형식으로 넣어야합니다.)
        public void AddItem(IItem item)
        {
            items.Add(item);
        }
        
        //아이템 삭제(아이템 매개변수 형)
        public void RemoveItem(IItem item)
        {
            items.Remove(item);
        }

        //아이템 삭제(인덱스 매개변수 형)
        public void RemoveItem(int index)
        {
            items.RemoveAt(index);
        }
    }
}
