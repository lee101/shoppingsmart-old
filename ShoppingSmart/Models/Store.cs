using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingSmart.Models
{
    /// <summary>
    /// represents a physical store that a company has
    /// </summary>
    public class Store
    {
        String address;
        String region;
        TimeSpan[,] openHours = new TimeSpan[7,2];
        private int phoneNumber;

        public Store(String address,String region,String openHours,int number)
        {
            this.address=address;
            this.region=region;
            this.phoneNumber = number;
            //openHours[0,0] = new TimeSpan(8, 30, 0);
            //openHours[1,0] = new TimeSpan(8, 30, 0);
            //openHours[2,0] = new TimeSpan(8, 30, 0);
            //openHours[3,0] = new TimeSpan(8, 30, 0);
            //openHours[4,0] = new TimeSpan(8, 30, 0);
            //openHours[5,0] = new TimeSpan(8, 30, 0);
            //openHours[6,0] = new TimeSpan(8, 30, 0);



        }
        public static Store asdf = new Store("John Goulter Drive", "auckland", "everyday 8:30am to 8pm",092750156);

    }
}
