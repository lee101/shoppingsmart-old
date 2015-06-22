using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.XPath;
using ShoppingSmart.Models;
using ShoppingSmart.Utils;

namespace ShoppingSmart.Controllers
{
    public class WarehouseCrawler
    {


        public WebClient client = new WebClient();


        /// <summary>
        /// starts the parser
        /// </summary>
        ShoppingSmartDAO SSDAO = new ShoppingSmartDAO();
        public void Go()
        {
            SSDAO.deleteAllProductsForCompany(Company.THE_WAREHOUSE);
            //women
            SSDAO.setRequiredCategories(new Categorie[] { Categorie.WOMANS });
            parseAllPages("http://www.thewarehouse.co.nz/red/catalog/clothing/women");
            //men
            SSDAO.setRequiredCategories(new Categorie[] { Categorie.MENS });
            parseAllPages("http://www.thewarehouse.co.nz/red/catalog/clothing/men");
            SSDAO.setAvailableCategories(Categorie.getJewelryCategories());
            SSDAO.setRequiredCategories(new Categorie[0]);
            parseAllPages("http://www.thewarehouse.co.nz/red/catalog/jewellery/bangles-bracelets");
            parseAllPages("http://www.thewarehouse.co.nz/red/catalog/jewellery/branded-watches");
        }


        void parseAllPages(String url)
        {

            var pagenums =SSUtils.getPage(url);
            IEnumerable<int> pagerange;
            int lastPageNum;
            XElement finalel;
            try
            {
                finalel = pagenums.XPathSelectElements("//p[@class='nav-pages']/a").Reverse().First();
                lastPageNum = Convert.ToInt32(finalel.Value);

                pagerange = Enumerable.Range(0, lastPageNum);
            }
            catch (NullReferenceException)//if there are no pages at all
            {
                
                parseThumbNails(url);//just parse this one page
                return;
            }

            ///go through all thumbs for all pages

            foreach (int i in pagerange)
            {
                String urltoget = Regex.Replace(finalel.Attribute("href").Value, @"PageNumber=[0-9]*", "PageNumber=" + i);
                parseThumbNails(urltoget);
            }

        }
        void parseThumbNails(String url)
        {

            //get thumbs for page

            var thumb = SSUtils.getPage(url);//,"//div[@class='thumbnail']");
            //go through all thumbs 
            /*
             * //div[@class="thumbnail"]//p[@class='price']/span.innerhtml <- price
             * 
             * //div[@class="thumbnail"]//div[@class='product']/a <- main info about product
             * //div[@class="thumbnail"]//div[@class='product']/a <- the title attributes value is title
             * //div[@class="thumbnail"]//div[@class='product']/a <- the baseUrl+(href attributes value) is url
             * //div[@class="thumbnail"]//div[@class='product']/a/img <- src should be downloaded is small image
             * visit the url and get more info
             * 
             * 
             * */

            var prices = thumb.XPathSelectElements("//div[@class='thumbnail']//p[@class='price']").Select(
                sp => sp.Value.StripHTML().FromPriceFormat()
                    ).ToArray();
            var links = thumb.XPathSelectElements("//div[@class='product']/a[1]");
            //var titles = (from link in links
            //              select link.Attribute("title").Value).ToArray();
            var titles = links.Select(
                link=> link.Attribute("title").Value
                ).ToArray();

            var urls = (from att in links
                        select "http://www.thewarehouse.co.nz" + att.Attribute("href").Value).ToArray();


            var smallimgUrls = (from imgat in links
                                select "http://www.thewarehouse.co.nz" + imgat.Element("img").Attribute("src").Value).ToArray();
            List<Image> SmallImages = new List<Image>();
            List<int?> productColours = new List<int?>();
            List<String> smallImageNames = new List<string>();
            List<int> smallImageWidths = new List<int>();
            List<int> smallImageHeights = new List<int>();
            //foreach image in smallimgurls
            for (int i = 0; i < titles.Length; i++)
            {
                Image smallImage = SSUtils.DownloadImage(smallimgUrls[i]);
                SmallImages.Add(smallImage);
                if (smallImage != null)
                {

                    SSDAO.saveSmallImage(smallImage, titles[i]);
                    productColours.Add(ImageUtils.getProductsColour(smallImage).id);
                    smallImageNames.Add((titles[i] + smallImage.RawFormat.getName()).StripInvalidPathChars());
                    smallImageWidths.Add(smallImage.Width);
                    smallImageHeights.Add(smallImage.Height);
                }
                else
                {
                    productColours.Add(null);
                    smallImageNames.Add(null);
                    smallImageWidths.Add(0);
                    smallImageHeights.Add(0);
                }
            }
            

            //now for each url in urls download url and get details about thumb
            //get description
            //get largeimg
            List<String> descriptions = new List<string>();
            List<String> largeimgNames = new List<string>();
            List<int> largeImgHeights = new List<int>();
            List<int> largeImgWidths = new List<int>();
            

            int idx = 0;
            var mylinks = (from productUrl in urls
                           select SSUtils.getPageElement(productUrl, "//div[@id='productInformation']//a[@id='enlargeImageLink']").Attribute("onclick").Value.getFirstStringBetween("'", "', '")).ToArray();


            foreach (String prodUrl in urls)
            {
                
                var page = SSUtils.getPage(prodUrl); //  '//div[@id='productInformation']' xpath
              
                //get large image
                String largeimgUrl = "http://www.thewarehouse.co.nz" + page.XPathSelectElement("//a[@id='enlargeImageLink']").Attribute("onclick").Value.getFirstStringBetween("'", "', '");
                Image largeImg = SSUtils.DownloadImage(largeimgUrl);
                if (largeImg == null)
                {
                    largeimgNames.Add(null);
                    largeImgWidths.Add(0);
                    largeImgHeights.Add(0);

                }
                else
                {
                    SSDAO.saveLargeImage(largeImg, titles[idx]);
                    largeimgNames.Add((titles[idx] + largeImg.RawFormat.getName()).StripInvalidPathChars());
                    largeImgWidths.Add(largeImg.Width);
                    largeImgHeights.Add(largeImg.Height);

                }
                //end get large image

                //get description
                var desc = page.XPathSelectElement("//p[@id='productDescription']");
                if (desc == null)
                {
                    descriptions.Add(null);
                }
                else
                {
                    descriptions.Add(desc.Value.Trim());
                }
                idx++;
            }

            product[] theProducts = new product[titles.Length];
            String[] theProductsKeyWords = new String[titles.Length];
            for (int j = 0; j < titles.Length; j++)
            {
                product aProduct = new product();
                theProductsKeyWords[j] = titles[j] + " " + descriptions[j];
                theProducts[j] = new product
                           {
                               title = titles[j],
                               price = prices[j],
                               description = descriptions[j],
                               url = urls[j],
                               largeimg = largeimgNames[j],
                               smallimg = smallImageNames[j],
                               largeimgheight = largeImgHeights[j],
                               largeimgwidth = largeImgWidths[j],
                               smallimgheight = smallImageHeights[j],
                               smallimgwidth = smallImageWidths[j],//,
                               company=Company.THE_WAREHOUSE.id,
                               colour=productColours[j]
                           };

            }
            SSDAO.save(theProducts, theProductsKeyWords);



        }
    }
}