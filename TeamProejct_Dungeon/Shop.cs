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
    }
}
