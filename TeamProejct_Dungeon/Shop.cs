using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{
    public class Shop
    {
        //List<List<Armour>> ShopList;

        //------갑옷류--------
        //1. 2. 3.for(int i ~~~~)
        //아머리스트[(int)ItemType.Armour][i]
        //------무기류--------
        //무기템리스트
        //----소모품---
        //7.8.9.소모품
        //----서비스류-----
        //휴식
        //플레이어한테 maxhp 50% 만큼 더한 다음. 최대치 안넘는지 검사.
        //반환할 때는 게임매니저 인벤토리한테 AddItem 써가지고 추가.(DeepCopy())
        private List<List<IItem>> shopList; // 접근

        public Shop()
        {
            // 아이템 데이터베이스에서 종류별 아이템 리스트를 추가
            shopList = new List<List<IItem>>
            {
                new List<IItem>(ItemDatabase.armourList),
                new List<IItem>(ItemDatabase.weaponList),
                new List<IItem>(ItemDatabase.consumableList)
            };
        }

        // switch case 써서 카테고리 나누기
        public void DisplayItems()
        {
            Console.WriteLine("--- 상점 목록 ---");

            int index = 1;
            string input = Console.ReadLine();
            int category = int.Parse(input);
            switch (category)
            {

                case (int)ItemType.Armour:

                    for (int i = 0; i < shopList[0].Count; i++)
                    {
                        Console.WriteLine($"{index}. {shopList[0][i].name} - 가격: {shopList[0][i].buyPrice}");
                        index++;
                    }

                    break;
                case (int)ItemType.Weapon:
                    for (int i = 0; i < shopList[1].Count; i++)
                    {
                        Console.WriteLine($"{index}. {shopList[1][i].name} - 가격: {shopList[1][i].buyPrice}");
                        index++;
                    }
                    break;
                case (int)ItemType.Consumable:
                    for (int i = 0; i < shopList[2].Count; i++)
                    {
                        Console.WriteLine($"{index}. {shopList[2][i].name} - 가격: {shopList[2][i].buyPrice}");
                        index++;
                    }
                    break;
                default:
                    Console.WriteLine("잘못입력하셨습니다.");
                    break;

            }

        }

     
        public void Rest() // 휴식하기
        {

            GameManager.player.hp += GameManager.player.maxHp / 2; // 플레이어 최대체력 50퍼센트 체력 회복
            if (GameManager.player.hp > GameManager.player.maxHp)
            {
                GameManager.player.hp = GameManager.player.maxHp; // 최대체력보다 체력이 높으면 최대체력으로 보정
            }
        }

    }
}
