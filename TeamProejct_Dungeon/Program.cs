using System.Threading;

namespace TeamProejct_Dungeon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string imagePath = AppDomain.CurrentDomain.BaseDirectory + "\\sample.jpg"; // 이미지 경로(상위 폴더/bin/Debug/net버전안에 넣어야함.)
            AsciiArt.Draw(imagePath, 30);
            GameStart();
        }
        

        // 김태겸
        // Battle_Init
        static void GameStart()
        {
            //여기에 게임 흐름
            // 플레이어와 몬스터 리스트 생성

            Player player = new Player("Chad", Job.Warrior);

            List<ICharacter> monsters = MonsterSpawn();


            // 전투 시작
            Battle(player, monsters);
        }

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
            // 전투 시작
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

            Console.WriteLine("원하시는 행동을 입력해주세요.\n>> ");

        }
        // 공격
        static void Battle_Player(ICharacter player, ICharacter monster)
        {

        }
        // Enemy Pahse 
        static void Battle_Enemy(ICharacter player, ICharacter monster)
        {

        }
        // 전투 결과
        static void Battle_End(ICharacter player, ICharacter monster)
        {

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
