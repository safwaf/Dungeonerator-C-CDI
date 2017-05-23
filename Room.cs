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

        Image wallSprite = Image.FromFile("Wall.bmp");
        public static int width = 20;       //make these even!
        public static int height = 20;
        public bool[,] walls = new bool[width, height];
        //public bool upDoor, leftDoor, rightDoor, downDoor = false;
        public Room upRoom, leftRoom, rightRoom, downRoom;
        public List<GameObject> Objects = new List<GameObject>();
        public Random R = Dungeon.R;

        public Room()
        {
            //this never gets overridden. for custom room code, use CreateRoom
            BasicWalls();
            CreateRoom();
            LinkObjects();
        }

        public virtual void CreateRoom()
        {   
            //This gets overridden in specific room types. All that should be different between room types is the setup code
        }


        public void LinkObjects()
        {
            foreach (GameObject o in Objects)
            {
                o.LinkRoom(this);
            }
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

    }

    class PacmanRoom : Room
    {
        /*
        The pacman room randomly generates a "Maze" full of switches patrolled by ghosts
        For the doors to open, the player must hit all of the switches without being killed by the ghosts
        In the future, different ghosts will be colour coded to different chasing strategies
        */
        public override void CreateRoom()
        {
            //generate the wall maze
            //we generate mazes using the "paper snowflake" method. each 8th of the map will be symmetrical
            for (int l=0; l<3; l++)  //which layer of walls are we in.
            {
                //corners of layers are always 
                walls[2 + (2 * l), 2 + (2 * l)] = true;
                walls[17 - (2 * l), 2 + (2 * l)] = true;
                walls[2 + (2 * l), 17 - (2 * l)] = true;
                walls[17 - (2 * l), 17 - (2 * l)] = true;
                int r1 = R.Next(7 - (2 * l)) +1;

                for (int x=1;x<8-(2*l);x++)
                {
                    if (x != r1)
                    {
                        walls[2 + (2 * l) + x, 2 + (2 * l)] = true;
                        walls[2 + (2 * l), 2 + (2 * l) + x] = true;
                        walls[17 - (2 * l), 2 + (2 * l)+x] = true;
                        walls[17 - (2 * l)-x, 2 + (2 * l)] = true;
                        walls[2 + (2 * l), 17 - (2 * l)-x] = true;
                        walls[2 + (2 * l)+x, 17 - (2 * l)] = true;
                        walls[17 - (2 * l), 17 - (2 * l)-x] = true;
                        walls[17 - (2 * l)-x, 17 - (2 * l)] = true;
                    }
                }

                //the inner room should contain 4 ghosts


                //place the switches
            }

        }
    }
}
