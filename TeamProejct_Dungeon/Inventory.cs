using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{
    public class Inventory
    {
        List<IItem> items;//인벤이 갖고 있는 아이템.
        void AddItem(IItem item)
        {
            items.Add(item);
        }
    }
}
