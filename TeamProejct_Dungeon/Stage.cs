using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{
    // 랜덤 관련 기능
    public static class Randomize
    {
        private static Random rand = new Random();
        
        // 지정 범위 내에서 랜덤한 수 반환
        public static int Makenum(int min, int max)
        {
            return rand.Next(min, max + 1);
        }

        // 확률(per)에 따라 플레이어에게 아이템 지급
        public static void RandomGain(Player player, IItem item, int per)
        {
            Random rand = new Random();
            if (rand.Next(0, 101) <= per)
               GameManager.inven.AddItem(item);  // 아이템 지급 메서드
        }
    }
    
    // 클리어 보상 관련
    public static class StageClear
    {
        public static List<Action<Player>> ClearMethod = new List<Action<Player>>
        {
            Clear1,     // 1단계 보상
            Clear2,     // 2단계 보상
            Clear3      // 3단계 보상
        };
        
        // 1단계 스테이지 클리어 보상
        public static void Clear1(Player player)
        {
            player.AddGold(Randomize.Makenum(20, 50));
            Randomize.RandomGain(player, ItemDatabase.weaponList[0], 20);
            Randomize.RandomGain(player, ItemDatabase.armourList[0], 20);
            Randomize.RandomGain(player, ItemDatabase.consumableList[0], 20);
            Randomize.RandomGain(player, ItemDatabase.consumableList[1], 5);
        }
        
        // 2단계 스테이지 클리어 보상
        public static void Clear2(Player player)
        {
            player.AddGold(Randomize.Makenum(40, 70));
            Randomize.RandomGain(player, ItemDatabase.weaponList[1], 20);
            Randomize.RandomGain(player, ItemDatabase.armourList[1], 20);
            Randomize.RandomGain(player, ItemDatabase.consumableList[0], 50);
            Randomize.RandomGain(player, ItemDatabase.consumableList[1], 10);
            Randomize.RandomGain(player, ItemDatabase.consumableList[2], 5);
        }
        
        // 3단계 스테이지 클리어 보상
        public static void Clear3(Player player)
        {
            player.AddGold(Randomize.Makenum(60, 90));
            Randomize.RandomGain(player, ItemDatabase.weaponList[2], 20);
            Randomize.RandomGain(player, ItemDatabase.armourList[2], 20);
            Randomize.RandomGain(player, ItemDatabase.consumableList[0], 70);
            Randomize.RandomGain(player, ItemDatabase.consumableList[1], 20);
            Randomize.RandomGain(player, ItemDatabase.consumableList[2], 20);
            
        }
    }

    // 전체 스테이지 데이터베이스
    public static class StageDB
    {
        //for문 돌려서 0~4,5~9,10~14 이렇게 순서대로 넣으셔도 되고. 임의로 넣으셔고 되고. 수정 가하셔도됩니다.
        public static List<Stage> StageList = new List<Stage>
        {
            new Stage(MonsterDB.GetMonstersByLevel(1, 8), StageClear.Clear1),   // 하급 몬스터
            new Stage(MonsterDB.GetMonstersByLevel(8, 16), StageClear.Clear2),  // 중급 몬스터
            new Stage(MonsterDB.GetMonstersByLevel(17, 31), StageClear.Clear3)  // 상급 몬스터
        };
    }

    // 개별 스테이지 클래스
    public class Stage
    {
        List<Monster> monsters;     // 스테이지에 등장하는 몬스터 목록(랜덤 3마리)
        Action<Player> ClearStage;  // 스테이지 클리어 시 보상 지급 메서드

        // 스테이지 생성자
        public Stage(List<Monster> monsterList, Action<Player> clearAction)
        {
            monsters = GetRandomMonsters(monsterList, 3);   // 몬스터 리스트에서 3마리를 랜덤하게 선택
            ClearStage = clearAction;       // 보상 지급 메서드
        }
        
        // 몬스터 리스트에서 랜덤하게 n마리의 몬스터를 생성
        private List<Monster> GetRandomMonsters(List<Monster> monsterList, int count)
        {
            Random random = new Random();  
            return monsterList.OrderBy(m => random.Next()).Take(count).Select(m => m.GetCopy()).ToList();
        }

        public void StageC(Player player)
        {
            ClearStage(player);
        }
    }
}
