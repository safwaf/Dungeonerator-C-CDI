using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GDIgame
{
    class Room
    {

        static Image wallSprite = Image.FromFile("Wall.bmp");
        public static int width = 20;       //make these even!
        public static int height = 20;
        public bool[,] walls = new bool[width, height];
        //public bool upDoor, leftDoor, rightDoor, downDoor = false;
        public Room upRoom, leftRoom, rightRoom, downRoom;
        public List<GameObject> Objects = new List<GameObject>();
        public Random R = Dungeon.R;
        public Player player;

        public Room()
        {
            //this never gets overridden. for custom room code, use CreateRoom
            BasicWalls();
            CreateRoom();
        }

        public virtual void CreateRoom()
        {   
            //This gets overridden in specific room types. All that should be different between room types is the setup code
        }

        public void DrawRoom(Graphics g)
        {
            foreach (GameObject o in Objects)
            {
                o.Draw(g);
            }
            DrawWalls(g);
        }

        public void DrawWalls(Graphics g)
        {
            Point p = new Point();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (walls[x, y])
                    {
                        p.X = x * 32;
                        p.Y = y * 32;
                        g.DrawImage(wallSprite, p);
                    }
                }
            }
        }

        public void UpdateRoom()
        {
            foreach (GameObject o in Objects)
            {
                o.Step();
            }
        }

        public void Create(GameObject o,int x, int y)
        {
            //pass a new object into this function to get it set up in one command.
            Objects.Add(o);
            o.myRoom = this;
            o.x = x;
            o.y = y;
            o.Create();
            
        }

        public void Create(GameObject o)
        {
            Objects.Add(o);
            o.myRoom = this;
            o.Create();
        }

        void BasicWalls()   //this method frames the room with walls. every room will call this method
        {
            for (int x = 0; x < (width - 1) / 2; x++)
            {
                walls[0, x] = true;
                walls[x, 0] = true;
                walls[19 - x, 0] = true;
                walls[0, 19 - x] = true;
                walls[19, x] = true;
                walls[x, 19] = true;
                walls[19 - x, 19] = true;
                walls[19, 19 - x] = true;
            }
        }

        public void PlaceDoors()
        {
            if (walls[0,9] == false)
            {
                Create(new LockedDoor(), 0, 9);
                Create(new LockedDoor(), 0, 10);
            }
            if (walls[19, 9] == false)
            {
                Create(new LockedDoor(), 19, 9);
                Create(new LockedDoor(), 19, 10);
            }
            if (walls[9, 0] == false)
            {
                Create(new LockedDoor(), 9, 0);
                Create(new LockedDoor(), 10, 0);
            }
            if (walls[0, 9] == false)
            {
                Create(new LockedDoor(), 9, 0);
                Create(new LockedDoor(), 10, 0);
            }
        }

    }
}
