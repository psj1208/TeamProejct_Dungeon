using System;
using System.Collections.Generic;
using TeamProejct_Dungeon;

namespace TeamProejct_Dungeon
{
    public class Quest_KTK
    {
        //public string name { get; set; }        // 이름
        public string title { get; set; }       // 제목
        public string description { get; set; } // 설명

        public int currenProgress { get; set; } // 현재 진행
        public int goalAmount { get; set; }     // 목표 진행
                                                // 보상
        public int rewardGold { get; set; }     // 획득한 돈
        public int rewardExp { get; set; }       // 획득한 경험치
        public bool IsCompleted => currenProgress >= goalAmount;    //  목표 달성 여부 0/5

        public Quest_KTK(string title, string descrip, int goalA, int rewardG, int rewardE)
        {
            this.title = title;
            this.description = descrip;
            currenProgress = 0;  // 초기 진행도는 항상 0
            goalAmount = goalA;
            rewardGold = rewardG;
            rewardExp = rewardE;
        }

        // 퀘스트 진행 상황
        public void updateProgress(int amount)
        {
            currenProgress += amount;
            if (currenProgress > goalAmount)
            {
                currenProgress = goalAmount;
            }
        }

    }
    // 퀘스트를 관리하는 생성자
    public class QuestManager_KTK
    {
        // 퀘스트 목록
        List<Quest_KTK> activeQuests = new List<Quest_KTK>();    // 진행 중인 퀘스트 목록
        List<Quest_KTK> availableQuests;   // 수락 가능한 퀘스트

        public QuestManager_KTK(QuestDB_KTK questDB_KTK)
        {
            availableQuests = questDB_KTK.GetAvailableQuests();
            showQuestMenu();

        }
        public void showQuestMenu()
        { 
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=============퀘스트 목록 =============");

                Console.WriteLine("===[진행 중인 퀘스트]===");
                foreach(var quest in activeQuests)
                {
                    Console.WriteLine($"- {quest.title}");
                }

                Console.WriteLine("\n[선택]");
                Console.WriteLine("1. 퀘스트 완료하기");
                Console.WriteLine("2. 새로운 퀘스트 수락");
                Console.WriteLine("3. 퀘스트 포기");
                Console.WriteLine("0. 나가기");

                int input = Text.GetInput(null, 1, 2, 3, 0);

                switch(input)
                {
                    case 1: completeQuest(); break;
                    case 2: acceptQuest(); break; 
                    case 3: abandonQuest(); break;
                    case 0: return;
                    default: Console.WriteLine("잘못된 입력입니다.");break;
                }
            }
        }
        private void completeQuest()
        {
            for (int i = activeQuests.Count - 1; i >= 0; i--)
            {
                if (activeQuests[i].IsCompleted)
                {
                    Console.WriteLine($"{activeQuests[i].title} 퀘스트 완료! 보상: {activeQuests[i].rewardGold}골드, {activeQuests[i].rewardExp}EXP");
                    activeQuests.RemoveAt(i);
                }
            }
            Console.ReadLine();
        }

        // 포기할 퀘스트 번호
        private void abandonQuest()
        {
            Console.Write("포기할 퀘스트 번호 입력: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= activeQuests.Count)
            {
                availableQuests.Add(activeQuests[index - 1]);
                activeQuests.RemoveAt(index - 1);
                Console.WriteLine("퀘스트를 포기했습니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            Console.ReadKey();
        }

        private void acceptQuest()
        {
            Console.Write("수락할 퀘스트 번호 입력: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= availableQuests.Count)
            {
                activeQuests.Add(availableQuests[index - 1]);
                availableQuests.RemoveAt(index - 1);
                Console.WriteLine("퀘스트를 수락했습니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
            Console.ReadKey();
        }

        
    }

    // 퀘스트를 저장하는 DB
    public class QuestDB_KTK
    {
        List<Quest_KTK> questList = new List<Quest_KTK>();
        public QuestDB_KTK()
        {
            loadQuest();
        }

        private void loadQuest()
        {
            //("첫 번째 퀘스트", "슬라임 5마리 처치", 5, 100, 50));
            questList.Add(new Quest_KTK("첫 번째 퀘스트", "슬라임 5마리 처치", 5, 100, 50));
            questList.Add(new Quest_KTK("두 번째 퀘스트", "고블린 3마리 처치", 3, 150, 70));
            questList.Add(new Quest_KTK("세 번째 퀘스트", "스켈레톤 4마리 처치", 4, 200, 90));

        }

        public List<Quest_KTK> GetAvailableQuests()
        {
            return new List<Quest_KTK>(questList);
        }
    }

}

