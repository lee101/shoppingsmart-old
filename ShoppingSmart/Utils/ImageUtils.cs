using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace ShoppingSmart.Utils
{
    public static class ImageUtils
    {

        public static double borderAmount = 1.0 / 15.0;
        public static double middleAmount = 0.75; //were the middle is (where the product should be)
        public static double backgroundColorAmount = 0.2; //how much background colour to expect in the middle

        public static Colour getProductsColour(this Image img)
        {
            //algorithm looks around the border to find background colour
            //then looks in the midddle to find product
            //keeps in mind that the background colour may be in the middle giving an unbalanced weighting toward the background colour
            /*
             * if borderAmount is how much of the image it will consider as the border each side
             * the border colour is assumed to e the background colour which is used to offset
             * 
             * backGroundColourAmount is 
             * */
            
            using (Bitmap b = new Bitmap(img))
            {
                Color bgColor = getBackgroundColour(b);
                Color productColor = getproductColour(b,bgColor);
                //get  shopping smart color that is most similar to product color
                var watch = Colour.getColours().OrderByDescending(c => getDifferenceTo(c.color, productColor));
                return productColor.getSSColour();
            }

        }

        private static Color getproductColour(Bitmap bmp,Color bgColor)
        {
            int xFrom = (int)((bmp.Width * (1-middleAmount))/2);
            int xTo = (int) (xFrom + (bmp.Width * middleAmount));

            int yFrom = (int)((bmp.Height * (1-middleAmount)) / 2);
            int yTo = (int)(yFrom + (bmp.Height * middleAmount));

            int r = 0;
            int g = 0;
            int b = 0;
            //total number of pixels in centre 
            int totalNumPixels = Math.Abs((xTo - xFrom) * (yTo - yFrom));
            int expectedNumBackgroundPixels = (int)(totalNumPixels * backgroundColorAmount);
            

            for (int x = xFrom; x < xTo; x++)
            {
                for (int y = yFrom; y < yTo; y++)
                {
                    Color clr = bmp.GetPixel(x, y);
                    r += clr.R;
                    g += clr.G;
                    b += clr.B;
                }
            }

            //subtract the expected amount of background colour from r g and b
            int backgroundR = expectedNumBackgroundPixels * bgColor.R;
            int backgroundG = expectedNumBackgroundPixels * bgColor.G;
            int backgroundB = expectedNumBackgroundPixels * bgColor.B;
            r = r - backgroundR;
            g = g - backgroundG;
            b = b - backgroundB;

            //subtract expected background pixels
            totalNumPixels = totalNumPixels - expectedNumBackgroundPixels;

            //average the color and return it
            Color prodColor = Color.FromArgb(r/totalNumPixels, g/totalNumPixels, b/totalNumPixels);
            return prodColor;

        }

        public static Colour getSSColour(this Color a)
        {
            var Hue = a.GetHue();
            var sat = a.GetSaturation();
            var bright = a.GetBrightness();

            // four special case colours white gray black and brown.
            //brown hue=23 - 45 ish
            //may have to add another case for pink as it is similar to red in hue just not in saturation
            if (bright < .36)
            {
                return Colour.BLACK;

            }
            if (bright < .6 && sat > .5 && Hue < 45 && Hue > 23)
            {
                return Colour.BROWN;
            }
            if (sat < .33 && bright < .80)
            {
                return Colour.GRAY;
            }
            if (sat < .27)
            {
                return Colour.WHITE;
            }
            return Colour.getBoldColours().OrderBy(c => getDifferenceTo(c.color, a)).First();

        }

        public static int getDifferenceTo(this Color a, Color b)
        {
            var HueDif = (int)Math.Abs(a.GetHue() - b.GetHue());
            var sat = (int)Math.Abs(a.GetSaturation() - b.GetSaturation())*3;
            var bright = (int)Math.Abs(a.GetBrightness() - b.GetBrightness())*3;
            
            //int redDiff = (int)Math.Abs(a.R - b.R);
            //int BlueDiff = (int)Math.Abs(a.B - b.B);
            //int GreenDiff = (int)Math.Abs(a.G - b.G);
            return HueDif + sat + bright;

        }
        delegate void getTotalRGBOfRect();
        private static Color getBackgroundColour(Bitmap bmp)
        {
            //simple average algorithm takes average of the pixels in the border to be the background colour
            int r = 0;
            int g = 0;
            int b = 0;
            var totalNumPixels = 0;

            int borderWidth=(int)(borderAmount * bmp.Width);
            int borderHeight=(int)(borderAmount * bmp.Height);
            //top rectangle
            int xFrom = 0;
            int xTo = bmp.Width;
            int yFrom = 0;
            int yTo = borderHeight;
            
            totalNumPixels = xTo * yTo; //num pixels = area of first rect
            //func gets the total rgb of a current rectangle
            getTotalRGBOfRect func = delegate()
            {
                for (int x = xFrom; x < xTo; x++)
                {
                    for (int y = yFrom; y < yTo; y++)
                    {
                        Color clr = bmp.GetPixel(x, y);
                        r += clr.R;
                        g += clr.G;
                        b += clr.B;
                    }
                }
            };

            func();
            // bottom rectangle
            xFrom = 0;
            xTo = borderWidth;
            yFrom = bmp.Height - borderHeight;
             yTo = bmp.Height;

            totalNumPixels += Math.Abs((xTo-xFrom) * (yTo-yFrom)); //num pixels 
            func();
            //left rect
             xFrom = 0;
             xTo =  borderWidth;
             yFrom = borderHeight;
             yTo =  bmp.Height - borderHeight;

             totalNumPixels += Math.Abs((xTo - xFrom) * (yTo - yFrom)); //num pixels 
            func();
            //right rect
            xFrom = bmp.Width - borderWidth;
             xTo = bmp.Width;
             yFrom = borderHeight;
             yTo = bmp.Height - borderHeight;

             totalNumPixels += Math.Abs((xTo - xFrom) * (yTo - yFrom)); //num pixels 
            func();

            Color bgColor = Color.FromArgb(r / totalNumPixels, g / totalNumPixels, b / totalNumPixels);
            return bgColor;
        }


        public static byte[] getBytes(this Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Gif);
            return ms.ToArray();
        }
    }
}