using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GDIgame
{
    class LightsOutRoom:Room
    {
        LightsOutBoard board = new LightsOutBoard();
        //this room is a variation of the classic "Lights Out" puzzle
        //it generates an 8-by-8 array of tiles and creates a random, solvable puzzle out of them

        public override void CreateRoom()
        {
            Create(board);
            LightsOutPanel current;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    current = new LightsOutPanel();
                    current.bx = i;
                    current.by = j;
                    current.board = board;
                    Create(current);
                    if (i < 4) current.x = 2 * i + 2;
                    else current.x = 2 * i + 3;
                    if (j < 4) current.y = 2 * j + 2;
                    else current.y = 2 * j + 3;

                }
            }

        }

    }

    class LightsOutPanel:GameObject
    {
        public int bx, by; //these are relative coordinates, specifying the position in the game board
        public static new Image sprite = Image.FromFile("Panel_Off.bmp");
        public static Image sprite2 = Image.FromFile("Panel_On.bmp");
        public bool pressed = false;
        public LightsOutBoard board;

        public override void Step()
        {

        }

        public override void Draw(Graphics g)
        {
            if (pressed) g.DrawImage(sprite2, new Point(x * 32, y * 32));
            else g.DrawImage(sprite, new Point(x * 32, y * 32));
        }
    }

    class LightsOutBoard:Control
    {
        bool[,] board = new bool[8, 8];

        public void PerformMove(int x, int y)
        {

        }

    }
}
