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
        public virtual void Create()
        {
            //this gets called when an object is created. 
        }
        public virtual void Step()
        {
            //override this with whatever code takes place each time the board updates
        }
        public virtual void Draw(Graphics g)
        {
            //if necessary put extra drawing code here. by default, draws sprite
            if (sprite != null) g.DrawImage(sprite, new Point(x * 32, y * 32));
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
                    d.active = false;
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
        public bool active = true;
    }

    class Enemy: GameObject
    {

    }

    class Switch : GameObject
    {
        public static new Image sprite = Image.FromFile("Button_Up.bmp");
        public static Image sprite2 = Image.FromFile("Button_Down.bmp");
        public bool pressed = false;

        public override void Step()
        {
            if (myRoom.player.x == x && myRoom.player.y == y) pressed = true;
        }

        public override void Draw(Graphics g)
        {
            if (pressed) g.DrawImage(sprite2, new Point(x * 32, y * 32));
            else g.DrawImage(sprite, new Point(x * 32, y * 32));
        }
    }


    class PacmanGhost : Enemy
    {
        //later, we will diversify this into different colours of ghost who use different movement strategies

        public static Image mySprite = Image.FromFile("Ghost.bmp");
        public direction dir;   //try to set the direction when creating, so it makes sense

        public override void Create()
        {
            sprite = PacmanGhost.mySprite;
            if (x==9)
            {
                if (y == 9) dir = direction.left;
                else dir = direction.down;
            }
            else
            {
                if (y == 9) dir = direction.up;
                else dir = direction.right;
            }
        }

        public override void Step()
        {
            if (CheckMove(dir))
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

        public bool CheckMove(direction d)
        {
            switch (d)
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

        public void ReverseDirection()
        {
            switch (dir)
            {
                case direction.up:
                    dir = direction.down;
                    break;
                case direction.down:
                    dir = direction.up;
                    break;
                case direction.left:
                    dir = direction.right;
                    break;
                case direction.right:
                    dir = direction.left;
                    break;
            }
        }

        public void NewDirection()
        {
            if (dir == direction.up || dir == direction.down)
            {
                //pick a direction between left and right
                if (Dungeon.R.Next(2) == 1)
                {
                    //check left first
                    if (CheckMove(direction.left)) dir = direction.left;
                    else
                    {
                        if (CheckMove(direction.right)) dir = direction.right;
                        else ReverseDirection();    //we can always go back the way we came
                    }


                }
                else
                {
                    //check right first
                    if (CheckMove(direction.right)) dir = direction.right;
                    else
                    {
                        if (CheckMove(direction.left)) dir = direction.left;
                        else ReverseDirection();    //we can always go back the way we came
                    }
                }
            }
            else
            {
                if (Dungeon.R.Next(2) == 1)

                {
                    //check up first
                    if (CheckMove(direction.up)) dir = direction.up;
                    else
                    {
                        if (CheckMove(direction.down)) dir = direction.down;
                        else ReverseDirection();    //we can always go back the way we came
                    }


                }
                else
                {
                    //check down first
                    if (CheckMove(direction.down)) dir = direction.down;
                    else
                    {
                        if (CheckMove(direction.up)) dir = direction.up;
                        else ReverseDirection();    //we can always go back the way we came
                    }
                }
            }
        }
    }
}
