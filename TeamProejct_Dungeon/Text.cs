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
    //문자열을 선택지로 만들기 위함
    public class TextString
    {
        (int x, int y) StartPos_;
        (int x, int y) EndPos_;
        public string str;
        public bool IsSelected;
        bool IsTexting;
        ConsoleColor color;

        public TextString(String s)
        {
            str = s;
            IsSelected = false;
            IsTexting = false;
            color = ConsoleColor.White;
        }

        //클래스 출력
        public void Print()
        {
            if (IsSelected == true)
                color = ConsoleColor.Blue;
            Text.Texting(str, color, false);
            IsTexting = true;
            Console.WriteLine();
        }

        //글자 색깔 바꾸기(현재 선택지)
        public void TurnOn()
        {
            this.color = ConsoleColor.Magenta;
        }

        //글자 색깔 끄기(선택지 변경)
        public void TurnOff()
        {
            if (IsSelected == false)
                this.color = ConsoleColor.White;
        }
        //청소용
        public void SaveStart()
        {
            StartPos_ = Console.GetCursorPosition();
        }
        //청소용22
        public void SaveEnd()
        {
            EndPos_ = Console.GetCursorPosition();
        }
    }
    //몬스터를 선택지로 만들기 위함
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
        //클래스 출력
        public void Print()
        {
            if (IsSelected == true)
                color = ConsoleColor.Blue;
            Text.Texting($"{monster.level} level , {monster.Name}", color, false);
            IsTexting = true;
            Console.WriteLine();
        }
        //글자 색깔 바꾸기(현재 선택지)
        public void TurnOn()
        {
            this.color = ConsoleColor.Magenta;
        }
        //글자 색깔 끄기(선택지 변경)
        public void TurnOff()
        {
            if(IsSelected == false)
                this.color = ConsoleColor.White;
        }
        //청소용
        public void SaveStart()
        {
            StartPos_ = Console.GetCursorPosition();
        }
        //청소용22
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

        //키보드로 선택하기(문자열형)
        public static int? GetInputMulti(bool CanNull, params string[] input)
        {
            //텍스트 제거와 위치 유지를 위한 포지션 변수
            (int x, int y) startPos_;
            (int x, int y) endPos_;
            //배열 중에 현재 선택중인 것.기본 값은 맨 처음 인덱스인 0
            int select = 0;
            //선택지 스타트 위치 저장
            startPos_ = Console.GetCursorPosition();
            //반환용, 선택지로 포함하지 않을 값을 기본 값으로.
            int result = -1;
            List<TextString> tm = new List<TextString>();
            //문자열을 클래스 형식으로 변환 후 배열에 추가
            foreach (string s in input)
            {
                tm.Add(new TextString(s));
            }
            while (result == -1)
            {
                (int x, int y) s;
                (int x, int y) e;
                int num = 0;
                s = Console.GetCursorPosition();
                Text.TextingLine("-------선택-------", ConsoleColor.Red, false);
                tm[select].TurnOn();
                foreach (TextString t in tm)
                {
                    Text.Texting($"{num + 1} . ", ConsoleColor.White, false);
                    t.Print();
                    num++;
                }
                ConsoleKeyInfo keyinfo = Console.ReadKey(true);
                //Esc를 누를 시 Null반납(매개 변수 값에 따라 다름)
                //키보드 위아래 방향키를 누를 시 배열의 길이를 벗어나지 않는 선에서 +와 -가 이뤄진다.
                //select와 같은 인덱스 값을 배열의 클래스를 Turnon. 현재 선택지가 무엇인지 시각적으로 보여줌.
                //키보드 입력이 이뤄지면 그 전 선택지의 색깔을 끈다.(위,아래)
                //엔터를 누르면 선택지 입력.
                //요구한 수만큼 선택지 배열의 길이가 충족되면 반환.
                switch (keyinfo.Key)
                {
                    case ConsoleKey.Escape:
                        if(CanNull == true)
                            return null;
                        break;
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
                        result = select;
                        break;
                }
                e = Console.GetCursorPosition();
                if (result != -1)
                    Thread.Sleep(500);
                else
                    Thread.Sleep(10);
                ClearTextBetween(s, e);
            }
            endPos_ = Console.GetCursorPosition();
            ClearTextBetween(startPos_, endPos_);
            return result + 1;
        }
        //키보드로 선택하기(몬스터형)
        public static List<Monster> GetInputMulti(int trynum, List<Monster> monster, bool Cannull = true)
        {
            //텍스트 제거와 위치 유지를 위한 포지션 변수
            (int x, int y) startPos_;
            (int x, int y) endPos_;
            //배열 중에 현재 선택중인 것.기본 값은 맨 처음 인덱스인 0
            int select = 0;
            //선택지 스타트 위치 저장
            startPos_ = Console.GetCursorPosition();
            //반환용
            List<Monster> mons = new List<Monster>();
            List<TextMonster> tm = new List<TextMonster>();
            //몬스터 수가 요구 선택지보다 적다면 그에 맞춤.
            if (monster.Count < trynum)
                trynum = 1;
            //문자열을 클래스 형식으로 변환 후 배열에 추가
            foreach (Monster mon in monster)
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
                //Esc를 누를 시 Null반납(매개 변수 값에 따라 다름)
                //키보드 위아래 방향키를 누를 시 배열의 길이를 벗어나지 않는 선에서 +와 -가 이뤄진다.
                //select와 같은 인덱스 값을 배열의 클래스를 Turnon. 현재 선택지가 무엇인지 시각적으로 보여줌.
                //키보드 입력이 이뤄지면 그 전 선택지의 색깔을 끈다.(위,아래)
                //엔터를 누르면 선택지 입력.
                //요구한 수만큼 선택지 배열의 길이가 충족되면 반환.
                switch (keyinfo.Key)
                {
                    case ConsoleKey.Escape:
                        if(Cannull == true)
                            return null;
                        break;
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
                else
                    Thread.Sleep(10);
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
