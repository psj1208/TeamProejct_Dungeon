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
        public List<IItem> items;//인벤이 갖고 있는 아이템.

        //인벤토리 생성자. 게임매니저에 인벤토리 전달.
        public Inventory()
        {
            items = new List<IItem>();
            GameManager.inven = this;
        }

        //인벤토리 화면만 출력
        public void ShowInventory()
        {
            Text.TextingLine("------------------------인벤토리-------------------------", ConsoleColor.Red, false);
            for (int i = 0; i < items.Count; i++)
            {
                Text.Texting($"{i + 1} . {items[i].name} : 판매 가격: {items[i].sellPrice} / {items[i].Description()}", ConsoleColor.Green, false);
                if (items[i] is Consumable)
                    Text.Texting($" , 수량 : {items[i].amt}", ConsoleColor.Green, false);
                Console.WriteLine();
            }
            Text.TextingLine("---------------------------------------------------------\n", ConsoleColor.Red, false);
        }

        public void UsingInventory()
        {
            while (true)
            {
                Console.Clear();
                ShowInventory();
                int input = Text.GetInput("사용하시려는 아이템의 번호를 입력해주세요.\n나가시려면 0을 입력해주세요.", Number.Make(0, items.Count));
                input--;
                //0 입력시 탈출.
                if (input == -1)
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    UseItem(input);
                    Console.ReadLine();
                }   
            }
        }

        public void SellingInventory()
        {
            while (true)
            {
                Console.Clear();
                ShowInventory();
                int input = Text.GetInput("판매하시려는 아이템의 번호를 입력해주세요.\n나가시려면 0을 입력해주세요.", Number.Make(0, items.Count));
                input--;
                //0 입력시 탈출.
                if (input == -1)
                {
                    Console.Clear();
                    break;
                }
                else
                {
                    GameManager.player.gold += (int)items[input].sellPrice;  
                    Text.TextingLine($"{input + 1} 아이템 판매 ! ", ConsoleColor.Magenta);
                    RemoveItem(input);
                    Console.ReadLine();
                }
            }
        }
        //아이템 사용(화면에 표기된 숫자 입력하시면 됩니다.실제 인덱스는 0부터 시작하나 화면에 표기는 1부터 시작하는 걸 감안했습니다.

        public bool Equipitem(IItem item)
        {
            if(item is Weapon)
            {
                if(GameManager.player.curWeapon == null)
                {
                    GameManager.player.curWeapon = item.DeepCopy();
                    GameManager.player.curWeapon.Use();
                    return true;
                }
                else
                {
                    AddItem(GameManager.player.curWeapon.DeepCopy());
                    GameManager.player.curWeapon.UnUse();
                    GameManager.player.curWeapon = item;
                    GameManager.player.curWeapon.Use();
                    return true;
                }
            }
            else if (item is Armour)
            {
                if (GameManager.player.curArmour == null)
                {
                    GameManager.player.curArmour = item.DeepCopy();
                    GameManager.player.curArmour.Use();
                    return true;
                }
                else
                {
                    AddItem(GameManager.player.curArmour.DeepCopy());
                    GameManager.player.curArmour.UnUse();
                    GameManager.player.curArmour = item;
                    GameManager.player.curArmour.Use();
                    return true;
                }
            }
            return false;
        }
        public void UseItem(int num)
        {
            if (items[num] is Armour || items[num] is Weapon)
            {
                if (Equipitem(items[num]))
                {
                    RemoveItem(num);
                }
                else
                {
                    Text.TextingLine("아이템 장착에 실패했습니다.", ConsoleColor.Red, false);
                }
            }
            else
            {
                items[num].Use();
                if (items[num].amt <= 0) 
                    RemoveItem(num);
            }
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
            bool isIn = false;
            if (item.type == ItemType.Consumable)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if(items[i].name == item.name)
                    {
                        //소비품 아이템 수량 추가.(이름 같을 경우)
                        items[i].amt++;
                        isIn = true;
                        break;
                    }
                }
                if (isIn == false)
                {
                    items.Add(item);
                }
            }
            else
            {
                items.Add(item);
            }
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
