using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingSmart.Utils;
using System.Xml.XPath;
using ShoppingSmart.Models;

namespace ShoppingSmart.Controllers
{
    public class PascoesCrawler
    {
        ShoppingSmartDAO ssdao = new ShoppingSmartDAO();
        public void Go()
        {
            parseAllPages("http://www.pascoes.co.nz/category/Diamond?c=101");
            parseAllPages("http://www.pascoes.co.nz/category/Gemstones?c=139");
            parseAllPages("http://www.pascoes.co.nz/category/Silver?c=93");
            parseAllPages("http://www.pascoes.co.nz/category/Watches?c=94");
            parseAllPages("http://www.pascoes.co.nz/category/Gold?c=90");
        }
        /// <summary>
        /// finds all of the pages then passes them all seperately to parseThumbnails
        /// </summary>
        /// <param name="url"></param>
        void parseAllPages(String url)
        {

            var paginator=SSUtils.getPageElement(url, "//div[@class='pagination']");
            var numPages = Convert.ToInt32(paginator.XPathSelectElements("*/a").Reverse().Skip(1).First().Value.Trim());
            //call parseThumbnails on all the pages
            for (int i = 1; i <= numPages; i++)
            {
                parseThumbNails(url + "&p=" + i);
            }
        }

        void parseThumbNails(String url)
        {
            //get the products
            var thumbs = SSUtils.getPageElements(url, "//div[substring-before(@class, ' ')='product-item']");

            var prices = thumbs.Select(
                thumb => thumb.XPathSelectElement("*//div[@class='price']").Value.FromPriceFormat()
                    );
            var detailUrls = thumbs.Select(
                thumb => "http://www.pascoes.co.nz" + thumb.XPathSelectElement("a[1]").Attribute("href").Value
                    );
            var smallImgNames = thumbs.Select(
                thumb => thumb.XPathSelectElement("*//a/img").Attribute("src").Value
                    );
            var titles = thumbs.Select(
                thumb => thumb.XPathSelectElement("*//a/img").Attribute("alt").Value
                    ).ToArray();
            var largeImgNames = detailUrls.Select(
                detailurl => SSUtils.getPageElement(detailurl, "//*[@id='productimage']/div[1]/img").Attribute("src").Value
                    ).ToArray();
            //download small images 
            var smallImages = smallImgNames.Select(
                imgName => SSUtils.DownloadImage("http://www.pascoes.co.nz" + imgName)
                    );
            var smallImageHeights = smallImages.Select(
                smallImage => smallImage.Height
                    );
            var smallImageWidths = smallImages.Select(
                smallImage => smallImage.Width
                    );
            var colors = smallImages.Select(
                smallImage => smallImage.getProductsColour()
                    ).ToArray();

        }
        //void parseThumbNails(String url)
        //{
        //    //get the products
        //    var thumbs = SSUtils.getPageElements(url, "//div[substring-before(@class, ' ')='product-item']");

        //    var prices = thumbs.Select(
        //        thumb => thumb.XPathSelectElement("div[@class='price']").Value.FromPriceFormat()
        //            );
        //    var detailUrls = thumbs.Select(
        //        thumb => "http://www.pascoes.co.nz" + thumb.XPathSelectElement("a[1]").Attribute("href").Value
        //            );
        //    var smallImgNames = thumbs.Select(
        //        thumb => thumb.XPathSelectElement("a/img").Attribute("src").Value
        //            );
        //    var titles = thumbs.Select(
        //        thumb => thumb.XPathSelectElement("a/img").Attribute("alt").Value
        //            ).ToArray();
        //    var largeImgNames = detailUrls.Select(
        //        detailurl => SSUtils.getPageElement(detailurl, "//*[@id='productimage']/div[1]/img").Attribute("src").Value
        //            ).ToArray();
        //    //download small images 
        //    var smallImages = smallImgNames.Select(
        //        imgName => SSUtils.DownloadImage("http://www.pascoes.co.nz" + imgName)
        //            );
        //    var smallImageHeights = smallImages.Select(
        //        smallImage => smallImage.Height
        //            );
        //    var smallImageWidths = smallImages.Select(
        //        smallImage => smallImage.Width
        //            );
        //    var colors = smallImages.Select(
        //        smallImage => smallImage.getProductsColour()
        //            ).ToArray();

        //}
    }
}