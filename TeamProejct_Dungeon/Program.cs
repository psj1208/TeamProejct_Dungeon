using System;
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


        // 전투 시작
        static void Battle(Player player)
        {
            List<Monster> monsters = MonsterSpawn();
            while (true)
            {
                // 전투 시작
                Console.Clear();
                Console.WriteLine("Battle!!\n");

                bool isPlayerTurn = true;
                Random random = new Random();

                // 몬스터 정보 출력
                ShowMonsterInfo(monsters, false);

                Console.ResetColor();
                Console.WriteLine("\n");

                Console.WriteLine("[내정보]");

                // 플레이어 정보 (레벨과 이름, 직업)
                Console.WriteLine($"Lv.{player.level} {player.Name}");
                Console.WriteLine($"HP {player.hp} / {player.maxHp}\n");
     
                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 스킬\n");
                Console.WriteLine("0. 전투 종료\n");

                int input = Text.GetInput(null, 0, 1, 2);

                if (input == 0)
                {
                    Console.WriteLine("전투를 종료합니다...");
                    Thread.Sleep(500);
                    sceneType = SceneType.Home;
                    Console.Clear();
                    //break;
                    return;
                }

                else if (input == 1)
                {
                    while (true) // 올바른 입력을 받을 때까지 반복
                    {
                        Console.Clear();
                        Console.WriteLine("Battle!!\n");

                        // 몬스터 정보 다시 출력
                        ShowMonsterInfo(monsters);

                        Console.ResetColor();

                        Console.WriteLine("\n[내정보]");
                        Console.WriteLine($"Lv.{player.level} {player.Name}");
                        Console.WriteLine($"HP {player.hp} / {player.maxHp}");
                        if (isPlayerTurn == true)
                        {
                            Console.WriteLine("\n취소 : ESC\n");

                            Console.WriteLine("대상을 선택해주세요.\n");
                            List<Monster> selectMonster = Text.GetInputMulti(1, monsters);
                            Monster monster = selectMonster[0];

                            if (monster.isDead)
                            {
                                Console.WriteLine("이미 쓰러진 몬스터 입니다.");
                                Console.ReadKey();
                                continue;
                            }

                            // 공격 처리
                            player.Attack(monster);
                            Console.WriteLine("---------------------------------------------");
                            Console.ReadKey();

                            if (monster.isDead)
                            {
                                monster.GrantReward(player);
                                //Text.TextingLine(원하는 문자열, 색깔, true or false 이거는 텍스트가 순차적으로 생길지 말지)
                                //줄 안 띄우는건 Text.Texting
                                Console.WriteLine($"{monster.Name}이(가) 쓰러졌습니다.\n");
                                Text.TextingLine($"{monster.Name}이(가) 쓰러졌습니다.\n", ConsoleColor.White, true);
                                Console.WriteLine($"{monster.exp} Exp를 얻었다!\n");
                                Console.WriteLine($"{monster.gold} G를 얻었다!\n");
                                Console.WriteLine("---------------------------------------------");

                                Console.ReadKey();
                            }
                            isPlayerTurn = !isPlayerTurn;
                        }
                        else
                        {
                            EnemyPhase(player, monsters);
                            isPlayerTurn = !isPlayerTurn;
                        }
                        if (player.isDead || monsters.All(m => m.isDead))
                        {
                            Battle_Result(player, monsters);
                            Thread.Sleep(500);
                            return;
                        }
                    }

                }
                else if (input == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Battle!!\n");

                    // 몬스터 정보 다시 출력
                    ShowMonsterInfo(monsters);

                    Console.ResetColor();

                    Console.WriteLine("\n[내정보]");
                    Console.WriteLine($"Lv.{player.level} {player.Name}");
                    Console.WriteLine($"HP {player.hp} / {player.maxHp}");

                    player.skill.Use(player, monsters);
                  
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
