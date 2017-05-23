using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GDIgame
{
    class Player : GameObject
    {
        public Dungeon dungeon;
        //player is one of the only objects that exists at dungeon level, not room level
        public Player()
        {
            sprite = Image.FromFile("Player.bmp");
        }

        public void MoveUp()
        {
            if (y==0)
            {
                dungeon.current = dungeon.current.upRoom;
                y = 18;
            }
            else if (dungeon.current.walls[x, y - 1] == false)
                {
                y -= 1;
                }
        }
        public void MoveDown()
        {
            if (y == 19)
            {
                dungeon.current = dungeon.current.downRoom;
                y = 1;
            }
            else if (dungeon.current.walls[x, y + 1] == false)
            {
                y += 1;
            }
        }
        public void MoveLeft()
        {
            if (x == 0)
            {
                dungeon.current = dungeon.current.leftRoom;
                x = 18;
            }
            else if (dungeon.current.walls[x-1, y] == false)
            {
                x -= 1;
            }
        }
        public void MoveRight()
        {
            if (x == 19)
            {
                dungeon.current = dungeon.current.leftRoom;
                x = 1;
            }
            else if (dungeon.current.walls[x + 1, y] == false)
            {
                x += 1;
            }
        }

    }
}
