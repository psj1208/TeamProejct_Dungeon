﻿using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Linq;

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


        // 김태겸
        // Battle_Init
        static void GameStart()
        {
            //여기에 게임 흐름
            // 플레이어와 몬스터 리스트 생성
            Player player = new Player();
            List<Monster> mons = MonsterSpawn();

            while (true)
            {
                //로비
                if (sceneType == SceneType.Lobby)
                {
                    string input_name = Text.GetInput("캐릭터의 이름을 입력해주세요.");
                    int input_job = Text.GetInput("플레이어의 직업을 선택해주세요\n\n1 . 전사\n\n2 . 도적\n\n", 1, 2);
                    if (input_job == 1)
                    {
                        player = new Player(input_name, Job.Warrior);
                    }
                    else
                    {
                        player = new Player(input_name, Job.Assassin);
                    }
                    Text.TextingLine($"이름 : {player.Name} , 직업 : {player.job} 캐릭터가 생성되었습니다.", ConsoleColor.Green);
                    Thread.Sleep(500);
                    Text.TextingLine($"\n\n잠시 후 마을에 입장합니다.", ConsoleColor.Green);
                    sceneType = SceneType.Home;
                    Thread.Sleep(500);
                    Console.Clear();
                }
                //마을
                else if (sceneType == SceneType.Home)
                {
                    Text.TextingLine("------------------------마을-------------------------", ConsoleColor.Magenta, true);
                    Text.TextingLine("\n\n1 . 상태 보기\n\n2 . 인벤토리\n\n3 . 상점\n\n4 . 던전\n\n5. 세이브\n", ConsoleColor.Green, false);
                    int input = Text.GetInput(null, 1, 2, 3, 4, 5);
                    switch (input)
                    {
                        case 1:
                            //플레이어 스탯 보기
                            Console.Clear();
                            player.StatusDisplay();
                            Console.Clear();
                            break;
                        case 2:
                            //플레이어 인벤토리(장착하는 기능)
                            player.inven.UsingInventory();
                            break;
                        case 3:
                            //상점 이동
                            break;
                        case 4:
                            //던전 이동
                            //테스트 코드 시작
                            int input_ = StageDB.ShowStageList();
                            Console.WriteLine(input_);
                            Thread.Sleep(500);
                            //테스트 코드 끝
                            sceneType = SceneType.Dungeon;
                            break;
                        case 5:
                            //세이브 기능
                            break;
                        default:
                            break;
                    };
                }
                //던전
                else if (sceneType == SceneType.Dungeon)
                {
                    // 전투 시작
                    Battle(player);
                }
            }
        }

        static void ShowMonsterInfo(List<Monster> monsters, bool Shownumber = false)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                Monster monster = monsters[i];
                string status = monster.isDead ? "Dead" : $"HP {monster.hp}/{monster.maxHp}";
                Console.ForegroundColor = monster.isDead ? ConsoleColor.DarkGray : ConsoleColor.White;
                if (Shownumber == true)
                {
                    Console.WriteLine($"{i + 1}.Lv.{monsters[i].level} {monsters[i].Name} HP {monsters[i].hp} ");
                }
                else
                {
                    Console.WriteLine($"Lv.{monsters[i].level} {monsters[i].Name} HP {monsters[i].hp} ");
                }
            }
        }

        // 몬스터 랜덤 스폰
        static List<Monster> MonsterSpawn()
        {
            Random random = new Random();
            List<Monster> monsterList = new List<Monster>();

            // 1~4마리의 몬스터가 랜덤하게 등장
            int monsterCount = random.Next(1, 5);

            for (int i = 0; i < monsterCount; i++)
            {
                monsterList.Add(Monster.GetRandomMonster());
            }
            return monsterList;
        }

        static void ShowBattleScreen(Player player, List<Monster> monsters)
        {
            Text.TextingLine("Battle!!\n", ConsoleColor.Yellow, false);
            // 몬스터 정보 출력
            ShowMonsterInfo(monsters, false);
            Battle_Profile(player);
        }

        static void Battle_Profile(Player player)
        {
            Text.TextingLine("\n[내정보]", ConsoleColor.Yellow, false);
            Console.WriteLine("---------------------------------------------");

            // 플레이어 정보 (레벨과 이름, 직업)
            Text.TextingLine($"Lv.{player.level} {player.Name}", ConsoleColor.Green, false);
            Text.TextingLine($"HP {player.hp} / {player.maxHp}", ConsoleColor.Red, false);
            Text.TextingLine($"HP {player.mp} / {player.maxMp}\n", ConsoleColor.Blue, false);


            Text.TextingLine("1. 공격", ConsoleColor.Cyan, false);
            Text.TextingLine("2. 스킬\n", ConsoleColor.Cyan, false);

            Text.TextingLine("0. 전투 종료\n", ConsoleColor.Red, false);
            
        }

        static void Battle_Dead(Player player, Monster monster)
        {
            monster.GrantReward(player);

            Text.TextingLine($"\n{monster.Name}이(가) 쓰러졌습니다.\n", ConsoleColor.White, false);
            Text.TextingLine($"{monster.exp} Exp를 얻었다!\n", ConsoleColor.White, false);
            Text.Texting($"{monster.gold}", ConsoleColor.White, false);
            Text.Texting($" G", ConsoleColor.Yellow, false);
            Text.TextingLine($"를 얻었다!\n", ConsoleColor.White, false);

            Console.WriteLine("---------------------------------------------");

            Console.ReadKey();
        }


        static void Battle(Player player)
        {
            List<Monster> monsters = MonsterSpawn();
            bool isPlayerTurn = true;

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

                //// 몬스터가 모두 쓰러지면 전투 종료
                //if (monsters.All(m => m.isDead))
                //{
                //    Battle_Result(player, monsters);
                //    return;
                //}

                // 적의 턴 진행
                EnemyPhase(player, monsters);

                // 플레이어가 사망하면 전투 종료
                if (player.isDead || monsters.All(m => m.isDead))
                {
                    Battle_Result(player, monsters);
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
                    Console.WriteLine("---------------------------------------------");
                    Console.ReadKey();

                    if (monster.isDead)
                    {
                        monster.GrantReward(player);
                        Battle_Dead(player, monster);
                    }

                    return true; // 플레이어 턴 종료
                }
                else if (action == 2) // 스킬
                {

                    Console.Clear();
                    Console.WriteLine("Battle!!\n");

                    // 몬스터 정보 다시 출력
                    ShowMonsterInfo(monsters);

                    Console.ResetColor();

                    Console.WriteLine("\n[내정보]");
                    Console.WriteLine($"Lv.{player.level} {player.Name}");
                    Console.WriteLine($"HP {player.hp} / {player.maxHp}");
                    //bool형식으로 선언해서 esc 누르면 null값받아오는게 멀티 메소드인데. null값을 if문으로 구분해서. false를 돌려받고.

                    bool skillUsed = player.skill.Use(player, monsters);
                    if (!skillUsed) return false; // 스킬 사용 취소 시 다시 선택하도록 처리

                    return true; // 스킬 사용 성공 시 턴 종료
                }
            }
        }


        static void EnemyPhase(Player player, List<Monster> monsters)
        {
            // 전투 시작
            Random random = new Random();
            List<Monster> aliveMonsters = monsters.Where(x => !x.isDead).ToList();

            if (aliveMonsters.Count > 0)
            {
                Monster attackingMonster = aliveMonsters[random.Next(aliveMonsters.Count)];
                int dmg = attackingMonster.atk;
                Console.WriteLine("\n\n-------------------적의 차례------------------\n");

                foreach (Monster monster in aliveMonsters)
                {
                    monster.Attack(player);
                    Console.WriteLine("---------^------------------------^----------");
                }
            }
            Console.ReadKey();
        }


        // 전투 결과
        static void Battle_Result(Player player, List<Monster> monsters)
        {
            Console.Clear();
            // 전투 시작
            Console.WriteLine("Battle!! - Result\n");

            // 이겼을 경우 - Victory, You Lose
            if (!player.isDead && monsters.All(m => m.isDead))
            {
                Console.WriteLine("Victory\n");
                int monsterCount = monsters.Count;
                int damageTaken = player.maxHp - player.hp;

                Console.WriteLine($"던전에서 몬스터 {monsterCount}마리를 잡았습니다!\n");
                Console.WriteLine($"Lv. {player.level} {player.Name}");
                Console.WriteLine($"HP {player.maxHp} -> {player.hp}");
            }
            else if (player.isDead)
            {
                Console.WriteLine("You Lose\n");

                Console.WriteLine("\n던전에서 패배했습니다. 다시 도전하세요!\n");

                Console.WriteLine($"Lv. {player.level} {player.Name}");
                Console.WriteLine($"HP {player.maxHp} -> {player.hp}");
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
