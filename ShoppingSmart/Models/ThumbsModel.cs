using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingSmart.Models
{
    public class ThumbsModel
    {
        //query arguments from user
        public String query;
        public int minPrice;
        public int maxPrice;
        public int[] categories;
        public int[] companies;
        public int[] colours;
        public int productsPerPage;
        public int pageNum;
        public int sorting;

        // query results
        public int numPages;
        public product[] products;
    }
}