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
        public void DisplayItems() // DisplayItems를 호출하면 => Buy호출
        {
            Console.WriteLine("--- 상점 목록 ---");
            Text.TextingLine("1. 갑옷", ConsoleColor.White);
            Text.TextingLine("2. 무기", ConsoleColor.White);
            Text.TextingLine("3. 소비", ConsoleColor.White);
            Text.TextingLine("4. 휴식", ConsoleColor.White);
            Text.Texting("구매하고싶은 목록을 선택해주세요>> ");
            int index = 1;
            string input = Console.ReadLine();
            int category = int.Parse(input);
            int categoryIndex = category - 1;
            switch (category - 1)
            {

                case (int)ItemType.Armour:
                    Console.WriteLine("----------갑옷류----------");
                    for (int i = 0; i < shopList[0].Count; i++)
                    {
                       
                        Console.WriteLine($"{index}. {shopList[0][i].name} - 가격: {shopList[0][i].buyPrice}");
                        index++;
                    }

                    break;
                case (int)ItemType.Weapon:
                    Console.WriteLine("----------무기류----------");
                    for (int i = 0; i < shopList[1].Count; i++)
                    {
                       
                        Console.WriteLine($"{index}. {shopList[1][i].name} - 가격: {shopList[1][i].buyPrice}");
                        index++;
                    }
                    break;
                case (int)ItemType.Consumable:
                    Console.WriteLine("----------소비류----------");
                    for (int i = 0; i < shopList[2].Count; i++)
                    {
                       
                        Console.WriteLine($"{index}. {shopList[2][i].name} - 가격: {shopList[2][i].buyPrice}");
                        index++;
                    }
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("----------휴식중----------");
                    Rest();
                    break;
                default:
                    Console.WriteLine("잘못입력하셨습니다.");
                    break;

            }
            Buy(categoryIndex); // 구매 호출
        }


        public void Buy(int categoryIndex)
        {

            Console.Write("\n구매하려는 물건의 번호를 입력하세요: ");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int itemNumber) || itemNumber < 1 || itemNumber > shopList[categoryIndex].Count)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }

            IItem selectedItem = shopList[categoryIndex][itemNumber - 1];

            // 골드 부족 확인
            if (GameManager.player.gold < selectedItem.buyPrice)
            {
                Console.WriteLine("골드가 부족합니다.");
                return;
            }

            // 구매 처리
            GameManager.player.gold -= (int)selectedItem.buyPrice;
            GameManager.player.inven.AddItem(selectedItem.DeepCopy());

            Console.WriteLine($"{selectedItem.name}을(를) 구매했습니다! 남은 골드: {GameManager.player.gold}");
        }




        public void Rest() // 휴식하기
        {
            Console.WriteLine($"현재 체력은{GameManager.player.hp} 입니다.");
            GameManager.player.hp += GameManager.player.maxHp / 2; // 플레이어 최대체력 50퍼센트 체력 회복
            if (GameManager.player.hp > GameManager.player.maxHp)
            {
                GameManager.player.hp = GameManager.player.maxHp; // 최대체력보다 체력이 높으면 최대체력으로 보정
            }
            Console.WriteLine($"휴식후 현재 체력은{GameManager.player.hp} 입니다.");
        }

    }
}
