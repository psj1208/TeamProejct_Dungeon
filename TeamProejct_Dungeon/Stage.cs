﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{
    public static class StageClear
    {
        public static void ClearOne()
        {
            //플레이어한테 보상 주는 ~~ 게임매니저 접근해도되는 것.
        }
    }

    // 스테이지 데이터베이스
    public static class StageDB
    {
        //for문 돌려서 0~4,5~9,10~14 이렇게 순서대로 넣으셔도 되고. 임의로 넣으셔고 되고. 수정 가하셔도됩니다.
        //예시
        public static List<Stage> StageList = new List<Stage>
        {
            new Stage(MonsterDB.GetMonsters(),StageClear.ClearOne)

        };
    }

    public class Stage
    {
        List<Monster> monsters;
        Action ClearStage;

        public Stage(List<Monster> m, Action a)
        {
            monsters = m;
            ClearStage = a;
        }
    }
}
