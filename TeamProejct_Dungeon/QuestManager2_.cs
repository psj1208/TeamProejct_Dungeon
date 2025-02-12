using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Numerics;

namespace TeamProejct_Dungeon
{

    public static class PlayerQuestManage_PSJ
    {
        public static List<Quest_PSJ> quests = new List<Quest_PSJ>();

        public static void ShowPlayerQuest()
        {
            Console.Clear();
            ConsoleKeyInfo keyinfo;
            while (true)
            {
                try
                {
                    if (quests == null || quests.Count == 0)
                        throw new Exception();
                    string[] questList = new string[quests.Count];
                    for (int i = 0; i < quests.Count; i++)
                        questList[i] = $"{i + 1} . {quests[i].title} : {quests[i].description}";
                    Text.TextingLine("------------------------플레이어 퀘스트-------------------------", ConsoleColor.Blue, false);
                    foreach (Quest_PSJ p in quests)
                    {
                        Text.TextingLine($"\n{p.title} : {p.description} , 진행도 : {p.curClearCount} / {p.TargetClearCount}\n", ConsoleColor.White, false);
                    }

                }
                catch (Exception ex)
                {
                    Text.TextingLine("------------------------플레이어 퀘스트-------------------------", ConsoleColor.Blue, false);
                    Text.TextingLine(" 현재 진행 중인 퀘스트가 없습니다.", ConsoleColor.Green, false);
                }
                finally
                {
                    Text.TextingLine("뒤로 가기 : ESC", ConsoleColor.Green, false);
                    keyinfo = Console.ReadKey(true);
                }
                if (keyinfo.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    break;
                }
                Console.Clear();
            }
        }
        public static void AddQuest(Quest_PSJ q)
        {
            quests.Add(q.DeepCopy());
        }
        public static void Alarm(string target, QuestType_PSJ ty)//
        {
            for (int i = 0; i < quests.Count; i++) 
            {
                if (quests[i].target == target)
                    if (quests[i].qt == ty)
                    {
                        if (quests[i].PlusCount() == true)
                            quests.RemoveAt(i);
                    }
            }
        }
    }
    public static class QuestShop
    {
        public static List<Quest_PSJ> ShopQuestList = QuestDb.lowQuestDb;

        public static void OpenQuestBoard()
        {
            Console.Clear();
            while (true)
            {
                string[] questList = new string[ShopQuestList.Count];
                for (int i = 0; i < ShopQuestList.Count; i++)
                    questList[i] = $"{ShopQuestList[i].title} : {ShopQuestList[i].description}";
                Text.TextingLine("------------------------퀘스트 보드-------------------------", ConsoleColor.Blue, false);
                Text.TextingLine("ESC로 돌아가기", ConsoleColor.Green, false);
                int? input = -1;
                if (questList.Count() > 0)
                    input = Text.GetInputMulti(true, questList);
                if (input == -1)
                {
                    Console.Clear();
                    Text.TextingLine("\n\n현재 퀘스트 보드는 비어있습니다.", ConsoleColor.Red);
                    Thread.Sleep(500);
                    break;
                }
                else if (input != null )
                {
                    input--;
                    Console.Clear();
                    Text.TextingLine($"\n{ShopQuestList[(int)input].title} 을 수주했습니다.");
                    PlayerQuestManage_PSJ.AddQuest(ShopQuestList[(int)input]);
                    ShopQuestList.RemoveAt((int)input);
                    Thread.Sleep(500);
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    break;
                }
            }
        }
    }

    public static class QuestDb
    {
        public static List<Quest_PSJ> lowQuestDb = new List<Quest_PSJ>
        {
            new Quest_PSJ("슬라임 소탕 !","슬라임을 5마리 처치.",QuestType_PSJ.kill,MonsterDB.GetMonsters()[0].Name,new Reward_PSJ(100,30,(ItemDatabase.weaponList[0].DeepCopy())),0,5),
            new Quest_PSJ("공허충 소탕 !","공허충을 5마리 소탕",QuestType_PSJ.kill,MonsterDB.GetMonsters()[2].Name, new Reward_PSJ(80,20,(ItemDatabase.armourList[0].DeepCopy())),0,5),
            new Quest_PSJ("스테이지 1 탐험 !","스테이지 1을 클리어.",QuestType_PSJ.clear,"Stage1",new Reward_PSJ(150,50))
        };

        public static List<List<Quest_PSJ>> totalQuestDb = new List<List<Quest_PSJ>>
        {
            lowQuestDb,
        };

        public static void SelectInQuestPanel()
        {
            Console.Clear();
            while (true)
            {
                Text.TextingLine("Esc로 돌아가기\n", ConsoleColor.Green, false);
                int? input = Text.GetInputMulti(true, "현재 진행 중인 퀘스트", "퀘스트 보드 보러가기");
                if (input == null)
                {
                    Console.Clear();
                    break;
                }
                else if (input == 1)
                {
                    PlayerQuestManage_PSJ.ShowPlayerQuest();
                }
                else
                {
                    QuestShop.OpenQuestBoard();
                }
            }
        }
    }

    public enum QuestType_PSJ
    {
        kill,
        find,
        clear
    }

    public class Reward_PSJ
    {
        public int gold;
        public int exp;
        public List<IItem> reward_items = new List<IItem>();
        public string description;

        public Reward_PSJ()
        {
            gold = 0;
            exp = 0;
            reward_items.Clear();
            description = "보상 없음";
        }
        public Reward_PSJ(int g, int e, params IItem[] items)
        {
            gold = g;
            exp = e;
            foreach (IItem item in items)
            {
                reward_items.Add(item);
            }
            if (gold != 0)
            {
                description += $"골드 : {gold} ";
            }
            if (exp != 0)
            {
                description += $"경험치 : {exp}";
            }
            if (reward_items.Count > 0)
            {
                description += "장비 : ";
                foreach (IItem item in reward_items)
                {
                    description += item.name + " ";
                }
            }
        }

        public void RewardGet()
        {
            if (gold != 0)
            {
                GameManager.player.AddGold(gold);
            }
            if (exp != 0)
            {
                GameManager.player.AddExp(exp);
            }
            if (reward_items.Count > 0)
            {
                foreach (IItem item in reward_items)
                    GameManager.inven.AddItem(item);
            }
        }
    }
    public class Quest_PSJ
    {
        public string title;
        public string description;
        public string target;
        public ICharacter targetIcharacter;
        public QuestType_PSJ qt;
        public int TargetClearCount;
        public int curClearCount;
        public Reward_PSJ reward;

        bool isCleared;

        public Quest_PSJ(string title, string des, QuestType_PSJ type, string target, Reward_PSJ re, int ClearCount = 0, int TargetCount = 1)
        {
            this.title = title;
            this.description = des;
            this.qt = type;
            this.target = target;
            reward = re;
            curClearCount = ClearCount;
            this.TargetClearCount = TargetCount;
            isCleared = false;
        }

        public bool PlusCount()
        {
            curClearCount++;
            Text.TextingLine($"\n{this.title} 퀘스트 진행: {this.curClearCount} / {this.TargetClearCount}", ConsoleColor.Magenta, false);
            return Renew();
        }
        public bool Renew()
        {
            if (curClearCount == TargetClearCount)
            {
                isCleared = true;
                Text.TextingLine($"\n{title} 퀘스트 완수 ! ");
                reward.RewardGet();
                Console.WriteLine();
                return true;
            }
            return false;
        }

        public Quest_PSJ DeepCopy()
        {
            return new Quest_PSJ(this.title, this.description, this.qt, this.target, this.reward, this.curClearCount, this.TargetClearCount);
        }
    }
}
