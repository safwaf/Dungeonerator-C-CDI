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

        public static Color Gray(int Value)
        {
            return Color.FromArgb(Value, Value, Value);
        }

        public static Color FromHV(int Hue, int Value)
        {
            //this method creates an RGB color from a hue between 0 and 300 and value between 0 and 255
            //does not currently support graying (lower Saturation values)
            int r, g, b;
            //if we get bad values, try to fix them. Hue is cyclic, so we try to find a good value
            while (Hue > 299) Hue = Hue - 300;
            while (Hue < 0) Hue = Hue + 300;
            //Values less than 0 get set to 0, and greater than 255 get set to 255
            if (Value > 255) Value = 255;
            if (Value < 0) Value = 0;

            //the machanism of this method is to divide the color wheel into 6 double-gradients, which have easy to interpolate mathematical properties.
            if (Hue < 49)//Hue is between red and yellow
            {
                r = 255;
                g = (255 * Hue) / 49;
                b = 0;
            }
            else if (Hue < 99)//Hue is between yellow and green
            {
                Hue = Hue - 50;
                Hue = 49 - Hue;
                r =(255*Hue)/49;
                g = 255;
                b = 0;
            }
            else if (Hue < 149)//Hue is between green and cyan
            {
                Hue = Hue - 100;
                r = 0;
                g = 255;
                b = (255 * Hue) / 49;
            }
            else if (Hue < 199) //Hue is between cyan and blue
            {
                Hue = Hue - 150;
                Hue = 49 - Hue;
                r = 0;
                g = (255 * Hue) / 49;
                b = 255;
            }
            else if (Hue < 249) //Hue is between blue and purple
            {
                Hue = Hue - 200;
                r = (255 * Hue)/ 49;
                g = 0;
                b = 255;

            }
            else //Hue is between purple and red
            {
                Hue = Hue - 250;
                Hue = 49 - Hue; ;
                r = 255;
                g = 0;
                b = (255 * Hue) / 49;
            }

            //Value calculation:
            r = (Value * r) / 255;
            g = (Value * g) / 255;
            b = (Value * b) / 255;

            if (r > 255) r = 255;
            if (r < 0) r = 0;
            if (g > 255) g = 255;
            if (g < 0) g = 0;
            if (b > 255) b = 255;
            if (b < 0) b = 0;

            return Color.FromArgb(r,g,b);
        }

    }
}
