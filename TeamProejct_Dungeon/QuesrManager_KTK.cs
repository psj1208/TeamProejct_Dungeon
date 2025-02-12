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
        public int rewarExp { get; set; }       // 획득한 경험치
        public bool IsCompleted => currenProgress >= goalAmount;    //  목표 달성 여부 0/5

        public Quest_KTK(string title, string descrip, int currenProg, int goalA, int rewardG, int rewaeE)
        {
            this.title = title;
            this.description = descrip;
            currenProgress = currenProg;
            goalAmount = goalA;
            rewardGold = rewardG;
            rewarExp = rewaeE;
            currenProgress = 0;
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
        List<Quest_KTK> availableQuests = new List<Quest_KTK>();    // 수락 가능한 퀘스트

        public QuestManager_KTK()
        {

        }

    }

    // 퀘스트를 저장하는 DB
    public class QuestDB_KTK
    {
        public QuestDB_KTK()
        {

        }
    }

}

