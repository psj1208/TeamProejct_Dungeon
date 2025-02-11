using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{

    class QuestScene
    {

        public QuestScene()
        {
            QuestDisplay();
        }

        public void QuestDisplay()
        {
            Console.Clear();
            Text.TextingLine("Quest!!", ConsoleColor.Yellow, false);


        }

    }

    abstract class Quest
    {
        public string questName { get; set; }
        public string questDescription { get; set; }

        public string QuestType { get; set; }
        public abstract void Reward();
        public abstract void SimpleDescription();
        public bool isProgress;

        //마을을 위협하는 미니언 처치

        //이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?

        //미니언 5마리 처치 (0/5)

        // 보상 - 아이템



        // 장비를 장착해보자

        // 자네! 멋진 장비를 들고 있군 장비를 한번 착용해보게

        // 장비 장착해보기.

        // 보상 - 1000G.


        // 더욱 더 강해지기!

        // 자네! 멋진 장비를 들고 있군 장비를 한번 착용해보게

        // 레벨 3 달성

        // 방어력 / 힘 10 영구 증가
    }

    public class Quest1 : Quest
    {
        public Quest1() {
    }



}
