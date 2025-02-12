using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{
    class QuestScene_KSJ
    {
        private List<Quest_KSJ> quests = new List<Quest_KSJ>();

        public QuestScene_KSJ()
        {
            AddQuests();
            QuestDisplay();
        }

        public void AddQuests()
        {
            quests.Add(new Quest1());
            quests.Add(new Quest2());
        }

        public void QuestDisplay()
        {
            Console.Clear();
            Text.TextingLine("퀘스트 목록\n", ConsoleColor.Yellow, false);
            foreach (Quest_KSJ quest in quests)
            {
                Console.WriteLine($"{quest.questName} - {quest.questDescription}\n");
            }

            int questChoice = Text.GetInput(null,1, 2);

            if (questChoice == 1)
            {
                
            }
            else if(questChoice ==2)
            {

            }

        }
    }

    public enum QuestStatus
    {
        NotProgress, // 미수락
        IsProgress,  // 진행중
        Completed,   // 완료
    }

    public abstract class Quest_KSJ
    {
        public string questName { get; set; }
        public string questDescription { get; set; }
        public QuestStatus status { get; set; } = QuestStatus.NotProgress;
        

        public void Accept()
        {
            status = QuestStatus.IsProgress;

        }

        public void Compelete()
        {
            status = QuestStatus.Completed;
        }

        public abstract void Reward(Player player);
    }

    //3킬퀘스트
    public class Quest1 : Quest_KSJ
    {
        private int totlalKill =  3;
        private int curKill = 0;

        public Quest1()
        {
            questName = "마을을 위협하는 몬스터 처치";
            questDescription = "몬스터 3킬";
        }

        public void KillMonster()
        {
            if (status == QuestStatus.IsProgress)
            {
                curKill++;
                if (curKill >= totlalKill)
                {
                    Compelete();
                }
            }
        }

        //퀘스트 완료시 1000골드 지급
        public override void Reward(Player player)
        {
            if (status == QuestStatus.Completed)
            {
                player.AddGold(1000);
            }
        }
    }

    //장비착용 퀘스트
    public class Quest2 : Quest_KSJ
    {
        private int totlalKill = 3;
        private int curKill = 0;

        public Quest2()
        {
            questName = "더욱 강해지기!";
            questDescription = "레벨 3달성";
        }

        public void EquipItem()
        {
            if (status == QuestStatus.IsProgress)
            {
                curKill++;
                if (curKill >= totlalKill)
                {
                    Compelete();
                }
            }
        }

        //퀘스트 완료시 1000골드 지급
        public override void Reward(Player player)
        {
            if (status == QuestStatus.Completed)
            {
                player.AddGold(1000);
            }
        }
    }



    // 더욱 더 강해지기!

    // 

    // 레벨 3 달성

    // 방어력 / 힘 10 영구 증가
}
