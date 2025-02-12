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
            if (rand.Next(0, 101) <= per)
            {
                Console.WriteLine($"{item.name}을 획득하였습니다"); // 아이템 획득 출력문

                GameManager.inven.AddItem(item);  // 아이템 지급 메서드
            }

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
            Randomize.RandomGain(player, ItemDatabase.weaponList[0].DeepCopy(), 20);
            Randomize.RandomGain(player, ItemDatabase.armourList[0].DeepCopy(), 20);
            Randomize.RandomGain(player, ItemDatabase.consumableList[0].DeepCopy(), 20);
            Randomize.RandomGain(player, ItemDatabase.consumableList[1].DeepCopy(), 5);
            PlayerQuestManage_PSJ.Alarm("Stage1", QuestType_PSJ.clear);
        }
        
        // 2단계 스테이지 클리어 보상
        public static void Clear2(Player player)
        {
            player.AddGold(Randomize.Makenum(40, 70));
            Randomize.RandomGain(player, ItemDatabase.weaponList[1].DeepCopy(), 20);
            Randomize.RandomGain(player, ItemDatabase.armourList[1].DeepCopy(), 20);
            Randomize.RandomGain(player, ItemDatabase.consumableList[0].DeepCopy(), 50);
            Randomize.RandomGain(player, ItemDatabase.consumableList[1].DeepCopy(), 10);
            Randomize.RandomGain(player, ItemDatabase.consumableList[2].DeepCopy(), 5);
            PlayerQuestManage_PSJ.Alarm("Stage2", QuestType_PSJ.clear);
        }
        
        // 3단계 스테이지 클리어 보상
        public static void Clear3(Player player)
        {
            player.AddGold(Randomize.Makenum(60, 90));
            Randomize.RandomGain(player, ItemDatabase.weaponList[2].DeepCopy(), 20);
            Randomize.RandomGain(player, ItemDatabase.armourList[2].DeepCopy(), 20);
            Randomize.RandomGain(player, ItemDatabase.consumableList[0].DeepCopy(), 70);
            Randomize.RandomGain(player, ItemDatabase.consumableList[1].DeepCopy(), 20);
            Randomize.RandomGain(player, ItemDatabase.consumableList[2].DeepCopy(), 20);
            PlayerQuestManage_PSJ.Alarm("Stage3", QuestType_PSJ.clear);
        }
    }

    // 전체 스테이지 데이터베이스
    public static class StageDB
    {
        // 스테이지 리스트
        public static List<Stage> StageList = new List<Stage>
        {
            new Stage(MonsterDB.GetMonstersByLevel(1, 7), StageClear.Clear1),   // 하급 몬스터
            new Stage(MonsterDB.GetMonstersByLevel(8, 15), StageClear.Clear2),  // 중급 몬스터
            new Stage(MonsterDB.GetMonstersByLevel(16, 30), StageClear.Clear3)  // 상급 몬스터
        };
        
        //  던전 입장 시 스테이지의 정보 출력(몬스터 이름, 레벨)
        public static int ShowStageList()
        {
            Console.Clear();
            Text.TextingLine("=============================던전=============================", ConsoleColor.DarkYellow, true);

            foreach (var stage in StageList)    // 모든 스테이지의 정보를 출력
            {
                stage.StageInfo();
            }
            int input = Text.GetInput(null, 1, 2, 3); // 입장할 스테이지 정보를 반환
            return input;
        }
    }

    // 개별 스테이지 클래스
    public class Stage
    {
        List<Monster> monsters;     // 스테이지에 등장하는 몬스터 목록(랜덤 3마리)
        Action<Player> ClearStage;  // 해당 스테이지의 클리어 보상 메서드를 저장
        public List<Monster> original;  // 해당 스테이지에서 등장할 가능성이 있는 전체 몬스터 목록(이후 여기서 3마리 픽)
        
        public string Name { get; private set; }
        public int rewardGold { get; private set; }
        public List<IItem> rewardItems { get; private set; }

        public Stage(string name, int gold, List<IItem> items)
        {
            Name = name;
            rewardGold = gold;
            rewardItems = items;
        }
        
        // 스테이지 생성자
        public Stage(List<Monster> monsterList, Action<Player> clearAction)
        {
            original = monsterList;                             // 전체 몬스터 리스트
            RefreshMonsters();                                // 새로 추가된 코드
            ClearStage = clearAction;                         // 보상 지급 메서드 실행
        }

        // 몬스터 3마리 랜덤 선택 반환하기
        public List<Monster> GetMonsters()
        {
            return monsters;
        }
        // 매번 3마리를 새롭게 선정
        public void RefreshMonsters()
        {
            monsters = GetRandomMonsters(original, 3);  // 몬스터 리스트에서 3마리를 랜덤하게 선택
        }


        // 몬스터 리스트에서 랜덤하게 count 마리의 몬스터를 선택
        private List<Monster> GetRandomMonsters(List<Monster> monsterList, int count)
        {
            Random random = new Random();  
            return monsterList.OrderBy(m => random.Next()).Take(count).Select(m => m.GetCopy()).ToList();
        }
        
        // 클리어 보상 실행
        public void ClearReward(Player player)
        {
            ClearStage(player);
        }

        // 스테이지 1, 2, 3 보여주는 창
        public void StageInfo()
        {   
            Text.TextingLine($"[스테이지 {StageDB.StageList.IndexOf(this) + 1}]", ConsoleColor.Green);
            
            for (int i = 0; i < original.Count; i++)    // 모든 몬스터 정보 출력
            {
                var monster = original[i];
                
                // 몬스터 나열 시, 마지막 몬스터에는 ',' 빼주기
                if (i < original.Count - 1)    
                {
                    // e.g. Lv.1 슬라임,
                    Text.Texting($"Lv.{monster.level} {monster.Name}, ", ConsoleColor.DarkRed, false);
                }
                else
                {   // e.g. Lv.1 슬라임
                    Text.Texting($"Lv.{monster.level} {monster.Name}", ConsoleColor.DarkRed, false);  // 마지막 몬스터는 ',' 없이 출력
                }
            }
            Console.WriteLine("\n");
        }
    }
}
