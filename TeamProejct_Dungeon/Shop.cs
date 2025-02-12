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
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- 상점 목록 ---");
                Text.TextingLine("1. 갑옷", ConsoleColor.White);
                Text.TextingLine("2. 무기", ConsoleColor.White);
                Text.TextingLine("3. 소비", ConsoleColor.White);
                Text.TextingLine("4. 휴식", ConsoleColor.White);
                Text.TextingLine("5. 나가기", ConsoleColor.White);
                Text.TextingLine("구매하고싶은 목록을 선택해주세요");
                int index = 1;
                int category = Text.GetInput(null, 1, 2, 3, 4, 5);
                Console.Clear();
                switch (category - 1)
                {
                    case (int)ItemType.Armour:
                        Console.WriteLine("----------갑옷류----------");
                        for (int i = 0; i < shopList[0].Count; i++)
                        {

                            Console.WriteLine($"{index}. {shopList[0][i].name} - 가격: {shopList[0][i].buyPrice}");
                            index++;
                        }

                        Buy(category); // 구매 호출
                        break;
                    case (int)ItemType.Weapon:
                        Console.WriteLine("----------무기류----------");
                        for (int i = 0; i < shopList[1].Count; i++)
                        {

                            Console.WriteLine($"{index}. {shopList[1][i].name} - 가격: {shopList[1][i].buyPrice}");
                            index++;
                        }
                        Buy(category); // 구매 호출
                        break;
                    case (int)ItemType.Consumable:
                        Console.WriteLine("----------소비류----------");
                        for (int i = 0; i < shopList[2].Count; i++)
                        {

                            Console.WriteLine($"{index}. {shopList[2][i].name} - 가격: {shopList[2][i].buyPrice}");
                            index++;
                        }
                        Buy(category); // 구매 호출
                        break;
                    case 3:
                        Console.WriteLine("----------휴식중----------");
                        Rest();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("잘못입력하셨습니다.");
                        break;

                }
            }
        }


        public void Buy(int categoryIndex)
        {
            Console.WriteLine("4. 나가기");
            Console.Write("\n구매하려는 물건의 번호를 입력하세요\n");
            
            int category = Text.GetInput(null, 1, 2, 3,4);
            if (category == 4) // 나가기
                return;
            IItem selectedItem = shopList[categoryIndex -1][category - 1];

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
            
            Console.ReadLine();
        }




        public void Rest() // 휴식하기
        {
            Console.WriteLine($"현재 체력은{GameManager.player.hp} 입니다.");
            Console.WriteLine($"현재 마나는{GameManager.player.mp} 입니다.");
            GameManager.player.hp += GameManager.player.maxHp / 2; // 플레이어 최대체력 50퍼센트 체력 회복
            GameManager.player.mp += (int)Math.Round(GameManager.player.maxMp * 0.7); // 플레이어 마나 반올림 
            if (GameManager.player.hp > GameManager.player.maxHp)
            {
                GameManager.player.hp = GameManager.player.maxHp; // 최대체력보다 체력이 높으면 최대체력으로 보정
            }
            if (GameManager.player.mp > GameManager.player.maxMp) // 최대 마나보다 마나가 높으면 최대마나로 보정
            {
                GameManager.player.mp = GameManager.player.maxMp;
            }

            Text.TextingLine($"휴식후 현재 체력은{GameManager.player.hp} 입니다.",ConsoleColor.DarkYellow);
            Text.TextingLine($"휴식후 현재 마나는{GameManager.player.mp} 입니다.", ConsoleColor.DarkYellow);
            Console.ReadLine();
        }

    }
}
