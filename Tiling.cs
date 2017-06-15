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

        public Tiling()
        {
            width = 640;
            height = 640;
        }

        public Tiling(int w,int h)
        {
            width = w;
            height = h;
        }

        public virtual Image DrawTiling()
        {
            //every tiling must be able to draw itself
            //this MUST be overridden
            return Image.FromFile("Wall.bmp");
        }

    }
}
