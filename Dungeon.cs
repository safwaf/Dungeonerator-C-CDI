using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GDIgame
{

    
    class Dungeon
    {
        public int Width;
        public int Height;
        public int[,] Layout;
        public List<Room> Rooms = new List<Room>();
        public Room current;
        public Player player = new Player();
        public static Random R = new Random();

        public Dungeon()
        {
            Width = 10;
            Height = 10;
            Layout = new int[Width, Height];
            Setup();
        }

        public void DrawDungeon(Graphics g)
        {
            current.DrawRoom(g);
            player.Draw(g);
        }
        public void Setup()
        {
            PhiDungeon();
            LinkRooms();
            
            foreach (Room r in Rooms)
            {
                r.player = player;
            }
        }

        public void LinkRooms() //This method establishes the door geometry of the dungeon onto the individual rooms
        {
            for (int x = 1; x < 9; x++)
            {
                for (int y = 1; y < 9; y++)
                {
                    if (Layout[x, y] != 0) //is there a room at these coordinates?
                    {
                        if (Layout[x - 1, y] != 0)//is there a room to the left of these coordinates?  
                            Rooms[Layout[x, y]].leftRoom = Rooms[Layout[x - 1, y]];
                        else
                        {
                            Rooms[Layout[x, y]].walls[0, 9] = true;
                            Rooms[Layout[x, y]].walls[0, 10] = true;
                        }
                        if (Layout[x + 1, y] != 0)//is there a room to the right of these coordinates?  
                            Rooms[Layout[x, y]].rightRoom = Rooms[Layout[x + 1, y]];
                        else
                        {
                            Rooms[Layout[x, y]].walls[19, 9] = true;
                            Rooms[Layout[x, y]].walls[19, 10] = true;
                        }
                        if (Layout[x, y - 1] != 0)//is there a room above these coordinates?  
                            Rooms[Layout[x, y]].upRoom = Rooms[Layout[x, y - 1]];
                        else
                        {
                            Rooms[Layout[x, y]].walls[9, 0] = true;
                            Rooms[Layout[x, y]].walls[10, 0] = true;
                        }
                        if (Layout[x, y + 1] != 0)//is there a room below these coordinates?  
                            Rooms[Layout[x, y]].downRoom = Rooms[Layout[x, y + 1]];
                        else
                        {
                            Rooms[Layout[x, y]].walls[9, 19] = true;
                            Rooms[Layout[x, y]].walls[10, 19] = true;
                        }

                    }
                }
            }
        }

        //Algorithms for dungeon creation of various shapes
        public void PhiDungeon()
        {
            //the phi dungeon creates a loop-style dungeon with a turnstyle in the middle
            //the turnstyle changes direction each time you use it
            //first, the player is turned into the loop, then completes the loop
            //the turnstyle will tilt him back to the beginning of the loop and change direction
            //after completing the loop again, the turnstyle turns the player into the terminal wing

            Rooms.Add(new Room()); //currently unused. will be for intro screen at some point

            int roominc = 1;
            int center_x = R.Next(6) + 2;
            int center_y = R.Next(6) + 2;

            //lobby room. This room will have a unique design and is the starting room
            Rooms.Add(new Room());
            Layout[center_x, 9] = roominc;
            current = Rooms[1];
            roominc++;


            //hub room for phi dungeon. in future this room will have a unique design
            Rooms.Add(new Room());
            Layout[center_x, center_y] = roominc;
            roominc++;

            for (int i = center_y + 1; i < 9; i++)    //Bottom wing
            {
                Rooms.Add(new PacmanRoom());
                Layout[center_x, i] = roominc;
                roominc++;
            }

            for (int i = center_y - 1; i > 0; i--)    //three top wings
            {
                Rooms.Add(new Room());
                Layout[1, i] = roominc;
                roominc++;
                Rooms.Add(new Room());
                Layout[center_x, i] = roominc;
                roominc++;
                Rooms.Add(new Room());
                Layout[8, i] = roominc;
                roominc++;
            }
            //if (R.Next(2)==1) //Chirality decision
            {
                //loop on the right side
                for (int i = center_x + 1; i < 9; i++)
                {
                    Rooms.Add(new Room());
                    Layout[i, center_y] = roominc;
                    roominc++;
                    Rooms.Add(new Room());
                    Layout[i, 1] = roominc;
                    roominc++;
                }
                for (int i = center_x - 1; i > 0; i--)
                {
                    Rooms.Add(new Room());
                    Layout[i, center_y] = roominc;
                    roominc++;
                }
            }

            //setup the lobby room
            Rooms[1].upRoom = Rooms[Layout[center_x, 8]];
            Rooms[1].walls[0, 9] = true;
            Rooms[1].walls[0, 10] = true;
            Rooms[1].walls[19, 9] = true;
            Rooms[1].walls[19, 10] = true;
            Rooms[1].walls[9, 19] = true;
            Rooms[1].walls[10, 19] = true;
            //setup the player
            player.x = 10;
            player.y = 10;
            player.dungeon = this;


        }
        public void CastleDungeon()
        {

        }
        public void StairwayDungeon()
        {

        }
    }
}
