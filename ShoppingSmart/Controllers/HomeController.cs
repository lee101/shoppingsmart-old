using System;
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Linq;
using log4net;
using ShoppingSmart.Lib;
using ShoppingSmart.Models;
using ShoppingSmart.Utils;

namespace ShoppingSmart.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(
MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// gets a int array from a comma seperated string of numbers
        /// returns null if its not well formed
        /// </summary>
        /// <param name="jsString"></param>
        /// <returns></returns>
        public int[] getFromString(String jsString)
        {
            try
            {
                return jsString.Split(',').Select(s => Convert.ToInt32(s)).ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// home page
        /// </summary>
        /// <param name="query"></param>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <param name="categorieIds"></param>
        /// <param name="companieIds"></param>
        /// <param name="productsPerPage"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult Index(String query = null, String minPrice = "0", String maxPrice = "1000000", String categorieIds = null, String companieIds = null, String colours = null, int productsPerPage = 20, int pageNum = 1,int sorting = 1)
        {
            //sorting ==2 then price asc sorting== 3 price desc
            //resolve categorie and company ids
            int mnPrice;
            try
            {
                mnPrice = minPrice.FromPriceFormat();
            }
            catch (Exception)
            {
                mnPrice = 0;
            }
            if (maxPrice.Equals(""))
                maxPrice = "1000000";
            ThumbsModel model=new ThumbsModel
            {
                query=query,
                minPrice = mnPrice,
                maxPrice = maxPrice.FromPriceFormat(),
                categories = getFromString(categorieIds),
                companies = getFromString(companieIds),
                colours = getFromString(colours),
                productsPerPage = productsPerPage,
                pageNum=pageNum,
                sorting = sorting
            };
            ViewBag.Message = "home";
            
            
            ShoppingSmartDAO ssdao = new ShoppingSmartDAO();
            ssdao.populateThumbsModel(model);
            //var products = ssdao.getProducts(out numPages, "", 1000, 4000,  new int[]{Categorie.MENS.id} , null, 40, 1);
            //ViewBag.numPages = numPages;
            //ViewBag.products = products;
            
            
            return View(model);
        }

        
        public ActionResult tests()
        {

            //string page = HtmlToXml.GetUrl("http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20%28%22MSFT%22%29&diagnostics=true", "dj0yJmk9VFpHMUg2azFPNHdNJmQ9WVdrOVoxWkxNa1JJTldFbWNHbzlNek0wTVRZNE16WXkmcz1jb25zdW1lcnNlY3JldCZ4PWMx", "dfc920a6cf029cb7b024a3084a3d0719732887da");
            XElement pages= HtmlToXml.getPage("http://www.thewarehouse.co.nz/red/");
            var a = SSUtils.FromPriceFormat("$65");
            //new PascoesCrawler().Go();
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            ViewBag.Message = Server.MapPath("~\\lib\\en_GB.aff");
            //test strip punctuaction
            var x = "asdf ;'[],./90--67% asdf ^*&*()^%#@ asdf".StripPunctuation();
            //test StripInvalidPathChars
            var path = "asdf,./lk;'zxcvjl;sfguiopweqrt7891025467089-!@#$$%&^&*()_+\"|<>*;:".StripInvalidPathChars();

            ShoppingSmartDAO ssdao = new ShoppingSmartDAO();
            //int numPages;
            //var asdf = ssdao.getProducts(out numPages, "", 1000, 4000, new Categorie[] { Categorie.MENS }, null, 40, 1);

            SSUtils.DownloadImage("http://www.thewarehouse.co.nz/is-bin/intershop.static/WFS/TWL-B2C-Site/TWL-B2C/en_NZ/product/large/2000001735909_l.jpg");

            //log4net.Config.XmlConfigurator.Configure();

            //log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController));

            Log.Info("asdf");
            Log.Warn("omg");
            //SSUtils.getPageElements("http://www.pascoes.co.nz/category/Diamond?c=101", "//div[substring-before(@class, ' ')='product-item']");
            //var aasfdasfdasdf=SSUtils.getPageElement("http://www.pascoes.co.nz/product/9ct-diamond-twist-ring/?i=340", "//*[@id=\"productimage\"]/div[1]/img").Attribute("src").Value;
            //test the save method
            //ShoppingSmartDAO ssDAO = new ShoppingSmartDAO();
            //product product = new product();
            //product.description = "asdf asdf asdf ";
            //product.largeimg = "image does go here";
            //ssDAO.save(product, "this thingy is a mens shoes");

            return View();
        }

        public ActionResult recreateDatabase()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            ShoppingSmartDAO.recreateDatabase();
            return View();
        }

        public WebClient client = new WebClient();

        public ActionResult Parser()//starts parser for now
        {

            ////testing product model
            //ProductModel prrr = new ProductModel();
            //prrr.Description = "example description";
            //prrr.Title = "asdf title asdf";
            //prrr.Url = "http://adfasdfadf";
            //prrr.Price = 5;
            //prrr.Cats = new List<string>();
            //prrr.Cats.Add("BAGs");
            //prrr.Cats.Add("hats");
            //prrr.Cats.Add("tieS");
            //prrr.Cats.Add("tS");
            //prrr.save();

            ////
            
            /* parse all pages
            String pagedata = client.DownloadString("http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20html%20where%20url%3D%22http%3A%2F%2Fwww.thewarehouse.co.nz%2Fred%2Fcatalog%2Fclothing%2Fwomen%22%20and%20xpath%3D'%2F%2Fp%5B%40class%3D%22nav-pages%22%5D%2Fa'&diagnostics=true");

            XElement pagenums = XElement.Parse(pagedata);

            var finalel = pagenums.XPathSelectElements("//a").Reverse().Take(1).ToArray()[0];

            var pagerange = Enumerable.Range(0,Convert.ToInt32(finalel.Value));
            
            
            ///go through all thumbs for all pages

            foreach (int i in pagerange)
            {
                String urltoget = Regex.Replace(finalel.Attribute("href").Value,@"PageNumber=[0-9]*", "PageNumber=" + i);
                String newurl ="http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20html%20where%20url%3D%22"+HttpUtility.UrlEncode(urltoget)+"%22%20and%20xpath%3D'%2F%2Fdiv%5B%40class%3D%22thumbnail%22%5D'&diagnostics=true";
            }
            */
            
            //String thumbdata = client.DownloadString("http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20html%20where%20url%3D%22http%3A%2F%2Fwww.thewarehouse.co.nz%2Fred%2Fcatalog%2Fclothing%2Fwomen%22%20and%20xpath%3D'%2F%2Fdiv%5B%40class%3D%22thumbnail%22%5D'&diagnostics=true");
            ////get thumbs for page
            //XElement thumbs=XElement.Parse(thumbdata);
            ////go through all thumbs 
            ///*
            // * //div[@class="thumbnail"]//p[@class='price']/span.innerhtml <- price
            // * 
            // * //div[@class="thumbnail"]//div[@class='product']/a <- main info about product
            // * //div[@class="thumbnail"]//div[@class='product']/a <- the title attributes value is title
            // * //div[@class="thumbnail"]//div[@class='product']/a <- the baseUrl+(href attributes value) is url
            // * //div[@class="thumbnail"]//div[@class='product']/a/img[1] <- src should be downloaded is small image
            // * visit the url and get more info
            // * 
            // * 
            // * */
            
            //var prices = from el in thumbs.XPathSelectElements("//div[@class='thumbnail']//p[@class='price']/span")
            //             select Convert.ToInt32(Convert.ToDouble(el.Value.Replace("$","").Trim())*100);
            //var links = thumbs.XPathSelectElements("//div[@class='thumbnail']//div[@class='product']/a");

            //var titles = from att in links.Attributes("title")
            //             select att.Value;

            //var urls = from att in links.Attributes("href")
            //           select "http://www.thewarehouse.co.nz" + att.Value;
            //var smallimgUrls= from imgat in links
            //                  select "http://www.thewarehouse.co.nz" + imgat.XPathSelectElement("//img[1]").Attribute("src").Value;

            //test the save method
            ShoppingSmartDAO ssDAO = new ShoppingSmartDAO();
            product product = new product();
            product.description = "asdf asdf asdf ";
            product.largeimg = "image does go here";
            ssDAO.save(product, "this thingy is a mens shoes");

            return View();
        }


        public ActionResult RealParser()//starts parser 
        {
            new ShoppingSmartDAO().deleteAllImages();
            new WarehouseCrawler().Go();


            return View();
        }
    }
}
