using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using log4net;
using System.Reflection;

using System.Xml.XPath;
using System.Net;
using System.Xml.Linq;
using System.Data.Linq;
using System.Xml;
using ShoppingSmart.Lib;

namespace ShoppingSmart.Utils
{
    public static class SSUtils
    {
        private static readonly ILog Log = LogManager.GetLogger(
        MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Function to download Image from website and save it to file system
        /// </summary>
        /// <param name="_URL">URL address to download image</param>
        /// <param name="name">name to save the image as (not including filetype but including folder) eg "large/black_dress"</param>
        /// <returns>Image</returns>
        public static Image DownloadImage(string _URL)
        {
            Image _tmpImage = null;

            try
            {
                // Open a connection
                System.Net.HttpWebRequest _HttpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(_URL);

                _HttpWebRequest.AllowWriteStreamBuffering = true;

                //// You can also specify additional header values like the user agent or the referer: (Optional)
                _HttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
                _HttpWebRequest.Referer = "http://www.google.com/";

                // set timeout for 20 seconds (Optional)
                _HttpWebRequest.Timeout = 20000;

                // Request response:
                System.Net.WebResponse _WebResponse = _HttpWebRequest.GetResponse();

                // Open data stream:
                System.IO.Stream _WebStream = _WebResponse.GetResponseStream();

                // convert webstream to image
                _tmpImage = Image.FromStream(_WebStream);
                String format = _tmpImage.RawFormat.ToString();
                // Cleanup
                _WebResponse.Close();
                _WebResponse.Close();
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
                return null;
            }
            

            return _tmpImage;
        }

       

        /// <summary>
        /// gets the name of an image format as a string
        /// </summary>
        /// <param name="format">the image format</param>
        /// <returns>the name of the image format preceeded with a dot eg ".jpg"</returns>
        public static String getName(this ImageFormat format)
        {
            if (format.Equals(ImageFormat.Jpeg))
            {
                return ".jpg";
            }
            else if (format.Equals(ImageFormat.Bmp))
            {
                return ".bmp";
            }
            else if (format.Equals(ImageFormat.Png))
            {
                return ".png";
            }
            else if (format.Equals(ImageFormat.Gif))
            {
                return ".gif";
            }
            else if (format.Equals(ImageFormat.Tiff))
            {
                return ".tiff";
            }
            return null;
        }
        /*
	 * 
	 */
        /// <summary>
        /// gets the first string between start and end
        /// getStringBetween("... a  asdf hello hi  b ...","asdf","hi") returns " hello "
        /// </summary>
        /// <param name="text"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static String getFirstStringBetween(this String text, String start, String end)
        {
            int firstpos = text.IndexOf(start) + start.Length;//olozl
            int endpos = text.IndexOf(end, firstpos);
            if (endpos < 0)
                return text.Substring(firstpos);
            return text.Substring(firstpos, endpos - firstpos);
        }


        /// <summary>
        /// removes all the html tags from a given string of text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static String StripHTML(this String text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
        }

        /// <summary>
        /// removes all the punctuation from a string leaves spaces ^ and numbers probly leaves alot more too
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static String StripPunctuation(this String text)
        {
            return new string(text.Where(c => !char.IsPunctuation(c)).ToArray());
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static String StripInvalidPathChars(this String text)
        {

            var invalidChars = Path.GetInvalidPathChars().ToDictionary(car => car);
            //for some reason this character is not included
            invalidChars.Add('/', '/');
            invalidChars.Add('*', '*');
            return new string(text.Where(c => !invalidChars.ContainsKey(c)).ToArray());
        }
        
        /// <summary>
        /// not implemented
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool hasCorrectSpelling(this String text)
        {

            return false;
        }

        public static XElement getPageElement(String url, String xpath)
        {
            try
            {

                return getPageElements(url, xpath).First();
            }
            catch (InvalidOperationException)// no items
            {
                return null;
            }
        }

        public static IEnumerable<XElement> getPageElements(String url, String xpath)
        {
            return HtmlToXml.getPage(url).XPathSelectElements(xpath);
            
        }

        public static XElement getPage(String url)
        {
            return HtmlToXml.getPage(url);
            
        }
 
        /// <summary>
        /// accepts strings like "$90" or "32.55"
        /// converts to integers like 9000 and 3255 to store in DB
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int FromPriceFormat(this String str)
        {
            return Convert.ToInt32(Convert.ToDouble(str.Replace("$", "").Trim()) * 100);
        }

        public static String ToPriceFormat(this int num)
        {
            return "$ " + Convert.ToDouble(num) / 100;

        }

    }
}