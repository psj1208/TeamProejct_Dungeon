using System;
using System.Collections.Generic;
using TeamProejct_Dungeon;

namespace TeamProject_Dungeon
{
    // 퀘스트 매니저 클래스
    public class QuestManager_KTK
    {
        private List<Quest_KTK> questList = new List<Quest_KTK>();

        // 퀘스트 추가
        public void AddQuest(Quest_KTK quest)
        {
            questList.Add(quest);
            Console.WriteLine($"🆕 새로운 퀘스트 추가 : {quest.name}");
        }

        // 퀘스트 목록 출력
        public void ShowQuestList()
        {
            Console.WriteLine("#___===[퀘스트 목록]===___#");

            foreach (var quest in questList)
            {
                string status = quest.IsCompleted ? "✅ 완료" : "🔄 진행 중";
                Console.WriteLine($"{quest.name} - {status}");
                Console.WriteLine($"설명 : {quest.description}");
                Console.WriteLine($"💰 보상 : {quest.rewardGold} Gold, 🌟 {quest.rewardExp} Exp\n");
            }
        }

        // 퀘스트 완료 체크
        public void CheckQuestCompletion()
        {
            foreach (var quest in questList)
            {
                quest.CheckCompletion();
            }
        }
    }

    // 퀘스트 클래스
    public class Quest_KTK
    {
        public string name { get; private set; } // 퀘스트 이름
        public string description { get; private set; } // 설명
        public bool IsCompleted { get; private set; } // 완료 여부
        public int rewardGold { get; private set; } // 보상 골드
        public int rewardExp { get; private set; } // 보상 경험치
        public Func<bool> completionCondition { get; private set; } // 완료 조건

        public Quest_KTK(string name, string description, int rewardGold, int rewardExp, Func<bool> completionCondition)
        {
            this.name = name;
            this.description = description;
            this.rewardGold = rewardGold;
            this.rewardExp = rewardExp;
            this.completionCondition = completionCondition;
            IsCompleted = false;
        }

        public void CheckCompletion()
        {
            if (!IsCompleted && completionCondition())
            {
                IsCompleted = true;
                Console.WriteLine($"✅ 퀘스트 완료: {name}");
                GameManager.player.AddGold(rewardGold);
                GameManager.player.AddExp(rewardExp);
                Console.WriteLine($"💰 {rewardGold} 골드, 🌟 {rewardExp} 경험치 획득!");
            }
        }
    }

    // 퀘스트 DB (초기 퀘스트 설정)
    public class QuestDB_KTK
    {
        private List<Quest_KTK> questList = new List<Quest_KTK>();

        public QuestDB_KTK(QuestManager_KTK questManager)
        {
            // 기본 퀘스트 추가
            questList.Add(new Quest_KTK(
                "자기소개",
                "자신의 상태를 확인하세요.",
                0, 10,  // 보상: 0골드, 10경험치
                () => GameManager.player.HasCheckedStatus // ✅ 상태 보기 완료 여부 확인
            ));

            // 퀘스트 매니저에 등록
            foreach (var quest in questList)
            {
                questManager.AddQuest(quest);
            }
        }
    }
}
