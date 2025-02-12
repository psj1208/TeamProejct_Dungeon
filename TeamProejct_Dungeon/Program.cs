using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Linq;
//using TeamProject_Dungeon;

namespace TeamProejct_Dungeon
{
    enum SceneType
    {
        Lobby,
        Home,
        Dungeon
    }
    internal class Program
    {
        static SceneType sceneType = SceneType.Lobby;
        static void Main(string[] args)
        {
            int input = 1;
            Stage stage = StageDB.StageList[input - 1];
            string imagePath = AppDomain.CurrentDomain.BaseDirectory + "\\sample.jpg"; // 이미지 경로(상위 폴더/bin/Debug/net버전안에 넣어야함.)
            AsciiArt.Draw(imagePath, 20);
            GameStart();
        }
        static void GameStart()
        {
            //여기에 게임 흐름
            // 플레이어와 몬스터 리스트 생성
            Shop shop = new Shop();
            QuestManager_KTK questManager_KTK = new QuestManager_KTK(GameManager.player);

            while (true)
            {
                //로비
                if (sceneType == SceneType.Lobby)
                {
                    string input_name = Text.GetInput("캐릭터의 이름을 입력해주세요.");
                    int input_job = Text.GetInput("플레이어의 직업을 선택해주세요\n\n1 . 전사 : 높은 방어력과 강력한 한 방이 있습니다.\n\n2 . 도적 : 높은 공격력과 다중공격을 할 수 있습니다.\n\n", 1, 2);
                    if (input_job == 1)
                    {
                        GameManager.player = new Player(input_name, Job.Warrior);
                    }
                    else
                    {
                        GameManager.player = new Player(input_name, Job.Assassin);
                    }
                    Text.TextingLine($"이름 : {GameManager.player.Name} , 직업 : {GameManager.player.job} 캐릭터가 생성되었습니다.", ConsoleColor.Green);
                    Thread.Sleep(500);
                    Text.TextingLine($"\n\n잠시 후 마을에 입장합니다.", ConsoleColor.Green);
                    sceneType = SceneType.Home;
                    Thread.Sleep(500);
                    Console.Clear();
                }
                //마을
                else if (sceneType == SceneType.Home)
                {
                    Console.Clear();
                    Text.TextingLine("------------------------마을-------------------------", ConsoleColor.Magenta, true);
                    Text.TextingLine("\n\n1 . 상태 보기\n\n2 . 인벤토리\n\n3 . 상점\n\n4 . 퀘스트 보드\n\n5 . 던전\n\n6. 세이브\n", ConsoleColor.Green, false);
                    int input = Text.GetInput(null, 1, 2, 3, 4, 5, 6);
                    switch (input)
                    {
                        case 1:
                            //플레이어 스탯 보기
                            Console.Clear();
                            GameManager.player.StatusDisplay();
                            Console.Clear();
                            break;
                        case 2:
                            //플레이어 인벤토리(장착하는 기능)
                            GameManager.player.inven.UsingInventory();
                            break;
                        case 3:
                            shop.DisplayItems();
                            break;
                        case 4:
                            //여기에 퀘스트 보드 보여주는 쪽으로.
                            Console.Clear();
                            //questManager_KTK.ShowQuestBoard_KTK();
                            break;
                        case 5:
                            //던전 이동
                            sceneType = SceneType.Dungeon;
                            break;
                        case 6:
                            //세이브 기능
                            break;
                        default:
                            break;
                    };
                }
                //던전
                else if (sceneType == SceneType.Dungeon)
                {
                    // 스테이지 선택
                    int stageIndex = StageDB.ShowStageList();
                    Stage selectedStage = StageDB.StageList[stageIndex - 1];

                    // 스테이지 입장 시 새로운 몬스터 3마리 선정
                    selectedStage.RefreshMonsters();

                    // 선택한 스테이지 출력
                    selectedStage.StageInfo();
                    Console.WriteLine("던전에 입장하겠습니다.");
                    Console.WriteLine("아무 키를 눌러 진행하기");
                    Console.ReadKey();


                    // 몬스터 리스트 가져오기
                    List<Monster> monsters = selectedStage.GetMonsters();

                    // 전투 시작
                    Battle(GameManager.player, selectedStage, monsters);

                    // 마을로 복귀
                    sceneType = SceneType.Home;
                }
            }
        }

        static void ShowMonsterInfo(List<Monster> monsters, bool Shownumber = false)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                Monster monster = monsters[i];
                string levelText = monster.level.ToString("D2");

                Console.ForegroundColor = monster.isDead ? ConsoleColor.DarkGray : ConsoleColor.White;

                if (Shownumber)
                {
                    Console.Write($"{i + 1}.Lv.{levelText} {monster.Name} ");
                }
                else
                {
                    Console.Write($"Lv.{levelText} {monster.Name} ");
                }

