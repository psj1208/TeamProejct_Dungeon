using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeamProejct_Dungeon
{
    public class TextMonster
    {
        (int x, int y) StartPos_;
        (int x, int y) EndPos_;
        public Monster monster;
        public bool IsSelected;
        bool IsTexting;
        ConsoleColor color;

        public TextMonster(Monster mon)
        {
            monster = mon;
            IsSelected = false;
            IsTexting = false;
            color = ConsoleColor.White;
        }

        public void Print()
        {
            if (IsSelected == true)
                color = ConsoleColor.Blue;
            Text.Texting($"{monster.level} level , {monster.Name}", color, false);
            IsTexting = true;
            Console.WriteLine();
        }

        public void TurnOn()
        {
            this.color = ConsoleColor.Magenta;
        }

        public void TurnOff()
        {
            if(IsSelected == false)
                this.color = ConsoleColor.White;
        }
        public void SaveStart()
        {
            StartPos_ = Console.GetCursorPosition();
        }

        public void SaveEnd()
        {
            EndPos_ = Console.GetCursorPosition();
        }
    }
    static class Text
    {
        //커서 위치 저장을 위한 변수
        static (int x, int y) Startpos;
        static (int x, int y) Endpos;

        public static List<Monster> GetInputMulti(int trynum, List<Monster> monster)
        {
            (int x, int y) startPos_;
            (int x, int y) endPos_;
            int select = 0;
            startPos_ = Console.GetCursorPosition();
            List<Monster> mons = new List<Monster>();
            List<TextMonster> tm = new List<TextMonster>();
            if (monster.Count < trynum)
                trynum = 1;
            foreach(Monster mon in monster)
            {
                tm.Add(new TextMonster(mon));
            }
            while(mons.Count != trynum)
            {
                (int x, int y) s;
                (int x, int y) e;
                int num = 0;
                s = Console.GetCursorPosition();
                Text.TextingLine("-------선택-------", ConsoleColor.Red, false);
                tm[select].TurnOn();
                foreach (TextMonster t in tm)
                {
                    Text.Texting($"{num + 1} . ", ConsoleColor.White, false);
                    t.Print();
                    num++;
                }
                ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                switch (keyinfo.Key)
                {
                    case ConsoleKey.Escape:
                        return null;
                    case ConsoleKey.DownArrow:
                        if (select < tm.Count - 1)
                        {
                            tm[select].TurnOff();
                            select++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (select > 0)
                        {
                            tm[select].TurnOff();
                            select--;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (tm[select].IsSelected == false)
                        {
                            mons.Add(tm[select].monster);
                            tm[select].IsSelected = true;
                        }
                        break;
                }
                e = Console.GetCursorPosition();
                if (mons.Count == trynum)
                    Thread.Sleep(500);
                ClearTextBetween(s, e);
            }
            endPos_ = Console.GetCursorPosition();
            ClearTextBetween(startPos_, endPos_);
            return mons;
        }
        //텍스트 효과(다음 줄로 넘어감, 텍스트 색깔 지정과 텍스트 바로 나오게 하기)
        public static void TextingLine(string text, ConsoleColor color = ConsoleColor.White, bool InterTime = true)
        {
            Console.ForegroundColor = color;
            foreach (char ch in text)
            {
                Console.Write(ch);
                if (InterTime == true)
                    Thread.Sleep(20);
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        //텍스트 효과(다음 줄로 안 넘어감)
        public static void Texting(string text, ConsoleColor color = ConsoleColor.White, bool InterTime = true)
        {
            Console.ForegroundColor = color;
            foreach (char ch in text)
            {
                Console.Write(ch);
                if (InterTime == true)
                    Thread.Sleep(20);
            }
            Console.ResetColor();
        }

        //게임 시작 시 이름 받아오기. 아래와 같은 베이스로 매개변수만 다름.
        public static string GetInput(string starttext = null)
        {
            SaveStartPos();
            if (!String.IsNullOrEmpty(starttext))
                TextingLine(starttext, ConsoleColor.Green);
            Texting(">>", ConsoleColor.Green);
            SaveEndPos();
            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (String.IsNullOrEmpty(input))
                {
                    ClearTextBetween();
                    SaveStartPos();
                    TextingLine("공백은 사용할 수 없습니다.", ConsoleColor.Red);
                    if (!String.IsNullOrEmpty(starttext))
                        TextingLine(starttext, ConsoleColor.Green);
                    Texting(">>", ConsoleColor.Green);
                    SaveEndPos();
                }
                else
                    break;
            }
            ClearTextBetween();
            return input;
        }

        //플레이어에게 제시하는 행동 선택지를 함수로 만듬.(Main문을 간결하게 만들어준다)
        public static int GetInput(string starttext = null, params int[] selectgroup)
        {
            SaveStartPos();
            if (!String.IsNullOrEmpty(starttext))
                TextingLine(starttext, ConsoleColor.Green);
            Texting(">>", ConsoleColor.Green);
            SaveEndPos();
            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (int.TryParse(input, out int result) && !String.IsNullOrEmpty(input) && selectgroup.Any(n => n == int.Parse(input)))
                    break;
                else
                {
                    ClearTextBetween();
                    SaveStartPos();
                    TextingLine("선택지 내의 번호를 입력해주세요.", ConsoleColor.Red);
                    if (!String.IsNullOrEmpty(starttext))
                        TextingLine(starttext, ConsoleColor.Green);
                    Texting(">>", ConsoleColor.Green);
                    SaveEndPos();
                }
            }
            ClearTextBetween();
            Console.WriteLine();
            return int.Parse(input);
        }


        //텍스트 청소를 위한 커서 위치 저장(시작)
        public static void SaveStartPos()
        {
            Startpos = Console.GetCursorPosition();
        }

        //텍스트 청소를 위한 커서 위치 저장(끝)
        public static void SaveEndPos()
        {
            Endpos = Console.GetCursorPosition();
        }

        //텍스트 청소
        public static void ClearTextBetween()
        {
            Console.SetCursorPosition(Startpos.x, Startpos.y);
            for (int i = Startpos.y; i <= Endpos.y; i++)
            {
                Console.Write("                                               ");
                Console.WriteLine(" ");
            }
            Console.SetCursorPosition(Startpos.x, Startpos.y);
        }

        //위치 두개 받아서 청소
        public static void ClearTextBetween((int x, int y) start, (int x, int y) end)
        {
            Console.SetCursorPosition(start.x, start.y);
            for (int i = start.y; i <= end.y; i++)
            {
                Console.Write("                                                ");
                Console.WriteLine();
            }
            Console.SetCursorPosition(start.x, start.y);
        }
    }
}
