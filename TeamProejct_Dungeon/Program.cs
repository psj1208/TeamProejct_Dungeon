using System.Runtime.InteropServices;
using System.Threading;

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
            SceneType sceneType = SceneType.Lobby;
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
                else if(sceneType == SceneType.Home)
                {
                    Text.TextingLine("------------------------마을-------------------------", ConsoleColor.Magenta, true);
                    Text.TextingLine("\n\n1 . 상태 보기\n\n2 . 인벤토리\n\n3 . 상점\n\n4 . 던전\n\n5. 세이브\n", ConsoleColor.Green, false);
                    int input = Text.GetInput(null, 1, 2, 3, 4, 5);
                    switch(input)
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
                else if(sceneType == SceneType.Dungeon)
                {
                    List<ICharacter> monsters = new List<ICharacter>
                    {
                        new Monster("미니언", 2, 15),
                        new Monster("대포미니언", 5, 25),
                        new Monster("공허충", 3, 10)
                    };


                    // 전투 시작
                    Battle(player, monsters);
                }
            }
        }

        // 몬스터 랜덤 스폰
        static List<ICharacter> MonsterSpawn()
        {
            Random random = new Random();
            List<ICharacter> monsterList = new List<ICharacter>();

            // 1~4마리의 몬스터가 랜덤하게 등장
            int monsterCount = random.Next(1, 5);

            for (int i = 0; i < monsterCount; i++)
            {
                int monsterType = random.Next(0, 3); // 0~2 중 랜덤 선택

                switch (monsterType)
                {
                    case 0:
                        monsterList.Add(new Monster("미니언", 2, 15));
                        break;
                    case 1:
                        monsterList.Add(new Monster("대포미니언", 5, 25));
                        break;
                    case 2:
                        monsterList.Add(new Monster("공허충", 3, 10));
                        break;
                }
            }
            return monsterList;
        }


        // 전투 시작
        static void Battle(Player player, List<ICharacter> monsters)
        {
            while (true)
            {
                Battle_Start(player, monsters);
                int input = GetInput(0, 1);
                if (input == 1)
                {
                    // 플레이어 공격
                }
            }
        }

        static void Battle_Start(Player player, List<ICharacter> monsters)
        {
            while (true)
            {
                // 전투 시작
                Console.Clear();
                Console.WriteLine("Battle!!\n");

                Random random = new Random();

                // 몬스터 정보 출력
                for (int i = 0; i < monsters.Count; i++)
                {
                    Console.WriteLine($"Lv.{monsters[i].level} {monsters[i].Name} HP {monsters[i].hp} ");
                }
                Console.WriteLine("\n");

                Console.WriteLine("[내정보]");
                // 플레이어 정보 (레벨과 이름, 직업)
                Console.WriteLine($"Lv.{player.level} {player.Name}");
                Console.WriteLine($"HP {player.maxHp}");

                Console.WriteLine();
                Console.WriteLine("1. 공격\n");
            }
        }

        static void Victory()
        {

        }

        static void Lose()
        {

        }

        // 전투 결과
        static void Battle_Result(ICharacter player, ICharacter monster)
        {
            // 전투 시작
            Console.WriteLine("Battle!! - Result\n");

            // 이겼을 경우 - Victory, You Lose

            Console.WriteLine("0. 다음\n");
            Console.WriteLine(">> ");

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

        class Monster : ICharacter
        {
            public string Name { get; set; }
            public int level { get; set; }
            public int hp { get; set; }

            public int exp { get; set; } = 0;  // 몬스터는 경험치를 가지지 않음
            public int gold { get; set; } = 0; // 몬스터는 골드를 가지지 않음

            public string Job => "없음";  // 몬스터는 직업이 없음

            public Monster(string name, int level, int hp)
            {
                Name = name;
                this.level = level;
                this.hp = hp;
            }

            public void Attack()
            {
                Console.WriteLine("구현 중");
            }

            public void TakeDamage(int damage)
            {
                Console.WriteLine("구현 중");
            }
        }
    }
}
