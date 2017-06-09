using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GDIgame
{
    public class ColorMethods
    {
        //This class is never instantiated
        //ColorMethods holds methods for altering colours that are used in the implementation of color schemes
        //Primarily used at dungeon level during generation
        public static Color SetValue(Color c,int newV)
        {
            //This method alters the RBG values of the color to assign a specfic light value
            //used primarily in shading
            if (newV > 255) newV = 255;

            int oldV = GetHighest(c.R, c.G, c.B);
            int newR = newV * (c.R / oldV);
            int newG = newV * (c.G / oldV);
            int newB = newV * (c.B / oldV);
            return Color.FromArgb(newR,newG,newB);
        }

        static int GetHighest(int r, int g, int b)
        {
            //finds which is largest of r, g, and b. used internally in ColorMethods for scaling
            if (r >= g && r >= b) return r;
            else if (g >= r && g >= b) return g;
            else return b;
        }
    }
}
