using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GDIgame
{
    enum direction {right, up, left, down};

    class GameObject
    {
        public int x;
        public int y;
        public Image sprite;
        public Room myRoom;//we use myRoom to access the other objects in the room

        public GameObject()
        {

        }
        public void LinkRoom(Room r)
        {
            myRoom = r;
        }
        public virtual void Step()
        {
            //override this with whatever code takes place each time the board updates
        }
        public virtual void Draw(Graphics g)
        {
            //if necessary put extra drawing code here. by default, draws sprite
            g.DrawImage(sprite, new Point(x * 32, y * 32));
        }
    }

    class LockedDoor : GameObject
    {
        public static new Image sprite = Image.FromFile("LockedDoor.bmp");
    }

    class Enemy: GameObject
    {

    }

    class Switch : GameObject
    {
        public static new Image sprite = Image.FromFile("Button.bmp");
    }
    class PacmanGhost : Enemy
    {
        public static new Image sprite = Image.FromFile("Ghost.bmp");


        public override void Step()
        {
            
        }
    }
}
