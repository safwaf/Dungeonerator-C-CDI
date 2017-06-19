using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GDIgame
{
    class Tiling
    {
        //in Dungeonerator, we would like to have a variety of options for tiling the floor of a dungeon

        public int width;   //width in pixels of the tiling. By default 640
        public int height;  //height in pixels of the tiling. By default 640
        public int tilewidth;   //width of tile in pixels. By default 8;
        public int tileheight;  //height of the tile in pixels. By default 8;
        private int twidth;     //width of the tiling in tiles
        private int theight;    //height of the tiling in tiles
        public Tiling()
        {
            width = 640;
            height = 640;
            tilewidth = 8;
            tileheight = 8;
            twidth = width / tilewidth;
            theight = height / tileheight;
        }

        public Tiling(int w,int h)
        {
            width = w;
            height = h;
            tilewidth = 8;
            tileheight = 8;
            twidth = width / tilewidth;
            theight = height / tileheight;
        }
        public Tiling(int w, int h,int tw,int th)
        {
            width = w;
            height = h;
            tilewidth = tw;
            tileheight = th;
            twidth = width / tilewidth;
            theight = height / tileheight;
        }


        public virtual Image DrawTiling()
        {
            //every tiling must be able to draw itself
            //this MUST be overridden
            return Image.FromFile("Wall.bmp");
        }

    }

    class TriominoTiling : Tiling
    {
        public override Image DrawTiling()
        {
            Image floor = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(floor);
            bool[,]tiled = new bool[width / tilewidth, height / tileheight];
            

            return floor;
        }
    }

    class TriTile
    {
        static Image LeftRight = new Bitmap("Triomino/Tile_LeftRight.bmp");
        static Image UpDown = new Bitmap("Triomino/Tile_UpDown.bmp");
        static Image UpRight = new Bitmap("Triomino/Tile_UpRight.bmp");
        static Image UpLeft = new Bitmap("Triomino/Tile_UpLeft.bmp");
        static Image DownRight = new Bitmap("Triomino/Tile_DownRight.bmp");
        static Image DownLeft = new Bitmap("Triomino/Tile_DownLeft.bmp");
        static Image Left = new Bitmap("Triomino/Tile_Left.bmp");
        static Image Right = new Bitmap("Triomino/Tile_Right.bmp");
        static Image Up = new Bitmap("Triomino/Tile_Up.bmp");
        static Image Down = new Bitmap("Triomino/Tile_Down.bmp");
        static Image Single = new Bitmap("Triomino/Tile_Single.bmp");
        //tiles are named according to the open direction they connect to other tiles from

        int[] x;
        int[] y;

        TriTile()
        {
            x = new int[3];
            y = new int[3];
        }

        TriTile(int x0, int y0, int x1, int y1, int x2, int y2)
        {
            x = new int[3];
            y = new int[3];
            x[0] = x0;
            x[1] = x1;
            x[2] = x2;
            y[0] = y0;
            y[1] = y1;
            y[2] = y2;
        }

        public void Draw(Graphics g)
        {
            if (x[0]==x[1]&&x[0]==x[2]) //Up-Down line block
            {
                if (y[0]<y[1])
                {
                    if (y[0] < y[2])
                    {
                        //y[0] is the highest point
                        g.DrawImage(Down, x[0], y[0]);
                        g.DrawImage(UpDown, x[0], y[0]+1);
                        g.DrawImage(Up, x[0], y[0]+2);
                    }
                    else
                    {
                        //y[2] is the highest point
                        g.DrawImage(Down, x[2], y[2]);
                        g.DrawImage(UpDown, x[2], y[2] + 1);
                        g.DrawImage(Up, x[2], y[2] + 2);
                    }
                }
                else
                {
                    if (y[1] < y[2])
                    {
                        //y[1] is the highest point
                        g.DrawImage(Down, x[1], y[1]);
                        g.DrawImage(UpDown, x[1], y[1] + 1);
                        g.DrawImage(Up, x[1], y[1] + 2);
                    }
                    else
                    {
                        //y[2] is the highest point
                        g.DrawImage(Down, x[2], y[2]);
                        g.DrawImage(UpDown, x[2], y[2] + 1);
                        g.DrawImage(Up, x[2], y[2] + 2);
                    }
                }
                //no need to check other possibilities
                return;                
            }
            if (y[0]==y[1]&&y[0]==y[2]) //Left-Right line block
            {
                if (x[0] < x[1])
                {
                    if (x[0] < x[2])
                    {
                        //x[0] is the leftmost point
                        g.DrawImage(Right, x[0], y[0]);
                        g.DrawImage(LeftRight, x[0] + 1, y[0]);
                        g.DrawImage(Left, x[0] + 2, y[0]);
                    }
                    else
                    {
                        //x[2] is the leftmost point
                        g.DrawImage(Right, x[2], y[2]);
                        g.DrawImage(LeftRight, x[2] + 1, y[2]);
                        g.DrawImage(Left, x[2] + 2, y[2]);
                    }
                }
                else
                {
                    if (x[1] < x[2])
                    {
                        //x[1] is the leftmost point
                        g.DrawImage(Right, x[1], y[1]);
                        g.DrawImage(LeftRight, x[1] + 1, y[1]);
                        g.DrawImage(Left, x[1] + 2, y[1]);
                    }
                    else
                    {
                        //x[2] is the leftmost point
                        g.DrawImage(Right, x[2], y[2]);
                        g.DrawImage(LeftRight, x[2] + 1, y[2]);
                        g.DrawImage(Left, x[2] + 2, y[2]);
                    }
                }
                //we don't need to check anything else
                return;
            }

        }
    }
}
