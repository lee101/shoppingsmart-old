using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ShoppingSmart.Utils
{
    public class Colour
    {
        public static Colour RED = new Colour(1, "Red", Color.FromArgb(204, 0, 0));

        public static Colour ORANGE = new Colour(2, "Orange",Color.FromArgb(251,148,11));

        public static Colour YELLOW = new Colour(3, "Yellow", Color.FromArgb(255, 255, 0));

        public static Colour GREEN = new Colour(4, "Green", Color.FromArgb(0, 204, 0));

        public static Colour AQUA = new Colour(5, "Aqua", Color.FromArgb(3, 192, 198));

        public static Colour BLUE = new Colour(6, "Blue", Color.FromArgb(0, 0, 255));

        public static Colour PURPLE = new Colour(7, "Purple", Color.FromArgb(146, 71, 200));

        public static Colour PINK = new Colour(8, "Pink", Color.FromArgb(246, 155, 192));

        public static Colour WHITE = new Colour(9, "White", Color.FromArgb(245, 245, 245));

        public static Colour GRAY = new Colour(10, "Gray", Color.FromArgb(153, 153, 153));

        public static Colour BLACK = new Colour(11, "Black", Color.FromArgb(10, 10, 10));

        public static Colour BROWN = new Colour(12, "Brown", Color.FromArgb(136, 84, 24));



        public int id;
        public string name;
        public Color color;

        public Colour(int id, string name, Color color)
        {
            this.id = id;
            this.name = name;
            this.color = color;
        }


        public static Colour[] getColours()
        {
            return new Colour[]{
                RED,
                ORANGE,
                YELLOW,
                GREEN,
                AQUA,
                BLUE,
                PURPLE,
                PINK,
                WHITE,
                GRAY,
                BLACK,
                BROWN
            };
        }

        public static Colour[] getBoldColours()
        {
            return new Colour[]{
                RED,
                ORANGE,
                YELLOW,
                GREEN,
                AQUA,
                BLUE,
                PURPLE,
                PINK
            };
        }
    }
}
