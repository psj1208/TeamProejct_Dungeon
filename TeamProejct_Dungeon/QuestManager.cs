using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{
    class QuestScene_KSJ
    {
        public List<Quest_KSJ> quests = new List<Quest_KSJ>();

        public QuestScene_KSJ()
        {
            AddQuests();
        }

        public void AddQuests()
        {
            quests.Add(new Quest1());
            quests.Add(new Quest2());
        }

        public void QuestDisplay()
        {
            while (true)
            {
                Console.Clear();
                Text.TextingLine("퀘스트 목록\n", ConsoleColor.Yellow, false);
                for (int i = 0; i < quests.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {quests[i].questName} - {quests[i].questDescription}");

                    if (quests[i].isAccept == false)
                    {
                        Console.WriteLine($"   보  상 : {quests[i].questReward}\n");
                    }

                    else if(quests[i].isClear == false || quests[i].isAccept == false)
                    {
                        Console.WriteLine($"   [퀘스트 진행중]\n");
                    }

                    else if (quests[i].isClear == true)
                    {
                        Console.WriteLine($"   [보상지급 완료]\n");
                    }

                }


                Console.WriteLine("0. 나가기");
                Console.WriteLine("퀘스트를 선택하세요");

                int questChoice = Text.GetInput(null, 1, 2, 0);


                if (questChoice == 1)
                {
                    QuestSelectDisplay(quests[questChoice - 1]);
                }
                else if (questChoice == 2 )
                {
                    QuestSelectDisplay(quests[questChoice - 1]);
                }

                else if (questChoice == 0) return;
            }
        }

        public void QuestSelectDisplay(Quest_KSJ quest)
        {
            Console.Clear();
            Text.TextingLine("Quest!!\n", ConsoleColor.Yellow, false);
            Console.WriteLine($"{quest.questName}\n");

            quest.questDescriptionDetail();

            Console.WriteLine($"\n- {quest.questDescription}\n");
            Console.WriteLine($"- 보상");
            Console.WriteLine($"  {quest.questReward}\n");

            if(quest.isProcress == false)
            {
                Console.WriteLine($"1. 수락\n2. 거절\n");
                Console.WriteLine($"원하시는 행동을 입력하세요.");

                int acceptChoice = Text.GetInput(null, 1, 2);

                switch (acceptChoice)
                {
                    case 1:
                        Console.WriteLine($" {quest.questName}  퀘스트가 수락 되었습니다.");
                        quest.Accept();
                        break;
                    case 2:
                        Console.WriteLine($" {quest.questName}  퀘스트가 거절 되었습니다.");
                        break;
                }
                Console.ReadLine();

            }
            else if(quest.isClear == false )
            {
                Console.WriteLine($"진행 중인 퀘스트입니다. ");
                Console.WriteLine($"0.나가기");
                Text.GetInput(null, 0);
            }

            else if (quest.isReward == false)
            {
                Console.WriteLine($"퀘스트를 완료 했습니다.");
                Console.WriteLine($"1. 보상받기\n2. 돌아가기\n");

                int acceptChoice = Text.GetInput(null, 1, 2);

                switch (acceptChoice)
                {
                    case 1:
                        quest.Reword(GameManager.player);
                        break;
                    case 2:
                        return;
                }
            }
        }
    }

    public abstract class Quest_KSJ
    {
        public string questName { get; set; }
        public string questDescription { get; set; }
        public string questReward {  get; set; }
        public bool isAccept = false;
        public bool isProcress = false;
        public bool isClear = false;
        public bool isReward = false;

        public void Accept()
        {
            isAccept = true;
            isProcress = true;
        }

        public abstract void Reword(Player player);

        public abstract void questDescriptionDetail();
    }

    //3킬퀘스트
    public class Quest1 : Quest_KSJ
    {
        public int totlalKill =  3;
        public int curKill = 0;

        public Quest1()
        {
            questName = "몬스터를 죽여라~";
            questDescription = "몬스터 3킬";
            questReward = "1000G";
            
        }

        public bool ClearCheck()
        {
            if (totlalKill >= curKill)
            {
                return true;
            }
            
            return false;
        }

        public override void questDescriptionDetail()
        {
            Console.WriteLine("이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나 ?");
            Console.WriteLine("마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!");
            Console.WriteLine("모험가인 자네가 좀 처치해주게!");
        }

        //보상받기
        public override void Reword(Player player)
        {
            GameManager.player.AddGold(1000);
            isClear = true;            
        }
    }

    //장비착용 퀘스트
    public class Quest2 : Quest_KSJ
    {
        private int totlalKill = 3;
        private int curKill = 0;
        

        public Quest2()
        {
            questName = "더욱 성장하기!";
            questDescription = "레벨 3달성";
            questReward = "Atk + 10 , Dfs + 10 ";
            isAccept = false;
        }

        public override void questDescriptionDetail()
        {

            Console.WriteLine("모험가라면 강해지는 게 당연하지!");
            Console.WriteLine("지금의 실력으로는 위험한 모험을 떠나기엔 아직 부족하다고.");
            Console.WriteLine("레벨을 올려 한층 더 강해진 모습을 보여주게!");
        }


        public override void Reword(Player player)
        {
            GameManager.player.atk += 10;
            GameManager.player.dfs += 10;
            isClear = true;
        }

    }



    // 더욱 더 강해지기!

    // 

    // 레벨 3 달성

    // 방어력 / 힘 10 영구 증가
}
