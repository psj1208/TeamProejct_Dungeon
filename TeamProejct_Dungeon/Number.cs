using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{
    //start이상 end 이하의 정수 배열을 만들어주는 클래스입니다.
    public static class Number
    {
        public static int[] Make(int start, int end)
        {
            int[] ints = new int[end - start + 1];
            for (int i = 0; i < ints.Length; i++)
            {
                ints[i] = start + i;
            }
            return ints;
        }
    }
}
