namespace TeamProejct_Dungeon;

public class QuestManager_LYR
{
    public enum QuestStatus_LYR { 대기중, 진행중, 완료 }

    public class Quest_LYR()
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int RequiredLevel { get; } // 퀘스트 완료 조건 (예: 레벨 2 달성)
        public int RewardGold { get; }    // 보상
        public QuestStatus Status { get; private set; }

    }
}