using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProejct_Dungeon
{
    public interface ICharacter
    {
        string Name { get; set; }
        int level { get; set; }
        int hp { get; set; }
        int exp {  get; set; }
        int gold {  get; set; }
        void Attack(int damge);
        void TakeDamage(int damge);
    }
}
