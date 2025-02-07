﻿using System.Threading;

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

            List<ICharacter> monsters = new List<ICharacter>
            {
                new Monster("미니언", 2, 15),
                new Monster("대포미니언", 5, 25),
                new Monster("공허충", 3, 10)
            };


            // 전투 시작
            Battle(player, monsters);
        }

        //static List<ICharacter> MonsterSpawn()
        //{
        //    List<ICharacter> Monsters = new List<ICharacter>();

        //}

        // 전투 시작
        static void Battle(Player player, List<ICharacter> monsters)
        {
            // 전투 시작
            Console.WriteLine("Battle!!\n");

            Random random = new Random();

            int monsterCount = 0;
            // 몬스터 정보 받아오기
            for (int i = 0; i < monsters.Count; i++)
            {
                int index = random.Next(0, monsters.Count - i);
                Console.WriteLine($"Lv.{monsters[index].level} {monsters[index].Name} HP {monsters[index].hp} ");
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


        // 몬스터 클래스
        class Monster : ICharacter
        {
            public string Name { get; private set; }
            public int level { get; set; }
            public int hp { get; set; }
            
            public string Job => "없음";  // 몬스터는 직업이 없음

            public Monster(string name, int level, int hp)
            {
                Name = name;
                this.level = level;
                this.hp = hp;
            }
        }
    }
}
