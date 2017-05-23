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

    class Control : GameObject
    {
        //this is the base for control objects.
        //every room type has it's own control object that decides when to open the locked doors
        public override void Step()
        {
            //do not override this, or the doors will never open
            if (CheckCriteria())
            {
                foreach(LockedDoor d in myRoom.Objects)
                {

                }
            }
        }

        public virtual bool CheckCriteria()
        {
            //ALWAYS override this, or the doors will always remain open
            return true;
        }
    }

    class LockedDoor : GameObject
    {
        public static new Image sprite = Image.FromFile("LockedDoor.bmp");
        bool active = true;
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
        //later, we will diversify this into different colours of ghost who use different movement strategies

        public static new Image sprite = Image.FromFile("Ghost.bmp");
        public direction dir;   //try to set the direction when creating, so it makes sense

        public override void Step()
        {
            if (CheckMove())
            {
                //needs improvement. ghost should consider every possible turn, not just the turns it has to make
                MoveForward();
            }
            else
            {
                NewDirection();
                MoveForward();
            }
        }

        public void MoveForward()
        {
            switch (dir)
            {
                case direction.up:
                    y -= 1;
                    break;
                case direction.down:
                    y += 1;
                    break;
                case direction.left:
                    x -= 1;
                    break;
                case direction.right:
                    x += 1;
                    break;
            }
        }

        public bool CheckMove()
        {
            switch (dir)
            {
                case direction.up:
                    if (myRoom.walls[x, y - 1] == false) return true;
                    break;
                case direction.down:
                    if (myRoom.walls[x, y + 1] == false) return true;
                    break;
                case direction.left:
                    if (myRoom.walls[x-1, y] == false) return true;
                    break;
                case direction.right:
                    if (myRoom.walls[x+1,y] == false) return true;
                    break;
            }
            //otherwise, return false
            return false;
        }

        public void NewDirection()
        {

        }
    }
}
