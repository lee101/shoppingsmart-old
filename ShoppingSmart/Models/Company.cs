using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingSmart.Models
{
    public class Company
    {
        public static Company THE_WAREHOUSE = new Company(1,null,"The Warehouse");
        public int id;
        private List<Store> stores;

        internal List<Store> Stores
        {
            get { return stores; }
            set { stores = value; }
        }
        public String name;

        public Company(int id, List<Store> stores, String name)
        {
            this.name = name;
            this.stores = stores;
            this.id = id;
        }
    }
}