using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{
    class QuestScene_SMK
    {
        private List<Quest_SMK> quests = new List<Quest_SMK>();

        public QuestScene_SMK()
        {
            AddQuests_SMK();
            QuestDisplay_SMK();
        }

        public void AddQuests_SMK()
        {
            quests.Add(new Quest1_SMK());
            quests.Add(new Quest2_SMK());
        }

        public void QuestDisplay_SMK()
        {
            Console.Clear();
            Text.TextingLine("퀘스트 목록\n", ConsoleColor.Yellow, false);
            foreach (Quest_SMK quest in quests)
            {
                Console.WriteLine($"{quest.questName} - {quest.questDescription}\n");
            }

            int questChoice = Text.GetInput(null, 1, 2);

            if (questChoice == 1)
            {

            }
            else if (questChoice == 2)
            {

            }

        }
    }

    public enum QuestStatus_SMK
    {
        NotProgress, // 미수락
        IsProgress,  // 진행중
        Completed,   // 완료
    }

    public abstract class Quest_SMK
    {
        public string questName { get; set; }
        public string questDescription { get; set; }
        public QuestStatus_SMK status { get; set; } = QuestStatus_SMK.NotProgress;


        public void Accept_SMK()
        {
            status = QuestStatus_SMK.IsProgress;

        }

        public void Compelete_SMK()
        {
            status = QuestStatus_SMK.Completed;
        }

        public abstract void Reward_SMK(Player player);
    }

    //3킬퀘스트
    public class Quest1_SMK : Quest_SMK
    {
        private int totlalKill = 3;
        private int curKill = 0;

        public Quest1_SMK()
        {
            questName = "마을을 위협하는 몬스터 처치";
            questDescription = "몬스터 3킬";
        }

        public void KillMonster_SMK()
        {
            if (status == QuestStatus_SMK.IsProgress)
            {
                curKill++;
                if (curKill >= totlalKill)
                {
                    Compelete_SMK();
                }
            }
        }

        //퀘스트 완료시 1000골드 지급
        public override void Reward_SMK(Player player)
        {
            if (status == QuestStatus_SMK.Completed)
            {
                player.AddGold(1000);
            }
        }
    }

    //장비착용 퀘스트
    public class Quest2_SMK : Quest_SMK
    {
        private int totlalKill = 3;
        private int curKill = 0;

        public Quest2_SMK()
        {
            questName = "더욱 강해지기!";
            questDescription = "레벨 3달성";
        }

        public void EquipItem()
        {
            if (status == QuestStatus_SMK.IsProgress)
            {
                curKill++;
                if (curKill >= totlalKill)
                {
                    Compelete_SMK();
                }
            }
        }

        //퀘스트 완료시 1000골드 지급
        public override void Reward_SMK(Player player)
        {
            if (status == QuestStatus_SMK.Completed)
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