                // "Dead"를 DarkGray로, HP 상태를 Red로 출력
                if (monster.isDead)
                {
                    Text.TextingLine("Dead", ConsoleColor.DarkGray, false);
                }
                else
                {
                    Text.TextingLine($"HP {monster.hp}/{monster.maxHp}", ConsoleColor.Red, false);
                }
            }

            Text.TextingLine("\n==================================================", ConsoleColor.White, false);
        }

        static void ShowBattleScreen(Player player, List<Monster> monsters)
        {
            Text.TextingLine("==================================================", ConsoleColor.White, false);
            Text.TextingLine("Battle!!", ConsoleColor.Yellow, false);
            Text.TextingLine("==================================================\n", ConsoleColor.White, false);
            // 몬스터 정보 출력
            ShowMonsterInfo(monsters, false);
            Battle_Profile(player);
        }

        static void Battle_Profile(Player player)
        {
            Text.TextingLine("\n[내정보]", ConsoleColor.Yellow, false);
            Console.WriteLine("--------------------------------------------------");

            // 플레이어 정보 (레벨과 이름, 직업)
            Text.TextingLine($"Lv.{player.level} {player.Name}", ConsoleColor.Green, false);
            Text.TextingLine($"HP {player.hp} / {player.maxHp}", ConsoleColor.Red, false);
            Text.TextingLine($"MP {player.mp} / {player.maxMp}\n", ConsoleColor.Blue, false);

            Text.TextingLine("0. 전투 종료\n", ConsoleColor.Red, false);
        }

        static void Battle_Dead(Player player, Monster monster)
        {
            if (!monster.isDead ) { return; }// 이미 죽지 않았다면 보상 X
            Text.TextingLine($"\n{monster.Name}이(가) 쓰러졌습니다.\n", ConsoleColor.White, false);

            // 보상 지급 (경험치 & 골드)
            monster.GrantReward(player, monster);
            //Text.TextingLine($"{monster.exp} Exp를 얻었다!\n", ConsoleColor.White, false);
            //Text.Texting($"{monster.gold}", ConsoleColor.White, false);
            //Text.Texting($" G", ConsoleColor.Yellow, false);
            //Text.TextingLine($"를 얻었다!\n", ConsoleColor.White, false);

            Console.WriteLine("--------------------------------------------------");

            Console.ReadKey();
        }

        static void Battle(Player player, Stage selectedStage, List<Monster> monsters)
        {
            bool isPlayerTurn = true;
            int initGold = player.gold;
            int initExp = player.exp;
            (int initgold,  int initexp, int initLevel) initStatus = (player.gold, player.exp, player.level);

            while (true)
            {
                Console.Clear();
                ShowBattleScreen(player, monsters);

                // ESC 처리: 도망가기
                Text.TextingLine("ESC : 도망가기");
                int? input = Text.GetInputMulti(true, "1. 공격", "2. 스킬");

                if (input == null) // ESC 입력 시 마을로 복귀
                {
                    Console.WriteLine("무사히 전투에서 도망쳤다...");
                    Thread.Sleep(500);
                    sceneType = SceneType.Home;
                    Console.Clear();
                    return;
                }

                // 플레이어 턴 진행
                bool playerActionSuccess = ExecutePlayerTurn(player, monsters, input.Value);
                if (!playerActionSuccess) continue; // 플레이어 행동 실패 시 다시 입력받음

                // 적의 턴 진행
                EnemyPhase(player, monsters);

                // 플레이어가 사망하면 전투 종료
                if (player.isDead || monsters.All(m => m.isDead))
                {
                    Battle_Result(player, selectedStage ,monsters, initStatus);
                    Thread.Sleep(500);
                    return;
                }
            }
        }
        static bool ExecutePlayerTurn(Player player, List<Monster> monsters, int action)
        {
            
            while (true) // 올바른 입력을 받을 때까지 반복
            {
                Console.Clear();
                ShowBattleScreen(player, monsters);

                if (action == 1) // 공격
                {
                    Console.WriteLine("취소 : ESC\n");
                    Console.WriteLine("대상을 선택해주세요.\n");
                    List<Monster> selectedMonster = Text.GetInputMulti(1, monsters);

                    if (selectedMonster == null) return false; // ESC 입력 시 턴 유지
                    Monster monster = selectedMonster[0];

                    if (monster.isDead)
                    {
                        Console.WriteLine("이미 쓰러진 몬스터 입니다.");
                        Console.ReadKey();
                        continue;
                    }

                    // 공격 실행
                    player.Attack(monster);
                    Console.WriteLine("--------------------------------------------------");
                    Console.ReadKey();

                    if (monster.isDead)
                    {
                        Battle_Dead(player, monster);
                    }

                    return true; // 플레이어 턴 종료
                }
                else if (action == 2) // 스킬
                {
                    Console.Clear();
                    ShowBattleScreen(player, monsters);

                    // 스킬 사용 전 몬스터의 생존 저장
                    List<Monster> beforDead = monsters.Where(m => m.isDead).ToList();
                    
                    // 스킬 사용
                    bool skillUsed = player.skill.Use(player, monsters);
                    if (!skillUsed) return false; // 스킬 사용 취소 시 다시 선택하도록 처리

                    // 스킬 사용 후, 죽은 몬스터 처리 (여러 마리 가능)
                    foreach (var monster in monsters)
                    {
                        if (monster.isDead && !beforDead.Contains(monster))
                        {
                            Battle_Dead(player, monster);
                        }
                    }
                    return true; // 스킬 사용 성공 시 턴 종료
                }
            }
        }

        // 적의 턴
        static void EnemyPhase(Player player, List<Monster> monsters)
        {
            // 전투 시작
            Random random = new Random();
            List<Monster> aliveMonsters = monsters.Where(x => !x.isDead).ToList();

            if (aliveMonsters.Count > 0)
            {
                Monster attackingMonster = aliveMonsters[random.Next(aliveMonsters.Count)];
                int dmg = attackingMonster.atk;
                Console.WriteLine("\n\n==================>>적의 차례<<==================\n");


                foreach (Monster monster in aliveMonsters)
                {
                    monster.Attack(player);
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");

                }
            }
            Console.ReadKey();
        }


        // 전투 결과
        static void Battle_Result(Player player, Stage selectedStage, List<Monster> monsters, (int g, int e, int l) p )
        {
            Console.Clear();
            // 전투 결과
            Text.TextingLine("==================================================", ConsoleColor.White, false);
            Text.TextingLine("Battle!! - Result", ConsoleColor.Yellow, false);
            Text.TextingLine("==================================================\n", ConsoleColor.White, false);
            

            // 이겼을 경우 - Victory, You Lose
            if (!player.isDead && monsters.All(m => m.isDead))
            {
                Text.TextingLine("Victory\n", ConsoleColor.Yellow, false);
                int monsterCount = monsters.Count;
                int damageTaken = player.maxHp - player.hp;

                int totalExpGained = 0;

                // 몬스터 처치 경험치 및 골드 획득
                foreach (var monster in monsters)
                {
                    int expGained = monster.exp;
                    totalExpGained += expGained;
                }

                Console.WriteLine($"던전에서 몬스터 {monsterCount}마리를 잡았습니다!\n");
                Text.Texting("던전에서 몬스터 ", ConsoleColor.White, false);
                Text.Texting($"{monsterCount}", ConsoleColor.Cyan, false);
                Text.TextingLine("마리를 잡았습니다!\n", ConsoleColor.White, false);

                Text.Texting($"Lv. {player.level}", ConsoleColor.DarkYellow, false); 
                Text.TextingLine($" {player.Name}", ConsoleColor.White, false);

                Text.Texting($"HP ", ConsoleColor.Red, false);
                Text.TextingLine($"{player.maxHp} -> {player.hp}", ConsoleColor.White, false);

                Console.WriteLine("\n--------------------------------------------------\n");

                // 스테이지 보상 지급 및 목록 출력
                Console.WriteLine("\n[클리어 보상]\n");
                selectedStage.ClearReward(player);

                Console.WriteLine("\n--------------------------------------------------\n");

                Console.WriteLine("[획득한 보상]\n");
                // 획득한 레벨 출력
                Text.Texting($"Lv. ", ConsoleColor.Yellow, false);
                Text.Texting($"{p.l} -> ", ConsoleColor.White, false);
                Text.TextingLine($"{player.level}", ConsoleColor.Yellow, false);

                // 획득한 경험치 출력
                Text.Texting($"Exp. ", ConsoleColor.DarkCyan, false);
                Text.Texting($"{p.e} -> ", ConsoleColor.White, false);
                Text.TextingLine($"{player.exp}", ConsoleColor.DarkCyan, false);

                // 골드 보상 출력
                Text.Texting($"Gold. ", ConsoleColor.DarkYellow, false);
                Text.Texting($"{p.g} -> ", ConsoleColor.White, false);
                Text.TextingLine($"{player.gold}", ConsoleColor.DarkYellow, false);

            }
            else if (player.isDead)
            {
                Console.WriteLine("You Lose\n");

                Console.WriteLine("\n던전에서 패배했습니다. 다시 도전하세요!\n");

                Console.WriteLine($"Lv. {player.level} {player.Name}");
                Console.WriteLine($"HP {player.maxHp} -> {player.hp}");

                // 패배 시 체력 일부 회복
                player.hp += 10;
            }
            Console.WriteLine("\n0. 마을로 ");
            GetInput(0, 0);

            sceneType = SceneType.Home;
            Console.Clear();
            return;
        }

        public static int GetInput(int min, int max)
        {
            while (true)
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.\n>> ");
                if (int.TryParse(Console.ReadLine(), out var input)
                    && (input >= min) && (input <= max))
                { return input; }
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
            }
        }
    }
}
