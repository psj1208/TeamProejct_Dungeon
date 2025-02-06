using System.Threading;

namespace TeamProejct_Dungeon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameStart();
        }


        // 김태겸
        // Battle_Init
        static void GameStart()
        {
            //여기에 게임 흐름
            // 플레이어와 몬스터 리스트 생성
            ICharacter player = new Player("Chad", "전사", 1, 100, 100);

            List<ICharacter> monsters = new List<ICharacter>
            {
                new Monster("미니언", 2, 15),
                new Monster("대포미니언", 5, 25),
                new Monster("공허충", 3, 10)
            };


            // 전투 시작
            Battle(player, monsters);
        }

        // 전투 시작
        static void Battle(ICharacter player, List<ICharacter> monsters)
        {
            // 전투 시작
            Console.WriteLine("Battle!!\n");

            Random random = new Random();
            for (int i = 0; i < monsters.Count; i++)
            {
                int index = random.Next(0, monsters.Count - 1);
            }
            // 몬스터 정보 받아오기
            for (int i = 0; i < monsters.Count; i++)
            {
                Console.WriteLine($"Lv.{monsters[i].Level} {monsters[i].Name} HP {monsters[i].HP} ");
            }
            Console.WriteLine("\n");

            Console.WriteLine("[내정보]");
            // 플레이어 정보 (레벨과 이름, 직업)
            Console.WriteLine($"Lv.{player.Level} {player.Name}");
            Console.WriteLine($"HP {player.HP}");

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

        // ICharacter 인터페이스
        interface ICharacter
        {
            string Name { get; }
            int Level { get; }
            int HP { get; }
            int MaxHP { get; }
            string Job { get; }
        }

        // 플레이어 클래스
        class Player : ICharacter
        {
            public string Name { get; set; }
            public string Job { get; set; }
            public int Level { get; set; }
            public int HP { get; private set; }
            public int MaxHP { get; set; }

            public Player(string name, string job, int level, int hp, int maxHp)
            {
                Name = name;
                Job = job;
                Level = level;
                HP = hp;
                MaxHP = maxHp;
            }
        }

        // 몬스터 클래스
        class Monster : ICharacter
        {
            public string Name { get; private set; }
            public int Level { get; set; }
            public int HP { get; private set; }
            public int MaxHP => HP;  // 몬스터는 초기 HP를 최대 HP로 설정
            public string Job => "없음";  // 몬스터는 직업이 없음

            public Monster(string name, int level, int hp)
            {
                Name = name;
                Level = level;
                HP = hp;
            }
        }
    }
}
