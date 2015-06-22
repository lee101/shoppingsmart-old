using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShoppingSmart.Utils;
using System.Drawing;
using System.IO;

namespace ShoppingSmart.Models
{
    public class ShoppingSmartDAO
    {
        private ShoppingSmartDataContext _DataContext = null;
        private Categorie[] AvailableCategories;
        private Categorie[] RequiredCategories;

        public ShoppingSmartDAO()
        {
            AvailableCategories = Categorie.getCategories();
            RequiredCategories = new Categorie[0];
        }
        protected ShoppingSmartDataContext DataContext
        {
            get
            {
                if (_DataContext == null)
                    _DataContext = new ShoppingSmartDataContext();

                //// Eager load Category info
                //var options = new DataLoadOptions();
                //options.LoadWith<Order>(p => p.Customer);
                //_DataContext.LoadOptions = options;

                return _DataContext;
            }
        }

        /// <summary>
        /// saves a product and calculates the categories based on a string of words, looks at each individual word
        /// 
        /// </summary>
        /// <param name="prod">the product to save</param>
        /// <param name="keywords">space seperated words to be used to find corresponding categories</param>
        public void save(product prod, String keywords)
        {
            keywords = keywords.StripPunctuation();
            String[] words = keywords == null ? null : keywords.Split(' ');

            var map = getNameToCategoryMap();
            List<int> categoryIds = new List<int>();
            foreach (String word in words)
            {
                if (map.ContainsKey(word.ToLower()))
                {
                    categoryIds.Add(map[word.ToLower()].id);
                }
            }
            //save prod to DB
            DataContext.products.InsertOnSubmit(prod);
            DataContext.SubmitChanges();//prod now has an id

            //now create list of cats and add them to DB
            List<cat> cats = new List<cat>(categoryIds.Count);
            //add the required categories
            foreach (var categ in RequiredCategories)
            {
                cat item = new cat();
                item.prodid = prod.id;
                item.catid = categ.id;
                cats.Add(item);
            }

            foreach (int catId in categoryIds)
            {
                cat item = new cat();
                item.prodid = prod.id;
                item.catid = catId;
                cats.Add(item);
            }
            //insert all distinct categories
            DataContext.cats.InsertAllOnSubmit(cats.Distinct());
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// when a record is saved these categories will be saved to it allways.
        /// </summary>
        /// <param name="cats"></param>
        public void setRequiredCategories(Categorie[] cats)
        {
            RequiredCategories = cats;
        }

        /// <summary>
        /// map used to map a description of an item to categories based on words in the description
        /// the map mapps keywords to specific categories
        /// </summary>
        /// <returns>the keyword to category map</returns>
        public Dictionary<String, Categorie> getNameToCategoryMap()
        {
            Dictionary<String, Categorie> KWtoCategoryMap = new Dictionary<String, Categorie>();
            foreach (Categorie cat in AvailableCategories)
            {
                foreach (String word in cat.keywords)
                {
                    KWtoCategoryMap.Add(word, cat);
                }
            }
            return KWtoCategoryMap;
        }

        /// <summary>
        /// sets the available categories to look through when saving records
        /// </summary>
        /// <param name="cats"></param>
        public void setAvailableCategories(Categorie[] cats)
        {
            AvailableCategories = cats;
        }


        public static void recreateDatabase()
        {
            ShoppingSmartDataContext DataContext = new ShoppingSmartDataContext();
            DataContext.DeleteDatabase();
            DataContext.CreateDatabase();
        }
        /// <summary>
        /// saves all the products to database
        /// </summary>
        /// <param name="theProducts"></param>
        /// <param name="theProductsKeyWords"></param>
        public void save(IEnumerable<product> theProducts, IEnumerable<String> theProductsKeyWords)
        {
            product[] products = theProducts.ToArray();
            String[] keywords = theProductsKeyWords.ToArray();
            for (int i = 0; i < products.Length; i++)
            {
                save(products[i], keywords[i]);
            }
        }
        /// <summary>
        /// selects products
        /// </summary>
        /// <param name="query"></param>
        /// <param name="minPrice">min price not null</param>
        /// <param name="maxPrice">max price not null</param>
        /// <param name="categories">products that contain all of these null for any</param>
        /// <param name="companies">products that contain any of these</param>
        /// <param name="productsPerPage"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public IEnumerable<product> getProducts(out int numPages, String query, int minPrice, int maxPrice, int[] categories, int[] companies,int[] colours, int productsPerPage, int pageNum,int sorting)
        {
            //company not yet implemented


            //var numPages = numProducts/productsPerPage;
            //var allCompany
            var products = from p in DataContext.products
                           join ct in DataContext.cats on p.id equals ct.prodid into prdCats
                           select new { p, prdCats };
            //var qProducts= from p in products
            //               where p.prdCats.
            //                p.price>=minPrice && p.price<=maxPrice
            if (categories != null)
            {
                foreach (int categoryid in categories)
                {
                    products = products.Where(p => p.prdCats.Any(f => f.catid == categoryid));
                }
            }

            products = products.Where(p => p.p.price >= minPrice && p.p.price <= maxPrice);

            if (companies != null)
            {
                //if any of the companies match the produts company
                products = products.Where(p => companies.Contains(p.p.company ?? -195));
            }
            if (colours != null)
            {
                //if any of the colours match the produts company
                products = products.Where(p => colours.Contains(p.p.colour ?? -195));
            }

            // needed to compute num pages for gui
            var numProducts = products.Count();
            numPages = (int)Math.Ceiling((double)numProducts / (double)productsPerPage); // round up numProducts / productsPerPage to get numpages
            if (sorting == 1)
            {

                products = products
                    .Skip((pageNum - 1) * productsPerPage)
                    .Take(productsPerPage);
            }
            if (sorting == 2)//price asc
            {
                products = products.OrderBy(p=>p.p.price)
                    .Skip((pageNum - 1) * productsPerPage)
                    .Take(productsPerPage);
            }
            if (sorting == 3)//price asc
            {
                products = products.OrderByDescending(p => p.p.price)
                    .Skip((pageNum - 1) * productsPerPage)
                    .Take(productsPerPage);
            }
            return products.Select(p => p.p);

        }
        /// <summary>
        /// uses getProducts to populate feilds in the thumbs model
        /// </summary>
        /// <param name="model"></param>
        public void populateThumbsModel(ThumbsModel m)
        {
            int numPages;
            m.products = getProducts(out numPages, m.query, m.minPrice, m.maxPrice, m.categories, m.companies, m.colours, m.productsPerPage, m.pageNum, m.sorting).ToArray();
            m.numPages = numPages;

        }


        public void deleteAllProductsForCompany(Company company)
        {
            var products = from p in DataContext.products
                           join ct in DataContext.cats on p.id equals ct.prodid into prdCats
                           where p.company == company.id
                           select new { p, prdCats };
            var allCats= products.SelectMany(p=>p.prdCats);
            DataContext.cats.DeleteAllOnSubmit(allCats);
            var allProducts = products.Select(p => p.p);
            DataContext.products.DeleteAllOnSubmit(allProducts);
            DataContext.SubmitChanges();

        }
        public void deleteAllImages()
        {
           var largeimages= (from img in DataContext.largeimages
             select img);
           DataContext.largeimages.DeleteAllOnSubmit(largeimages);
           var smallimages = (from img in DataContext.smallimages
                              select img);

           DataContext.smallimages.DeleteAllOnSubmit(smallimages);
           DataContext.SubmitChanges();


        }

        public void saveSmallImage(Image img, String name)
        {
            // strip invalid path characters
            String imgName =  name + img.RawFormat.getName();
            imgName = imgName.StripInvalidPathChars();
            smallimage smallImage = new smallimage();
            smallImage.name= imgName;
            MemoryStream ms = new MemoryStream();
            img.Save(ms, img.RawFormat);
            
            smallImage.image = ms.ToArray();
            DataContext.smallimages.InsertOnSubmit(smallImage);
            DataContext.SubmitChanges();
        }
        public void saveLargeImage(Image img, String name)
        {
            // strip invalid path characters
            String imgName = name + img.RawFormat.getName();
            imgName = imgName.StripInvalidPathChars();
            largeimage largeImage = new largeimage();
            largeImage.name = imgName;
            MemoryStream ms = new MemoryStream();
            img.Save(ms, img.RawFormat);
            largeImage.image = ms.ToArray();
            DataContext.largeimages.InsertOnSubmit(largeImage);
            DataContext.SubmitChanges();
        }
        /// <summary>
        /// gets a large image by name throws exeption if none exiists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public largeimage getLargeImage(string name)
        {
            return (from img in DataContext.largeimages
                     where img.name.Equals(name)
                     select img).First();
                  
        }
        public smallimage getSmallImage(string name)
        {
            return (from img in DataContext.smallimages
                    where img.name.Equals(name)
                    select img).First();

        }
    }
}