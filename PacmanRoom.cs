using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDIgame
{
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
            for (int l = 0; l < 3; l++)  //which layer of walls are we in.
            {
                //corners of layers are always 
                walls[2 + (2 * l), 2 + (2 * l)] = true;
                walls[17 - (2 * l), 2 + (2 * l)] = true;
                walls[2 + (2 * l), 17 - (2 * l)] = true;
                walls[17 - (2 * l), 17 - (2 * l)] = true;
                int r1 = R.Next(7 - (2 * l)) + 1;

                for (int x = 1; x < 8 - (2 * l); x++)
                {
                    if (x != r1)
                    {
                        walls[2 + (2 * l) + x, 2 + (2 * l)] = true;
                        walls[2 + (2 * l), 2 + (2 * l) + x] = true;
                        walls[17 - (2 * l), 2 + (2 * l) + x] = true;
                        walls[17 - (2 * l) - x, 2 + (2 * l)] = true;
                        walls[2 + (2 * l), 17 - (2 * l) - x] = true;
                        walls[2 + (2 * l) + x, 17 - (2 * l)] = true;
                        walls[17 - (2 * l), 17 - (2 * l) - x] = true;
                        walls[17 - (2 * l) - x, 17 - (2 * l)] = true;
                    }
                }
            }
            //the inner room is special
            walls[8, 8] = true;
            walls[9, 8] = true;
            walls[11, 8] = true;
            walls[8, 10] = true;
            walls[8, 11] = true;
            walls[11, 11] = true;
            walls[11, 9] = true;
            walls[10, 11] = true;


            //the inner layer should contain 4 ghosts, pointing out the hallways
            Create(new PacmanGhost(), 9, 9);
            Create(new PacmanGhost(), 9, 10);
            Create(new PacmanGhost(), 10, 9);
            Create(new PacmanGhost(), 10, 10);
            //place the switches

        }
    }
    class PacmanControl : Control
    {

    }
}
