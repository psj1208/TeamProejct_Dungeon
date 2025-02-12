using System;
using System.Collections.Generic;
using TeamProejct_Dungeon;

public class QuestManager_KTK
{
    private List<Quest_KTK> availableQuests = new List<Quest_KTK>();
    private List<Quest_KTK> activeQuests = new List<Quest_KTK>();
    private Player player; // 플레이어 객체 추가

    public QuestManager_KTK(Player player)
    {
        this.player = player; // 플레이어 객체 초기화
        InitializeQuests_KTK();
    }

    private void InitializeQuests_KTK()
    {
        availableQuests.Add(new Quest_KTK("마을을 위협하는 미니언 처치", "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나? 마을 주민들의 안전을 위해 처치해주게!", "미니언", 1, 10, 10));
        availableQuests.Add(new Quest_KTK("장비를 장착해보자", "모험을 떠나기 전에 장비를 장착해보게나!", "장비 장착", 1, 3, 10));
        availableQuests.Add(new Quest_KTK("더욱 더 강해지기!", "강해지고 싶다면 레벨을 올려보게나!", "레벨업", 3, 10, 20));
    }

    public void ShowQuestBoard_KTK()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n Quest Board\n");

            for (int i = 0; i < availableQuests.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableQuests[i].Title}");
            }
            Console.WriteLine("\n0. 나가기");
            Console.Write("\n원하시는 퀘스트를 선택해주세요.\n>> ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice == 0) return;
                if (choice >= 1 && choice <= availableQuests.Count)
                {
                    ShowQuestDetails_KTK(availableQuests[choice - 1]);
                }
            }
        }
    }

    public void ShowQuestDetails_KTK(Quest_KTK quest)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n Quest Details\n");
            Console.WriteLine($" {quest.Title}\n");
            Console.WriteLine($"{quest.Description}\n");
            Console.WriteLine($"목표: {quest.Objective} ({quest.CurrentProgress}/{quest.TargetCount})\n");
            Console.WriteLine(" 보상:");
            Console.WriteLine($"\t- {quest.RewardGold}G");

            if (quest.IsCompleted)
            {
                Console.WriteLine("\n1. 보상 받기");
            }
            else
            {
                Console.WriteLine("\n1. 수락");
            }
            Console.WriteLine("2. 거절");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice == 1)
                {
                    if (quest.IsCompleted)
                    {
                        CompleteQuest_KTK(quest);
                        return;
                    }
                    else
                    {
                        AcceptQuest_KTK(quest);
                        return;
                    }
                }
                else if (choice == 2)
                {
                    return;
                }
            }
        }
    }

    private void AcceptQuest_KTK(Quest_KTK quest)
    {
        if (!activeQuests.Contains(quest))
        {
            activeQuests.Add(quest);
            Console.WriteLine("\n 퀘스트를 수락했습니다!\n아무 키나 눌러서 돌아가세요.");
        }
        else
        {
            Console.WriteLine("\n 이미 수락한 퀘스트입니다.");
        }
        Console.ReadKey();
    }

    public void TrackMonsterKill_KTK(string monsterName)
    {
        foreach (var quest in activeQuests)
        {
            if (quest.Objective == monsterName && !quest.IsCompleted)
            {
                quest.IncreaseProgress_KTK();
                Console.WriteLine($"\n {monsterName} 처치! ({quest.CurrentProgress}/{quest.TargetCount})");

                if (quest.IsCompleted)
                {
                    Console.WriteLine("\n 퀘스트 완료! 마을로 돌아가 보상을 받으세요.");
                }
            }
        }
    }

    public void CompleteQuest_KTK(Quest_KTK quest)
    {
        Console.Clear();
        Console.WriteLine($"\n🎉 {quest.Title} 완료!\n");
        Console.WriteLine("🎁 보상을 받았습니다:\n");
        Console.WriteLine($"- {quest.RewardItem} x1");
        Console.WriteLine($"- {quest.RewardGold}G\n");

        player.AddGold(quest.RewardGold);

        activeQuests.Remove(quest);
        Console.WriteLine("\n아무 키나 눌러서 나가기...");
        Console.ReadKey();
    }

    public void ClaimReward_KTK()
    {
        foreach (var quest in activeQuests)
        {
            if (quest.IsCompleted)
            {
                CompleteQuest_KTK(quest);
                break;
            }
        }
    }
}

public class Quest_KTK
{
    public string Title { get; }
    public string Description { get; }
    public string Objective { get; }
    public int TargetCount { get; }
    public int CurrentProgress { get; private set; }
    public string RewardItem { get; }
    public int RewardGold { get; }
    public int rewaedExp { get; }
    public bool IsCompleted { get; private set; }

    public Quest_KTK(string title, string description, string objective, int targetCount, int rewardGold, int rewaedExp)
    {
        Title = title;
        Description = description;
        Objective = objective;
        TargetCount = targetCount;
        RewardGold = rewardGold;
        this.rewaedExp = rewaedExp;
        CurrentProgress = 0;
        IsCompleted = false;
    }

    public void IncreaseProgress_KTK()
    {
        if (!IsCompleted)
        {
            CurrentProgress++;
            if (CurrentProgress >= TargetCount)
            {
                MarkComplete_KTK();
            }
        }
    }

    public void MarkComplete_KTK()
    {
        IsCompleted = true;
    }
}
