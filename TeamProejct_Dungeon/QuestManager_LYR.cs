namespace TeamProejct_Dungeon;


    // 퀘스트 상태 열거형
    public enum QuestStatus_LYR
    {
        NotProgress,    // 미진행
        InProgress,     // 진행중
        Completed       // 완료
    }

    // 퀘스트 클래스 정의
    public class Quest_LYR
    {
        public string Name { get; }
        public string description { get; }
        public int requiredLevel { get; } // 퀘스트 완료 조건 (e.g. 최소 레벨 2)
        public int rewardGold { get; } // 보상
        public QuestStatus_LYR status { get; private set; }

        // 생성자. 퀘스트 기본 정보
        public Quest_LYR(string name, string description, int requiredLevel, int rewardGold)
        {
            Name = name;
            this.description = description;
            this.requiredLevel = requiredLevel;
            this.rewardGold = rewardGold;
            status = QuestStatus_LYR.NotProgress;   // 미진행 상태로 시작
        }

        // 퀘스트 수락 메서드
        public void AcceptQuest_LYR()
        {
            if (status == QuestStatus_LYR.NotProgress)  // 퀘스트가 미진행 상태일 때만 수락할 수 있게
            {
                status = QuestStatus_LYR.InProgress;    // 퀘스트 상태 변경
                Console.WriteLine($"퀘스트 '{Name}'를 수락했습니다.");
            }
        }

        // 퀘스트 완료 여부를 확인하는 메서드
        public bool CheckCompletion_LYR(Player player)
        {
            // 퀘스트 미진행 상태이고, 플레이어 레벨이 요구 레벨 이상이면
            if (status == QuestStatus_LYR.NotProgress && player.level >= requiredLevel) // 진행상태 체크
            {
                status = QuestStatus_LYR.Completed; // 퀘스트 완료 처리
                player.gold += rewardGold;          // 보상 지급
                Console.WriteLine($"퀘스트 '{Name}' 완료! 보상: {rewardGold}G");
                return true;    // 퀘스트 완료 시 true 반환
            }
            
            return false;       // 퀘스트 완료 못할 시 false 반환
        }
    }
    
    // 퀘스트 관리
    public class QuestManager_LYR
    {
        public static QuestManager_LYR _instance;   // 싱글톤 

        // 싱글톤 인스턴스 반환 프로퍼티
        public static QuestManager_LYR Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new QuestManager_LYR(); // 처음 호출될 때 인스턴스 생성
                return _instance;
            }
        }
        
        // 퀘스트 리스트
        public List<Quest_LYR> Quests { get; } = new List<Quest_LYR>();
        
        // 퀘스트 생성자
        private QuestManager_LYR()
        {
            // 퀘스트 목록(name, description, requiredLevel, rewardGold)
            Quests.Add(new Quest_LYR("첫 걸음", "레벨 2를 달성하세요.", 1, 500));
            Quests.Add(new Quest_LYR("레벨 업!", "무기를 구매하세요.", 3, 1000));
        }
        
        // 퀘스트 목록을 출력하는 메서드
        public void ShowQuestBoard_LYR()
        {
            Console.WriteLine("\n====== 퀘스트 목록 ======");
            foreach (var quest in Quests)
            {
                Console.WriteLine($"[{quest.status}] {quest.Name} - {quest.description}");
            }
        }
        
        // 특정 퀘스트를 수락하는 메서드
        public void AcceptQuest_LYR(string questName)
        {
            var quest = Quests.FirstOrDefault(q => q.Name == questName);
            quest?.AcceptQuest_LYR();   // 퀘스트가 있으면 수락
        }
        
        // 모든 퀘스트의 완료 여부를 체크하는 메서드
        public void CheckQuests_LYR(Player player)
        {
            foreach (var quest in Quests)
            {
                if (quest.CheckCompletion_LYR(player))  // 퀘스트 완료 여부 체크
                {
                    Console.WriteLine($"'{quest.Name}' 퀘스트 완료!");
                }
            }
        }
    }